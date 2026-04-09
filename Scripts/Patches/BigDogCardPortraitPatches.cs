using HarmonyLib;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Cards;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace BigDogMod.Scripts.Patches;

public static class BigDogCardPortraitFallbacks
{
    public static CardModel? GetFallbackCard(CardModel card)
    {
        if (card is StrikeBigDog or RendingBite or BigDogChew or BigDogFakeChew or BigDogHowl
            or Feint or Bloodthirst or FlurryScratch or RipOpen or ProofOfDeath or MightyBlow or ForgetChew or BackDigging
            or AwakenImpulse or SharpenClaws or SneakAttack)
        {
            return ModelDb.Card<StrikeSilent>();
        }

        if (card is AncientWildness)
        {
            return ModelDb.Card<Relax>();
        }

        if (card is WolfHowl)
        {
            return ModelDb.Card<TheSealedThrone>();
        }

        if (card is DefendBigDog or StokeWildness or BleedOut or BloodDrink or ForceAwaken or VigilantHowl or IntimidatingHowl
            or PiercingScreech or WarmUpVoice or SustainedCharge or AllOutForce or FocusedForce or DauntingHowl or PanicScamper
            or FriendlyHowlFlow or AdmireVictory or Prelude or TailWag or Forget or LickWounds or JoyOfRegen or DogSage
            or ForcedDefense or BerserkerDog or SharkDog or ChewAtWill or CowardDog or BluesDog or EndlessBleeding
            or WiseHowl or HoldOn or FearlessBeast or FullyPrepared or FrenziedGrowth or HeavenlyHowl or Coagulate
            or PainIntoPower or SuddenRampage or LingeringEcho or EncouragingHowl or ResistImpulse or Impulse
            or ToxicBlood or BigOpenClose or Hesitation or SplashWater or RunAway or WatcherDog)
        {
            return ModelDb.Card<DefendSilent>();
        }

        if (card is Finale)
        {
            return ModelDb.Card<StrikeSilent>();
        }

        if (card is BloodlettingSlot or Cuteify or BelCantoHowl or HighSongForm or Makeover or PetrifiedSkin
            or HellhoundIncarnation or OffenseAndDefense or LoyalFriend or HealingSong or AttackWindup)
        {
            return ModelDb.Card<Survivor>();
        }

        return null;
    }

