using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class SplashWater : CustomCardModel
{
    protected override bool IsPlayable => BigDogChewLocator.FindInHand(base.Owner) != null;

    protected override bool ShouldGlowRedInternal => !IsPlayable;

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("splash_water");

    public SplashWater()
        : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        BigDogChew? chew = BigDogChewLocator.FindInHand(base.Owner);
        if (chew == null || base.Owner.PlayerCombatState == null)
        {
            return;
        }

        List<CardModel> cardsToExhaust = base.Owner.PlayerCombatState.Hand.Cards
            .Where(card => card == chew || card.Type == CardType.Status)
            .ToList();
        if (cardsToExhaust.Count == 0)
        {
            return;
        }

        await CardPileCmd.Add(cardsToExhaust, PileType.Exhaust);
        await CardPileCmd.Draw(choiceContext, cardsToExhaust.Count, base.Owner);
    }

    protected override void OnUpgrade()
    {
    }
}
