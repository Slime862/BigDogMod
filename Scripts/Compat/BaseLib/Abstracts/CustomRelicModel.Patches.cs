using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;

namespace BaseLib.Abstracts;

[HarmonyPatch(typeof(TouchOfOrobas), nameof(TouchOfOrobas.GetUpgradedStarterRelic))]
internal static class StarterUpgradeCompatPatch
{
    [HarmonyPrefix]
    private static bool CustomStarterUpgrade(RelicModel starterRelic, ref RelicModel? __result)
    {
        if (starterRelic is not CustomRelicModel customRelic)
        {
            return true;
        }

        __result = customRelic.GetUpgradeReplacement();
        return __result == null;
    }
}
