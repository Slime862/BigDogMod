using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.ChewPrep;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class WiseHowl : CustomCardModel, IBigDogChewPrepSource, IJiaoCard
{

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BigDogChewPrepPower>()];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new EnergyVar(1)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("wise_howl");

    public WiseHowl()
        : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    public IEnumerable<BigDogChewPrepEffect> GetBigDogChewPrepEffects()
    {
        yield return new BigDogChewPrepEffect(BigDogChewPrepEffectType.Energy, base.DynamicVars.Energy.IntValue);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await BigDogChewPrepCmd.QueueFromSource(base.Owner, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Energy.UpgradeValueBy(1m);
    }
}



