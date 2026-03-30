using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Cards;

public sealed class Bloodthirst : CustomCardModel
{
    protected override bool ShouldGlowGoldInternal =>
        base.CombatState?.HittableEnemies.Any(enemy => enemy.HasPower<BleedingPower>()) ?? false;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<BleedingPower>(),
            HoverTipFactory.FromPower<WildnessPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(5m, ValueProp.Move),
            new PowerVar<WildnessPower>(2m)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("bloodthirst");

    public Bloodthirst()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null)
        {
            return;
        }

        if (cardPlay.Target.HasPower<BleedingPower>())
        {
            decimal amount = base.DynamicVars["WildnessPower"].BaseValue;
            await PowerCmd.Apply<WildnessPower>(base.Owner.Creature, amount, base.Owner.Creature, this);
            base.Owner.Creature.GetPower<WildnessPower>()?.AddTemporaryAmount(amount);
        }

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(2m);
        base.DynamicVars["WildnessPower"].UpgradeValueBy(2m);
    }
}
