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
}
