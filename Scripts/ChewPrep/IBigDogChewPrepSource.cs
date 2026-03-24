using System.Collections.Generic;

namespace BigDogMod.Scripts.ChewPrep;

public interface IBigDogChewPrepSource
{
    IEnumerable<BigDogChewPrepEffect> GetBigDogChewPrepEffects();
}
