using System.Collections.Generic;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Scaffolding.Content;

namespace BaseLib.Abstracts;

public abstract class CustomPowerModel : ModPowerTemplate, ICustomPower
{
    public virtual string? CustomPackedIconPath => CustomIconPath;

    public virtual string? CustomBigBetaIconPath => CustomBigIconPath;

    protected virtual new IEnumerable<IHoverTip> ExtraHoverTips => [];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => ExtraHoverTips;
}
