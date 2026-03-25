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

public sealed class LickWounds : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BleedingPower>()];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("lick_wounds");

    public LickWounds()
        : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        BleedingPower? power = base.Owner.Creature.GetPower<BleedingPower>();
        if (power == null || power.Amount <= 0)
        {
            return;
        }

        int amountToRemove = base.IsUpgraded ? power.Amount : power.Amount / 2;
        if (amountToRemove > 0)
        {
            await PowerCmd.ModifyAmount(power, -amountToRemove, base.Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
    }
}
