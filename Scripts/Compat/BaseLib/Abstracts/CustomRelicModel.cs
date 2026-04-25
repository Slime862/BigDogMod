using System.Collections.Generic;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Scaffolding.Content;

namespace BaseLib.Abstracts;

public abstract class CustomRelicModel : ModRelicTemplate, ICustomModel
{
    protected CustomRelicModel(bool autoAdd = true)
    {
    }

    protected virtual new IEnumerable<IHoverTip> ExtraHoverTips => [];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => ExtraHoverTips;

    public virtual RelicModel? GetUpgradeReplacement() => null;
}
