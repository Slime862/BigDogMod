using System.Threading.Tasks;

namespace BigDogMod.Scripts.Cards;

public interface IOnEnterDiscardPileCard
{
    Task OnEnterDiscardPile();
}
