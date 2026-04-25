using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
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
public sealed class ProofOfDeath : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BleedingPower>()];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new EnergyVar(2)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("proof_of_death");

    public ProofOfDeath()
        : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null)
        {
            return;
        }

        int bleeding = cardPlay.Target.GetPower<BleedingPower>()?.Amount ?? 0;
        if (bleeding > 0)
        {
            await DamageCmd.Attack(bleeding * 2)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .Execute(choiceContext);
        }

        if (!cardPlay.Target.IsAlive)
        {
            await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Energy.UpgradeValueBy(1m);
    }
}


