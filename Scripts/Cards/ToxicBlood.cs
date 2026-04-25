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
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class ToxicBlood : CustomCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BleedingPower>(), HoverTipFactory.FromPower<PoisonPower>(), HoverTipFactory.FromKeyword(CardKeyword.Exhaust)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("toxic_blood");

    public ToxicBlood()
        : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int poisonAmount = base.Owner.Creature.GetPowerAmount<BleedingPower>() * 2;
        if (poisonAmount <= 0 || base.CombatState == null)
        {
            return;
        }

        await PowerCmd.Apply<PoisonPower>(base.CombatState.HittableEnemies.Where(enemy => enemy.IsAlive), poisonAmount, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}


