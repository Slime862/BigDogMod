using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

using STS2RitsuLib.Interop.AutoRegistration;

namespace BigDogMod.Scripts.Powers;

[RegisterPower()]
public sealed class MightyBlowStrengthDownPower : TemporaryStrengthPower
{
    public override AbstractModel OriginModel => ModelDb.Card<MightyBlow>();

    protected override bool IsPositive => false;
}

