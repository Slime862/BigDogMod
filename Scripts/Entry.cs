using Godot;
using Godot.Bridge;
using HarmonyLib;
using BaseLib.Config;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using BigDogMod.Scripts.Config;
using STS2RitsuLib.Interop;

namespace BigDogMod.Scripts;

[ModInitializer("Init")]
public static class Entry
{
    public static void Init()
    {
        ModConfigRegistry.Register("BigDogMod", new BigDogModConfig());
        ModTypeDiscoveryHub.RegisterModAssembly("BigDogMod", typeof(Entry).Assembly);

        var harmony = new Harmony("sts2.bigdog.mod");
        harmony.PatchAll();
        ScriptManagerBridge.LookupScriptsInAssembly(typeof(Entry).Assembly);
        Log.Debug("BigDog mod initialized.");
    }
}
