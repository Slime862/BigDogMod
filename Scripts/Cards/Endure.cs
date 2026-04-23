using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class Endure : CustomCardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromCard<BigDogChew>(), HoverTipFactory.FromPower<WildnessPower>()];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<WildnessPower>(-4m)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("endure");

    public Endure()
        : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        decimal wildnessAmount = base.DynamicVars["WildnessPower"].BaseValue;
        await PowerCmd.Apply<WildnessPower>(base.Owner.Creature, wildnessAmount, base.Owner.Creature, this);
        base.Owner.Creature.GetPower<WildnessPower>()?.AddTemporaryAmount(wildnessAmount);

        BigDogChew? chew = BigDogChewLocator.FindAnywhere(base.Owner);
        if (chew != null && BigDogChewLocator.FindPileType(base.Owner, chew) != PileType.Hand)
        {
            await CardPileCmd.Add(chew, PileType.Hand);
        }

        await PowerCmd.Apply<ForgetChewPower>(base.Owner.Creature, 1m, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
    }
}
