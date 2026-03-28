using System.Collections.Generic;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.Pools;
using BigDogMod.Scripts.Relics;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;

namespace BigDogMod.Scripts.Characters;

public sealed class BigDog : PlaceholderCharacterModel
{
    public override string PlaceholderID => "silent";

    public override Color NameColor => StsColors.green;

    public override Color EnergyLabelOutlineColor => new("004f04FF");

    public override CharacterGender Gender => CharacterGender.Masculine;

    protected override CharacterModel? UnlocksAfterRunAs => null;

    public override int StartingHp => 75;

    public override int StartingGold => 99;

    public override int BaseOrbSlotCount => 0;

    public override CardPoolModel CardPool => ModelDb.CardPool<BigDogCardPool>();

    public override RelicPoolModel RelicPool => ModelDb.RelicPool<BigDogRelicPool>();

    public override PotionPoolModel PotionPool => ModelDb.PotionPool<BigDogPotionPool>();

    // Uncomment these one by one after the matching assets are ready.
    // Until then, keep borrowing Silent assets through PlaceholderID = "silent".
    // public override string CustomVisualPath => BigDogAssetPaths.CharacterVisualsScene;
    // public override string CustomTrailPath => BigDogAssetPaths.CharacterTrailScene;
    // public override string? CustomMapMarkerPath => BigDogAssetPaths.CharacterMapMarker;
    // public override string CustomIconPath => BigDogAssetPaths.CharacterIconScene;
    // public override string? CustomIconTexturePath => BigDogAssetPaths.CharacterTopPanelIcon;
    // public override string CustomEnergyCounterPath => BigDogAssetPaths.CharacterEnergyCounterScene;
    // public override string CustomRestSiteAnimPath => BigDogAssetPaths.CharacterRestSiteScene;
    // public override string CustomMerchantAnimPath => BigDogAssetPaths.CharacterMerchantScene;
    // public override string CustomArmPointingTexturePath => BigDogAssetPaths.HandPoint;
    // public override string CustomArmRockTexturePath => BigDogAssetPaths.HandRock;
    // public override string CustomArmPaperTexturePath => BigDogAssetPaths.HandPaper;
    // public override string CustomArmScissorsTexturePath => BigDogAssetPaths.HandScissors;
    // public override string CustomCharacterSelectBg => BigDogAssetPaths.CharacterSelectBgScene;
    // public override string CustomCharacterSelectTransitionPath => BigDogAssetPaths.CharacterTransitionMaterial;
    // public override string? CustomCharacterSelectIconPath => BigDogAssetPaths.CharacterSelectIcon;
    // public override string? CustomCharacterSelectLockedIconPath => BigDogAssetPaths.CharacterSelectLockedIcon;

    public override IEnumerable<CardModel> StartingDeck =>
        [
            ModelDb.Card<StrikeSilent>(),
            ModelDb.Card<StrikeSilent>(),
            ModelDb.Card<StrikeSilent>(),
            ModelDb.Card<StrikeSilent>(),
            ModelDb.Card<DefendSilent>(),
            ModelDb.Card<DefendSilent>(),
            ModelDb.Card<DefendSilent>(),
            ModelDb.Card<DefendSilent>(),
            ModelDb.Card<StokeWildness>(),
            ModelDb.Card<BigDogHowl>()
        ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
        [ModelDb.Relic<HoundCollar>()];

    public override float AttackAnimDelay => 0.15f;

    public override float CastAnimDelay => 0.25f;

    public override Color DialogueColor => new("284719");

    public override Color MapDrawingColor => new("2F6729");

    public override Color RemoteTargetingLineColor => new("2EBD5EFF");

    public override Color RemoteTargetingLineOutline => new("004f04FF");

    public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";

    public override List<string> GetArchitectAttackVfx()
    {
        return
        [
            "vfx/vfx_attack_lightning",
            "vfx/vfx_attack_blunt",
            "vfx/vfx_scratch",
            "vfx/vfx_attack_slash",
            "vfx/vfx_heavy_blunt"
        ];
    }
}
