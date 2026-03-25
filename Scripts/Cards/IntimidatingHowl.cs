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
using MegaCrit.Sts2.Core.Models.Powers;

namespace BigDogMod.Scripts.Cards;

public sealed class IntimidatingHowl : CustomCardModel, IBigDogChewPrepSource
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        base.IsUpgraded ? [] : [CardKeyword.Exhaust];

    protected override HashSet<CardTag> CanonicalTags => new() { BigDogTags.Jiao };

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<WeakPower>(),
            HoverTipFactory.FromPower<BigDogChewPrepPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<WeakPower>(1m)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("intimidating_howl");

    public IntimidatingHowl()
        : base(0, CardType.Skill, CardRarity.Common, TargetType.AllEnemies, autoAdd: false)
    {
    }

    public IEnumerable<BigDogChewPrepEffect> GetBigDogChewPrepEffects()
    {
        yield return new BigDogChewPrepEffect(BigDogChewPrepEffectType.Weak, base.DynamicVars["WeakPower"].IntValue);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.CombatState == null)
        {
            return;
        }

        await PowerCmd.Apply<WeakPower>(base.CombatState.HittableEnemies, base.DynamicVars["WeakPower"].BaseValue, base.Owner.Creature, this);
        await BigDogChewPrepCmd.QueueFromSource(base.Owner, this);
    }

    protected override void OnUpgrade()
    {
    }
}
