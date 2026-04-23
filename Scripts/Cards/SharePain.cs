using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class SharePain : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BleedingPower>()];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("share_pain");

    public SharePain()
        : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        IEnumerable<Creature> creatures = (base.CombatState?.PlayerCreatures ?? [])
            .Concat(base.CombatState?.HittableEnemies ?? [])
            .Prepend(base.Owner.Creature)
            .Distinct();
        foreach (Creature creature in creatures)
        {
            BleedingPower? bleeding = creature.GetPower<BleedingPower>();
            if (bleeding?.Amount > 0)
            {
                await PowerCmd.ModifyAmount(bleeding, bleeding.Amount, base.Owner.Creature, this);
            }
        }
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}
