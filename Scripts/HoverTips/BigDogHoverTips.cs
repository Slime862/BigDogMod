using System.Collections.Generic;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using BigDogMod.Scripts.Cards;

namespace BigDogMod.Scripts.HoverTips;

public static class BigDogHoverTips
{
    public static IEnumerable<IHoverTip> FromWantChew(DynamicVar amount)
    {
        LocString title = new("static_hover_tips", "BIGDOGMOD-WANT_CHEW.title");
        LocString description = new("static_hover_tips", "BIGDOGMOD-WANT_CHEW.description");
        description.Add(amount);
        return
        [
            new HoverTip(title, description),
            .. HoverTipFactory.FromCardWithCardHoverTips<BigDogChew>()
        ];
    }
}
