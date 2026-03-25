using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;

namespace BigDogMod.Scripts.ChewPrep;

public static class BigDogChewPrepEffectTypeExtensions
{
    public static string LocLineKey(this BigDogChewPrepEffectType effectType)
    {
        return effectType switch
        {
            BigDogChewPrepEffectType.Block => "blockLine",
            BigDogChewPrepEffectType.Weak => "weakLine",
            BigDogChewPrepEffectType.Bleeding => "bleedingLine",
            BigDogChewPrepEffectType.Draw => "drawLine",
            _ => effectType.ToString().ToLowerInvariant() + "Line"
        };
    }

    public static IHoverTip? HoverTip(this BigDogChewPrepEffectType effectType)
    {
        return effectType switch
        {
            BigDogChewPrepEffectType.Block => HoverTipFactory.Static(StaticHoverTip.Block),
            BigDogChewPrepEffectType.Weak => HoverTipFactory.FromPower<WeakPower>(),
            BigDogChewPrepEffectType.Bleeding => HoverTipFactory.FromPower<BleedingPower>(),
            _ => null
        };
    }
}
