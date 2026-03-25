# 项目约定

## 卡牌配置表

`BigDogMod` 后续卡牌相关改动，以 [CARD_CONFIG_TABLE.csv](/d:/UsedToMakeMod/Mods/big-dog/CARD_CONFIG_TABLE.csv) 为唯一基准。

执行规则：

- 改卡牌数值、类型、费用、标签、效果、描述时，先更新配置表。
- 后续实现代码时，以配置表为准修正：
  - 卡牌代码
  - 本地化描述
  - 相关 Power / Command / HoverTip
- 若表格与现有实现冲突，以表格内容为准。
- 若用户在对话里临时口头修改卡牌，也应在落地代码时同步更新这张表。

## BaseLib

- 优先使用 `BaseLib` 已有的 `Custom*` 系列基类。
- 卡牌优先用 `CustomCardModel`
- Power 优先用 `CustomPowerModel`
- 角色优先用 `PlaceholderCharacterModel` 或 `CustomCharacterModel`

## 资源替换

- 角色当前稳定方案是 `PlaceholderID = "defect"`
- 替换素材时遵循“逐项替换、逐项测试”
- 没有准备好的角色资源，不要提前接入角色类

## 表驱动协作补充规则（2026-03-25）
- 以后实现卡牌时，默认只“读取” `CARD_CONFIG_TABLE.csv`，并且只补充当前为空的格子。
- 表中已经有内容的字段一律不主动改写，包括卡名、基础效果、升级效果、描述、升级描述、稀有度、费用、目标、标签/关键词等。
- 只有字段为空时，助手才可以补充；`卡牌ID` 缺失时，也只补 `卡牌ID`，不顺带改其他列。
- 用户只需要指出“哪些行更新了”，助手再去表中读取这些行并实现逻辑。
- 如果表与现有代码不一致：以表为准改代码，不反向改表。
- 卡牌描述和升级描述只有在对应单元格为空时，才允许根据前面的效果列补全；如果表里已有现成描述，则保持不动。
- 若实现过程中发现表信息不足，先保持表原文不变，再单独向用户指出缺失项，不自行补写额外设计结论。

## 大狗用力扩展规范（2026-03-25）
- 所有“下次大狗嚼获得额外效果”的来源，都统一接入 `BigDogChewPrepPower` 体系。
- 来源卡不要自己拼描述、自己存独立字段；只负责注册 `BigDogChewPrepEffect(Type, Amount)`。
- 效果类型统一定义在 `Scripts/ChewPrep/BigDogChewPrepEffectType.cs`。
- 枚举到描述 key 的映射统一写在 `Scripts/ChewPrep/BigDogChewPrepEffectTypeExtensions.cs`。
- `BigDogChewPrepPower` 负责：
  - 累积同类效果数量
  - 只显示数量大于 0 的效果描述
  - 生成 HoverTip
  - 在消费时清空自身状态
- `BigDogChewPrepCmd.Resolve(...)` 负责统一结算各效果；新增效果时，结算逻辑也只加在这里。
- 本地化规则：
  - 总描述使用 `BIG_DOG_CHEW_PREP_POWER.description`，并通过 `{EffectsText}` 占位插入当前有效效果文本
  - 每个效果单独提供一条 `xxxLine`
  - 例如：`weakLine`、`bleedingLine`、`drawLine`
- 新增一种大狗用力效果时，按以下顺序修改：
  1. 在 `BigDogChewPrepEffectType` 中新增枚举值
  2. 在 `BigDogChewPrepEffectTypeExtensions` 中补 `LocLineKey()` 映射；如需要 HoverTip，也在这里补
  3. 在 `BigDogChewPrepCmd.Resolve(...)` 中补充实际结算逻辑
  4. 在 `powers.json` 中补 `xxxLine` 本地化
  5. 让来源卡通过 `IBigDogChewPrepSource` 返回该效果
- 如果某个效果当前数量为 0，则不显示它的描述，也不显示对应 tip。
- 若以后出现“不适合等到下次大狗嚼统一结算”的效果，不要强行塞进这套体系，应单独讨论是否属于大狗用力机制。
