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
        [new CardsVar(1)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("run_away");

    public RunAway()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        CardModel? cardFromDraw = PileType.Draw.GetPile(base.Owner).Cards.FirstOrDefault();
        if (cardFromDraw != null)
        {
            await CardPileCmd.Add(cardFromDraw, PileType.Discard);
        }

        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
