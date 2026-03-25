using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Cards;

public sealed class Feint : CustomCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        base.IsUpgraded ? [] : [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(7m, ValueProp.Move)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("feint");

    public Feint()
        : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null)
        {
            return;
        }

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .Execute(choiceContext);

        BigDogChew? chew = PileType.Draw.GetPile(base.Owner).Cards.OfType<BigDogChew>().FirstOrDefault()
            ?? PileType.Discard.GetPile(base.Owner).Cards.OfType<BigDogChew>().FirstOrDefault();
        if (chew != null)
        {
            await CardPileCmd.Add(chew, PileType.Hand);
        }
    }

    protected override void OnUpgrade()
    {
    }
}
