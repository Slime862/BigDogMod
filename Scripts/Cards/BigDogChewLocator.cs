using System.Linq;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

internal static class BigDogChewLocator
{
    public static BigDogChew? FindInHand(Player? player) =>
        player?.PlayerCombatState?.Hand.Cards.OfType<BigDogChew>().FirstOrDefault();

    public static BigDogChew? FindInDraw(Player? player) =>
        player == null ? null : PileType.Draw.GetPile(player).Cards.OfType<BigDogChew>().FirstOrDefault();

    public static BigDogChew? FindInDiscard(Player? player) =>
        player == null ? null : PileType.Discard.GetPile(player).Cards.OfType<BigDogChew>().FirstOrDefault();

    public static BigDogChew? FindAnywhere(Player? player) =>
        FindInHand(player) ?? FindInDraw(player) ?? FindInDiscard(player);

    public static PileType? FindPileType(Player? player, BigDogChew chew)
    {
        if (player?.PlayerCombatState?.Hand.Cards.Contains(chew) == true)
        {
            return PileType.Hand;
        }

        if (player != null && PileType.Draw.GetPile(player).Cards.Contains(chew))
        {
            return PileType.Draw;
        }

        if (player != null && PileType.Discard.GetPile(player).Cards.Contains(chew))
        {
            return PileType.Discard;
        }

        return null;
    }
}
