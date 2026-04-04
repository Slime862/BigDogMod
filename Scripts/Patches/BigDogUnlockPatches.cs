using HarmonyLib;
using BigDogMod.Scripts.Pools;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Saves.Managers;

namespace BigDogMod.Scripts.Patches;

[HarmonyPatch(typeof(ProgressSaveManager), nameof(ProgressSaveManager.LoadProgress))]
public static class BigDogUnlockLoadProgressPatch
{
    public static void Postfix(ProgressSaveManager __instance)
    {
        bool changed = false;

        foreach (CardModel card in ModelDb.CardPool<BigDogCardPool>().AllCards)
        {
            if (__instance.Progress.MarkCardAsSeen(card.Id))
            {
                changed = true;
            }
        }

        if (changed)
        {
            __instance.SaveProgress();
        }
    }
}
