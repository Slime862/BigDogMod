# 流血与回合切换架构说明

这份说明专门解释两件事：

1. `BigDog` 的 `流血` 为什么会在敌人死亡时卡住，以及这次怎么修。
2. StS2 战斗里一整个“回合结束 -> 换边 -> 新回合开始”的执行链是怎样串起来的。

## 这次流血卡住的根因

`BigDog` 的 [BleedingPower.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Powers/BleedingPower.cs) 原本是在 `AfterTurnEnd(...)` 里直接掉血，然后如果目标还活着就 `+1` 层。

问题出在：

- 当 DOT 伤害把敌人打死时，死亡动画、死亡 Hook、实体移除、队列推进并不是“同步瞬间完成”的。
- 如果 `流血` 在目标刚死的那一刻立刻继续跑“下一个额外触发”或直接结束 `AfterTurnEnd(...)`，就可能把后续流程压在一个还没完全收完的死亡状态上。
- 这类问题最典型的表现就是：
  - 当前敌人流血致死后卡住
  - 下一个敌人的流血不再继续结算
  - 或者敌方回合结束后，玩家回合进不来

原版 [PoisonPower.cs](/d:/UsedToMakeMod/SlayTheSpire2/src/Core/Models/Powers/PoisonPower.cs) 已经处理了这个问题：

- 中毒在 `AfterSideTurnStart(...)` 里逐次掉血
- 如果 DOT 没打死目标，就继续减层
- 如果 DOT 打死了目标，它不会马上继续往下跑，而是：

```csharp
await Cmd.CustomScaledWait(0.1f, 0.25f);
```

这段小等待的作用不是“拖时间”，而是给死亡/移除链一个安全落地窗口。

## 这次的修法

`流血` 现在也改成了和原版 `Poison` 一样的思路：

- 每次触发 `TriggerBleeding()` 后返回“目标是否还活着”
- 如果目标活着：
  - 正常 `+1` 层流血
- 如果目标死了：
  - 立刻 `await Cmd.CustomScaledWait(0.1f, 0.25f)`
  - 然后结束这次 `流血` 的后续触发

这样有几个好处：

- 不去碰全局死亡逻辑
- 不去强行手动 `Kill(...)`
- 不改变 `灾厄 / Doom`、防死、复活、尸体保留等系统自己的处理方式
- 只是在“DOT 致死”这个时间点，给回合切换链一个稳定的缓冲

## 为什么这不会和灾厄等效果冲突

原版 [DoomPower.cs](/d:/UsedToMakeMod/SlayTheSpire2/src/Core/Models/Powers/DoomPower.cs) 的思路和 DOT 不一样：

- `Doom` 在 `BeforeTurnEnd(...)` 判定是否斩杀
- 真正死亡时会单独走 `DoomKill(...)`
- 里面会先播 Doom 特效，再 `CreatureCmd.Kill(...)`
- 最后再触发 `Hook.AfterDiedToDoom(...)`

所以：

- `Doom` 是“专门的斩杀流程”
- `流血` 是“普通 DOT 掉血流程”

这次修复没有去复制 `DoomKill(...)`，也没有去改 `CreatureCmd.Kill(...)`，因此不会把 `Doom` 的专属逻辑抢走。

同理，对这些系统也尽量保持兼容：

- 防死 / 复活
- `ShouldDie(...)` 一类 Hook
- 死后不立即移除的怪
- 死亡后还会触发额外效果的 Power/Relic

因为这些都还是继续走原版的伤害与死亡链，我们只是在 DOT 致死后“等它们收完”。

## 回合切换的主链路

核心在 [CombatManager.cs](/d:/UsedToMakeMod/SlayTheSpire2/src/Core/Combat/CombatManager.cs) 和 [Hook.cs](/d:/UsedToMakeMod/SlayTheSpire2/src/Core/Hooks/Hook.cs)。

可以把一整个回合切换理解成 5 段。

## 1. 回合结束前

当玩家或敌人准备结束当前 side 时，会先进入：

- `Hook.BeforeTurnEnd(combatState, side)`

这个 Hook 里又分成三层顺序：

1. `BeforeTurnEndVeryEarly`
2. `BeforeTurnEndEarly`
3. `BeforeTurnEnd`

这一步更适合做：

- 斩杀判定
- 结束前的资源或状态处理
- 必须在弃牌/清理之前完成的事情

例如 `DoomPower` 就是在这里先判断要不要直接斩杀。

