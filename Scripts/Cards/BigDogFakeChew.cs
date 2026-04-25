using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
using MegaCrit.Sts2.Core.Models.CardPools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(TokenCardPool))]
public sealed class BigDogFakeChew : CustomCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [CardKeyword.Retain, CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromKeyword(CardKeyword.Retain), HoverTipFactory.FromKeyword(CardKeyword.Exhaust)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("big_dog_fake_chew");

    public BigDogFakeChew()
        : base(0, CardType.Skill, CardRarity.Token, TargetType.AnyEnemy, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null)
        {
            return;
        }

        await BigDogChewPrepCmd.Resolve(choiceContext, this, cardPlay.Target, base.Owner, consume: false);
    }
}



