using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Patches.Localization;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.HoverTips;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace BigDogMod.Scripts.Powers;

public sealed class PreludePower : CustomPowerModel, IAddDumbVariablesToPowerDescription
{
    private int _remainingTurns = 2;

    public override bool IsInstanced => true;

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public void AddDumbVariablesToPowerDescription(LocString description)
    {
        description.Add("WantChew", Amount);
        description.Add("Turns", _remainingTurns);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        BigDogHoverTips.FromWantChew(new DynamicVar("WantChew", Amount));

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        if (player != base.Owner.Player || _remainingTurns <= 0)
        {
            return;
        }

        Flash();
        decimal amount = (base.Owner.HasPower<ChewAtWillPower>() || base.Owner.HasPower<CowardDogPower>()) ? 0m : Amount;
        await WantChewCmd.WantChew(amount, player, this);
        _remainingTurns--;
        if (_remainingTurns <= 0)
        {
            await PowerCmd.Remove(this);
        }
    }
}
