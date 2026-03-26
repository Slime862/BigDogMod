using BigDogMod.Scripts.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.DynamicVars;

public sealed class WantChewVar : DynamicVar
{
    public WantChewVar(decimal baseValue)
        : base("WantChew", baseValue)
    {
    }

    public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks)
    {
        EnchantedValue = BaseValue;
        PreviewValue = WantChewModifiers.GetEffectiveWantChewAmount(card, BaseValue);
    }

    protected override decimal GetBaseValueForIConvertible()
    {
        return PreviewValue;
    }
}
