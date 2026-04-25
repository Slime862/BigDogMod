using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.DynamicVars;
using BigDogMod.Scripts.HoverTips;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class AllOutForce : CustomCardModel
{
    protected override bool IsPlayable => BigDogChewLocator.FindInHand(base.Owner) != null;

    protected override bool ShouldGlowRedInternal => !IsPlayable;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        BigDogHoverTips.FromWantChew(base.DynamicVars["WantChew"])
            .Concat(HoverTipFactory.FromCardWithCardHoverTips<BigDogChew>());

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new WantChewVar(24m)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("all_out_force");

    public AllOutForce()
        : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await WantChewCmd.WantChew(WantChewModifiers.GetEffectiveWantChewAmount(this, base.DynamicVars["WantChew"].BaseValue), base.Owner, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["WantChew"].UpgradeValueBy(12m);
    }
}


