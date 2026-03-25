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
                : ModelDb.Card<StrikeSilent>().PortraitPath;
            return false;
        }

        if (__instance is BigDogChew)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("big_dog_chew"))
                ? BigDogAssetPaths.CardPortrait("big_dog_chew")
                : ModelDb.Card<StrikeSilent>().PortraitPath;
            return false;
        }

        if (__instance is BigDogHowl)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("big_dog_howl"))
                ? BigDogAssetPaths.CardPortrait("big_dog_howl")
                : ModelDb.Card<StrikeSilent>().PortraitPath;
            return false;
        }

        if (__instance is StokeWildness)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("stoke_wildness"))
                ? BigDogAssetPaths.CardPortrait("stoke_wildness")
                : ModelDb.Card<DefendSilent>().PortraitPath;
            return false;
        }

        if (__instance is BleedOut or BloodDrink or ForceAwaken or VigilantHowl or IntimidatingHowl or PiercingScreech or WarmUpVoice
            or SustainedCharge or AllOutForce or FocusedForce or DauntingHowl or PanicScamper or FriendlyHowlFlow
            or AdmireVictory or Prelude or TailWag or Forget or LickWounds or JoyOfRegen or DogSage or ForcedDefense
            or BerserkerDog or ChewAtWill or CowardDog or EndlessBleeding)
        {
            __result = ModelDb.Card<DefendSilent>().PortraitPath;
            return false;
        }

        if (__instance is BloodlettingSlot or Cuteify or BelCantoHowl or HighSongForm or Makeover)
        {
            __result = ModelDb.Card<Survivor>().PortraitPath;
            return false;
        }

        if (__instance is Feint or Bloodthirst or FlurryScratch or RipOpen or ProofOfDeath)
        {
            __result = ModelDb.Card<StrikeSilent>().PortraitPath;
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
                : ModelDb.Card<StrikeSilent>().BetaPortraitPath;
            return false;
        }

        if (__instance is BigDogChew)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("big_dog_chew"))
                ? BigDogAssetPaths.CardBetaPortrait("big_dog_chew")
                : ModelDb.Card<StrikeSilent>().BetaPortraitPath;
            return false;
        }

        if (__instance is BigDogHowl)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("big_dog_howl"))
                ? BigDogAssetPaths.CardBetaPortrait("big_dog_howl")
                : ModelDb.Card<StrikeSilent>().BetaPortraitPath;
            return false;
        }

        if (__instance is StokeWildness)
        {
            __result = BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("stoke_wildness"))
                ? BigDogAssetPaths.CardBetaPortrait("stoke_wildness")
                : ModelDb.Card<DefendSilent>().BetaPortraitPath;
            return false;
        }

        if (__instance is BleedOut or BloodDrink or ForceAwaken or VigilantHowl or IntimidatingHowl or PiercingScreech or WarmUpVoice
            or SustainedCharge or AllOutForce or FocusedForce or DauntingHowl or PanicScamper or FriendlyHowlFlow
            or AdmireVictory or Prelude or TailWag or Forget or LickWounds or JoyOfRegen or DogSage or ForcedDefense
            or BerserkerDog or ChewAtWill or CowardDog or EndlessBleeding)
        {
            __result = ModelDb.Card<DefendSilent>().BetaPortraitPath;
            return false;
        }

        if (__instance is BloodlettingSlot or Cuteify or BelCantoHowl or HighSongForm or Makeover)
        {
            __result = ModelDb.Card<Survivor>().BetaPortraitPath;
            return false;
        }

        if (__instance is Feint or Bloodthirst or FlurryScratch or RipOpen or ProofOfDeath)
        {
            __result = ModelDb.Card<StrikeSilent>().BetaPortraitPath;
            return false;
        }

        return true;
    }
}
