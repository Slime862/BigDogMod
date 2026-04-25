using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Utils;
using BigDogMod.Scripts.Assets;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Cards;

public sealed class SplashWater : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.Static(StaticHoverTip.Block), HoverTipFactory.FromKeyword(CardKeyword.Retain)];

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

        CardPile drawPile = PileType.Draw.GetPile(base.Owner);
        if (drawPile.Cards.Count > 0)
        {
            IEnumerable<CardModel> selectedCards = await CommonActions.SelectCards(
                this,
                new LocString("cards", "BIGDOGMOD-SPLASH_WATER.select_prompt"),
                choiceContext,
                PileType.Draw,
                0,
                drawPile.Cards.Count);

            List<CardModel> selectedList = selectedCards.ToList();
            if (selectedList.Count > 0)
            {
                await CardPileCmd.Add(selectedList, PileType.Discard);
            }
        }

        int discardPileCount = PileType.Discard.GetPile(base.Owner).Cards.Count;
        if (discardPileCount > 0)
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, discardPileCount, ValueProp.Move, cardPlay);
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
