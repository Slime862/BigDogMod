using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.Fonts;

namespace BigDogMod.Scripts.Patches;

[HarmonyPatch(typeof(FontControlUtils), nameof(FontControlUtils.ApplyLocaleFontSubstitution))]
public static class LocManagerSafetyPatches
{
    public static bool Prefix(Control control, FontType fontType, StringName themeFontName)
    {
        return LocManager.Instance != null;
    }
}
