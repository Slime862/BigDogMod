using System.Collections.Generic;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Powers;

public sealed class HighSongFormPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromCard<BigDogChew>()];

    public override (PileType, CardPilePosition) ModifyCardPlayResultPileTypeAndPosition(CardModel card, bool isAutoPlay, ResourceInfo resources, PileType pileType, CardPilePosition position)
    {
        if (card.Owner.Creature != base.Owner || card is not BigDogChew || pileType != PileType.Exhaust)
        {
            return (pileType, position);
        }

        return (PileType.Discard, position);
    }
}
