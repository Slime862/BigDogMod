using System.Collections.Generic;
using System.Linq;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Cards;
using BigDogMod.Scripts.ChewPrep;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace BigDogMod.Scripts.Powers;

public sealed class BigDogChewPrepPower : CustomPowerModel
{
    private readonly Dictionary<BigDogChewPrepEffect, int> _effects = [];

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public bool HasAnyEffects => _effects.Values.Any(amount => amount > 0);

    public int ActiveEffectTypeCount => _effects
        .Where(pair => pair.Value > 0)
        .Select(pair => pair.Key.Type)
        .Distinct()
        .Count();

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
        if (base.Owner.HasPower<LingeringEchoPower>())
        {
            return;
        }

        foreach (BigDogChewPrepEffect effect in effects)
        {
            if (effect.Amount <= 0)
            {
                continue;
            }

            BigDogChewPrepEffect key = effect with { Amount = 0 };
            _effects[key] = GetAmount(key) + effect.Amount;
        }

        //Amount = _effects.Values.Sum();
    }

    public IReadOnlyList<BigDogChewPrepEffect> ConsumeAll()
    {
        List<BigDogChewPrepEffect> snapshot = ActiveEffects().ToList();
        if (!base.Owner.HasPower<LingeringEchoPower>())
        {
            _effects.Clear();
        }
        return snapshot;
    }

    public IReadOnlyList<BigDogChewPrepEffect> SnapshotAll()
    {
        return ActiveEffects().ToList();
    }

    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        if (card.Owner.Creature != base.Owner || card is not BigDogChew)
        {
            return playCount;
        }

        int repeatCount = ActiveEffects()
            .Where(effect => effect.Type == BigDogChewPrepEffectType.RepeatPlay)
            .Sum(effect => effect.Amount);
        return playCount + repeatCount;
    }

    public override Task AfterCombatEnd(CombatRoom room)
    {
        _effects.Clear();
        return Task.CompletedTask;
    }

    private int GetAmount(BigDogChewPrepEffect effect)
    {
        return _effects.GetValueOrDefault(effect);
    }

    private IEnumerable<BigDogChewPrepEffect> ActiveEffects()
    {
        return _effects
            .Where(pair => pair.Value > 0)
            .Select(pair => pair.Key with { Amount = pair.Value })
            .OrderBy(effect => effect.Type)
            .ThenBy(effect => effect.ApplyToAllEnemies ? 1 : 0);
    }

    private string BuildEffectsText()
    {
        return string.Concat(ActiveEffects().Select(BuildEffectText));
    }

    private LocString BuildEffectLine(BigDogChewPrepEffect effect)
    {
        LocString line = new("powers", base.Id.Entry + "." + effect.Type.LocLineKey(effect.ApplyToAllEnemies));
        line.Add("Amount", effect.Amount);
        return line;
    }

    private string BuildEffectText(BigDogChewPrepEffect effect)
    {
        return BuildEffectLine(effect).GetFormattedText();
    }
}
