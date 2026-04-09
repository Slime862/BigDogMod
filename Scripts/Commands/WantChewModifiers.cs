using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace BigDogMod.Scripts.Commands;

public static class WantChewModifiers
{
    public static decimal GetEffectiveWantChewAmount(CardModel card, decimal baseAmount)
    {
        if (baseAmount <= 0m)
        {
            return 0m;
        }

        if (card.Owner?.Creature?.HasPower<CowardDogPower>() == true)
        {
            return 0m;
        }

        return baseAmount;
    }

    public static decimal GetAttackDamageBonus(CardModel card, decimal baseWantChew)
    {
        if (baseWantChew <= 0m || card.Type != CardType.Attack)
        {
            return 0m;
        }

        return 0m;
    }

    public static void RefreshWantChewCards(Player player)
    {
        if (player.PlayerCombatState == null)
        {
            return;
        }

        foreach (CardModel card in player.PlayerCombatState.AllCards)
        {
            if (!card.DynamicVars.ContainsKey("WantChew"))
            {
                continue;
            }

            NCard.FindOnTable(card)?.UpdateVisuals(card.Pile?.Type ?? PileType.None, CardPreviewMode.Normal);
        }
    }
}
