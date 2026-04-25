using System.Collections.Generic;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Pools;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Characters;

namespace BigDogMod.Scripts.Characters;

[RegisterCharacter]
public sealed class BigDog : ModCharacterTemplate<BigDogCardPool, BigDogRelicPool, BigDogPotionPool>
{
    public override string? PlaceholderCharacterId => "silent";

    public override Color NameColor => StsColors.green;

    public override Color EnergyLabelOutlineColor => new("004f04FF");

    public override CharacterGender Gender => CharacterGender.Masculine;

    public override int StartingHp => 75;

    public override int StartingGold => 99;

    public override int BaseOrbSlotCount => 0;

    // Uncomment these one by one after the matching assets are ready.
    // Until then, keep borrowing Silent assets through PlaceholderCharacterId = "silent".
    // public override string CustomVisualsPath => BigDogAssetPaths.CharacterVisualsScene;
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
