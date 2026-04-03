using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.ChewPrep;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Commands;

public static class BigDogChewPrepCmd
{
    public static async Task QueueFromSource(Player owner, CardModel source)
    {
        if (source is not IBigDogChewPrepSource prepSource)
        {
            return;
        }

        await Queue(owner, prepSource.GetBigDogChewPrepEffects(), source);
    }

    public static async Task Queue(Player owner, IEnumerable<BigDogChewPrepEffect> effects, CardModel? source)
    {
        List<BigDogChewPrepEffect> effectList = effects.Where(effect => effect.Amount > 0).ToList();
        if (effectList.Count == 0)
        {
            return;
        }

        BigDogChewPrepPower? power = owner.Creature.GetPower<BigDogChewPrepPower>();
        if (power == null)
        {
            power = await PowerCmd.Apply<BigDogChewPrepPower>(owner.Creature, 1m, owner.Creature, source);
        }

        power?.AddEffects(effectList);
    }

    public static async Task Resolve(PlayerChoiceContext choiceContext, CardModel targetCard, Creature target, Player owner, bool consume = true)
    {
        BigDogChewPrepPower? power = owner.Creature.GetPower<BigDogChewPrepPower>();
        if (power == null || !power.HasAnyEffects)
        {
            return;
        }

        IReadOnlyList<BigDogChewPrepEffect> effects = consume ? power.ConsumeAll() : power.SnapshotAll();
        foreach (BigDogChewPrepEffect effect in effects)
        {
            if (effect.Amount <= 0)
            {
                continue;
            }

            switch (effect.Type)
            {
                case BigDogChewPrepEffectType.Block:
                    await CreatureCmd.GainBlock(owner.Creature, effect.Amount, ValueProp.Move, null);
                    break;
                case BigDogChewPrepEffectType.Weak:
                    if (effect.ApplyToAllEnemies && owner.Creature.CombatState != null)
                    {
                        await PowerCmd.Apply<WeakPower>(owner.Creature.CombatState.HittableEnemies.Where(enemy => enemy.IsAlive), effect.Amount, owner.Creature, targetCard);
                    }
                    else if (target.IsAlive)
                    {
                        await PowerCmd.Apply<WeakPower>(target, effect.Amount, owner.Creature, targetCard);
                    }
                    break;
                case BigDogChewPrepEffectType.Vulnerable:
                    if (effect.ApplyToAllEnemies && owner.Creature.CombatState != null)
                    {
                        await PowerCmd.Apply<VulnerablePower>(owner.Creature.CombatState.HittableEnemies.Where(enemy => enemy.IsAlive), effect.Amount, owner.Creature, targetCard);
                    }
                    else if (target.IsAlive)
                    {
                        await PowerCmd.Apply<VulnerablePower>(target, effect.Amount, owner.Creature, targetCard);
                    }
                    break;
                case BigDogChewPrepEffectType.Bleeding:
                    if (effect.ApplyToAllEnemies && owner.Creature.CombatState != null)
                    {
                        await PowerCmd.Apply<BleedingPower>(owner.Creature.CombatState.HittableEnemies.Where(enemy => enemy.IsAlive), effect.Amount, owner.Creature, targetCard);
                    }
                    else if (target.IsAlive)
                    {
                        await PowerCmd.Apply<BleedingPower>(target, effect.Amount, owner.Creature, targetCard);
                    }
                    break;
                case BigDogChewPrepEffectType.Draw:
                    await CardPileCmd.Draw(choiceContext, effect.Amount, owner);
                    break;
                case BigDogChewPrepEffectType.Energy:
                    await PlayerCmd.GainEnergy(effect.Amount, owner);
                    break;
                case BigDogChewPrepEffectType.RepeatPlay:
                    break;
                case BigDogChewPrepEffectType.WantChew:
                    await WantChewCmd.WantChew(effect.Amount, owner, targetCard);
                    break;
            }
        }

        if (consume)
        {
            await PowerCmd.Remove(power);
        }
    }
}
