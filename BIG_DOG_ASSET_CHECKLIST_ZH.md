# BigDog 绱犳潗鏇挎崲娓呭崟

`BigDog` 鐜板湪浠嶇劧缁ф壙 `PlaceholderCharacterModel`锛屽苟缁х画鍊熺敤 `defect` 鐨勫師鐗堣祫婧愩€?
杩欐宸茬粡鍋氬ソ鐨勭粨鏋勬槸锛?
- 瑙掕壊涓荤被鏀逛负浣跨敤鐙珛姹狅細
  - `BigDogCardPool`
  - `BigDogRelicPool`
  - `BigDogPotionPool`
- `BigDogCardPool` 鐜板湪瑁呯殑鏄ぇ鐙楄嚜宸辩殑鍗＄墝
- `BigDogRelicPool`銆乣BigDogPotionPool` 鐩墠鍙槸鈥滃ぇ鐙楄嚜宸辩殑姹犵被澶栧３鈥濓紝鍐呴儴涓存椂澶嶇敤鐚庝汉鐨勫唴瀹?- `BigDog.cs` 閲屽凡缁忔妸鍚庣画瑕佹浛鎹㈢殑 `Custom*` 璺緞閮藉啓濂戒簡锛屼絾鍏堟敞閲婃帀浜?
杩欐牱浣犲悗闈㈡浛鎹㈣祫婧愭椂锛屽彧闇€瑕侊細

1. 鎶婄礌鏉愭斁鍒颁笅闈㈣繖浜涜矾寰?2. 鍥炲埌 [BigDog.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Characters/BigDog.cs) 閲岋紝鎶婂搴旈偅涓€琛屽彇娑堟敞閲?3. 閲嶆柊鏋勫缓骞跺鍑烘祴璇?
## 褰撳墠鐙珛姹犺鏄?
- 鍗＄墝姹犵被锛?  - [BigDogCardPool.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Pools/BigDogCardPool.cs)
- 閬楃墿姹犵被锛?  - [BigDogRelicPool.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Pools/BigDogRelicPool.cs)
  - 鐩墠鍐呴儴涓存椂浣跨敤 `SilentRelicPool`
- 鑽按姹犵被锛?  - [BigDogPotionPool.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Pools/BigDogPotionPool.cs)
  - 鐩墠鍐呴儴涓存椂浣跨敤 `SilentPotionPool`

## 褰撳墠澶х嫍鍗＄墝姹犲唴瀹?
杩欎簺鍗″凡缁忔敞鍐岃繘 `BigDogCardPool`锛?
- `StokeWildness`
- `BigDogHowl`
- `RendingBite`
- `BleedOut`
- `BloodDrink`
- `BloodlettingSlot`
- `ForceAwaken`
- `Cuteify`
- `VigilantHowl`

璇存槑锛?
- `BigDogChew` 浠嶇劧鏄鐢熺墝锛屼笉杩涙櫘閫氬崱姹?- 瀹冪幇鍦ㄦ寕鍦?`TokenCardPool`锛岀敤浜庢垬鏂椾腑鐢熸垚

## 宸查鐣欎絾鏆傛椂娉ㄩ噴鐨勮鑹茶祫婧愯矾寰?
杩欎簺璺緞宸茬粡鍦?[BigDogAssetPaths.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Assets/BigDogAssetPaths.cs) 閲屽啓濂斤紝骞朵笖鍦?[BigDog.cs](/d:/UsedToMakeMod/Mods/big-dog/Scripts/Characters/BigDog.cs) 閲岀暀浜嗗搴旂殑娉ㄩ噴浠ｇ爜銆?
### 瑙掕壊閫夋嫨鐣岄潰

- `BigDogMod/assets/character/select/char_select_big_dog.png`
  - 瀵瑰簲锛?  - `CustomCharacterSelectIconPath`
- `BigDogMod/assets/character/select/char_select_big_dog_locked.png`
  - 瀵瑰簲锛?  - `CustomCharacterSelectLockedIconPath`
- `BigDogMod/assets/character/scenes/char_select_bg_big_dog.tscn`
  - 瀵瑰簲锛?  - `CustomCharacterSelectBg`
- `BigDogMod/assets/materials/big_dog_transition_mat.tres`
  - 瀵瑰簲锛?  - `CustomCharacterSelectTransitionPath`

### 鎴樻枟涓庨《閮?UI

- `BigDogMod/assets/character/scenes/big_dog_visuals.tscn`
  - 瀵瑰簲锛?  - `CustomVisualPath`
- `BigDogMod/assets/vfx/card_trail_big_dog.tscn`
  - 瀵瑰簲锛?  - `CustomTrailPath`
- `BigDogMod/assets/character/map/map_marker_big_dog.png`
  - 瀵瑰簲锛?  - `CustomMapMarkerPath`
- `BigDogMod/assets/character/scenes/big_dog_icon.tscn`
  - 瀵瑰簲锛?  - `CustomIconPath`
- `BigDogMod/assets/character/top_panel/character_icon_big_dog.png`
  - 瀵瑰簲锛?  - `CustomIconTexturePath`
- `BigDogMod/assets/character/scenes/big_dog_energy_counter.tscn`
  - 瀵瑰簲锛?  - `CustomEnergyCounterPath`

### 鍟嗕汉銆佺瘽鐏€佸浜烘墜鍔?
- `BigDogMod/assets/character/merchant/big_dog_merchant.tscn`
  - 瀵瑰簲锛?  - `CustomMerchantAnimPath`
- `BigDogMod/assets/character/rest_site/big_dog_rest_site.tscn`
  - 瀵瑰簲锛?  - `CustomRestSiteAnimPath`
- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_point.png`
  - 瀵瑰簲锛?  - `CustomArmPointingTexturePath`
- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_rock.png`
  - 瀵瑰簲锛?  - `CustomArmRockTexturePath`
- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_paper.png`
  - 瀵瑰簲锛?  - `CustomArmPaperTexturePath`
- `BigDogMod/assets/character/hands/multiplayer_hand_big_dog_scissors.png`
  - 瀵瑰簲锛?  - `CustomArmScissorsTexturePath`

## 鍗″浘璺緞

杩欎簺璺緞鐜板湪宸茬粡琚崱鍥惧厹搴曢€昏緫浣跨敤銆備綘鎶婂浘鏀捐繘鍘诲悗锛屼細浼樺厛鏄剧ず鏂板浘锛涙病鍥炬椂缁х画鍥為€€鍘熺増銆?
- `BigDogMod/assets/cards/stoke_wildness.png`
- `BigDogMod/assets/cards/big_dog_howl.png`
- `BigDogMod/assets/cards/rending_bite.png`
- `BigDogMod/assets/cards/big_dog_chew.png`
- `BigDogMod/assets/cards/bleed_out.png`
- `BigDogMod/assets/cards/blood_drink.png`
- `BigDogMod/assets/cards/bloodletting_slot.png`
- `BigDogMod/assets/cards/force_awaken.png`
- `BigDogMod/assets/cards/Cuteify.png`
- `BigDogMod/assets/cards/vigilant_howl.png`

鍙€?Beta 鍗″浘锛?
- `BigDogMod/assets/cards/beta/stoke_wildness.png`
- `BigDogMod/assets/cards/beta/big_dog_howl.png`
- `BigDogMod/assets/cards/beta/rending_bite.png`
- `BigDogMod/assets/cards/beta/big_dog_chew.png`
- `BigDogMod/assets/cards/beta/bleed_out.png`
- `BigDogMod/assets/cards/beta/blood_drink.png`
- `BigDogMod/assets/cards/beta/bloodletting_slot.png`
- `BigDogMod/assets/cards/beta/force_awaken.png`
- `BigDogMod/assets/cards/beta/Cuteify.png`
- `BigDogMod/assets/cards/beta/vigilant_howl.png`

## Power 鍥炬爣璺緞

杩欎簺璺緞鐜板湪宸茬粡琚?Power 鍥炬爣鍏滃簳閫昏緫浣跨敤銆備綘鎶婂浘鏀捐繘鍘诲悗锛屼細浼樺厛鏄剧ず鏂板浘锛涙病鍥炬椂缁х画鍥為€€鍘熺増銆?
- `BigDogMod/assets/powers/wildness.png`
- `BigDogMod/assets/powers/temporary_wildness.png`
- `BigDogMod/assets/powers/bleeding.png`
- `BigDogMod/assets/powers/bleeding_boost.png`
- `BigDogMod/assets/powers/big_dog_chew_prep_power.png`

鍙€?Beta 鍥炬爣锛?
- `BigDogMod/assets/powers/beta/wildness.png`
- `BigDogMod/assets/powers/beta/temporary_wildness.png`
- `BigDogMod/assets/powers/beta/bleeding.png`
- `BigDogMod/assets/powers/beta/bleeding_boost.png`
- `BigDogMod/assets/powers/beta/big_dog_chew_prep_power.png`

## 鎺ㄨ崘鏇挎崲椤哄簭

1. 鍏堟浛鎹㈣鑹查€夋嫨澶村儚鍜岃儗鏅?2. 鍐嶆浛鎹㈡垬鏂楃珛缁樹笌椤堕儴鍥炬爣
3. 鍐嶆浛鎹㈠崱鍥惧拰 Power 鍥炬爣
4. 鏈€鍚庢浛鎹㈠晢浜恒€佺瘽鐏€佸湴鍥炬爣璁般€佸浜烘墜鍔胯繖浜涜竟缂樿祫婧?
## 浠€涔堟椂鍊欏彇娑堟敞閲?
寤鸿涓€椤逛竴椤规潵锛屼笉瑕佷竴娆″叏寮€銆?
- 瑙掕壊閫夋嫨澶村儚鍋氬ソ浜嗭細
  - 鍙栨秷 `CustomCharacterSelectIconPath`
  - 鍙栨秷 `CustomCharacterSelectLockedIconPath`
- 瑙掕壊閫夋嫨鑳屾櫙鍋氬ソ浜嗭細
  - 鍙栨秷 `CustomCharacterSelectBg`
  - 鍙栨秷 `CustomCharacterSelectTransitionPath`
- 鎴樻枟绔嬬粯鍋氬ソ浜嗭細
  - 鍙栨秷 `CustomVisualPath`
- 椤堕儴鍥炬爣鍜岃兘閲忛潰鏉垮仛濂戒簡锛?  - 鍙栨秷 `CustomIconPath`
  - 鍙栨秷 `CustomIconTexturePath`
  - 鍙栨秷 `CustomEnergyCounterPath`
- 鍏朵綑璺緞鍋氬ソ鍚庡啀鍒嗗埆鍙栨秷瀵瑰簲娉ㄩ噴
