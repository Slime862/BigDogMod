using System.Collections.Generic;
using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Unlocks;

namespace BigDogMod.Scripts.Pools;

public sealed class BigDogPotionPool : CustomPotionPoolModel
{
    public override string EnergyColorName => "defect";

    public override Color LabOutlineColor => StsColors.blue;

    public override string? BigEnergyIconPath => null;

    public override string? TextEnergyIconPath => null;

    protected override IEnumerable<PotionModel> GenerateAllPotions()
    {
        return ModelDb.PotionPool<SilentPotionPool>().AllPotions;
    }

    public override IEnumerable<PotionModel> GetUnlockedPotions(UnlockState unlockState)
    {
        return ModelDb.PotionPool<SilentPotionPool>().GetUnlockedPotions(unlockState);
    }
}
