using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BigDogMod.Scripts.Assets;
using BigDogMod.Scripts.Config;
using Godot;
using MegaCrit.Sts2.Core.Entities.Players;

namespace BigDogMod.Scripts.Audio;

public static class BigDogSfxHelper
{
    private const float BasePlaybackRate = 1f;
    private const float MaxPlaybackRate = 6f;
    private const float HowlPlaybackRateIncrease = 0.25f;

    private sealed class AudioState
    {
        public float PlaybackRate { get; set; } = BasePlaybackRate;
    }

    private static readonly ConditionalWeakTable<Player, AudioState> _audioStates = new();
    private static readonly Dictionary<string, AudioStream?> _streamCache = new(StringComparer.OrdinalIgnoreCase);

    public static void PlayHowl(Player player)
    {
        if (BigDogModConfig.DisableVoiceLines)
        {
            return;
        }

        AudioState state = _audioStates.GetOrCreateValue(player);
        state.PlaybackRate = Mathf.Clamp(state.PlaybackRate + HowlPlaybackRateIncrease, BasePlaybackRate, MaxPlaybackRate);
        Play(BigDogAssetPaths.BigDogHowlSfx, state.PlaybackRate);
    }

    public static void PlayChew(Player player)
    {
        if (BigDogModConfig.DisableVoiceLines)
        {
            return;
        }

        AudioState state = _audioStates.GetOrCreateValue(player);
        Play(BigDogAssetPaths.BigDogChewSfx, state.PlaybackRate);
        state.PlaybackRate = BasePlaybackRate;
    }

    private static void Play(string path, float playbackRate)
    {
        AudioStream? stream = LoadStream(path);
        if (stream == null)
        {
            return;
        }

        if (Engine.GetMainLoop() is not SceneTree sceneTree || sceneTree.Root == null)
        {
            return;
        }

        AudioStreamPlayer audioPlayer = new AudioStreamPlayer
        {
            Stream = stream,
            PitchScale = playbackRate
        };
        audioPlayer.Finished += audioPlayer.QueueFree;
        sceneTree.Root.AddChild(audioPlayer);
        audioPlayer.Play();
    }

    private static AudioStream? LoadStream(string preferredPath)
    {
        string? resolvedPath = ResolveAudioPath(preferredPath);
        if (resolvedPath == null)
        {
            return null;
        }

        if (_streamCache.TryGetValue(resolvedPath, out AudioStream? cachedStream))
        {
            return cachedStream;
        }

        AudioStream? loadedStream = ResourceLoader.Load<AudioStream>(resolvedPath);
        _streamCache[resolvedPath] = loadedStream;
        return loadedStream;
    }

    private static string? ResolveAudioPath(string preferredPath)
    {
        if (ResourceLoader.Exists(preferredPath))
        {
            return preferredPath;
        }

        string mp3Path = preferredPath.Replace(".mp3", ".ogg", StringComparison.OrdinalIgnoreCase);
        if (ResourceLoader.Exists(mp3Path))
        {
            return mp3Path;
        }

        return null;
    }
}
