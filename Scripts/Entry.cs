using Godot;
using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;

namespace BigDogMod.Scripts;

[ModInitializer("Init")]
public static class Entry
{
    public static void Init()
    {
        ModHelper.AddModelToPool<BigDogCardPool, StokeWildness>();
        ModHelper.AddModelToPool<BigDogCardPool, BigDogHowl>();
        ModHelper.AddModelToPool<BigDogCardPool, StrikeBigDog>();
        ModHelper.AddModelToPool<BigDogCardPool, DefendBigDog>();
        ModHelper.AddModelToPool<BigDogCardPool, RendingBite>();
        ModHelper.AddModelToPool<BigDogCardPool, BleedOut>();
        ModHelper.AddModelToPool<BigDogCardPool, BloodDrink>();
        ModHelper.AddModelToPool<BigDogCardPool, BloodlettingSlot>();
        ModHelper.AddModelToPool<BigDogCardPool, ForceAwaken>();
        ModHelper.AddModelToPool<BigDogCardPool, Cuteify>();
        ModHelper.AddModelToPool<BigDogCardPool, VigilantHowl>();
        ModHelper.AddModelToPool<BigDogCardPool, IntimidatingHowl>();
        ModHelper.AddModelToPool<BigDogCardPool, BelCantoHowl>();
        ModHelper.AddModelToPool<BigDogCardPool, PiercingScreech>();
        ModHelper.AddModelToPool<BigDogCardPool, WarmUpVoice>();
        ModHelper.AddModelToPool<BigDogCardPool, SustainedCharge>();
        ModHelper.AddModelToPool<BigDogCardPool, AllOutForce>();
        ModHelper.AddModelToPool<BigDogCardPool, FocusedForce>();
        ModHelper.AddModelToPool<BigDogCardPool, HighSongForm>();
        ModHelper.AddModelToPool<BigDogCardPool, DauntingHowl>();
        ModHelper.AddModelToPool<BigDogCardPool, Feint>();
        ModHelper.AddModelToPool<BigDogCardPool, PanicScamper>();
        ModHelper.AddModelToPool<BigDogCardPool, FriendlyHowlFlow>();
        ModHelper.AddModelToPool<BigDogCardPool, AdmireVictory>();
        ModHelper.AddModelToPool<BigDogCardPool, Bloodthirst>();
        ModHelper.AddModelToPool<BigDogCardPool, Prelude>();
        ModHelper.AddModelToPool<BigDogCardPool, TailWag>();
        ModHelper.AddModelToPool<BigDogCardPool, Makeover>();
        ModHelper.AddModelToPool<BigDogCardPool, FlurryScratch>();
        ModHelper.AddModelToPool<BigDogCardPool, Forget>();
        ModHelper.AddModelToPool<BigDogCardPool, LickWounds>();
        ModHelper.AddModelToPool<BigDogCardPool, RipOpen>();
        ModHelper.AddModelToPool<BigDogCardPool, JoyOfRegen>();
        ModHelper.AddModelToPool<BigDogCardPool, DogSage>();
        ModHelper.AddModelToPool<BigDogCardPool, ForcedDefense>();
        ModHelper.AddModelToPool<BigDogCardPool, BerserkerDog>();
        ModHelper.AddModelToPool<BigDogCardPool, ChewAtWill>();
        ModHelper.AddModelToPool<BigDogCardPool, CowardDog>();
        ModHelper.AddModelToPool<BigDogCardPool, ProofOfDeath>();
        ModHelper.AddModelToPool<BigDogCardPool, WiseHowl>();
        ModHelper.AddModelToPool<BigDogCardPool, PetrifiedSkin>();
        ModHelper.AddModelToPool<BigDogCardPool, HoldOn>();
        ModHelper.AddModelToPool<BigDogCardPool, FearlessBeast>();
        ModHelper.AddModelToPool<BigDogCardPool, MightyBlow>();
        ModHelper.AddModelToPool<BigDogCardPool, EndlessBleeding>();
        ModHelper.AddModelToPool<BigDogCardPool, FullyPrepared>();
        ModHelper.AddModelToPool<BigDogCardPool, FrenziedGrowth>();
        ModHelper.AddModelToPool<TokenCardPool, BigDogChew>();

        var harmony = new Harmony("sts2.bigdog.mod");
        harmony.PatchAll();
        ScriptManagerBridge.LookupScriptsInAssembly(typeof(Entry).Assembly);
        Log.Debug("BigDog mod initialized.");
    }
}
