using HarmonyLib;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace BigDogMod.Scripts.Patches;

[HarmonyPatch(typeof(PowerModel), nameof(PowerModel.IconPath), MethodType.Getter)]
public static class BigDogPowerIconPathPatch
{
    public static void Postfix(PowerModel __instance, ref string __result)
    {
        if (__instance is WildnessPower or TemporaryWildnessPower)
        {
            __result = ModelDb.Power<StrengthPower>().IconPath;
        }
        else if (__instance is BleedingPower)
        {
            __result = ModelDb.Power<PoisonPower>().IconPath;
        }
    }
}

[HarmonyPatch(typeof(PowerModel), nameof(PowerModel.ResolvedBigIconPath), MethodType.Getter)]
public static class BigDogPowerBigIconPathPatch
{
    public static void Postfix(PowerModel __instance, ref string __result)
    {
        if (__instance is WildnessPower or TemporaryWildnessPower)
        {
            __result = ModelDb.Power<StrengthPower>().ResolvedBigIconPath;
        }
        else if (__instance is BleedingPower)
        {
            __result = ModelDb.Power<PoisonPower>().ResolvedBigIconPath;
        }
    }
}
