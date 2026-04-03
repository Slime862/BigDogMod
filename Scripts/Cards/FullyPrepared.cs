using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.DynamicVars;
using BigDogMod.Scripts.HoverTips;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class FullyPrepared : CustomCardModel
{
    protected override bool IsPlayable => (base.Owner.Creature.GetPower<BigDogChewPrepPower>()?.ActiveEffectTypeCount ?? 0) >= 3;

    protected override bool ShouldGlowGoldInternal =>
        base.Owner.Creature.GetPower<BigDogChewPrepPower>()?.ActiveEffectTypeCount >= 3;

    protected override IEnumerable<MegaCrit.Sts2.Core.HoverTips.IHoverTip> ExtraHoverTips =>
        BigDogHoverTips.FromWantChew(base.DynamicVars["WantChew"]);

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new WantChewVar(10m)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("fully_prepared");

    public FullyPrepared()
        : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await WantChewCmd.WantChew(WantChewModifiers.GetEffectiveWantChewAmount(this, base.DynamicVars["WantChew"].BaseValue), base.Owner, this);
        await CardPileCmd.Draw(choiceContext, 1m, base.Owner);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["WantChew"].UpgradeValueBy(7m);
    }
}
