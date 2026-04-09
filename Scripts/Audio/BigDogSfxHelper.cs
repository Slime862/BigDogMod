using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        public int SequenceVersion { get; set; }
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
        Play(BigDogAssetPaths.BigDogHowlSfx, state.PlaybackRate);
        state.PlaybackRate = Mathf.Clamp(state.PlaybackRate + HowlPlaybackRateIncrease, BasePlaybackRate, MaxPlaybackRate);
    }

    public static void PlayChew(Player player)
    {
        if (BigDogModConfig.DisableVoiceLines)
        {
            return;
        }

        AudioState state = _audioStates.GetOrCreateValue(player);
        float targetPlaybackRate = state.PlaybackRate;
        state.SequenceVersion++;
        PlayChewSequence(targetPlaybackRate, state.SequenceVersion);
        state.PlaybackRate = BasePlaybackRate;
    }

    private static async void PlayChewSequence(float targetPlaybackRate, int sequenceVersion)
    {
        AudioStream? stream = LoadStream(BigDogAssetPaths.BigDogChewSfx);
        if (stream == null)
        {
            return;
        }

        if (Engine.GetMainLoop() is not SceneTree sceneTree || sceneTree.Root == null)
        {
            return;
        }

        float playbackRate = BasePlaybackRate;
        while (playbackRate <= targetPlaybackRate + 0.001f)
        {
            await PlaySequenceStep(sceneTree, stream, playbackRate);
            playbackRate = Mathf.Clamp(playbackRate + HowlPlaybackRateIncrease, BasePlaybackRate, MaxPlaybackRate + HowlPlaybackRateIncrease);
        }
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

    private static Task PlaySequenceStep(SceneTree sceneTree, AudioStream stream, float playbackRate)
    {
        TaskCompletionSource<bool> completion = new();
        AudioStreamPlayer audioPlayer = new AudioStreamPlayer
        {
            Stream = stream,
            PitchScale = playbackRate
        };
        audioPlayer.Finished += () =>
        {
            audioPlayer.QueueFree();
            completion.TrySetResult(true);
        };
        sceneTree.Root.AddChild(audioPlayer);
        audioPlayer.Play();
        return completion.Task;
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
