using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization;

namespace BigDogMod.Scripts.Powers;

public sealed class EndlessBleedingPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override LocString Description
    {
        get
        {
            LocString description = new("powers", base.Id.Entry + ".description");
            description.Add("Amount", base.Amount);
            return description;
        }
    }
}
