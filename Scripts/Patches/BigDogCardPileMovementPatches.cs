using System.Threading.Tasks;
using HarmonyLib;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace BigDogMod.Scripts.Patches;

[HarmonyPatch(typeof(CardPileCmd), nameof(CardPileCmd.Add), new[] { typeof(CardModel), typeof(CardPile), typeof(CardPilePosition), typeof(AbstractModel), typeof(bool) })]
public static class BigDogCardPileMovementPatch
{
    public static void Postfix(ref Task<CardPileAddResult> __result, CardModel card)
    {
        __result = HandleMove(__result, card);
    }

    private static async Task<CardPileAddResult> HandleMove(Task<CardPileAddResult> task, CardModel card)
    {
        CardPileAddResult result = await task;
        if (!result.success)
        {
            return result;
        }

        PileType? oldPileType = result.oldPile?.Type;
        PileType? newPileType = card.Pile?.Type;
        if (oldPileType == null || newPileType == null || oldPileType == newPileType || card.Owner == null)
        {
            return result;
        }

        if (card is BigDogChew && card.Owner.Creature.GetPower<HesitationPower>() is HesitationPower hesitationPower)
        {
            await WantChewCmd.WantChew(hesitationPower.Amount, card.Owner, hesitationPower);
        }

        if (card is SharpenClaws sharpenClaws)
        {
            sharpenClaws.AddPileMoveDamage(sharpenClaws.DynamicVars["Grow"].BaseValue);
        }

        return result;
    }
}
