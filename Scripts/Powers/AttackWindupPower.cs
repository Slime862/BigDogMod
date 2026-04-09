using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace BigDogMod.Scripts.Powers;

public sealed class AttackWindupPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override LocString Description
    {
        get
        {
            LocString description = new("powers", base.Id.Entry + ".description");
            description.Add("Amount", base.Amount);
            return description;
        }
    }

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card.Owner?.Creature != base.Owner || card is not BigDogChew)
        {
            return;
        }

        Flash();
        await CardPileCmd.Draw(choiceContext, base.Amount, base.Owner.Player!);
    }
}
