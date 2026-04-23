using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.ChewPrep;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;

namespace BigDogMod.Scripts.Cards;

public sealed class WarmUpVoice : CustomCardModel
{
    protected override bool HasEnergyCostX => true;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BigDogChewPrepPower>()];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("warm_up_voice");

    public WarmUpVoice()
        : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int extraPlays = ResolveEnergyXValue() + (base.IsUpgraded ? 1 : 0);
        if (extraPlays <= 0)
        {
            return;
        }

        await BigDogChewPrepCmd.Queue(base.Owner, [new BigDogChewPrepEffect(BigDogChewPrepEffectType.RepeatPlay, extraPlays)], this);
    }

    protected override void OnUpgrade()
    {
    }
}
