using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;

namespace BigDogMod.Scripts.Powers;

public sealed class BerserkerDogPower : CustomPowerModel
{
    public sealed class Data
    {
        public int energyPerTurn = 1;
    }

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override LocString Description
    {
        get
        {
            LocString description = new("powers", base.Id.Entry + ".description");
            description.Add("Amount", base.Amount);
            description.Add("EnergyPerTurn", GetEnergyPerTurn());
            return description;
        }
    }

    protected override object InitInternalData()
    {
        return new Data();
    }

    public void SetEnergyPerTurn(int amount)
    {
        GetInternalData<Data>().energyPerTurn = amount;
    }

    public int GetEnergyPerTurn()
    {
        return GetInternalData<Data>().energyPerTurn;
    }

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        if (player != base.Owner.Player)
        {
            return;
        }

        Flash();
        await PlayerCmd.GainEnergy(GetEnergyPerTurn(), player);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != base.Owner.Side)
        {
            return;
        }

        Flash();
        await PowerCmd.Apply<BleedingPower>(base.Owner, Amount, base.Owner, null);
    }
}
