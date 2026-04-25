using Godot;
using STS2RitsuLib.Scaffolding.Content;

namespace BigDogMod.Scripts.Pools;

public sealed class BigDogCardPool : TypeListCardPoolModel
{
    public override string Title => "big_dog";

    public override string EnergyColorName => "silent";

    public override string CardFrameMaterialPath => "card_frame_green";

    public override Color DeckEntryCardColor => new("5EBD00");

    public override Color EnergyOutlineColor => new("1A6625");

    public override bool IsColorless => false;
}
