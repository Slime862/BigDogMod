using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Commands;
using BigDogMod.Scripts.DynamicVars;
using BigDogMod.Scripts.HoverTips;
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
public sealed class Impulse : CustomCardModel
{
    protected override bool IsPlayable => BigDogChewLocator.FindInDraw(base.Owner) != null;

    protected override bool ShouldGlowRedInternal => !IsPlayable;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        BigDogHoverTips.FromWantChew(base.DynamicVars["WantChew"])
            .Concat(HoverTipFactory.FromCardWithCardHoverTips<BigDogChew>());

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new WantChewVar(8m)];

    public override string? CustomPortraitPath => BigDogAssetPaths.TryCardPortrait("impulse");

    public Impulse()
        : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        BigDogChew? chew = BigDogChewLocator.FindInDraw(base.Owner);
        if (chew == null)
        {
            return;
        }

        await CardPileCmd.Add(chew, PileType.Hand);
        await WantChewCmd.WantChew(base.DynamicVars["WantChew"].BaseValue, base.Owner, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["WantChew"].UpgradeValueBy(4m);
    }
}


