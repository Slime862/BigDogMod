using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarmonyLib;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.Characters;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace BigDogMod.Scripts.Patches;

[HarmonyPatch(typeof(ArchaicTooth), nameof(ArchaicTooth.SetupForPlayer))]
public static class BigDogArchaicToothSetupPatch
{
    public static bool Prefix(ArchaicTooth __instance, Player player, ref bool __result)
    {
        if (player.Character is not BigDog)
        {
            return true;
        }

        CardModel? starterCard = player.Deck.Cards.FirstOrDefault(c => c.Id == ModelDb.Card<StokeWildness>().Id);
        if (starterCard == null)
        {
            return true;
        }

        CardModel ancientCard = CreateAncientWildnessFromStarter(player, starterCard);
        __instance.SetupForTests(starterCard.ToSerializable(), ancientCard.ToSerializable());
        __result = true;
        return false;
    }

    internal static CardModel CreateAncientWildnessFromStarter(Player player, CardModel starterCard)
    {
        CardModel ancientCard = player.RunState.CreateCard<AncientWildness>(player);
        if (starterCard.IsUpgraded)
        {
            CardCmd.Upgrade(ancientCard);
        }

        if (starterCard.Enchantment != null)
        {
            EnchantmentModel enchantment = (EnchantmentModel)starterCard.Enchantment.MutableClone();
            CardCmd.Enchant(enchantment, ancientCard, enchantment.Amount);
        }

        return ancientCard;
    }
}

[HarmonyPatch(typeof(ArchaicTooth), nameof(ArchaicTooth.AfterObtained))]
public static class BigDogArchaicToothAfterObtainedPatch
{
    public static bool Prefix(ArchaicTooth __instance, ref Task __result)
    {
        if (__instance.Owner?.Character is not BigDog)
        {
            return true;
        }

        __result = ApplyBigDogAncientTransform(__instance);
        return false;
    }

    private static async Task ApplyBigDogAncientTransform(ArchaicTooth relic)
    {
        CardModel? starterCard = relic.Owner.Deck.Cards.FirstOrDefault(c => c.Id == ModelDb.Card<StokeWildness>().Id);
        if (starterCard == null)
        {
            return;
        }

        CardModel ancientCard = BigDogArchaicToothSetupPatch.CreateAncientWildnessFromStarter(relic.Owner, starterCard);
        await CardCmd.Transform(starterCard, ancientCard);
    }
}

[HarmonyPatch(typeof(DustyTome), nameof(DustyTome.SetupForPlayer))]
public static class BigDogDustyTomeSetupPatch
{
    public static bool Prefix(DustyTome __instance, Player player)
    {
        if (player.Character is not BigDog)
        {
            return true;
        }

        IEnumerable<CardModel> items = player.Character.CardPool
            .GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint)
            .Where(c => c.Rarity == CardRarity.Ancient && c.Id != ModelDb.Card<AncientWildness>().Id);

        CardModel? selected = player.PlayerRng.Rewards.NextItem(items);
        if (selected != null)
        {
            __instance.AncientCard = selected.Id;
        }

        return false;
    }
}
