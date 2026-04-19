# BaseLib v3.0.7 接入说明

检查日期：2026-04-19

## 当前已接入

### 1. Power 描述变量接口

新版 BaseLib 提供了：

```csharp
BaseLib.Patches.Localization.IAddDumbVariablesToPowerDescription
```

这个接口用于给 Power 的本地化描述补充变量，例如 `{Amount}`、`{WantChew}`、`{Turns}`。

以前 BigDog 的很多 Power 会为了填一个变量而重写整个 `Description`：

```csharp
public override LocString Description
{
    get
    {
        LocString description = new("powers", base.Id.Entry + ".description");
        description.Add("Amount", base.Amount);
        return description;
    }
}
```

现在改为：

```csharp
public void AddDumbVariablesToPowerDescription(LocString description)
{
    description.Add("Amount", base.Amount);
}
```

这样 Power 仍然使用默认的本地化路径，但变量由 BaseLib 的新接口统一注入。

已迁移的 Power：

- `AttackWindupPower`
- `BigDogChewPrepPower`
- `DogSagePower`
- `EndlessBleedingPower`
- `ForgetChewPower`
- `HealingSongPower`
- `HesitationPower`
- `LoyalFriendPower`
- `PreludePower`
- `SustainedChargePower`
- `WolfHowlPower`

以后新增 Power 时，如果只是给描述填变量，优先使用这个接口，不要再重写整个 `Description`。

## 暂时不接入但值得关注

### 2. 自定义临时 Power

新版 BaseLib 提供了：

```csharp
BaseLib.Abstracts.CustomTemporaryPowerModel
BaseLib.Abstracts.CustomTemporaryPowerModelWrapper<...>
```

适合表达“本回合临时力量”“本回合力量下降”“回合结束自动移除”这类效果。

BigDog 当前相关类包括：

- `MightyBlowStrengthDownPower`
- `WolfHowlStrengthDownPower`
- `EncouragingHowlTemporaryStrengthPower`
- `EncouragingHowlTemporaryStrengthDownPower`

这些目前逻辑能正常工作，暂时不强行迁移。后续如果临时 Power 数量继续增加，可以集中改成新版 BaseLib 的临时 Power 基类，减少手写回合结束逻辑。

### 3. 先古卡与起始遗物升级链

新版 BaseLib 暴露了：

```csharp
BaseLib.Abstracts.ITranscendenceCard
```

以及若干先古卡、起始遗物升级、百科图鉴相关入口。

BigDog 当前已经通过 Harmony patch 接入了：

- `ArchaicTooth`：把 `激发野性` 映射为 `远古野性`
- `DustyTome`：让大狗角色能被原版先古卡逻辑识别
- `TouchOfOrobas`：把 `猎犬项圈` 升级为 `荣誉项圈`
- 百科图鉴补丁：保证先古卡和升级遗物直接可见

这条链之前花了不少时间才稳定，且直接关系到事件、遗物、图鉴扫描。现在不建议立刻换成新 API，避免和原有 patch 双重触发。等确认 BaseLib 的新入口能完整覆盖这三条链之后，再做一次单独迁移。

### 4. 自定义计算型变量

新版 BaseLib 提供了：

```csharp
BaseLib.Cards.Variables.CustomCalculatedVar
```

它适合处理卡面数值预览，例如“根据当前流血层数显示数值”“根据连续叫牌数显示伤害”等。

BigDog 里未来适合接入的卡：

- `终曲`：根据本回合连续打出叫牌次数增加伤害
- `毒血`：根据自身流血层数给予中毒
- `痛苦化作力量`：根据自身流血倍数获得想嚼
- `偷袭`：负野性时反向加伤

这类迁移主要改善卡面预览，不是基础逻辑修复。建议等卡牌机制稳定后再集中处理。

### 5. 配置 UI 条件显示

新版 BaseLib 提供了：

```csharp
ConfigVisibleIfAttribute
ConfigVisibleWhenAttribute
ConfigHoverTipsByDefaultAttribute
```

BigDog 当前只有一个设置项：

- 是否禁用大狗叫和大狗嚼语音

现在还不需要条件显示。以后如果增加“语音音量”“音效倍率上限”“调试日志”等设置，可以用这些 attribute 做更干净的设置界面。

## 固定开发约定

新增 Power 时：

- 如果描述里需要 `{Amount}`、`{WantChew}`、`{Turns}` 等变量，优先实现 `IAddDumbVariablesToPowerDescription`。
- 不要为了单纯填变量而重写 `Description`。
- 只有描述文本本身需要动态拼接时，才考虑重写 `Description`，例如大狗用力效果列表这种复杂文本。

新增临时 Power 时：

- 如果是“回合结束自动移除/回滚”的效果，先评估能否使用 BaseLib 的 `CustomTemporaryPowerModel`。
- 如果继承原版临时 Power 更稳定，可以先保留原写法，不要为了迁移而破坏已验证逻辑。

新增先古卡或升级遗物时：

- 先沿用当前已经稳定的事件和遗物映射链。
- 不要同时让 Harmony patch 和 BaseLib 新 API 对同一张卡或同一个遗物重复注册。
- 如果改用 BaseLib 官方入口，需要单独测试百科图鉴、事件奖励、遗物升级、存档读档四条路径。

## 本次验证

已执行：

```powershell
dotnet build "D:\UsedToMakeMod\Mods\big-dog\BigDogMod.sln" /property:GenerateFullPaths=true /p:Configuration=Debug /p:Platform="Any CPU" /consoleloggerparameters:NoSummary
```

结果：编译通过，并复制 `BigDogMod.dll` 到游戏 mods 目录。
