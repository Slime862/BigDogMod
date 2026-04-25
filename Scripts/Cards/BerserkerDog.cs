using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class BerserkerDog : CustomCardModel
{
    protected override bool IsPlayable => base.Owner.Creature.GetPowerAmount<WildnessPower>() >= base.DynamicVars.Cards.BaseValue;

    protected override bool ShouldGlowGoldInternal => IsPlayable;

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("berserker_dog");

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new CardsVar(4)];

    public BerserkerDog()
        : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<BerserkerDogPower>(base.Owner.Creature, 1m, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(-1m);
    }
}


