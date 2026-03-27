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
using MegaCrit.Sts2.Core.Rooms;

namespace BigDogMod.Scripts.Powers;

public sealed class BigDogChewPrepPower : CustomPowerModel
{
    private readonly Dictionary<BigDogChewPrepEffectType, int> _effects = [];

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public bool HasAnyEffects => _effects.Values.Any(amount => amount > 0);

    public override LocString Description
    {
        get
        {
            LocString description = new("powers", base.Id.Entry + ".description");
            description.Add("EffectsText", BuildEffectsText());
            return description;
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            List<IHoverTip> tips = HoverTipFactory.FromCardWithCardHoverTips<BigDogChew>().ToList();
            foreach (BigDogChewPrepEffectType effectType in ActiveEffects().Select(effect => effect.Type))
            {
                IHoverTip? tip = effectType.HoverTip();
                if (tip != null)
                {
                    tips.Add(tip);
                }
            }

            return tips;
        }
    }

    public void AddEffects(IEnumerable<BigDogChewPrepEffect> effects)
    {
        foreach (BigDogChewPrepEffect effect in effects)
        {
            if (effect.Amount <= 0)
            {
                continue;
            }

            _effects[effect.Type] = GetAmount(effect.Type) + effect.Amount;
        }

        //Amount = _effects.Values.Sum();
    }

    public IReadOnlyList<BigDogChewPrepEffect> ConsumeAll()
    {
        List<BigDogChewPrepEffect> snapshot = ActiveEffects().ToList();
        _effects.Clear();
        Amount = 0;
        return snapshot;
    }

    public override Task AfterCombatEnd(CombatRoom room)
    {
        _effects.Clear();
        Amount = 0;
        return Task.CompletedTask;
    }

    private int GetAmount(BigDogChewPrepEffectType effectType)
    {
        return _effects.GetValueOrDefault(effectType);
    }

    private IEnumerable<BigDogChewPrepEffect> ActiveEffects()
    {
        return Enum.GetValues<BigDogChewPrepEffectType>()
            .Select(type => new BigDogChewPrepEffect(type, GetAmount(type)))
            .Where(effect => effect.Amount > 0);
    }

    private string BuildEffectsText()
    {
        return string.Concat(ActiveEffects().Select(BuildEffectText));
    }

    private LocString BuildEffectLine(BigDogChewPrepEffect effect)
    {
        LocString line = new("powers", base.Id.Entry + "." + effect.Type.LocLineKey());
        line.Add("Amount", effect.Amount);
        return line;
    }

    private string BuildEffectText(BigDogChewPrepEffect effect)
    {
        return BuildEffectLine(effect).GetFormattedText();
    }
}
