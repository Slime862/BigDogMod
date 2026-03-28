using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.ChewPrep;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class PiercingScreech : CustomCardModel, IBigDogChewPrepSource
{
    protected override HashSet<CardTag> CanonicalTags => new() { BigDogTags.Jiao };

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<BleedingPower>(),
            HoverTipFactory.FromPower<BigDogChewPrepPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<BleedingPower>(2m)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("piercing_screech");

    public PiercingScreech()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies, autoAdd: false)
    {
    }

    public IEnumerable<BigDogChewPrepEffect> GetBigDogChewPrepEffects()
    {
        yield return new BigDogChewPrepEffect(BigDogChewPrepEffectType.Bleeding, base.DynamicVars["BleedingPower"].IntValue, true);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.CombatState == null)
        {
            return;
        }

        await PowerCmd.Apply<BleedingPower>(base.CombatState.HittableEnemies, base.DynamicVars["BleedingPower"].BaseValue, base.Owner.Creature, this);
        await BigDogChewPrepCmd.QueueFromSource(base.Owner, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["BleedingPower"].UpgradeValueBy(1m);
    }
}
