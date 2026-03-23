# 当前 Mod 结构

当前项目是一个与 `RenAmamiyaMod` 同风格的 `BigDogMod` 骨架，方便直接照着扩展。

```text
BigDogMod/
├─ Scripts/
│  ├─ Entry.cs
│  ├─ Cards/
│  │  ├─ BigBite.cs
│  │  └─ LoyalGuard.cs
│  └─ Characters/
│     └─ BigDog.cs
├─ BigDogMod/
│  └─ localization/
│     ├─ eng/
│     │  ├─ cards.json
│     │  └─ characters.json
│     └─ zhs/
│        ├─ cards.json
│        └─ characters.json
├─ ThirdParty/
│  └─ BaseLib/
│     └─ BaseLib.dll
├─ BigDogMod.csproj
├─ BigDogMod.json
├─ BigDogMod.sln
├─ export_presets.cfg
└─ project.godot
```

## 当前内容

- 一个占位新角色 `BigDog`
- 通过 `PlaceholderCharacterModel` 复用 `Defect` 素材
- 两张示例卡：
  - `BigBite`
  - `LoyalGuard`

## 用途

- 先验证 `BaseLib` 接入是否正常
- 先验证角色、本地化、卡牌注册是否正常
- 后续再逐步替换为你自己的素材和正式机制
