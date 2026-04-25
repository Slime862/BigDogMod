using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.ChewPrep;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.DynamicVars;
using BigDogMod.Scripts.HoverTips;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class HeavenlyHowl : CustomCardModel, IBigDogChewPrepSource, IJiaoCard
{

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        BigDogHoverTips.FromWantChew(base.DynamicVars["WantChew"])
            .Concat(
            [
                HoverTipFactory.FromPower<BigDogChewPrepPower>(),
                HoverTipFactory.FromPower<WeakPower>(),
                HoverTipFactory.Static(StaticHoverTip.Block),
                HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
            ]);

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new WantChewVar(3m),
            new CardsVar(1),
            new EnergyVar(1),
            new PowerVar<WeakPower>(1m),
            new BlockVar(7m, ValueProp.Move)
        ];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("heavenly_howl");

    public HeavenlyHowl()
        : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, autoAdd: false)
    {
    }

    public IEnumerable<BigDogChewPrepEffect> GetBigDogChewPrepEffects()
    {
        yield return new BigDogChewPrepEffect(BigDogChewPrepEffectType.Draw, base.DynamicVars.Cards.IntValue);
        yield return new BigDogChewPrepEffect(BigDogChewPrepEffectType.Energy, base.DynamicVars.Energy.IntValue);
        yield return new BigDogChewPrepEffect(BigDogChewPrepEffectType.Weak, base.DynamicVars["WeakPower"].IntValue, ApplyToAllEnemies: true);
        yield return new BigDogChewPrepEffect(BigDogChewPrepEffectType.Block, base.DynamicVars.Block.IntValue);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await WantChewCmd.WantChew(WantChewModifiers.GetEffectiveWantChewAmount(this, base.DynamicVars["WantChew"].BaseValue), base.Owner, this);
        await BigDogChewPrepCmd.QueueFromSource(base.Owner, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["WantChew"].UpgradeValueBy(3m);
        base.DynamicVars.Cards.UpgradeValueBy(1m);
        base.DynamicVars.Energy.UpgradeValueBy(1m);
        base.DynamicVars["WeakPower"].UpgradeValueBy(1m);
        base.DynamicVars.Block.UpgradeValueBy(3m);
    }
}



