using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class CatchBreath : CustomCardModel
{
    protected override bool IsPlayable => BigDogChewLocator.FindInHand(base.Owner) != null;

    protected override bool ShouldGlowRedInternal => !IsPlayable;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromCard<BigDogChew>()];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new EnergyVar(1)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("catch_breath");

    public CatchBreath()
        : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        BigDogChew? chew = BigDogChewLocator.FindInHand(base.Owner);
        if (chew == null)
        {
            return;
        }

        await CardCmd.Discard(choiceContext, chew);
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Energy.UpgradeValueBy(1m);
    }
}
