using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class JoyOfRegen : CustomCardModel
{
    protected override bool ShouldGlowGoldInternal => base.Owner.Creature.HasPower<BleedingPower>();

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BleedingPower>()];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("joy_of_regen");

    public JoyOfRegen()
        : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        BleedingPower? bleeding = base.Owner.Creature.GetPower<BleedingPower>();
        if (bleeding == null || bleeding.Amount <= 0)
        {
            return;
        }

        await CreatureCmd.Heal(base.Owner.Creature, bleeding.Amount);
        await PowerCmd.Remove(bleeding);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}


