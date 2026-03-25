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
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Cards;

public sealed class RendingBite : CustomCardModel
{
    protected override HashSet<CardTag> CanonicalTags => new() { CardTag.Strike };

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<BleedingPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(1m, ValueProp.Move),
            new PowerVar<BleedingPower>(1m)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("rending_bite");

    public RendingBite()
        : base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null)
        {
            return;
        }

        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Attack", base.Owner.Character.AttackAnimDelay);
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .Execute(choiceContext);
        await PowerCmd.Apply<BleedingPower>(cardPlay.Target, base.DynamicVars["BleedingPower"].BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(1m);
        base.DynamicVars["BleedingPower"].UpgradeValueBy(1m);
    }
}
