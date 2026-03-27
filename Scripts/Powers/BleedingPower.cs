using System.Threading.Tasks;
using System.Linq;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Combat;

namespace BigDogMod.Scripts.Powers;

public sealed class BleedingPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide endingSide)
    {
        if (endingSide != base.Owner.Side || base.Owner.CombatState == null || base.Owner.IsDead)
        {
            return;
        }

        bool isAliveAfterBaseTrigger = await TriggerBleeding();
        if (!isAliveAfterBaseTrigger)
        {
            return;
        }

        int extraTriggers = 0;
        if (base.Owner.IsMonster)
        {
            extraTriggers = base.Owner.CombatState.PlayerCreatures
                .Select(creature => creature.GetPower<EndlessBleedingPower>()?.Amount ?? 0)
                .Sum();
        }

        for (int i = 0; i < extraTriggers && base.Owner.IsAlive; i++)
        {
            bool isAliveAfterExtraTrigger = await TriggerBleeding();
            if (!isAliveAfterExtraTrigger)
            {
                return;
            }
        }
    }

    public async Task<bool> TriggerBleeding()
    {
        if (base.Amount <= 0)
        {
            return base.Owner.IsAlive;
        }

        await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount, ValueProp.Unblockable | ValueProp.Unpowered, null, null);

        if (base.Owner.IsAlive)
        {
            await PowerCmd.ModifyAmount(this, 1m, null, null);
            return true;
        }

        // Match Poison's timing: if DOT kills the owner, briefly yield so the
        // death/removal pipeline can finish before the next DOT or turn switch.
        await Cmd.CustomScaledWait(0.1f, 0.25f);
        return false;
    }
}
