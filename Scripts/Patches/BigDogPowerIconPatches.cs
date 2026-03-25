using HarmonyLib;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace BigDogMod.Scripts.Patches;

internal static class BigDogPowerIconHelper
{
    public static string? GetCustomIconPath(PowerModel power)
    {
        if (power is WildnessPower)
        {
            string custom = BigDogAssetPaths.PowerIcon("wildness_power");
            return BigDogAssetPaths.Exists(custom) ? custom : ModelDb.Power<StrengthPower>().PackedIconPath;
        }

        if (power is BleedingPower)
        {
            string custom = BigDogAssetPaths.PowerIcon("bleeding_power");
            return BigDogAssetPaths.Exists(custom) ? custom : ModelDb.Power<PoisonPower>().PackedIconPath;
        }

        if (power is BleedingBoostPower)
        {
            return ModelDb.Power<PoisonPower>().PackedIconPath;
        }

        if (power is BelCantoHowlPower)
        {
            return ModelDb.Power<EchoFormPower>().PackedIconPath;
        }

        if (power is SustainedChargePower)
        {
            return ModelDb.Power<InfiniteBladesPower>().PackedIconPath;
        }

        if (power is HighSongFormPower)
        {
            return ModelDb.Power<CorruptionPower>().PackedIconPath;
        }

        if (power is PreludePower)
        {
            return ModelDb.Power<BufferPower>().PackedIconPath;
        }

        if (power is DogSagePower)
        {
            return ModelDb.Power<InfiniteBladesPower>().PackedIconPath;
        }

        if (power is BerserkerDogPower)
        {
            return ModelDb.Power<RitualPower>().PackedIconPath;
        }

        if (power is ChewAtWillPower)
        {
            return ModelDb.Power<DoubleDamagePower>().PackedIconPath;
        }

        if (power is CowardDogPower)
        {
            return ModelDb.Power<WeakPower>().PackedIconPath;
        }

        if (power is EndlessBleedingPower)
        {
            return ModelDb.Power<PoisonPower>().PackedIconPath;
        }

        if (power is BigDogChewPrepPower)
        {
            string custom = BigDogAssetPaths.PowerIcon("big_dog_chew_prep_power");
            return BigDogAssetPaths.Exists(custom) ? custom : ModelDb.Power<WeakPower>().PackedIconPath;
        }

        return null;
    }

    public static string? GetCustomBigIconPath(PowerModel power)
    {
        if (power is WildnessPower)
        {
            string beta = BigDogAssetPaths.PowerBetaIcon("wildness_power");
            if (BigDogAssetPaths.Exists(beta))
            {
                return beta;
            }

            string custom = BigDogAssetPaths.PowerIcon("wildness_power");
            return BigDogAssetPaths.Exists(custom) ? custom : ModelDb.Power<StrengthPower>().ResolvedBigIconPath;
        }

        if (power is BleedingPower)
        {
            string beta = BigDogAssetPaths.PowerBetaIcon("bleeding_power");
            if (BigDogAssetPaths.Exists(beta))
            {
                return beta;
            }

            string custom = BigDogAssetPaths.PowerIcon("bleeding_power");
            return BigDogAssetPaths.Exists(custom) ? custom : ModelDb.Power<PoisonPower>().ResolvedBigIconPath;
        }

        if (power is BleedingBoostPower)
        {
            return ModelDb.Power<PoisonPower>().ResolvedBigIconPath;
        }

        if (power is BelCantoHowlPower)
        {
            return ModelDb.Power<EchoFormPower>().ResolvedBigIconPath;
        }

        if (power is SustainedChargePower)
        {
            return ModelDb.Power<InfiniteBladesPower>().ResolvedBigIconPath;
        }

        if (power is HighSongFormPower)
        {
            return ModelDb.Power<CorruptionPower>().ResolvedBigIconPath;
        }

        if (power is PreludePower)
        {
            return ModelDb.Power<BufferPower>().ResolvedBigIconPath;
        }

        if (power is DogSagePower)
        {
            return ModelDb.Power<InfiniteBladesPower>().ResolvedBigIconPath;
        }

        if (power is BerserkerDogPower)
        {
            return ModelDb.Power<RitualPower>().ResolvedBigIconPath;
        }

        if (power is ChewAtWillPower)
        {
            return ModelDb.Power<DoubleDamagePower>().ResolvedBigIconPath;
        }

        if (power is CowardDogPower)
        {
            return ModelDb.Power<WeakPower>().ResolvedBigIconPath;
        }

        if (power is EndlessBleedingPower)
        {
            return ModelDb.Power<PoisonPower>().ResolvedBigIconPath;
        }

        if (power is BigDogChewPrepPower)
        {
            string beta = BigDogAssetPaths.PowerBetaIcon("big_dog_chew_prep_power");
            if (BigDogAssetPaths.Exists(beta))
            {
                return beta;
            }

            string custom = BigDogAssetPaths.PowerIcon("big_dog_chew_prep_power");
            return BigDogAssetPaths.Exists(custom) ? custom : ModelDb.Power<WeakPower>().ResolvedBigIconPath;
        }

        return null;
    }
}

[HarmonyPatch(typeof(PowerModel), nameof(PowerModel.PackedIconPath), MethodType.Getter)]
public static class BigDogPowerPackedIconPathPatch
{
    public static void Postfix(PowerModel __instance, ref string __result)
    {
        string? custom = BigDogPowerIconHelper.GetCustomIconPath(__instance);
        if (custom != null)
        {
            __result = custom;
        }
    }
}

[HarmonyPatch(typeof(PowerModel), nameof(PowerModel.ResolvedBigIconPath), MethodType.Getter)]
public static class BigDogPowerBigIconPathPatch
{
    public static void Postfix(PowerModel __instance, ref string __result)
    {
        string? custom = BigDogPowerIconHelper.GetCustomBigIconPath(__instance);
        if (custom != null)
        {
            __result = custom;
        }
    }
}
