using System.Collections.Generic;
using System.Linq;
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

public sealed class AdmireVictory : CustomCardModel
{
    protected override bool ShouldGlowGoldInternal =>
        base.CombatState?.HittableEnemies.Any(enemy => enemy.HasPower<BleedingPower>()) ?? false;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BleedingPower>()];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CardsVar(1),
            new DynamicVar("BonusCards", 1m)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("admire_victory");

    public AdmireVictory()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int bleedingEnemies = base.CombatState?.HittableEnemies.Count(enemy => enemy.HasPower<BleedingPower>()) ?? 0;
        decimal cardsToDraw = base.DynamicVars.Cards.BaseValue + (bleedingEnemies * base.DynamicVars["BonusCards"].BaseValue);
        await CardPileCmd.Draw(choiceContext, cardsToDraw, base.Owner);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
