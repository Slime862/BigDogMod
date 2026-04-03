using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace BigDogMod.Scripts.Powers;

public sealed class LingeringEchoPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;
}
