using MegaCrit.Sts2.Core.Models;

namespace BigDogMod.Scripts.Cards;

public interface IJiaoCard;

public static class BigDogCardTraits
{
    public static bool IsJiao(CardModel? card)
    {
        return card is IJiaoCard;
    }
}
