using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

using STS2RitsuLib.Interop.AutoRegistration;

namespace BigDogMod.Scripts.Powers;

[RegisterPower()]
public sealed class WildnessPower : CustomPowerModel
{
    private decimal _temporaryAmount;
    public decimal TemporaryAmount => _temporaryAmount;

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool AllowNegative => true;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.Static(StaticHoverTip.Block)];

    public void AddTemporaryAmount(decimal amount)
    {
        _temporaryAmount += amount;
    }

    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (base.Owner != dealer)
        {
            return 0m;
        }

        if (!props.HasFlag(ValueProp.Move) || props.HasFlag(ValueProp.Unpowered))
        {
            return 0m;
        }

        if (cardSource is MightyBlow or ForgetChew)
        {
            return base.Amount * 2m;
        }

        if (cardSource is SneakAttack && base.Amount < 0m)
        {
            return -base.Amount * 2m;
        }

        return base.Amount;
    }

    public override decimal ModifyBlockAdditive(Creature target, decimal block, ValueProp props, CardModel? cardSource, CardPlay? cardPlay)
    {
        if (cardSource != null)
        {
            if (cardSource.Owner.Creature != base.Owner)
            {
                return 0m;
            }
        }
        else if (base.Owner != target)
        {
            return 0m;
        }

        if (!props.HasFlag(ValueProp.Move) || props.HasFlag(ValueProp.Unpowered))
        {
            return 0m;
        }

        if (base.Owner.HasPower<OffenseAndDefensePower>() && base.Amount > 0m)
        {
            return 0m;
        }

        return -base.Amount;
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != base.Owner.Side || _temporaryAmount == 0m)
        {
            return;
        }

        decimal amountToRemove = _temporaryAmount;
        _temporaryAmount = 0m;
        Flash();
        await PowerCmd.Apply<WildnessPower>(base.Owner, -amountToRemove, base.Owner, null, silent: true);
    }
}

