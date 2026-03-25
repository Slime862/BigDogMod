using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace BigDogMod.Scripts.Powers;

public sealed class EndlessBleedingPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
}
