using Godot;
using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.Characters;

namespace BigDogMod.Scripts;

[ModInitializer("Init")]
public static class Entry
{
    public static void Init()
    {
        ModHelper.AddModelToPool<DefectCardPool, BigBite>();
        ModHelper.AddModelToPool<DefectCardPool, LoyalGuard>();

        var harmony = new Harmony("sts2.bigdog.mod");
        harmony.PatchAll();
        ScriptManagerBridge.LookupScriptsInAssembly(typeof(Entry).Assembly);
        DumpLocalizationDiagnostics();
        Log.Debug("BigDog mod initialized.");
    }

    private static void DumpLocalizationDiagnostics()
    {
        Log.Info($"BigDog loc diag: character id = {ModelDb.GetId<BigDog>().Entry}");
        Log.Info($"BigDog loc diag: BigBite id = {ModelDb.GetId<BigBite>().Entry}");
        Log.Info($"BigDog loc diag: LoyalGuard id = {ModelDb.GetId<LoyalGuard>().Entry}");

        string[] languages = ["zhs", "eng"];
        string[] files = ["characters.json", "cards.json"];
        foreach (string language in languages)
        {
            foreach (string file in files)
            {
                string path = $"res://BigDogMod/localization/{language}/{file}";
                bool exists = ResourceLoader.Exists(path);
                Log.Info($"BigDog loc diag: ResourceLoader.Exists({path}) = {exists}");

                using FileAccess? fileAccess = FileAccess.Open(path, FileAccess.ModeFlags.Read);
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
