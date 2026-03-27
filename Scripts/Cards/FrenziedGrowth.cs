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
using MegaCrit.Sts2.Core.Models.Powers;

namespace BigDogMod.Scripts.Cards;

public sealed class FrenziedGrowth : CustomCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override bool ShouldGlowGoldInternal => base.Owner.Creature.HasPower<BleedingPower>();

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<BleedingPower>(),
            HoverTipFactory.FromPower<RegenPower>()
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("frenzied_growth");

    public FrenziedGrowth()
        : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        BleedingPower? bleeding = base.Owner.Creature.GetPower<BleedingPower>();
        int amount = bleeding?.Amount ?? 0;
        if (amount <= 0)
        {
            return;
        }

        await PowerCmd.Apply<RegenPower>(base.Owner.Creature, amount, base.Owner.Creature, this);
        await PowerCmd.Remove(bleeding);
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}
