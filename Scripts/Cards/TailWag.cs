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
using MegaCrit.Sts2.Core.Models.Powers;

namespace BigDogMod.Scripts.Cards;

public sealed class TailWag : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<WeakPower>(),
            HoverTipFactory.FromPower<WildnessPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new PowerVar<WeakPower>(1m),
            new PowerVar<WildnessPower>(-3m)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("tail_wag");

    public TailWag()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<WeakPower>(base.Owner.Creature, base.DynamicVars["WeakPower"].BaseValue, base.Owner.Creature, this);
        decimal amount = base.DynamicVars["WildnessPower"].BaseValue;
        await PowerCmd.Apply<WildnessPower>(base.Owner.Creature, amount, base.Owner.Creature, this);
        base.Owner.Creature.GetPower<WildnessPower>()?.AddTemporaryAmount(amount);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["WildnessPower"].UpgradeValueBy(-2m);
    }
}
