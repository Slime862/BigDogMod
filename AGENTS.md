# BigDog Mod 开发规范

## BaseLib 源码

长期放在项目内固定位置。


`ThirdParty/BaseLib/`

- 发布产物：`BaseLib.dll`、`BaseLib.pck`、`BaseLib.json`

`ThirdParty/BaseLibSource/BaseLib-StS2/`

- 源码仓库：`https://github.com/Alchyr/BaseLib-StS2`

- `BaseLib` 发布包是“编译依赖”
- `BaseLib` 源码是“开发参考依赖”

## 开发总原则

- 优先使用 `BaseLib` 已经提供好的类，尤其是 `Custom*` 系列。
- 能走 `BaseLib` 抽象层时，不要优先直接继承原版底层模型。
- 能不写 Harmony patch 时，就不写。
- 如果必须写 Harmony patch，优先做“只处理本 mod 自己类型”的窄 patch。
- 素材不全时，可以先复用原版资源；但这类兜底 patch 视为临时方案，后续应逐步替换成真实资源。

## 模型基类优先级

新增内容时，优先按下面顺序选基类：

- 卡牌：优先 `BaseLib.Abstracts.CustomCardModel`
- 遗物：优先 `BaseLib.Abstracts.CustomRelicModel`
- Power / Buff / Debuff：优先 `BaseLib.Abstracts.CustomPowerModel`
- 角色：优先 `BaseLib.Abstracts.CustomCharacterModel` 或 `BaseLib.Abstracts.PlaceholderCharacterModel`
- Ancient：优先 `BaseLib.Abstracts.CustomAncientModel`
- 卡池：优先 `BaseLib.Abstracts.CustomCardPoolModel`
- 遗物池：优先 `BaseLib.Abstracts.CustomRelicPoolModel`
- 药水：优先 `BaseLib.Abstracts.CustomPotionModel`
- 药水池：优先 `BaseLib.Abstracts.CustomPotionPoolModel`

只有在下面这些情况，才考虑直接继承原版类：

- `BaseLib` 没有对应抽象层
- `BaseLib` 抽象层当前版本不可用或有明确 bug
- 你已经确认原版基类更适合当前目标，并且不会破坏 BaseLib 的前缀、本地化、资源路径规则

## Harmony 使用规则

- Harmony patch 仅用于补 `BaseLib` 抽象层覆盖不到的地方。
- patch 必须优先限定到本 mod 自己的类型，例如：
  - 只在 `__instance is MyCustomCard`
  - 只在 `__instance is MyCustomPower`
- 不允许为了省事去全局覆盖原版逻辑。
- 不允许把 patch 当成常规注册机制使用；注册优先走 `BaseLib`。

## 本地化规则

- 本地化路径按 `{modId}/localization/{lang}/` 组织。
- `cards.json`、`characters.json`、`powers.json` 等表名必须和游戏实际查找的表一致。
- key 以运行时真实 `Id.Entry` 为准，不要只凭类名猜。
- 如果使用 `BaseLib` 的 `Custom*` 模型，优先按 BaseLib 生成的前缀规则写 key。

## 资源规则

- 自定义内容优先提供正式资源。
- 没有素材时允许临时复用原版资源。
- 临时复用资源时，优先：
  - 直接覆写 `Custom...Path` 一类属性
  - 其次才是 Harmony patch getter
- 临时资源 patch 必须在注释或文档里标明“后续待替换”。

## BaseLib Custom 类路径

以下路径来自 BaseLib 官方仓库 `Alchyr/BaseLib-StS2` 的 `Abstracts/` 目录：

- `Abstracts/CustomAncientModel.cs`
- `Abstracts/CustomCardModel.cs`
- `Abstracts/CustomCardPoolModel.cs`
- `Abstracts/CustomCharacterModel.cs`
- `Abstracts/CustomPotionModel.cs`
- `Abstracts/CustomPotionPoolModel.cs`
- `Abstracts/CustomPowerModel.cs`
- `Abstracts/CustomRelicModel.cs`
- `Abstracts/CustomRelicPoolModel.cs`
- `Abstracts/PlaceholderCharacterModel.cs`
- `Abstracts/ICustomModel.cs`
- `Abstracts/ICustomPower.cs`
- `Abstracts/ICustomEnergyIconPool.cs`
- `Abstracts/IHealAmountModifier.cs`

官方仓库：

- `https://github.com/Alchyr/BaseLib-StS2`

官方 `Abstracts` 目录：

- `https://github.com/Alchyr/BaseLib-StS2/tree/master/Abstracts`

## 当前项目的落实要求

- 以后新增卡牌，默认先看 `CustomCardModel.cs`
- 以后新增 Power，默认先看 `CustomPowerModel.cs`
- 以后新增角色，默认先看 `CustomCharacterModel.cs` 和 `PlaceholderCharacterModel.cs`
- 以后新增遗物，默认先看 `CustomRelicModel.cs`
- 如果实现上出现“不知道 BaseLib 有没有现成入口”，先查 `ThirdParty/BaseLibSource/BaseLib-StS2/Abstracts/`
## Card Config Source Of Truth

- Use `CARD_CONFIG_TABLE.csv` as the single source of truth for future card changes.
- Update the table first, then update code.
- If code and table disagree, follow the table.

# 卡牌配置读取规则
- 如果卡名以中文名为准，如果卡牌ID为空，则先从卡名翻译过去，然后在根据卡牌描述来写卡牌逻辑。另外升级效果这一列是对升级效果的说明，升级后卡牌的描述不要错误地使用了这个列的文本。
- 根据表生成卡牌时，表里少什么字段就帮我补充上什么字段。
- 空行是我留出放其他卡牌的，别删掉了。

## 表驱动协作补充规则（2026-03-25）
- 以后默认把 `CARD_CONFIG_TABLE.csv` 当作只读规格来源，而不是由助手主动回写设计内容的地方。
- 助手不会主动修改表中的卡名、基础效果、升级效果、稀有度、费用、目标、标签/关键词。
- 只有 `卡牌ID` 缺失时，助手才会根据中文卡名生成稳定英文 ID，并在实现时同步补回。
- 用户在对话里只需说明“哪些卡更新了”；助手负责去表里找到这些行并按表实现。
- 若代码与表冲突，改代码，不改表。
- 卡牌描述和升级描述默认根据表中前面的效果列补全到本地化文本；如果表里已明确写好描述，则优先按表中描述落地。
- 若表信息不足，只指出缺失点，不代写额外设计结论。

## 大狗用力扩展规范（2026-03-25）
- 所有“下次大狗嚼获得额外效果”的来源，都统一接入 `BigDogChewPrepPower` 体系。
- 来源卡不要自己拼描述、自己存独立字段；只负责注册 `BigDogChewPrepEffect(Type, Amount)`。
- 效果类型统一定义在 `Scripts/ChewPrep/BigDogChewPrepEffectType.cs`。
- 枚举到描述 key 的映射统一写在 `Scripts/ChewPrep/BigDogChewPrepEffectTypeExtensions.cs`。
- `BigDogChewPrepPower` 负责累积数量、生成描述、提供 tip、并在消费时清空状态。
- `BigDogChewPrepCmd.Resolve(...)` 负责统一结算；新增效果时只在这里补结算逻辑。
- 本地化规则：总描述走 `BIG_DOG_CHEW_PREP_POWER.description` 的 `{EffectsText}`，每个效果独立提供 `xxxLine`。
- 新增一种效果时，按顺序修改：枚举 -> 扩展映射 -> Resolve 结算 -> powers.json -> 来源卡。
- 若某效果数量为 0，则不显示对应描述，也不显示对应 tip。
