using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace BigDogMod.Scripts.Powers;

public sealed class EncouragingHowlTemporaryStrengthDownPower : TemporaryStrengthPower
{
    public override AbstractModel OriginModel => ModelDb.Card<EncouragingHowl>();

    protected override bool IsPositive => false;
}
