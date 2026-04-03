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
            HoverTipFactory.FromPower<BleedingGuardPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new PowerVar<BleedingPower>(2m),
            new CardsVar(1)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("coagulate");

    public Coagulate()
        : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<BleedingPower>(base.Owner.Creature, base.DynamicVars["BleedingPower"].BaseValue, base.Owner.Creature, this);
        await PowerCmd.Apply<BleedingGuardPower>(base.Owner.Creature, 1m, base.Owner.Creature, this);
        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["BleedingPower"].UpgradeValueBy(-1m);
    }
}
