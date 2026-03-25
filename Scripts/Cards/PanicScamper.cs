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

public sealed class PanicScamper : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.Static(StaticHoverTip.Block),
            HoverTipFactory.FromPower<WildnessPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new BlockVar(7m, ValueProp.Move),
            new PowerVar<WildnessPower>(-2m)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("panic_scamper");

    public PanicScamper()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        decimal amount = base.DynamicVars["WildnessPower"].BaseValue;
        await PowerCmd.Apply<WildnessPower>(base.Owner.Creature, amount, base.Owner.Creature, this);
        base.Owner.Creature.GetPower<WildnessPower>()?.AddTemporaryAmount(amount);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Block.UpgradeValueBy(1m);
        base.DynamicVars["WildnessPower"].UpgradeValueBy(-1m);
    }
}
