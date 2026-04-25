using System.Collections.Generic;
using System.Linq;
using BigDogMod.Scripts.Relics;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Unlocks;

namespace BigDogMod.Scripts.Pools;

public sealed class BigDogRelicPool : RelicPoolModel
{
    public override string EnergyColorName => "silent";

    public override Color LabOutlineColor => StsColors.green;

    protected override IEnumerable<RelicModel> GenerateAllRelics()
    {
        return ModelDb.RelicPool<SilentRelicPool>().AllRelics.Append(ModelDb.Relic<HoundCollar>());
    }

    public override IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
    {
        return ModelDb.RelicPool<SilentRelicPool>().GetUnlockedRelics(unlockState).Append(ModelDb.Relic<HoundCollar>());
    }
}
