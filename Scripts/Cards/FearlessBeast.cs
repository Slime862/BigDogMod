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
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Cards;

public sealed class FearlessBeast : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.Static(StaticHoverTip.Block),
            HoverTipFactory.FromPower<BleedingPower>(),
            HoverTipFactory.FromPower<WildnessPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new BlockVar(5m, ValueProp.Move),
            new DynamicVar("BleedingPerWildness", 1m)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("fearless_beast");

    public FearlessBeast()
        : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);

        BleedingPower? bleeding = base.Owner.Creature.GetPower<BleedingPower>();
        if (bleeding == null || bleeding.Amount <= 0)
        {
            return;
        }

        int wildness = Math.Max(0, base.Owner.Creature.GetPowerAmount<WildnessPower>());
        int amountToRemove = wildness * base.DynamicVars["BleedingPerWildness"].IntValue;
        if (amountToRemove > 0)
        {
            await PowerCmd.ModifyAmount(bleeding, -amountToRemove, base.Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Block.UpgradeValueBy(2m);
        base.DynamicVars["BleedingPerWildness"].UpgradeValueBy(1m);
    }
}
