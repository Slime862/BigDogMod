using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.ChewPrep;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.DynamicVars;
using BigDogMod.Scripts.HoverTips;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class WarmUpVoice : CustomCardModel, IBigDogChewPrepSource
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        BigDogHoverTips.FromWantChew(base.DynamicVars["WantChew"])
            .Concat([HoverTipFactory.FromPower<BigDogChewPrepPower>()]);

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new WantChewVar(3m)
        ];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("warm_up_voice");

    public WarmUpVoice()
        : base(0, CardType.Skill, CardRarity.Common, TargetType.Self, autoAdd: false)
    {
    }

    public IEnumerable<BigDogChewPrepEffect> GetBigDogChewPrepEffects()
    {
        yield return new BigDogChewPrepEffect(BigDogChewPrepEffectType.RepeatPlay, 1);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await WantChewCmd.WantChew(WantChewModifiers.GetEffectiveWantChewAmount(this, base.DynamicVars["WantChew"].BaseValue), base.Owner, this);
        await BigDogChewPrepCmd.QueueFromSource(base.Owner, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["WantChew"].UpgradeValueBy(2m);
    }
}
