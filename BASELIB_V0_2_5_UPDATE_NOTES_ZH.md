# BaseLib v3.0.7 版本记录与 BigDog 接入建议

当前检查日期：2026-04-19

## 当前本地状态

项目引用目录和游戏 `mods` 目录已经替换为 `BaseLib v3.0.7`：

- 项目引用目录：`D:\UsedToMakeMod\Mods\big-dog\ThirdParty\BaseLib`
- 游戏 mod 目录：`D:\steam\steamapps\common\Slay the Spire 2\mods\BaseLib`

这两个目录里的 `BaseLib.json` 当前版本字段都是：

```json
"version": "v3.0.7"
```

## 从 v0.2.5 到 v3.0.7 的主要新增能力

根据新版 `BaseLib.dll` 暴露的类型和 release 页面，当前可重点关注这些能力：

- `CustomTemporaryPowerModel`
  - BaseLib 现在提供自定义临时 Power 基类。
  - BigDog 目前有多处“本回合临时效果”仍是手写 `AfterTurnEnd` 或继承原版 `TemporaryStrengthPower`，可以逐步收敛。

- `IAddDumbVariablesToPowerDescription`
  - BaseLib 现在提供给 Power 描述追加变量的接口。
  - BigDog 目前多个 Power 为了填 `{Amount}` 手动重写 `Description`，可以迁移成接口式变量注入。

- `ITranscendenceCard` / `AddTranscendenceUpgradeForCustomCharacters`
  - BaseLib 现在提供先古化升级链支持。
  - BigDog 目前用 Harmony patch 接 `ArchaicTooth` 和 `DustyTome`，后续可以考虑改成 BaseLib 官方入口。

- `CustomStarterUpgrade`
  - BaseLib 现在提供自定义起始遗物升级相关能力。
  - BigDog 目前用 Harmony patch 接 `TouchOfOrobas.GetUpgradedStarterRelic`，后续可以考虑迁移。

- `AddCustomAncientForCompendium`
  - BaseLib 现在有把自定义先古内容加入百科/图鉴的入口。
  - BigDog 目前仍保留 `BigDogCardLibraryPatches` 强行插卡库，后续可以优先替换这块。

- `IMaxHandSizeModifier`
  - BaseLib 现在支持修改最大手牌数。
  - BigDog 当前没有最大手牌机制，暂时不需要接。

- `CustomCalculatedVar`
  - BaseLib 现在支持自定义计算型动态变量。
  - BigDog 目前有一些运行时计算显示需求，例如 `终曲` 的连续叫加伤、负野性加伤、流血倍率等，后续可以用它减少手动描述/预览差异。

- `CustomCardFrameMaterial`
  - BaseLib 现在支持按卡或卡池自定义卡框材质。
  - BigDog 当前仍大量 fallback 到猎手卡图/卡框，可以等美术资源稳定后再接。

- `CustomSingletonModel`
  - BaseLib 支持自定义单例模型。
  - BigDog 当前没有明显需要注册为单例模型的系统；音频配置和全局规则仍用静态类即可。

- Config 系统增强
  - 新增 `ConfigVisibleIfAttribute`、`ConfigVisibleWhenAttribute`、`ConfigHoverTipsByDefaultAttribute` 等。
  - BigDog 目前只有“禁用语音”一个配置项，暂时不需要复杂化；后续如果添加音量、音效倍率、调试开关，可以接这些 attribute。

- `Purge`
  - BaseLib 新增 Purge 相关能力。
  - BigDog 当前没有“净化/移除出牌组”的新机制，暂时不需要接。

## BigDog 当前最值得接入的 5 项

### 1. 先古卡与起始遗物升级链

当前相关文件：

- `Scripts\Patches\BigDogAncientRelicPatches.cs`
- `Scripts\Patches\BigDogTouchOfOrobasPatches.cs`
- `Scripts\Patches\BigDogCardLibraryPatches.cs`

建议：

