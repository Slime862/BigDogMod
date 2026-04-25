using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.ChewPrep;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

using STS2RitsuLib.Interop.AutoRegistration;

using BigDogMod.Scripts.Characters;
using BigDogMod.Scripts.Pools;
namespace BigDogMod.Scripts.Cards;

[RegisterCard(typeof(BigDogCardPool))]
public sealed class FriendlyHowlFlow : CustomCardModel, IBigDogChewPrepSource, IJiaoCard
{

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BigDogChewPrepPower>()];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new CardsVar(1)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("friendly_howl_flow");

    public FriendlyHowlFlow()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    public IEnumerable<BigDogChewPrepEffect> GetBigDogChewPrepEffects()
    {
        yield return new BigDogChewPrepEffect(BigDogChewPrepEffectType.Draw, base.DynamicVars.Cards.IntValue);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
        await BigDogChewPrepCmd.QueueFromSource(base.Owner, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1m);
    }
}



