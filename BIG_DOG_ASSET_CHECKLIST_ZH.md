# BigDog 素材路径清单

下面这些路径已经在项目里预留好了。你后续把素材填进去后，`BigDog` 会优先使用这些新资源；如果文件还没放进去，当前代码会继续回退到 `Defect` 的原版资源。

## 卡图

- `BigDogMod/assets/cards/rending_bite.png`
- `BigDogMod/assets/cards/stoke_wildness.png`
- `BigDogMod/assets/cards/big_dog_howl.png`
- `BigDogMod/assets/cards/big_dog_chew.png`

可选 Beta 卡图：

- `BigDogMod/assets/cards/beta/rending_bite.png`
- `BigDogMod/assets/cards/beta/stoke_wildness.png`
- `BigDogMod/assets/cards/beta/big_dog_howl.png`
- `BigDogMod/assets/cards/beta/big_dog_chew.png`

## Power 图标

- `BigDogMod/assets/powers/wildness.png`
- `BigDogMod/assets/powers/temporary_wildness.png`
- `BigDogMod/assets/powers/bleeding.png`

可选大图或 Beta 图：

- `BigDogMod/assets/powers/beta/wildness.png`
- `BigDogMod/assets/powers/beta/temporary_wildness.png`
- `BigDogMod/assets/powers/beta/bleeding.png`

## 角色选择界面

- 角色头像：
  - `BigDogMod/assets/character/select/char_select_big_dog.png`
- 未解锁头像：
  - `BigDogMod/assets/character/select/char_select_big_dog_locked.png`
- 角色大图背景场景：
  - `BigDogMod/assets/character/scenes/char_select_bg_big_dog.tscn`
- 角色切换转场材质：
  - `BigDogMod/assets/materials/big_dog_transition_mat.tres`

说明：
- `char_select_big_dog.png` 是角色选择界面的小头像。
- `char_select_bg_big_dog.tscn` 是角色选择界面的“大图/背景”入口。通常这里会是一个场景，里面再引用你的大图贴图。

## 战斗角色与顶部 UI

- 战斗角色立绘场景：
  - `BigDogMod/assets/character/scenes/big_dog_visuals.tscn`
- 顶部头像：
  - `BigDogMod/assets/character/top_panel/character_icon_big_dog.png`
- 顶部头像描边：
  - `BigDogMod/assets/character/top_panel/character_icon_big_dog_outline.png`
- 顶部角色图标场景：
  - `BigDogMod/assets/character/scenes/big_dog_icon.tscn`
- 能量球/能量计数器场景：
  - `BigDogMod/assets/character/scenes/big_dog_energy_counter.tscn`

## 地图与休息点等角色场景

- 地图节点标记：
  - `BigDogMod/assets/character/map/map_marker_big_dog.png`
- 商人界面角色场景：
  - `BigDogMod/assets/character/merchant/big_dog_merchant.tscn`
- 休息点角色场景：
  - `BigDogMod/assets/character/rest_site/big_dog_rest_site.tscn`
- 卡牌拖尾特效场景：
  - `BigDogMod/assets/vfx/card_trail_big_dog.tscn`

## 联机手势贴图

- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_point.png`
- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_rock.png`
- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_paper.png`
- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_scissors.png`

## 建议的填充顺序

1. 先填角色选择头像：
   `char_select_big_dog.png`
2. 再填角色选择大图：
   `char_select_bg_big_dog.tscn`
3. 再填战斗立绘：
   `big_dog_visuals.tscn`
4. 再填四张卡图和三个 Power 图标
5. 最后补顶部头像、地图图标、休息点/商人/拖尾这些边缘资源

## 当前代码如何使用这些路径

- 角色现在按教程切回了 `PlaceholderCharacterModel`
- 当前代码为了保持稳定，角色还在完整借用 `PlaceholderID = "defect"` 的原版资源
- 上面列出的路径目前是“你接下来要填的目标路径清单”，还没有重新接回角色类
- 等你开始逐项放图后，再把对应 `Custom*` 属性一点点接回去最稳
- 卡图和 Power 图标目前仍然是“新路径优先，原版回退”

所以你可以按这个清单逐个填，不需要一次性把所有素材都准备齐。
