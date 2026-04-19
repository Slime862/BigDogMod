# BaseLib 版本记录

当前检查日期：2026-04-19

## 当前本地状态

项目引用目录和游戏 `mods` 目录目前仍是 `BaseLib v0.2.5`：

- 项目引用目录：`D:\UsedToMakeMod\Mods\big-dog\ThirdParty\BaseLib`
- 游戏 mod 目录：`D:\steam\steamapps\common\Slay the Spire 2\mods\BaseLib`

这两个目录里的 `BaseLib.json` 当前版本字段都是：

```json
"version": "v0.2.5"
```

## 最新正式版

GitHub release 页面显示当前最新正式版是：

- `v3.0.6`
- 发布时间：2026-04-16
- 官方说明：`Log window fixes`
- 来源：`https://github.com/Alchyr/BaseLib-StS2/releases`

## 从 v0.2.5 到 v3.0.6 的主要新增内容

根据 GitHub release 页面，`v0.2.5` 之后新增/调整的重点包括：

- `v0.2.6 / v0.3.0`
  - 同时兼容 main branch 和 beta branch。
  - 新增配置功能。
  - Logger 改为使用 Godot log，以便获得和正常日志文件一致的文本。
  - 新增 `CustomSingletonModel`。
  - 新增 Harmony Patch Dump。
  - 调整 `.csproj` setup。

- `v3.0.1`
  - 修复若干 bug。
  - 更新配置系统。

- `v3.0.2 / v3.0.3`
  - 修复若干 bug。
  - 更新 Mod config。
  - 增加 `zhs` 相关支持。
  - 新增 `IAddDumbVariablesToPowerDescription`。
  - 自定义角色可在角色选择/随机角色中启用或禁用。
  - 新增 simplified localization 支持。
  - 新增 `NRestSiteCharacterFactory`。
  - 自定义枚举改为用 hash 生成，保证值生成更稳定。
  - 新增 `CustomCalculatedVar`，支持任意数量的 calculated variables。

- `v3.0.5`
  - 新增 `Purge`。
  - 新增 `IMaxHandSizeModifier`。
  - 支持用 `##` 禁用 simplified loc。
  - 配置 attribute 重做。
  - 修复若干 bug。

- `v3.0.6`
  - 修复 Log window。

## 本次替换状态

这次我已经确认最新正式版信息，但还没有成功替换本地文件。

原因是当前终端环境无法稳定下载 GitHub release 资产：

- GitHub API 请求触发未认证 rate limit。
- `Invoke-WebRequest` 下载 release 资产时连接失败。
- `curl` 下载 release 资产时无法解析 `github.com`。

因此，为避免把旧版误标成新版，当前没有改动以下文件：

- `D:\UsedToMakeMod\Mods\big-dog\ThirdParty\BaseLib\BaseLib.dll`
- `D:\UsedToMakeMod\Mods\big-dog\ThirdParty\BaseLib\BaseLib.json`
- `D:\UsedToMakeMod\Mods\big-dog\ThirdParty\BaseLib\BaseLib.pck`
- `D:\steam\steamapps\common\Slay the Spire 2\mods\BaseLib\BaseLib.dll`
- `D:\steam\steamapps\common\Slay the Spire 2\mods\BaseLib\BaseLib.json`
- `D:\steam\steamapps\common\Slay the Spire 2\mods\BaseLib\BaseLib.pck`

## 手动替换步骤

如果浏览器可以正常访问 GitHub，可以手动下载 `v3.0.6` release 的资产，然后覆盖以下两个目录：

项目引用目录：

- `D:\UsedToMakeMod\Mods\big-dog\ThirdParty\BaseLib\BaseLib.dll`
- `D:\UsedToMakeMod\Mods\big-dog\ThirdParty\BaseLib\BaseLib.json`
- `D:\UsedToMakeMod\Mods\big-dog\ThirdParty\BaseLib\BaseLib.pck`

游戏 mod 目录：

- `D:\steam\steamapps\common\Slay the Spire 2\mods\BaseLib\BaseLib.dll`
- `D:\steam\steamapps\common\Slay the Spire 2\mods\BaseLib\BaseLib.json`
- `D:\steam\steamapps\common\Slay the Spire 2\mods\BaseLib\BaseLib.pck`

替换后请确认两个 `BaseLib.json` 都显示：

```json
"version": "v3.0.6"
```

