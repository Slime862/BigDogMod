using System.Collections.Generic;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.Pools;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;

namespace BigDogMod.Scripts.Characters;

public sealed class BigDog : PlaceholderCharacterModel
{
    public override string PlaceholderID => "defect";

    public override Color NameColor => StsColors.blue;

    public override Color EnergyLabelOutlineColor => new("163E64FF");

    public override CharacterGender Gender => CharacterGender.Masculine;

    protected override CharacterModel? UnlocksAfterRunAs => null;

    public override int StartingHp => 75;

    public override int StartingGold => 99;

    public override int BaseOrbSlotCount => 3;

    public override CardPoolModel CardPool => ModelDb.CardPool<BigDogCardPool>();

    public override RelicPoolModel RelicPool => ModelDb.RelicPool<BigDogRelicPool>();

    public override PotionPoolModel PotionPool => ModelDb.PotionPool<BigDogPotionPool>();

    // Uncomment these one by one after the matching assets are ready.
    // Until then, keep borrowing Defect assets through PlaceholderID = "defect".
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
            ModelDb.Card<StrikeDefect>(),
            ModelDb.Card<StrikeDefect>(),
            ModelDb.Card<StrikeDefect>(),
            ModelDb.Card<StrikeDefect>(),
            ModelDb.Card<DefendDefect>(),
            ModelDb.Card<DefendDefect>(),
            ModelDb.Card<DefendDefect>(),
            ModelDb.Card<DefendDefect>(),
            ModelDb.Card<StokeWildness>(),
            ModelDb.Card<BigDogHowl>()
        ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
        [ModelDb.Relic<CrackedCore>()];

    public override float AttackAnimDelay => 0.15f;

    public override float CastAnimDelay => 0.25f;

    public override Color DialogueColor => new("13446B");

    public override Color MapDrawingColor => new("0D638C");

    public override Color RemoteTargetingLineColor => new("70B6EDFF");

    public override Color RemoteTargetingLineOutline => new("163E64FF");

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
