using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

using STS2RitsuLib.Interop.AutoRegistration;

namespace BigDogMod.Scripts.Powers;

[RegisterPower()]
public sealed class BluesDogPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? cardSource)
    {
        if (creature != base.Owner || amount <= 0m || base.Owner.Player == null)
        {
            return;
        }

        Flash();
        await WantChewCmd.WantChew(amount, base.Owner.Player, this);
    }
}

