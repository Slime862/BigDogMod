using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace BigDogMod.Scripts.Powers;

public sealed class CowardDogPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        WantChewModifiers.RefreshWantChewCards(base.Owner.Player);
        return Task.CompletedTask;
    }

    public override Task AfterRemoved(Creature oldOwner)
    {
        WantChewModifiers.RefreshWantChewCards(oldOwner.Player);
        return Task.CompletedTask;
    }
}
