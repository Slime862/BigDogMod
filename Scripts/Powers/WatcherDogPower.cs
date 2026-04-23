using System;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace BigDogMod.Scripts.Powers;

public sealed class WatcherDogPower : CustomPowerModel
{
    private bool _skipNextTrigger = true;

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override bool IsInstanced => true;

    protected override bool IsVisibleInternal => false;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner?.Creature != base.Owner)
        {
            return;
        }

        if (_skipNextTrigger)
        {
            _skipNextTrigger = false;
            return;
        }

        WildnessPower? wildness = base.Owner.GetPower<WildnessPower>();
        if (wildness == null)
        {
            return;
        }

        decimal step = base.Amount;
        if (step <= 0m)
        {
            return;
        }

        decimal current = wildness.TemporaryAmount;
        decimal next = current >= 0m ? -(current + step) : Math.Abs(current) + step;
        decimal delta = next - current;
        if (delta == 0)
        {
            return;
        }

        Flash();
        await PowerCmd.Apply<WildnessPower>(base.Owner, delta, base.Owner, null, silent: true);
        wildness.AddTemporaryAmount(delta);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == base.Owner.Side)
        {
            await PowerCmd.Remove(this);
        }
    }
}
