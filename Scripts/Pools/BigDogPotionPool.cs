using System.Collections.Generic;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Unlocks;

namespace BigDogMod.Scripts.Pools;

public sealed class BigDogPotionPool : PotionPoolModel
{
    public override string EnergyColorName => "silent";

    public override Color LabOutlineColor => StsColors.green;

    protected override IEnumerable<PotionModel> GenerateAllPotions()
    {
        return ModelDb.PotionPool<SilentPotionPool>().AllPotions;
    }

    public override IEnumerable<PotionModel> GetUnlockedPotions(UnlockState unlockState)
    {
        return ModelDb.PotionPool<SilentPotionPool>().GetUnlockedPotions(unlockState);
    }
}
