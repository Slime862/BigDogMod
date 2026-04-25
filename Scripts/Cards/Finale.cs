using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class Finale : CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(10m, ValueProp.Move),
            new DynamicVar("Bonus", 6m)
        ];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("finale");

    public Finale()
        : base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.CombatState == null)
        {
            return;
        }

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(base.CombatState)
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["Bonus"].UpgradeValueBy(2m);
    }
}


