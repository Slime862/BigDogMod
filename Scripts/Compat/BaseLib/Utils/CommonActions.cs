using System.Collections.Generic;
using System.Linq;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace BaseLib.Utils;

public static class CommonActions
{
    public static AttackCommand CardAttack(CardModel card, CardPlay play, int hitCount = 1, string? vfx = null, string? sfx = null, string? tmpSfx = null)
    {
        return CardAttack(card, play.Target, hitCount, vfx, sfx, tmpSfx);
    }

    public static AttackCommand CardAttack(CardModel card, Creature? target, int hitCount = 1, string? vfx = null, string? sfx = null, string? tmpSfx = null)
    {
        if (card.DynamicVars.ContainsKey(CalculatedDamageVar.defaultName))
        {
            return CardAttack(card, target, card.DynamicVars.CalculatedDamage, hitCount, vfx, sfx, tmpSfx);
        }

        if (card.DynamicVars.ContainsKey(DamageVar.defaultName))
        {
            return CardAttack(card, target, card.DynamicVars.Damage.BaseValue, hitCount, vfx, sfx, tmpSfx);
        }

        throw new InvalidOperationException($"Card {card.Title} does not expose a supported damage variable.");
    }

    public static AttackCommand CardAttack(CardModel card, Creature? target, decimal damage, int hitCount = 1, string? vfx = null, string? sfx = null, string? tmpSfx = null)
    {
        var cmd = DamageCmd.Attack(damage).WithHitCount(hitCount).FromCard(card);
        ApplyTargeting(card, target, cmd);
        if (vfx != null || sfx != null || tmpSfx != null)
        {
            cmd.WithHitFx(vfx: vfx, sfx: sfx, tmpSfx: tmpSfx);
        }

        return cmd;
    }

    public static AttackCommand CardAttack(CardModel card, Creature? target, CalculatedDamageVar calculatedDamage, int hitCount = 1, string? vfx = null, string? sfx = null, string? tmpSfx = null)
    {
        var cmd = DamageCmd.Attack(calculatedDamage).WithHitCount(hitCount).FromCard(card);
        ApplyTargeting(card, target, cmd);
        if (vfx != null || sfx != null || tmpSfx != null)
        {
            cmd.WithHitFx(vfx: vfx, sfx: sfx, tmpSfx: tmpSfx);
        }

        return cmd;
    }

    public static Task<decimal> CardBlock(CardModel card, CardPlay play)
    {
        return CardBlock(card, card.DynamicVars.Block, play);
    }

    public static Task<decimal> CardBlock(CardModel card, BlockVar blockVar, CardPlay play)
    {
        return CreatureCmd.GainBlock(card.Owner.Creature, blockVar, play);
    }

    public static Task<IEnumerable<CardModel>> Draw(CardModel card, PlayerChoiceContext context)
    {
        return CardPileCmd.Draw(context, card.DynamicVars.Cards.BaseValue, card.Owner);
    }

    public static Task<T?> Apply<T>(Creature target, CardModel? card, decimal amount, bool silent = false) where T : PowerModel
    {
        return PowerCmd.Apply<T>(target, amount, card?.Owner.Creature, card, silent);
    }

    public static Task<T?> ApplySelf<T>(CardModel card, decimal amount, bool silent = false) where T : PowerModel
    {
        return PowerCmd.Apply<T>(card.Owner.Creature, amount, card.Owner.Creature, card, silent);
    }

    public static Task<IEnumerable<CardModel>> SelectCards(CardModel card, LocString selectionPrompt, PlayerChoiceContext context, PileType pileType, int count = 1)
    {
        var prefs = new CardSelectorPrefs(selectionPrompt, count);
        var pile = pileType.GetPile(card.Owner);
        return CardSelectCmd.FromSimpleGrid(context, pile.Cards, card.Owner, prefs);
    }

    public static Task<IEnumerable<CardModel>> SelectCards(CardModel card, LocString selectionPrompt, PlayerChoiceContext context, PileType pileType, int minCount, int maxCount)
    {
        var prefs = new CardSelectorPrefs(selectionPrompt, minCount, maxCount);
        var pile = pileType.GetPile(card.Owner);
        return CardSelectCmd.FromSimpleGrid(context, pile.Cards, card.Owner, prefs);
    }

    public static async Task<CardModel?> SelectSingleCard(CardModel card, LocString selectionPrompt, PlayerChoiceContext context, PileType pileType)
    {
        return (await SelectCards(card, selectionPrompt, context, pileType, 1)).FirstOrDefault();
    }

    private static void ApplyTargeting(CardModel card, Creature? target, AttackCommand cmd)
    {
        var combatState = card.CombatState;
        switch (card.TargetType)
        {
            case TargetType.AnyEnemy:
                if (target != null)
                {
                    cmd.Targeting(target);
                }
                break;
            case TargetType.AllEnemies:
                if (combatState != null)
                {
                    cmd.TargetingAllOpponents(combatState);
                }
                break;
            case TargetType.RandomEnemy:
                if (combatState != null)
                {
                    cmd.TargetingRandomOpponents(combatState);
                }
                break;
            default:
                throw new InvalidOperationException($"Unsupported attack target type {card.TargetType} for {card.Title}.");
        }
    }
}
