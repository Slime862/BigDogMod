using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class SharpenClaws : CustomCardModel, IOnEnterDiscardPileCard
{
    private decimal _extraDamageFromDiscardTriggers;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(6m, ValueProp.Move),
            new DynamicVar("Grow", 3m)
        ];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("sharpen_claws");

    public SharpenClaws()
        : base(2, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        return Task.CompletedTask;
    }

    public async Task OnEnterDiscardPile()
    {
        if (base.CombatState == null)
        {
            return;
        }

        List<Creature> targets = base.CombatState.HittableEnemies.Where(enemy => enemy.IsAlive).ToList();
        if (targets.Count == 0)
        {
            return;
        }

        Creature? target = base.Owner.RunState.Rng.CombatTargets.NextItem(targets);
        if (target == null)
        {
            return;
        }

        decimal damage = Hook.ModifyDamage(base.Owner.RunState, base.CombatState, target, base.Owner.Creature, base.DynamicVars.Damage.BaseValue, ValueProp.Move, this, ModifyDamageHookType.All, CardPreviewMode.None, out IEnumerable<AbstractModel> _);
        await DamageCmd.Attack(damage)
            .FromCard(this)
            .Targeting(target)
            .Execute(null);

        AddPileMoveDamage(base.DynamicVars["Grow"].BaseValue);
    }

    public void AddPileMoveDamage(decimal amount)
    {
        base.DynamicVars.Damage.BaseValue += amount;
        _extraDamageFromDiscardTriggers += amount;
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(2m);
        base.DynamicVars["Grow"].UpgradeValueBy(1m);
    }

    protected override void AfterDowngraded()
    {
        base.AfterDowngraded();
        base.DynamicVars.Damage.BaseValue += _extraDamageFromDiscardTriggers;
    }
}


