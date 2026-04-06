using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Screens.CardLibrary;

namespace BigDogMod.Scripts.Patches;

[HarmonyPatch(typeof(NCardLibraryGrid), "_Ready")]
public static class BigDogCardLibraryReadyPatch
{
    public static void Postfix(NCardLibraryGrid __instance)
    {
        List<CardModel> allCards = AccessTools.Field(typeof(NCardLibraryGrid), "_allCards").GetValue(__instance) as List<CardModel>
            ?? new List<CardModel>();

        AddIfMissing(allCards, ModelDb.Card<AncientWildness>());
        AddIfMissing(allCards, ModelDb.Card<WolfHowl>());
    }

    private static void AddIfMissing(List<CardModel> allCards, CardModel card)
    {
        if (!allCards.Contains(card))
        {
            allCards.Add(card);
        }
    }
}

[HarmonyPatch(typeof(NCardLibraryGrid), nameof(NCardLibraryGrid.RefreshVisibility))]
public static class BigDogCardLibraryVisibilityPatch
{
    public static void Postfix(NCardLibraryGrid __instance)
    {
        HashSet<CardModel> unlockedCards = AccessTools.Field(typeof(NCardLibraryGrid), "_unlockedCards").GetValue(__instance) as HashSet<CardModel>
            ?? new HashSet<CardModel>();

        unlockedCards.Add(ModelDb.Card<AncientWildness>());
        unlockedCards.Add(ModelDb.Card<WolfHowl>());
    }
}

[HarmonyPatch(typeof(NCardLibraryGrid), nameof(NCardLibraryGrid.FilterCards), [typeof(System.Func<CardModel, bool>), typeof(List<SortingOrders>)])]
public static class BigDogCardLibraryFilterPatch
{
    private static readonly System.Reflection.MethodInfo DisplayCardsMethod =
        AccessTools.Method(typeof(NCardLibraryGrid), "DisplayCards");

    public static bool Prefix(NCardLibraryGrid __instance, System.Func<CardModel, bool> filter, List<SortingOrders> sortingPriority)
    {
        List<CardModel> allCards = AccessTools.Field(typeof(NCardLibraryGrid), "_allCards").GetValue(__instance) as List<CardModel>
            ?? new List<CardModel>();

        List<CardModel> cards = allCards.Where(filter).ToList();
        AddIfMatches(cards, filter, ModelDb.Card<AncientWildness>());
        AddIfMatches(cards, filter, ModelDb.Card<WolfHowl>());

        DisplayCardsMethod.Invoke(__instance, [cards, sortingPriority]);
        return false;
    }

    private static void AddIfMatches(List<CardModel> cards, System.Func<CardModel, bool> filter, CardModel card)
    {
        if (!cards.Contains(card) && filter(card))
        {
            cards.Add(card);
        }
    }
}
