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

public sealed class CowardDog : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<WildnessPower>()];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<WildnessPower>(-4m)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("coward_dog");

    public CowardDog()
        : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<WildnessPower>(base.Owner.Creature, base.DynamicVars["WildnessPower"].BaseValue, base.Owner.Creature, this);
        await PowerCmd.Apply<CowardDogPower>(base.Owner.Creature, 1m, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["WildnessPower"].UpgradeValueBy(-2m);
    }
}

