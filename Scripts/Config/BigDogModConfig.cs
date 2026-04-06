using BaseLib.Config;

namespace BigDogMod.Scripts.Config;

internal sealed class BigDogModConfig : SimpleModConfig
{
    [ConfigSection("AudioSettings")]
    public static bool DisableVoiceLines { get; set; } = false;
}
