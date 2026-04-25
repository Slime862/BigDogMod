using HarmonyLib;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace BaseLib.Patches.Localization;

public interface IAddDumbVariablesToPowerDescription
{
    void AddDumbVariablesToPowerDescription(LocString description);
}

[HarmonyPatch(typeof(PowerModel), nameof(PowerModel.Description), MethodType.Getter)]
internal static class DumbPowerDescriptionPatch
{
    [HarmonyPostfix]
    private static void AddPowerDescriptionVariables(PowerModel __instance, ref LocString __result)
    {
        if (__instance is not IAddDumbVariablesToPowerDescription variableProvider)
        {
            return;
        }

        variableProvider.AddDumbVariablesToPowerDescription(__result);
    }
}
