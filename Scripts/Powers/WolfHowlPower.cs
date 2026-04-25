using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Patches.Localization;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Cards;

using STS2RitsuLib.Interop.AutoRegistration;

namespace BigDogMod.Scripts.Powers;

[RegisterPower()]
public sealed class WolfHowlPower : CustomPowerModel, IAddDumbVariablesToPowerDescription
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public void AddDumbVariablesToPowerDescription(LocString description)
    {
        description.Add("Amount", base.Amount);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != base.Owner || base.Owner.CombatState == null)
        {
            return;
        }

        if (cardPlay.Card is not BigDogChew && !BigDogCardTraits.IsJiao(cardPlay.Card))
        {
            return;
        }

        await PowerCmd.Apply<WolfHowlStrengthDownPower>(
            base.Owner.CombatState.HittableEnemies.Where(enemy => enemy.IsAlive),
            base.Amount,
            base.Owner,
            cardPlay.Card);
    }
}


