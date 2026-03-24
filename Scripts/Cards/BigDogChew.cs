using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Commands;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace BigDogMod.Scripts.Cards;

public sealed class BigDogChew : CustomCardModel
{
    private decimal _currentDamage = 0m;

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [CardKeyword.Retain, CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromKeyword(CardKeyword.Retain),
            HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
        ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(0m, ValueProp.Move)];

    public override string CustomPortraitPath => BigDogAssetPaths.CardPortrait("big_dog_chew");

    public BigDogChew()
        : base(1, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy, autoAdd: false)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null)
        {
            return;
        }

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            //.WithHitFx("vfx/vfx_bite")
            .Execute(choiceContext);

        await BigDogChewPrepCmd.Resolve(choiceContext, this, cardPlay.Target, base.Owner);
    }


    public void AddDamage(decimal amount)
    {
        base.DynamicVars.Damage.BaseValue += amount;
        _currentDamage = base.DynamicVars.Damage.BaseValue;
        UpdateDynamicVarPreview(CardPreviewMode.None, null, base.DynamicVars);
        NCard.FindOnTable(this)?.UpdateVisuals(Pile?.Type ?? PileType.None, CardPreviewMode.Normal);
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }

    protected override void AfterDowngraded()
    {
        base.AfterDowngraded();
        base.DynamicVars.Damage.BaseValue = _currentDamage;
    }
}
