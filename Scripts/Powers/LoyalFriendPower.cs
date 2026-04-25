using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Patches.Localization;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

using STS2RitsuLib.Interop.AutoRegistration;

namespace BigDogMod.Scripts.Powers;

[RegisterPower()]
public sealed class LoyalFriendPower : CustomPowerModel, IAddDumbVariablesToPowerDescription
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public void AddDumbVariablesToPowerDescription(LocString description)
    {
        description.Add("Amount", base.Amount);
    }

    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
    {
        if (dealer != base.Owner || cardSource is not BigDogChew || result.UnblockedDamage <= 0 || base.Owner.CombatState == null)
        {
            return;
        }

        foreach (Creature ally in base.Owner.CombatState.Players.Select(player => player.Creature).Where(creature => creature != base.Owner))
        {
            await CreatureCmd.Heal(ally, base.Amount);
        }
    }
}

