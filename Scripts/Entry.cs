using Godot;
using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
using BigDogMod.Scripts.Powers;

namespace BigDogMod.Scripts;

[ModInitializer("Init")]
public static class Entry
{
    public static void Init()
    {
        ModHelper.AddModelToPool<BigDogCardPool, StokeWildness>();
        ModHelper.AddModelToPool<BigDogCardPool, BigDogHowl>();
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
        ModHelper.AddModelToPool<TokenCardPool, BigDogChew>();

        var harmony = new Harmony("sts2.bigdog.mod");
        harmony.PatchAll();
        ScriptManagerBridge.LookupScriptsInAssembly(typeof(Entry).Assembly);
        DumpLocalizationDiagnostics();
        Log.Debug("BigDog mod initialized.");
    }

    private static void DumpLocalizationDiagnostics()
    {
        Log.Info($"BigDog loc diag: character id = {ModelDb.GetId<BigDog>().Entry}");
        Log.Info($"BigDog loc diag: StokeWildness id = {ModelDb.GetId<StokeWildness>().Entry}");
        Log.Info($"BigDog loc diag: BigDogHowl id = {ModelDb.GetId<BigDogHowl>().Entry}");
        Log.Info($"BigDog loc diag: RendingBite id = {ModelDb.GetId<RendingBite>().Entry}");
        Log.Info($"BigDog loc diag: BigDogChew id = {ModelDb.GetId<BigDogChew>().Entry}");
        Log.Info($"BigDog loc diag: WildnessPower id = {ModelDb.GetId<WildnessPower>().Entry}");
        Log.Info($"BigDog loc diag: BleedingPower id = {ModelDb.GetId<BleedingPower>().Entry}");

        string[] languages = ["zhs", "eng"];
        string[] files = ["characters.json", "cards.json", "powers.json", "static_hover_tips.json", "card_keywords.json"];
        foreach (string language in languages)
        {
            foreach (string file in files)
            {
                string path = $"res://BigDogMod/localization/{language}/{file}";
                bool exists = ResourceLoader.Exists(path);
                Log.Info($"BigDog loc diag: ResourceLoader.Exists({path}) = {exists}");

                using Godot.FileAccess? fileAccess = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
                if (fileAccess == null)
                {
                    Log.Warn($"BigDog loc diag: FileAccess.Open failed for {path}");
                    continue;
                }

                string text = fileAccess.GetAsText();
                string preview = text.Length > 180 ? text.Substring(0, 180) : text;
                Log.Info($"BigDog loc diag: opened {path}, preview = {preview}");
            }
        }
    }
}
