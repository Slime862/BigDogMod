using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Audio;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Relics;

public sealed class HonorCollar : CustomRelicModel
{
    public HonorCollar()
        : base(autoAdd: false)
    {
    }

    public override RelicRarity Rarity => RelicRarity.Starter;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new HealVar(2m)];

    public override string PackedIconPath => ImageHelper.GetImagePath("atlases/relic_atlas.sprites/ring_of_the_snake.tres");

    protected override string PackedIconOutlinePath => ImageHelper.GetImagePath("atlases/relic_outline_atlas.sprites/ring_of_the_snake.tres");

    protected override string BigIconPath => ImageHelper.GetImagePath("relics/ring_of_the_snake.png");

    public override async Task BeforeCombatStart()
    {
        if (!base.Owner.Creature.HasPower<FinaleTrackerPower>())
        {
            await PowerCmd.Apply<FinaleTrackerPower>(base.Owner.Creature, 1m, base.Owner.Creature, null, silent: true);
        }
    }

    public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != base.Owner)
        {
            return Task.CompletedTask;
        }

        if (cardPlay.Card is BigDogChew)
        {
            BigDogSfxHelper.PlayChew(base.Owner);
            return Task.CompletedTask;
        }

        if (cardPlay.Card.Tags.Contains(BigDogTags.Jiao))
        {
            BigDogSfxHelper.PlayHowl(base.Owner);
        }

        return Task.CompletedTask;
    }

    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
    {
        if (dealer != base.Owner.Creature || cardSource is not BigDogChew || result.UnblockedDamage <= 0)
        {
            return;
        }

        Flash();
        await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue);
    }
}