## 2. 当前 side 的结束清理

玩家回合和敌人回合这里略有不同。

### 玩家回合结束

大致会经过：

1. `EndPlayerTurnPhaseOneInternal()`
2. 处理手牌里 `OnTurnEndInHand`
3. `BeforeFlush`
4. `EndPlayerTurnPhaseTwoInternal()`
5. 手牌保留 / 弃牌 / 回合结束清理

在 `EndPlayerTurnPhaseTwoInternal()` 末尾，会调用：

- `Hook.AfterTurnEnd(_state, _state.CurrentSide)`

### 敌人回合结束

敌人行动跑完后，会走：

- `EndEnemyTurnInternal()`

里面顺序是：

1. `Hook.BeforeTurnEnd(_state, _state.CurrentSide)`
2. 玩家 `EndOfTurnCleanup()`
3. `Hook.AfterTurnEnd(_state, _state.CurrentSide)`

## 3. AfterTurnEnd 的执行顺序

[Hook.cs](/d:/UsedToMakeMod/SlayTheSpire2/src/Core/Hooks/Hook.cs) 里，`AfterTurnEnd(...)` 不是单个调用，而是两层：

1. `AfterTurnEnd`
2. `AfterTurnEndLate`

而且它会遍历 `combatState.IterateHookListeners()` 里的所有模型。

这意味着：

- 你的 `流血`、`再生`、`虚弱`、遗物、姿态、其他角色特性
- 都可能在同一个 `AfterTurnEnd` 阶段里依次执行

所以这里最重要的原则是：

- 不要假设别的 Hook 已经完全收尾
- 如果会造成死亡，最好像原版 `Poison` 一样给死亡链一个小等待

## 4. 换边

当前 side 的 `AfterTurnEnd(...)` 全跑完以后，`CombatManager` 才会：

- `SwitchSides()`

也就是：

- 玩家 side -> 敌人 side
- 或敌人 side -> 玩家 side

如果这里之前的死亡状态没收干净，就容易表现成“换边换不过去”。

## 5. 新回合开始

换边以后，战斗会进入新回合开始流程，典型会触发：

- `AfterSideTurnStart(...)`
- 以及其他 turn-start 相关 Hook

原版 `PoisonPower` 就是挂在这里触发的。

而 `BigDog` 的 `流血` 现在是挂在：

- `AfterTurnEnd(...)`

这本身没有问题，但因为它是在“回合切换前的最后阶段”做掉血，所以更要注意死亡链的收尾。

## DOT、Doom、死亡、换边的职责分层

可以按下面理解：

- `CreatureCmd.Damage(...)`
  - 负责普通伤害结算
  - 包括扣血、格挡、受击、死亡判定等

- `CreatureCmd.Kill(...)`
  - 负责明确杀死一个单位
  - 常见于特殊斩杀、事件、强制死亡

- `DoomPower`
  - 在 `BeforeTurnEnd(...)` 决定是否 doom kill
  - 用专门的 `DoomKill(...)` 跑独立特效和死亡后续

- `PoisonPower`
  - 在 `AfterSideTurnStart(...)` 做普通 DOT
  - DOT 致死后短暂等待

- `BleedingPower`
  - 在 `AfterTurnEnd(...)` 做普通 DOT
  - 现在也采用和 `Poison` 相同的“致死后短暂等待”策略

## 对 BigDog 后续写状态效果的建议

以后如果再做类似：

- 回合结束掉血
- 回合开始掉血
- 多次连锁触发的 DOT
- 结束时斩杀 / 判定 / 变形

建议遵守这几个规则：

1. 普通 DOT 尽量走 `CreatureCmd.Damage(...)`
2. 不要在 DOT 里手写一套新的 `Kill(...)`
3. DOT 致死时，优先参考 `PoisonPower` 的等待方式
4. 专属斩杀机制，如果需要独立特效/独立后处理，再参考 `DoomPower`
5. 如果效果挂在 `AfterTurnEnd(...)`，要特别小心它会直接影响换边

## 一句话总结

这次卡住的本质不是“流血不会杀人”，而是“流血杀人后，没有给死亡与移除链足够的收尾时间，就直接继续推进了回合结算”。

修复方式是参考原版 `PoisonPower`：

- DOT 正常掉血
- 活着就继续
- 死了就短暂等待死亡链落地

这样既能修住卡死，又尽量不和 `Doom`、防死、复活、死亡后触发效果这些原版系统冲突。
