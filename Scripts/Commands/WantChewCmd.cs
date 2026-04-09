using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;

namespace BigDogMod.Scripts.Commands;

public static class WantChewCmd
{
    public static async Task<IEnumerable<BigDogChew>> WantChew(decimal amount, Player player, AbstractModel? source)
    {
        if (CombatManager.Instance.IsOverOrEnding)
        {
            return Array.Empty<BigDogChew>();
        }

        if (amount <= 0m || player.Creature.HasPower<CowardDogPower>())
        {
            return Array.Empty<BigDogChew>();
        }

        List<BigDogChew> chews = GetBigDogChews(player, includeExhausted: false).ToList();
        if (chews.Count == 0)
        {
            CombatState? combatState = player.Creature.CombatState;
            if (combatState == null)
            {
                return chews;
            }

            BigDogChew bigDogChew = combatState.CreateCard<BigDogChew>(player);
            PileType pileType = player.Creature.HasPower<AttackWindupPower>() ? PileType.Draw : PileType.Hand;
            await CardPileCmd.AddGeneratedCardToCombat(bigDogChew, pileType, addedByPlayer: true);
            chews.Add(bigDogChew);
        }

        IncreaseBigDogChewDamage(amount, player);
        return chews;
    }

    private static void IncreaseBigDogChewDamage(decimal amount, Player player)
    {
        foreach (BigDogChew chew in GetBigDogChews(player, includeExhausted: true))
        {
            chew.AddDamage(amount);
        }
    }

    private static IEnumerable<BigDogChew> GetBigDogChews(Player player, bool includeExhausted)
    {
        if (player.PlayerCombatState == null)
        {
            return Array.Empty<BigDogChew>();
        }

        return player.PlayerCombatState.AllCards.Where(delegate(CardModel c)
        {
            if (!c.IsDupe)
            {
                if (!includeExhausted)
                {
                    CardPile? pile = c.Pile;
                    if (pile == null)
                    {
                        return true;
                    }

                    return pile.Type != PileType.Exhaust;
                }

                return true;
            }

            return false;
        }).OfType<BigDogChew>();
    }
}
