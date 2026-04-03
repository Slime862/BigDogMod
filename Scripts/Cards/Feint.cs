using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class Feint : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        new IHoverTip[] { HoverTipFactory.FromKeyword(CardKeyword.Retain) }
            .Concat(HoverTipFactory.FromCardWithCardHoverTips<BigDogFakeChew>());

    public override IEnumerable<CardKeyword> CanonicalKeywords => [];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("feint");

    public Feint()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        BigDogFakeChew fakeChew = base.Owner.Creature.CombatState!.CreateCard<BigDogFakeChew>(base.Owner);
        await CardPileCmd.AddGeneratedCardToCombat(fakeChew, PileType.Hand, addedByPlayer: true);
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}
