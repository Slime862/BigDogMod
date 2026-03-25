using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class BelCantoHowl : CustomCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [];

    protected override HashSet<CardTag> CanonicalTags => new() { BigDogTags.Jiao };

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<BelCantoHowlPower>(1m)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("bel_canto_howl");

    public BelCantoHowl()
        : base(2, CardType.Power, CardRarity.Rare, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<BelCantoHowlPower>(base.Owner.Creature, 1m, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}
