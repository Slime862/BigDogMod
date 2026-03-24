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
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Cards;

public sealed class BloodDrink : CustomCardModel
{
    protected override bool ShouldGlowGoldInternal
    {
        get
        {
            Creature? currentTarget = base.CurrentTarget;
            if (currentTarget != null)
            {
                return currentTarget.HasPower<BleedingPower>();
            }

            return base.CombatState?.HittableEnemies.Any(enemy => enemy.HasPower<BleedingPower>()) ?? false;
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.Static(StaticHoverTip.Block),
            HoverTipFactory.FromPower<BleedingPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new BlockVar(4m, ValueProp.Move),
            new DynamicVar("BlockPerBleed", 2m)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("blood_drink");

    public BloodDrink()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null)
        {
            return;
        }

        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);

        BleedingPower? bleeding = cardPlay.Target.GetPower<BleedingPower>();
        int stacks = bleeding?.Amount ?? 0;
        if (stacks <= 0)
        {
            return;
        }

        await PowerCmd.Remove(bleeding);
        await CreatureCmd.GainBlock(base.Owner.Creature, stacks * base.DynamicVars["BlockPerBleed"].BaseValue, ValueProp.Move, cardPlay);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["BlockPerBleed"].UpgradeValueBy(1m);
    }
}
