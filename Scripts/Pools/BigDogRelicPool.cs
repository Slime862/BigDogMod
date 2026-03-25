using System.Collections.Generic;
using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Unlocks;

namespace BigDogMod.Scripts.Pools;

public sealed class BigDogRelicPool : CustomRelicPoolModel
{
    public override string EnergyColorName => "silent";

    public override Color LabOutlineColor => StsColors.green;

    public override string? BigEnergyIconPath => null;

    public override string? TextEnergyIconPath => null;

    protected override IEnumerable<RelicModel> GenerateAllRelics()
    {
        return ModelDb.RelicPool<SilentRelicPool>().AllRelics;
    }

    public override IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
    {
        return ModelDb.RelicPool<SilentRelicPool>().GetUnlockedRelics(unlockState);
    }
}
