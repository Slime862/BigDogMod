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

public sealed class ShockingOpening : CustomCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust, CardKeyword.Innate, CardKeyword.Ethereal];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BleedingPower>(), HoverTipFactory.FromKeyword(CardKeyword.Exhaust), HoverTipFactory.FromKeyword(CardKeyword.Ethereal)];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<BleedingPower>(4m)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("shocking_opening");

    public ShockingOpening()
        : base(1, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.CombatState == null)
        {
            return;
        }

        await PowerCmd.Apply<BleedingPower>(base.CombatState.HittableEnemies, base.DynamicVars["BleedingPower"].BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["BleedingPower"].UpgradeValueBy(2m);
    }
}
