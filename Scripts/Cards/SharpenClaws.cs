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

namespace BigDogMod.Scripts.Cards;

public sealed class SharpenClaws : CustomCardModel
{
    private decimal _extraDamageFromPileMoves;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(4m, ValueProp.Move),
            new DynamicVar("Grow", 2m)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("sharpen_claws");

    public SharpenClaws()
        : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null)
        {
            return;
        }

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .Execute(choiceContext);
    }

    public void AddPileMoveDamage(decimal amount)
    {
        base.DynamicVars.Damage.BaseValue += amount;
        _extraDamageFromPileMoves += amount;
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["Grow"].UpgradeValueBy(1m);
    }

    protected override void AfterDowngraded()
    {
        base.AfterDowngraded();
        base.DynamicVars.Damage.BaseValue += _extraDamageFromPileMoves;
    }
}
