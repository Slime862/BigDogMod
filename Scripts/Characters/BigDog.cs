using System.Collections.Generic;
using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using BigDogMod.Scripts.Cards;

namespace BigDogMod.Scripts.Characters;

public sealed class BigDog : PlaceholderCharacterModel
{
    public override string PlaceholderID => "defect";

    public override Color NameColor => StsColors.blue;

    public override CharacterGender Gender => CharacterGender.Masculine;

    protected override CharacterModel? UnlocksAfterRunAs => null;

    public override int StartingHp => 75;

    public override int StartingGold => 99;

    public override int BaseOrbSlotCount => 3;

    public override CardPoolModel CardPool => ModelDb.CardPool<DefectCardPool>();

    public override RelicPoolModel RelicPool => ModelDb.RelicPool<DefectRelicPool>();

    public override PotionPoolModel PotionPool => ModelDb.PotionPool<DefectPotionPool>();

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
            ModelDb.Card<RendingBite>(),
            ModelDb.Card<StokeWildness>()
        ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
        [ModelDb.Relic<CrackedCore>()];

    public override float AttackAnimDelay => 0.15f;

    public override float CastAnimDelay => 0.25f;

    public override Color EnergyLabelOutlineColor => new("163E64FF");

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
