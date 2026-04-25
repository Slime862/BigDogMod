using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

using STS2RitsuLib.Interop.AutoRegistration;

namespace BigDogMod.Scripts.Powers;

[RegisterPower()]
public sealed class FinaleTrackerPower : CustomPowerModel
{
    private int _jiaoCountThisTurn;

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override bool IsInstanced => true;

    protected override bool IsVisibleInternal => false;

    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (dealer != base.Owner || cardSource is not Finale finale)
        {
            return 0m;
        }

        if (!props.HasFlag(ValueProp.Move) || props.HasFlag(ValueProp.Unpowered))
        {
            return 0m;
        }

        return _jiaoCountThisTurn * finale.DynamicVars["Bonus"].BaseValue;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner?.Creature != base.Owner)
        {
            return Task.CompletedTask;
        }

        if (BigDogCardTraits.IsJiao(cardPlay.Card))
        {
            _jiaoCountThisTurn++;
        }

        return Task.CompletedTask;
    }

    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == base.Owner.Side)
        {
            _jiaoCountThisTurn = 0;
        }

        return Task.CompletedTask;
    }
}


