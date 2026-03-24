using Godot;

namespace BigDogMod.Scripts.Assets;

public static class BigDogAssetPaths
{
    private const string BasePath = "res://BigDogMod/assets";

    public static string CardPortrait(string fileName) => $"{BasePath}/cards/{fileName}.png";

    public static string CardBetaPortrait(string fileName) => $"{BasePath}/cards/beta/{fileName}.png";

    public static string PowerIcon(string fileName) => $"{BasePath}/powers/{fileName}.png";

    public static string PowerBetaIcon(string fileName) => $"{BasePath}/powers/beta/{fileName}.png";

    public static string CharacterSelectIcon => $"{BasePath}/character/select/char_select_big_dog.png";

    public static string CharacterSelectLockedIcon => $"{BasePath}/character/select/char_select_big_dog_locked.png";

    public static string CharacterSelectBgScene => $"{BasePath}/character/scenes/char_select_bg_big_dog.tscn";

    public static string CharacterTransitionMaterial => $"{BasePath}/materials/big_dog_transition_mat.tres";

    public static string CharacterVisualsScene => $"{BasePath}/character/scenes/big_dog_visuals.tscn";

    public static string CharacterTopPanelIcon => $"{BasePath}/character/top_panel/character_icon_big_dog.png";

    public static string CharacterTopPanelIconOutline => $"{BasePath}/character/top_panel/character_icon_big_dog_outline.png";

    public static string CharacterIconScene => $"{BasePath}/character/scenes/big_dog_icon.tscn";

    public static string CharacterEnergyCounterScene => $"{BasePath}/character/scenes/big_dog_energy_counter.tscn";

    public static string CharacterMerchantScene => $"{BasePath}/character/merchant/big_dog_merchant.tscn";

    public static string CharacterRestSiteScene => $"{BasePath}/character/rest_site/big_dog_rest_site.tscn";

    public static string CharacterMapMarker => $"{BasePath}/character/map/map_marker_big_dog.png";

    public static string CharacterTrailScene => $"{BasePath}/vfx/card_trail_big_dog.tscn";

    public static string HandPoint => $"{BasePath}/character/hands/multiplayer_hand_big_dog_point.png";

    public static string HandRock => $"{BasePath}/character/hands/multiplayer_hand_big_dog_rock.png";

    public static string HandPaper => $"{BasePath}/character/hands/multiplayer_hand_big_dog_paper.png";

    public static string HandScissors => $"{BasePath}/character/hands/multiplayer_hand_big_dog_scissors.png";

    public static bool Exists(string path) => ResourceLoader.Exists(path);
}
