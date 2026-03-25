using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace BigDogMod.Scripts.Powers;

public sealed class DogSagePower : CustomPowerModel
{
    private bool _triggeredThisTurn;

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (_triggeredThisTurn || cardPlay.Card.Owner.Creature != base.Owner || !cardPlay.Card.Tags.Contains(BigDogTags.Jiao))
        {
            return;
        }

        if (base.Owner.Player == null)
        {
            return;
        }

        _triggeredThisTurn = true;
        Flash();
        await CardPileCmd.Draw(context, Amount, base.Owner.Player);
    }

    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == base.Owner.Side)
        {
            _triggeredThisTurn = false;
        }

        return Task.CompletedTask;
    }
}
