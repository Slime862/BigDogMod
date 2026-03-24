# BigDog 素材替换清单

`BigDog` 现在仍然继承 `PlaceholderCharacterModel`，并继续借用 `defect` 的原版资源。

这次已经做好的结构是：

- 角色主类改为使用独立池：
  - `BigDogCardPool`
  - `BigDogRelicPool`
  - `BigDogPotionPool`
- `BigDogCardPool` 现在装的是大狗自己的卡牌
- `BigDogRelicPool`、`BigDogPotionPool` 目前只是“大狗自己的池类外壳”，内部临时复用猎人的内容
- `BigDog.cs` 里已经把后续要替换的 `Custom*` 路径都写好了，但先注释掉了

这样你后面替换资源时，只需要：

1. 把素材放到下面这些路径
2. 回到 [BigDog.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Characters/BigDog.cs) 里，把对应那一行取消注释
3. 重新构建并导出测试

## 当前独立池说明

- 卡牌池类：
  - [BigDogCardPool.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Pools/BigDogCardPool.cs)
- 遗物池类：
  - [BigDogRelicPool.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Pools/BigDogRelicPool.cs)
  - 目前内部临时使用 `SilentRelicPool`
- 药水池类：
  - [BigDogPotionPool.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Pools/BigDogPotionPool.cs)
  - 目前内部临时使用 `SilentPotionPool`

## 当前大狗卡牌池内容

这些卡已经注册进 `BigDogCardPool`：

- `StokeWildness`
- `BigDogHowl`
- `RendingBite`
- `BleedOut`
- `BloodDrink`
- `BloodlettingSlot`
- `ForceAwaken`
- `Hemophobia`
- `VigilantHowl`

说明：

- `BigDogChew` 仍然是衍生牌，不进普通卡池
- 它现在挂在 `TokenCardPool`，用于战斗中生成

## 已预留但暂时注释的角色资源路径

这些路径已经在 [BigDogAssetPaths.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Assets/BigDogAssetPaths.cs) 里写好，并且在 [BigDog.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Characters/BigDog.cs) 里留了对应的注释代码。

### 角色选择界面

- `BigDogMod/assets/character/select/char_select_big_dog.png`
  - 对应：
  - `CustomCharacterSelectIconPath`
- `BigDogMod/assets/character/select/char_select_big_dog_locked.png`
  - 对应：
  - `CustomCharacterSelectLockedIconPath`
- `BigDogMod/assets/character/scenes/char_select_bg_big_dog.tscn`
  - 对应：
  - `CustomCharacterSelectBg`
- `BigDogMod/assets/materials/big_dog_transition_mat.tres`
  - 对应：
  - `CustomCharacterSelectTransitionPath`

### 战斗与顶部 UI

- `BigDogMod/assets/character/scenes/big_dog_visuals.tscn`
  - 对应：
  - `CustomVisualPath`
- `BigDogMod/assets/vfx/card_trail_big_dog.tscn`
  - 对应：
  - `CustomTrailPath`
- `BigDogMod/assets/character/map/map_marker_big_dog.png`
  - 对应：
  - `CustomMapMarkerPath`
- `BigDogMod/assets/character/scenes/big_dog_icon.tscn`
  - 对应：
  - `CustomIconPath`
- `BigDogMod/assets/character/top_panel/character_icon_big_dog.png`
  - 对应：
  - `CustomIconTexturePath`
- `BigDogMod/assets/character/scenes/big_dog_energy_counter.tscn`
  - 对应：
  - `CustomEnergyCounterPath`

### 商人、篝火、多人手势

- `BigDogMod/assets/character/merchant/big_dog_merchant.tscn`
  - 对应：
  - `CustomMerchantAnimPath`
- `BigDogMod/assets/character/rest_site/big_dog_rest_site.tscn`
  - 对应：
  - `CustomRestSiteAnimPath`
- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_point.png`
  - 对应：
  - `CustomArmPointingTexturePath`
- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_rock.png`
  - 对应：
  - `CustomArmRockTexturePath`
- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_paper.png`
  - 对应：
  - `CustomArmPaperTexturePath`
- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_scissors.png`
  - 对应：
  - `CustomArmScissorsTexturePath`

## 卡图路径

这些路径现在已经被卡图兜底逻辑使用。你把图放进去后，会优先显示新图；没图时继续回退原版。

- `BigDogMod/assets/cards/stoke_wildness.png`
- `BigDogMod/assets/cards/big_dog_howl.png`
- `BigDogMod/assets/cards/rending_bite.png`
- `BigDogMod/assets/cards/big_dog_chew.png`
- `BigDogMod/assets/cards/bleed_out.png`
- `BigDogMod/assets/cards/blood_drink.png`
- `BigDogMod/assets/cards/bloodletting_slot.png`
- `BigDogMod/assets/cards/force_awaken.png`
- `BigDogMod/assets/cards/hemophobia.png`
- `BigDogMod/assets/cards/vigilant_howl.png`

可选 Beta 卡图：

- `BigDogMod/assets/cards/beta/stoke_wildness.png`
- `BigDogMod/assets/cards/beta/big_dog_howl.png`
- `BigDogMod/assets/cards/beta/rending_bite.png`
- `BigDogMod/assets/cards/beta/big_dog_chew.png`
- `BigDogMod/assets/cards/beta/bleed_out.png`
- `BigDogMod/assets/cards/beta/blood_drink.png`
- `BigDogMod/assets/cards/beta/bloodletting_slot.png`
- `BigDogMod/assets/cards/beta/force_awaken.png`
- `BigDogMod/assets/cards/beta/hemophobia.png`
- `BigDogMod/assets/cards/beta/vigilant_howl.png`

## Power 图标路径

这些路径现在已经被 Power 图标兜底逻辑使用。你把图放进去后，会优先显示新图；没图时继续回退原版。

- `BigDogMod/assets/powers/wildness.png`
- `BigDogMod/assets/powers/temporary_wildness.png`
- `BigDogMod/assets/powers/bleeding.png`
- `BigDogMod/assets/powers/bleeding_boost.png`
- `BigDogMod/assets/powers/big_dog_chew_prep_power.png`

可选 Beta 图标：

- `BigDogMod/assets/powers/beta/wildness.png`
- `BigDogMod/assets/powers/beta/temporary_wildness.png`
- `BigDogMod/assets/powers/beta/bleeding.png`
- `BigDogMod/assets/powers/beta/bleeding_boost.png`
- `BigDogMod/assets/powers/beta/big_dog_chew_prep_power.png`

## 推荐替换顺序

1. 先替换角色选择头像和背景
2. 再替换战斗立绘与顶部图标
3. 再替换卡图和 Power 图标
4. 最后替换商人、篝火、地图标记、多人手势这些边缘资源

## 什么时候取消注释

建议一项一项来，不要一次全开。

- 角色选择头像做好了：
  - 取消 `CustomCharacterSelectIconPath`
  - 取消 `CustomCharacterSelectLockedIconPath`
- 角色选择背景做好了：
  - 取消 `CustomCharacterSelectBg`
  - 取消 `CustomCharacterSelectTransitionPath`
- 战斗立绘做好了：
  - 取消 `CustomVisualPath`
- 顶部图标和能量面板做好了：
  - 取消 `CustomIconPath`
  - 取消 `CustomIconTexturePath`
  - 取消 `CustomEnergyCounterPath`
- 其余路径做好后再分别取消对应注释
