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

namespace BigDogMod.Scripts.Cards;

public sealed class Makeover : CustomCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<WildnessPower>()];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("makeover");

    public Makeover()
        : base(0, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        WildnessPower? power = base.Owner.Creature.GetPower<WildnessPower>();
        if (power == null || power.Amount == 0)
        {
            return;
        }

        await PowerCmd.ModifyAmount(power, -power.Amount, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
