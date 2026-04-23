using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class Coagulate : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<BleedingPower>(),
            HoverTipFactory.Static(StaticHoverTip.Block),
            HoverTipFactory.FromPower<CoagulatePower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<BleedingPower>(3m)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("coagulate");

    public Coagulate()
        : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<BleedingPower>(base.Owner.Creature, base.DynamicVars["BleedingPower"].BaseValue, base.Owner.Creature, this);
        decimal multiplier = base.IsUpgraded ? 2m : 1m;
        await PowerCmd.Apply<CoagulatePower>(base.Owner.Creature, multiplier, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
    }
}
