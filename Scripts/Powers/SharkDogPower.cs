using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

using STS2RitsuLib.Interop.AutoRegistration;

namespace BigDogMod.Scripts.Powers;

[RegisterPower()]
public sealed class SharkDogPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        if (player != base.Owner.Player)
        {
            return;
        }

        int maxBleeding = combatState.HittableEnemies
            .Where(enemy => enemy.IsAlive)
            .Select(enemy => enemy.GetPowerAmount<BleedingPower>())
            .DefaultIfEmpty(0)
            .Max();

        if (maxBleeding <= 0)
        {
            return;
        }

        Flash();
        await WantChewCmd.WantChew(maxBleeding, player, this);
    }
}

