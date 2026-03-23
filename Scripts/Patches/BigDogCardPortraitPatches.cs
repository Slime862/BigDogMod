using HarmonyLib;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Patches;

[HarmonyPatch(typeof(CardModel), nameof(CardModel.PortraitPath), MethodType.Getter)]
public static class BigDogCardPortraitPathPatch
{
    public static void Postfix(CardModel __instance, ref string __result)
    {
        if (__instance is RendingBite)
        {
            __result = ModelDb.Card<StrikeDefect>().PortraitPath;
        }
        else if (__instance is StokeWildness)
        {
            __result = ModelDb.Card<DefendDefect>().PortraitPath;
        }
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.BetaPortraitPath), MethodType.Getter)]
public static class BigDogCardBetaPortraitPathPatch
{
    public static void Postfix(CardModel __instance, ref string __result)
    {
        if (__instance is RendingBite)
        {
            __result = ModelDb.Card<StrikeDefect>().BetaPortraitPath;
        }
        else if (__instance is StokeWildness)
        {
            __result = ModelDb.Card<DefendDefect>().BetaPortraitPath;
        }
    }
}
