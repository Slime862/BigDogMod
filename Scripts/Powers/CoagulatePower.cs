using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Patches.Localization;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Powers;

public sealed class CoagulatePower : CustomPowerModel, IAddDumbVariablesToPowerDescription
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public void AddDumbVariablesToPowerDescription(LocString description)
    {
        description.Add("Multiplier", base.Amount);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != base.Owner.Side)
        {
            return;
        }

        BleedingPower? bleeding = base.Owner.GetPower<BleedingPower>();
        if (bleeding != null && bleeding.Amount > 0)
        {
            await CreatureCmd.GainBlock(base.Owner, bleeding.Amount * base.Amount, ValueProp.Move, null);
            await PowerCmd.Remove(bleeding);
        }

        await PowerCmd.Remove(this);
    }
}
