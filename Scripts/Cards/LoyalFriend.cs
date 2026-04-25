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
public sealed class LoyalFriend : CustomCardModel
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override System.Collections.Generic.IEnumerable<DynamicVar> CanonicalVars =>
        [new HealVar(1m)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("loyal_friend");

    public LoyalFriend()
        : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<LoyalFriendPower>(base.Owner.Creature, base.DynamicVars.Heal.BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}


