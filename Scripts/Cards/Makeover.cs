using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class Makeover : CustomCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<WildnessPower>()];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("makeover");

    public Makeover()
        : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        WildnessPower? power = base.Owner.Creature.GetPower<WildnessPower>();
        if (power == null || power.Amount == 0)
        {
            return;
        }

        decimal delta = -power.Amount * 2m;
        await PowerCmd.ModifyAmount(power, delta, base.Owner.Creature, this);
        base.Owner.Creature.GetPower<WildnessPower>()?.AddTemporaryAmount(delta);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}


