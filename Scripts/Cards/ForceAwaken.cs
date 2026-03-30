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

namespace BigDogMod.Scripts.Cards;

public sealed class ForceAwaken : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<BleedingPower>(),
            HoverTipFactory.ForEnergy(this)
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new PowerVar<BleedingPower>(1m),
            new EnergyVar(2)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("force_awaken");

    public ForceAwaken()
        : base(0, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<BleedingPower>(base.Owner.Creature, base.DynamicVars["BleedingPower"].BaseValue, base.Owner.Creature, this);
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Energy.UpgradeValueBy(1m);
    }
}
