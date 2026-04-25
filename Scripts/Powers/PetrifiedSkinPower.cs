using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

using STS2RitsuLib.Interop.AutoRegistration;

namespace BigDogMod.Scripts.Powers;

[RegisterPower()]
public sealed class PetrifiedSkinPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;
}