- 优先研究 `ITranscendenceCard`、`AddTranscendenceUpgradeForCustomCharacters`、`CustomStarterUpgrade`、`AddCustomAncientForCompendium`。
- 如果 BaseLib 的入口能覆盖当前需求，就可以逐步移除这三类 Harmony patch。
- 这是当前最值得迁移的部分，因为这些 patch 直接碰原版事件、遗物和百科 UI，维护风险最高。

### 2. Power 描述变量注入

当前 BigDog 中有多个 Power 手动重写 `Description` 只为了填 `{Amount}` 或其他变量，例如：

- `DogSagePower`
- `LoyalFriendPower`
- `WolfHowlPower`
- `HesitationPower`
- `AttackWindupPower`
- `HealingSongPower`

建议：

- 统一迁移到 `IAddDumbVariablesToPowerDescription`。
- 好处是减少重复 `new LocString(...); description.Add(...)`，也更贴合新版 BaseLib 的描述扩展方式。

### 3. 临时 Power 和本回合效果

当前 BigDog 中有多种临时效果：

- `MightyBlowStrengthDownPower`
- `ForgetChewStrengthDownPower`
- `WolfHowlStrengthDownPower`
- `EncouragingHowlTemporaryStrengthPower`
- `EncouragingHowlTemporaryStrengthDownPower`
- `WatcherDogPower`
- `WildnessPower.AddTemporaryAmount(...)`

建议：

- 可优先评估 `CustomTemporaryPowerModel`。
- 如果它能表达“本回合结束自动移除/回滚”的通用逻辑，`野性` 的临时量追踪也可以从 `_temporaryAmount` 逐步改成更统一的临时 Power 方案。

### 4. 自定义动态变量

当前 BigDog 已有：

- `WantChewVar`

后续可考虑接：

- `CustomCalculatedVar`

适合候选：

- `终曲`：根据本回合连续打出“叫”的数量预览加伤。
- `痛苦化作力量`：根据自身流血倍数预览想嚼。
- `毒血`：根据自身流血预览中毒。
- `偷袭`：负野性时的特殊加伤预览。

建议：

- 这类属于体验优化，不是必须立刻做。
- 等当前逻辑稳定后，再集中处理“卡面预览更准确”。

### 5. 配置系统

当前相关文件：

- `Scripts\Config\BigDogModConfig.cs`
- `BigDogMod\localization\eng\settings_ui.json`
- `BigDogMod\localization\zhs\settings_ui.json`

当前 JSON 语法检查通过。

建议：

- 现阶段无需大改。
- 如果后续添加更多设置，可以接 `ConfigVisibleIfAttribute` / `ConfigVisibleWhenAttribute` 做条件显示。

## 暂时不建议立刻迁移的部分

- 卡图 fallback：`BigDogCardPortraitPatches.cs`
  - 虽然 BaseLib 有 `CustomCardPortrait*` 能力，但 BigDog 之前在新版 BaseLib 上遇到过卡图透明和 `CustomCardPortraitPath.UseAltTexture` 空指针。
  - 当前 fallback 方案已经稳定，建议等自定义卡图资源补齐后再重构。

- Power 图标 fallback：`BigDogPowerIconPatches.cs`
  - BaseLib 是否有完全覆盖 Power 图标 fallback 的高层 API 还需要进一步验证。
  - 当前 patch 虽长，但行为明确，暂时保留更稳。

- 最大手牌数：`IMaxHandSizeModifier`
  - BigDog 当前没有相关设计，暂不接。

- `Purge`
  - BigDog 当前没有净化类机制，暂不接。

## 推荐迁移顺序

1. 先古卡/起始遗物升级链：替换 `BigDogAncientRelicPatches`、`BigDogTouchOfOrobasPatches`、`BigDogCardLibraryPatches`。
2. Power 描述变量注入：用 `IAddDumbVariablesToPowerDescription` 简化 `{Amount}` 填充。
3. 临时 Power：评估 `CustomTemporaryPowerModel`，逐步替换手写回合结束回滚。
4. 计算型动态变量：用 `CustomCalculatedVar` 改善复杂卡的预览显示。
5. 配置 UI：等设置项变多后再接新版可见性/hover tip attribute。

