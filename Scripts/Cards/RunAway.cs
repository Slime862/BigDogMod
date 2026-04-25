using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Utils;
using BigDogMod.Scripts.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class RunAway : CustomCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("run_away");

    public RunAway()
        : base(0, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        CardPile drawPile = PileType.Draw.GetPile(base.Owner);
        int selectionCount = drawPile.Cards.Count >= 2 ? 2 : drawPile.Cards.Count;
        if (selectionCount > 0)
        {
            IEnumerable<CardModel> selectedCards = await CommonActions.SelectCards(
                this,
                new LocString("cards", "BIGDOGMOD-RUN_AWAY.select_prompt"),
                choiceContext,
                PileType.Draw,
                selectionCount,
                selectionCount);

            List<CardModel> selectedList = selectedCards.ToList();
            if (selectedList.Count > 0)
            {
                await CardPileCmd.Add(selectedList, PileType.Discard);
            }
        }

        await CardPileCmd.Draw(choiceContext, 1, base.Owner);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}
