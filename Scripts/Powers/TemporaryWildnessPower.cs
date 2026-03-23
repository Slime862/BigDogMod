using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace BigDogMod.Scripts.Powers;

public sealed class TemporaryWildnessPower : CustomPowerModel, ITemporaryPower
{
    private bool _shouldIgnoreNextInstance;

    public override PowerType Type => base.Amount < 0 ? PowerType.Debuff : PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override LocString Title => new("powers", base.Id.Entry + ".title");

    public override LocString Description => new("powers", base.Id.Entry + ".description");

    public AbstractModel OriginModel => ModelDb.Card<StokeWildness>();

    public PowerModel InternallyAppliedPower => ModelDb.Power<WildnessPower>();

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromCard(ModelDb.Card<StokeWildness>()),
            HoverTipFactory.FromPower<WildnessPower>()
        ];

    public void IgnoreNextInstance()
    {
        _shouldIgnoreNextInstance = true;
    }

    public override async Task BeforeApplied(Creature target, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (_shouldIgnoreNextInstance)
        {
            _shouldIgnoreNextInstance = false;
            return;
        }

        await PowerCmd.Apply<WildnessPower>(target, amount, applier, cardSource, silent: true);
    }

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power != this || amount == base.Amount)
        {
            return;
        }

        if (_shouldIgnoreNextInstance)
        {
            _shouldIgnoreNextInstance = false;
            return;
        }

        await PowerCmd.Apply<WildnessPower>(base.Owner, amount, applier, cardSource, silent: true);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != base.Owner.Side)
        {
            return;
        }

        Flash();
        await PowerCmd.Remove(this);
        await PowerCmd.Apply<WildnessPower>(base.Owner, -base.Amount, base.Owner, null);
    }
}
