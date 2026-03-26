using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Powers;

public sealed class ChewAtWillPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (base.Owner != dealer || cardSource == null || cardSource.Type != CardType.Attack)
        {
            return 0m;
        }

        if (!cardSource.DynamicVars.TryGetValue("WantChew", out DynamicVar wantChewVar))
        {
            return 0m;
        }

        if (!props.HasFlag(ValueProp.Move) || props.HasFlag(ValueProp.Unpowered))
        {
            return 0m;
        }

        return wantChewVar.BaseValue;
    }

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
