using System.Collections.Generic;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Scaffolding.Content;

namespace BaseLib.Abstracts;

public abstract class CustomCardModel : ModCardTemplate, ICustomModel
{
    protected CustomCardModel(int baseCost, CardType type, CardRarity rarity, TargetType target, bool showInCardLibrary = true, bool autoAdd = true)
        : base(baseCost, type, rarity, target, showInCardLibrary)
    {
    }

    protected virtual new IEnumerable<IHoverTip> ExtraHoverTips => [];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => ExtraHoverTips;
}
