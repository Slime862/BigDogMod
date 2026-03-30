using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class BluesDog : CustomCardModel
{
    protected override bool IsPlayable => base.Owner.Creature.GetPowerAmount<WildnessPower>() <= base.DynamicVars.Cards.BaseValue;

    protected override bool ShouldGlowGoldInternal => IsPlayable;

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("blues_dog");

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new CardsVar(-4)];

    public BluesDog()
        : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<BluesDogPower>(base.Owner.Creature, 1m, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
