using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Cards;

public sealed class BackDigging : CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(1m, ValueProp.Move),
            new DynamicVar("Hits", 10m)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("back_digging");

    public BackDigging()
        : base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.CombatState == null)
        {
            return;
        }

        for (int i = 0; i < base.DynamicVars["Hits"].IntValue; i++)
        {
            List<Creature> targets = base.CombatState.HittableEnemies.Where(enemy => enemy.IsAlive).ToList();
            if (targets.Count == 0)
            {
                break;
            }

            Creature? target = base.Owner.RunState.Rng.CombatTargets.NextItem(targets);
            if (target == null)
            {
                break;
            }

            decimal damage = Hook.ModifyDamage(base.Owner.RunState, base.CombatState, target, base.Owner.Creature, base.DynamicVars.Damage.BaseValue, ValueProp.Move, this, ModifyDamageHookType.All, CardPreviewMode.None, out IEnumerable<AbstractModel> _);
            await DamageCmd.Attack(damage)
                .FromCard(this)
                .Targeting(target)
                .Execute(choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["Hits"].UpgradeValueBy(5m);
    }
}
