using System.Collections.Generic;
using System.Linq;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.ChewPrep;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace BigDogMod.Scripts.Powers;

public sealed class BigDogChewPrepPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public int PendingWeak { get; private set; }

    public int PendingDraw { get; private set; }

    public bool HasAnyEffects => PendingWeak > 0 || PendingDraw > 0;

    public override LocString Description
    {
        get
        {
            LocString description = new("powers", base.Id.Entry + ".description");
            description.Add("WeakText", PendingWeak > 0 ? BuildEffectText("weakLine", PendingWeak) : string.Empty);
            description.Add("DrawText", PendingDraw > 0 ? BuildEffectText("drawLine", PendingDraw) : string.Empty);
            return description;
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            List<IHoverTip> tips = HoverTipFactory.FromCardWithCardHoverTips<BigDogChew>().ToList();

            if (PendingWeak > 0)
            {
                tips.Add(HoverTipFactory.FromPower<WeakPower>());
            }

            return tips;
        }
    }

    public void AddEffects(IEnumerable<BigDogChewPrepEffect> effects)
    {
        foreach (BigDogChewPrepEffect effect in effects)
        {
            switch (effect.Type)
            {
                case BigDogChewPrepEffectType.Weak:
                    PendingWeak += effect.Amount;
                    break;
                case BigDogChewPrepEffectType.Draw:
                    PendingDraw += effect.Amount;
                    break;
            }
        }

        Amount = PendingWeak + PendingDraw;
    }

    public BigDogChewPrepSnapshot ConsumeAll()
    {
        BigDogChewPrepSnapshot snapshot = new(PendingWeak, PendingDraw);
        PendingWeak = 0;
        PendingDraw = 0;
        Amount = 0;
        return snapshot;
    }

    private LocString BuildEffectLine(string key, int amount)
    {
        LocString line = new("powers", base.Id.Entry + "." + key);
        line.Add("Amount", amount);
        return line;
    }

    private string BuildEffectText(string key, int amount)
    {
        return BuildEffectLine(key, amount).GetFormattedText();
    }
}