    public static string? GetCustomPortraitPath(CardModel card)
    {
        return card switch
        {
            StrikeBigDog => BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("strike_big_dog")) ? BigDogAssetPaths.CardPortrait("strike_big_dog") : null,
            DefendBigDog => BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("defend_big_dog")) ? BigDogAssetPaths.CardPortrait("defend_big_dog") : null,
            RendingBite => BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("rending_bite")) ? BigDogAssetPaths.CardPortrait("rending_bite") : null,
            BigDogChew => BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("big_dog_chew")) ? BigDogAssetPaths.CardPortrait("big_dog_chew") : null,
            BigDogFakeChew => BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("big_dog_fake_chew")) ? BigDogAssetPaths.CardPortrait("big_dog_fake_chew") : null,
            BigDogHowl => BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("big_dog_howl")) ? BigDogAssetPaths.CardPortrait("big_dog_howl") : null,
            StokeWildness => BigDogAssetPaths.Exists(BigDogAssetPaths.CardPortrait("stoke_wildness")) ? BigDogAssetPaths.CardPortrait("stoke_wildness") : null,
            _ => null
        };
    }

    public static string? GetCustomBetaPortraitPath(CardModel card)
    {
        return card switch
        {
            StrikeBigDog => BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("strike_big_dog")) ? BigDogAssetPaths.CardBetaPortrait("strike_big_dog") : null,
            DefendBigDog => BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("defend_big_dog")) ? BigDogAssetPaths.CardBetaPortrait("defend_big_dog") : null,
            RendingBite => BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("rending_bite")) ? BigDogAssetPaths.CardBetaPortrait("rending_bite") : null,
            BigDogChew => BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("big_dog_chew")) ? BigDogAssetPaths.CardBetaPortrait("big_dog_chew") : null,
            BigDogFakeChew => BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("big_dog_fake_chew")) ? BigDogAssetPaths.CardBetaPortrait("big_dog_fake_chew") : null,
            BigDogHowl => BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("big_dog_howl")) ? BigDogAssetPaths.CardBetaPortrait("big_dog_howl") : null,
            StokeWildness => BigDogAssetPaths.Exists(BigDogAssetPaths.CardBetaPortrait("stoke_wildness")) ? BigDogAssetPaths.CardBetaPortrait("stoke_wildness") : null,
            _ => null
        };
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.PortraitPath), MethodType.Getter)]
[HarmonyPriority(Priority.First)]
public static class BigDogCardPortraitPathPatch
{
    public static bool Prefix(CardModel __instance, ref string __result)
    {
        string? customPath = BigDogCardPortraitFallbacks.GetCustomPortraitPath(__instance);
        if (customPath != null)
        {
            __result = customPath;
            return false;
        }

        CardModel? fallback = BigDogCardPortraitFallbacks.GetFallbackCard(__instance);
        if (fallback != null)
        {
            __result = fallback.PortraitPath;
            return false;
        }

        return true;
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.BetaPortraitPath), MethodType.Getter)]
[HarmonyPriority(Priority.First)]
public static class BigDogCardBetaPortraitPathPatch
{
    public static bool Prefix(CardModel __instance, ref string __result)
    {
        string? customPath = BigDogCardPortraitFallbacks.GetCustomBetaPortraitPath(__instance);
        if (customPath != null)
        {
            __result = customPath;
            return false;
        }

        CardModel? fallback = BigDogCardPortraitFallbacks.GetFallbackCard(__instance);
        if (fallback != null)
        {
            __result = fallback.BetaPortraitPath;
            return false;
        }

        return true;
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.Portrait), MethodType.Getter)]
[HarmonyPriority(Priority.First)]
public static class BigDogCardPortraitTexturePatch
{
    public static bool Prefix(CardModel __instance, ref Texture2D __result)
    {
        string? customPath = BigDogCardPortraitFallbacks.GetCustomPortraitPath(__instance);
        if (customPath != null)
        {
            __result = ResourceLoader.Load<Texture2D>(customPath);
            return false;
        }

        CardModel? fallback = BigDogCardPortraitFallbacks.GetFallbackCard(__instance);
        if (fallback != null)
        {
            __result = fallback.Portrait;
            return false;
        }

        return true;
    }
}

[HarmonyPatch(typeof(NCard), nameof(NCard.UpdateVisuals))]
public static class BigDogNCardPortraitFallbackPatch
{
    private static readonly AccessTools.FieldRef<NCard, TextureRect> PortraitField =
        AccessTools.FieldRefAccess<NCard, TextureRect>("_portrait");

    private static readonly AccessTools.FieldRef<NCard, TextureRect> AncientPortraitField =
        AccessTools.FieldRefAccess<NCard, TextureRect>("_ancientPortrait");

    public static void Postfix(NCard __instance)
    {
        CardModel? model = __instance.Model;
        if (model == null)
        {
            return;
        }

        string? customPath = BigDogCardPortraitFallbacks.GetCustomPortraitPath(model);
        if (customPath != null)
        {
            Texture2D? customPortrait = ResourceLoader.Load<Texture2D>(customPath);
            if (customPortrait != null)
            {
                PortraitField(__instance).Texture = customPortrait;
                AncientPortraitField(__instance).Texture = customPortrait;
                return;
            }
        }

        CardModel? fallback = BigDogCardPortraitFallbacks.GetFallbackCard(model);
        if (fallback == null)
        {
            return;
        }

        Texture2D portrait = fallback.Portrait;
        PortraitField(__instance).Texture = portrait;
        AncientPortraitField(__instance).Texture = portrait;
    }
}
