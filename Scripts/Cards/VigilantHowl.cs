using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.ChewPrep;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.DynamicVars;
using BigDogMod.Scripts.HoverTips;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class VigilantHowl : CustomCardModel, IBigDogChewPrepSource, IJiaoCard
{

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.Static(StaticHoverTip.Block),
            ..BigDogHoverTips.FromWantChew(base.DynamicVars["WantChew"]),
            HoverTipFactory.FromPower<BigDogChewPrepPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new BlockVar(6m, ValueProp.Move), new WantChewVar(2m)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("vigilant_howl");

    public VigilantHowl()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    public IEnumerable<BigDogChewPrepEffect> GetBigDogChewPrepEffects()
    {
        yield return new BigDogChewPrepEffect(BigDogChewPrepEffectType.Block, base.DynamicVars.Block.IntValue);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        await BigDogChewPrepCmd.QueueFromSource(base.Owner, this);
        await WantChewCmd.WantChew(base.DynamicVars["WantChew"].BaseValue, base.Owner, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Block.UpgradeValueBy(3m);
    }
}



