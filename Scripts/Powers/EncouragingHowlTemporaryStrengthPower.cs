using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace BigDogMod.Scripts.Powers;

public sealed class EncouragingHowlTemporaryStrengthPower : TemporaryStrengthPower
{
    public override AbstractModel OriginModel => ModelDb.Card<EncouragingHowl>();
}
