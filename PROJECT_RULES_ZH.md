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
