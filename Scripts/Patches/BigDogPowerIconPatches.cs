using HarmonyLib;
using BigDogMod.Scripts.Assets;
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
            string fileName = __instance is TemporaryWildnessPower ? "temporary_wildness" : "wildness";
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.PowerIcon(fileName))
                ? BigDogAssetPaths.PowerIcon(fileName)
                : ModelDb.Power<StrengthPower>().IconPath;
        }
        else if (__instance is BleedingPower)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.PowerIcon("bleeding"))
                ? BigDogAssetPaths.PowerIcon("bleeding")
                : ModelDb.Power<PoisonPower>().IconPath;
        }
        else if (__instance is BleedingBoostPower)
        {
            __result = ModelDb.Power<PoisonPower>().IconPath;
        }
        else if (__instance is BigDogChewPrepPower)
        {
            __result = ModelDb.Power<WeakPower>().IconPath;
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
            string fileName = __instance is TemporaryWildnessPower ? "temporary_wildness" : "wildness";
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.PowerBetaIcon(fileName))
                ? BigDogAssetPaths.PowerBetaIcon(fileName)
                : ModelDb.Power<StrengthPower>().ResolvedBigIconPath;
        }
        else if (__instance is BleedingPower)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.PowerBetaIcon("bleeding"))
                ? BigDogAssetPaths.PowerBetaIcon("bleeding")
                : ModelDb.Power<PoisonPower>().ResolvedBigIconPath;
        }
        else if (__instance is BleedingBoostPower)
        {
            __result = ModelDb.Power<PoisonPower>().ResolvedBigIconPath;
        }
        else if (__instance is BigDogChewPrepPower)
        {
            __result = ModelDb.Power<WeakPower>().ResolvedBigIconPath;
        }
    }
}
