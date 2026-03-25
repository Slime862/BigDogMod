using HarmonyLib;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Patches;

[HarmonyPatch(typeof(CardModel), nameof(CardModel.PortraitPath), MethodType.Getter)]
public static class BigDogCardPortraitPathPatch
{
    public static bool Prefix(CardModel __instance, ref string __result)
    {
        if (__instance is RendingBite)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("rending_bite"))
                ? BigDogAssetPaths.CardPortrait("rending_bite")
                : ModelDb.Card<StrikeDefect>().PortraitPath;
            return false;
        }

        if (__instance is BigDogChew)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("big_dog_chew"))
                ? BigDogAssetPaths.CardPortrait("big_dog_chew")
                : ModelDb.Card<StrikeDefect>().PortraitPath;
            return false;
        }

        if (__instance is BigDogHowl)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("big_dog_howl"))
                ? BigDogAssetPaths.CardPortrait("big_dog_howl")
                : ModelDb.Card<Zap>().PortraitPath;
            return false;
        }

        if (__instance is StokeWildness)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("stoke_wildness"))
                ? BigDogAssetPaths.CardPortrait("stoke_wildness")
                : ModelDb.Card<DefendDefect>().PortraitPath;
            return false;
        }

        if (__instance is BleedOut or BloodDrink or ForceAwaken or VigilantHowl or IntimidatingHowl or PiercingScreech or WarmUpVoice)
        {
            __result = ModelDb.Card<DefendDefect>().PortraitPath;
            return false;
        }

        if (__instance is BloodlettingSlot or Cuteify or BelCantoHowl)
        {
            __result = ModelDb.Card<Zap>().PortraitPath;
            return false;
        }

        return true;
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.BetaPortraitPath), MethodType.Getter)]
public static class BigDogCardBetaPortraitPathPatch
{
    public static bool Prefix(CardModel __instance, ref string __result)
    {
        if (__instance is RendingBite)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("rending_bite"))
                ? BigDogAssetPaths.CardBetaPortrait("rending_bite")
                : ModelDb.Card<StrikeDefect>().BetaPortraitPath;
            return false;
        }

        if (__instance is BigDogChew)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("big_dog_chew"))
                ? BigDogAssetPaths.CardBetaPortrait("big_dog_chew")
                : ModelDb.Card<StrikeDefect>().BetaPortraitPath;
            return false;
        }

        if (__instance is BigDogHowl)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("big_dog_howl"))
                ? BigDogAssetPaths.CardBetaPortrait("big_dog_howl")
                : ModelDb.Card<Zap>().BetaPortraitPath;
            return false;
        }

        if (__instance is StokeWildness)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("stoke_wildness"))
                ? BigDogAssetPaths.CardBetaPortrait("stoke_wildness")
                : ModelDb.Card<DefendDefect>().BetaPortraitPath;
            return false;
        }

        if (__instance is BleedOut or BloodDrink or ForceAwaken or VigilantHowl or IntimidatingHowl or PiercingScreech or WarmUpVoice)
        {
            __result = ModelDb.Card<DefendDefect>().BetaPortraitPath;
            return false;
        }

        if (__instance is BloodlettingSlot or Cuteify or BelCantoHowl)
        {
            __result = ModelDb.Card<Zap>().BetaPortraitPath;
            return false;
        }

        return true;
    }
}
