using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Commands;

public static class WantChewModifiers
{
    public static decimal GetEffectiveWantChewAmount(CardModel card, decimal baseAmount)
    {
        if (baseAmount <= 0m)
        {
            return 0m;
        }

        if (card.Owner.Creature.HasPower<ChewAtWillPower>() || card.Owner.Creature.HasPower<CowardDogPower>())
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

        return card.Owner.Creature.HasPower<ChewAtWillPower>() ? baseWantChew : 0m;
    }
}
