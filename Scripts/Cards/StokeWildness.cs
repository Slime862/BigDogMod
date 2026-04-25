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
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
[RegisterCharacterStarterCard(typeof(BigDog), 1)]
public sealed class StokeWildness : CustomCardModel
{
    protected override HashSet<CardTag> CanonicalTags => new() { CardTag.Defend };

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.Static(StaticHoverTip.Block),
            HoverTipFactory.FromPower<WildnessPower>()
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new BlockVar(7m, ValueProp.Move),
            new PowerVar<WildnessPower>(1m)
        ];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("stoke_wildness");

    public StokeWildness()
        : base(1, CardType.Skill, CardRarity.Token, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        decimal amount = base.DynamicVars["WildnessPower"].BaseValue;
        await PowerCmd.Apply<WildnessPower>(base.Owner.Creature, amount, base.Owner.Creature, this);
        base.Owner.Creature.GetPower<WildnessPower>()?.AddTemporaryAmount(amount);
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}


