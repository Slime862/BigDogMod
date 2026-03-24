using BaseLib.Abstracts;
using Godot;

namespace BigDogMod.Scripts.Pools;

public sealed class BigDogCardPool : CustomCardPoolModel
{
    public override string Title => "big_dog";

    public override string EnergyColorName => "defect";

    public override string CardFrameMaterialPath => "card_frame_blue";

    public override Color DeckEntryCardColor => new("3EB3ED");

    public override Color EnergyOutlineColor => new("1D5673");

    public override bool IsColorless => false;
}
