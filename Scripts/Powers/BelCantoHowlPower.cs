using System.Collections.Generic;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Powers;

public sealed class BelCantoHowlPower : CustomPowerModel
{
    private sealed class Data
    {
        public bool TriggeredThisTurn;
    }

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromCard<BelCantoHowl>()];

    public override bool IsInstanced => true;

    protected override object InitInternalData()
    {
        return new Data();
    }

    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        if (card.Owner.Creature != base.Owner)
        {
            return playCount;
        }

        if (!card.Tags.Contains(BigDogTags.Jiao))
        {
            return playCount;
        }

        if (GetInternalData<Data>().TriggeredThisTurn)
        {
            return playCount;
        }

        return playCount + 1;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature == base.Owner && cardPlay.Card.Tags.Contains(BigDogTags.Jiao))
        {
            GetInternalData<Data>().TriggeredThisTurn = true;
        }

        return Task.CompletedTask;
    }

    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == base.Owner.Side)
        {
            GetInternalData<Data>().TriggeredThisTurn = false;
        }

        return Task.CompletedTask;
    }
}
