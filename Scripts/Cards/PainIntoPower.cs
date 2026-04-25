using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
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
public sealed class PainIntoPower : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BleedingPower>()];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DynamicVar("Multiplier", 3m)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("pain_into_power");

    public PainIntoPower()
        : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int amount = base.Owner.Creature.GetPowerAmount<BleedingPower>();
        if (amount <= 0)
        {
            return;
        }

        await WantChewCmd.WantChew(amount * base.DynamicVars["Multiplier"].IntValue, base.Owner, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["Multiplier"].UpgradeValueBy(1m);
    }
}


