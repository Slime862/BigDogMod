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
using MegaCrit.Sts2.Core.Models.Powers;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class DauntingHowl : CustomCardModel, IBigDogChewPrepSource, IJiaoCard
{

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<VulnerablePower>(),
            ..BigDogHoverTips.FromWantChew(base.DynamicVars["WantChew"]),
            HoverTipFactory.FromPower<BigDogChewPrepPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<VulnerablePower>(1m), new WantChewVar(2m)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("daunting_howl");

    public DauntingHowl()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies, autoAdd: false)
    {
    }

    public IEnumerable<BigDogChewPrepEffect> GetBigDogChewPrepEffects()
    {
        yield return new BigDogChewPrepEffect(BigDogChewPrepEffectType.Vulnerable, base.DynamicVars.Vulnerable.IntValue, true);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.CombatState == null)
        {
            return;
        }

        await PowerCmd.Apply<VulnerablePower>(base.CombatState.HittableEnemies, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this);
        await BigDogChewPrepCmd.QueueFromSource(base.Owner, this);
        await WantChewCmd.WantChew(base.DynamicVars["WantChew"].BaseValue, base.Owner, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Vulnerable.UpgradeValueBy(1m);
    }
}



