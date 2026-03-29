# BigDog 音效资源路径

当前已经接入了两段可替换音效：

- `res://BigDogMod/assets/audio/big_dog_howl.ogg`
  - 或 `res://BigDogMod/assets/audio/big_dog_howl.mp3`
  - 对应：大狗叫
  - 触发时机：每次打出“叫”类型的卡时播放

- `res://BigDogMod/assets/audio/big_dog_chew.ogg`
  - 或 `res://BigDogMod/assets/audio/big_dog_chew.mp3`
  - 对应：大狗嚼
  - 触发时机：打出 `大狗嚼` 时播放

当前倍速规则：

- 初始倍率为 `1x`
- 每次打出“叫”类型的卡，倍率 `+0.5x`
- 最高 `4x`
- 打出 `大狗嚼` 时，按当前倍率播放 `BigDogChewSfx`
- `大狗嚼` 播放后，倍率重置为 `1x`

格式说明：

- `ogg` 和 `mp3` 都可以
- 当前代码会优先读取同名 `ogg`
- 如果没有 `ogg`，会自动尝试读取同名 `mp3`

对应代码位置：

- [BigDogAssetPaths.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Assets/BigDogAssetPaths.cs)
- [BigDogSfxHelper.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Audio/BigDogSfxHelper.cs)
- [BigDogHowl.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Cards/BigDogHowl.cs)
- [BigDogChew.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Cards/BigDogChew.cs)
