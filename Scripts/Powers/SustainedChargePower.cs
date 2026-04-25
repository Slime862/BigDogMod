using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Patches.Localization;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.HoverTips;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

using STS2RitsuLib.Interop.AutoRegistration;

namespace BigDogMod.Scripts.Powers;

[RegisterPower()]
public sealed class SustainedChargePower : CustomPowerModel, IAddDumbVariablesToPowerDescription
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public void AddDumbVariablesToPowerDescription(LocString description)
    {
        description.Add("WantChew", Amount);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        BigDogHoverTips.FromWantChew(new DynamicVar("WantChew", Amount));

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        if (player != base.Owner.Player)
        {
            return;
        }

        Flash();
        decimal amount = (base.Owner.HasPower<ChewAtWillPower>() || base.Owner.HasPower<CowardDogPower>()) ? 0m : Amount;
        await WantChewCmd.WantChew(amount, player, this);
    }
}

