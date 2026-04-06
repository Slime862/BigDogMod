using HarmonyLib;
using BigDogMod.Scripts.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;

namespace BigDogMod.Scripts.Patches;

[HarmonyPatch(typeof(TouchOfOrobas), nameof(TouchOfOrobas.GetUpgradedStarterRelic))]
public static class BigDogTouchOfOrobasUpgradePatch
{
    public static void Postfix(RelicModel starterRelic, ref RelicModel __result)
    {
        if (starterRelic is HoundCollar)
        {
            __result = ModelDb.Relic<HonorCollar>();
        }
    }
}
