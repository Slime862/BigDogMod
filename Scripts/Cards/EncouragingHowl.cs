using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

namespace BigDogMod.Scripts.Cards;

public sealed class EncouragingHowl : CustomCardModel
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override HashSet<CardTag> CanonicalTags => new() { BigDogTags.Jiao };

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<StrengthPower>(),
            HoverTipFactory.FromPower<WildnessPower>(),
            HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<StrengthPower>(2m)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("encouraging_howl");

    public EncouragingHowl()
        : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.Owner.Creature.CombatState == null)
        {
            return;
        }

        foreach (Player player in base.Owner.Creature.CombatState.Players)
        {
            await PowerCmd.Apply<StrengthPower>(player.Creature, base.DynamicVars["StrengthPower"].BaseValue, base.Owner.Creature, this);
        }

        int extraStrength = base.Owner.Creature.GetPowerAmount<WildnessPower>();
        if (extraStrength != 0)
        {
            await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, extraStrength, base.Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}
