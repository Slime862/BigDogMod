using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Cards;

public sealed class SplashWater : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.Static(StaticHoverTip.Block), HoverTipFactory.FromKeyword(CardKeyword.Retain)];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new BlockVar(1m, ValueProp.Move)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("splash_water");

    public SplashWater()
        : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.Owner.PlayerCombatState == null)
        {
            return;
        }

        List<CardModel> statusCards = base.Owner.PlayerCombatState.Hand.Cards
            .Where(card => card.Type == CardType.Status)
            .ToList();
        if (statusCards.Count > 0)
        {
            await CardPileCmd.Add(statusCards, PileType.Exhaust);
        }

        List<CardModel> discardCards = PileType.Discard.GetPile(base.Owner).Cards.ToList();
        if (discardCards.Count > 0)
        {
            await CardPileCmd.Add(discardCards, PileType.Draw);
        }

        int drawPileCount = PileType.Draw.GetPile(base.Owner).Cards.Count;
        if (drawPileCount > 0)
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, drawPileCount, ValueProp.Move, cardPlay);
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
