using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class RunAway : CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new CardsVar(3)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("run_away");

    public RunAway()
        : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
        {
            IEnumerable<CardModel> drawnCards = await CardPileCmd.Draw(choiceContext, 1m, base.Owner);
            CardModel? drawn = drawnCards.FirstOrDefault();
            if (drawn?.Type == CardType.Attack)
            {
                await CardPileCmd.Add(drawn, PileType.Discard);
            }
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
