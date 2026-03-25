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
        if (endingSide != base.Owner.Side || base.Owner.CombatState == null)
        {
            return;
        }

        await TriggerBleeding();

        int extraTriggers = base.Owner.CombatState.PlayerCreatures
            .Select(creature => creature.GetPower<EndlessBleedingPower>()?.Amount ?? 0)
            .Sum();
        for (int i = 0; i < extraTriggers && base.Owner.IsAlive; i++)
        {
            await TriggerBleeding();
        }
    }

    public async Task TriggerBleeding()
    {
        if (base.Amount <= 0)
        {
            return;
        }

        await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount, ValueProp.Unblockable | ValueProp.Unpowered, null, null);

        if (base.Owner.IsAlive)
        {
            await PowerCmd.ModifyAmount(this, 1m, null, null);
        }
    }
}
