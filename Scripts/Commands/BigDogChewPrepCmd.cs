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

    public static async Task Resolve(PlayerChoiceContext choiceContext, BigDogChew targetCard, Creature target, Player owner)
    {
        BigDogChewPrepPower? power = owner.Creature.GetPower<BigDogChewPrepPower>();
        if (power == null || !power.HasAnyEffects)
        {
            return;
        }

        BigDogChewPrepSnapshot snapshot = power.ConsumeAll();

        if (snapshot.Weak > 0 && target.IsAlive)
        {
            await PowerCmd.Apply<WeakPower>(target, snapshot.Weak, owner.Creature, targetCard);
        }

        if (snapshot.Draw > 0)
        {
            await CardPileCmd.Draw(choiceContext, snapshot.Draw, owner);
        }

        await PowerCmd.Remove(power);
    }
}
