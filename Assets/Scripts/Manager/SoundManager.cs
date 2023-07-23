using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBehaviorDDOL<SoundManager>
{
    private readonly List<AudioSource> _audioSources = new List<AudioSource>();
    private readonly HashSet<int> _usingIndexes = new HashSet<int>();

    public float masterVolume = 0.6f;

    private void Awake()
    {
        _audioSources.Add(gameObject.AddComponent<AudioSource>());
    }

    /// <summary>
    /// 원하는 효과음을 재생
    /// </summary>
    /// <param name="soundName">재생할 사운드의 이름 (SoundManager.Sound Enum 참고)</param>
    /// <param name="volume">0~1 사이의 float value</param>
    public void PlayEffectSound(Sounds soundName, float volume = -1)
    {
        var soundSettings = GetSoundSettings(soundName);
        if (volume > 0)
        {
            soundSettings.volume = volume;
        }

        int emptyAudioIndex = -1;
        for (int i = 0; i < _audioSources.Count; ++i)
        {
            if (!_usingIndexes.Contains(i) && !_audioSources[i].isPlaying)
            {
                emptyAudioIndex = i;
                _usingIndexes.Add(emptyAudioIndex);
                break;
            }
        }
        // 만일 모든 AudioSource가 사용중일때
        if (emptyAudioIndex < 0)
        {
            _audioSources.Add(gameObject.AddComponent<AudioSource>());
            emptyAudioIndex = _audioSources.Count - 1;
        }

        var audioSourceToUse = _audioSources[emptyAudioIndex];

        StartCoroutine(LoadSoundAsync(soundSettings.name, (a) =>
        {
            audioSourceToUse.clip = a;
            audioSourceToUse.volume = soundSettings.volume * masterVolume;
            audioSourceToUse.Play();
            _usingIndexes.Remove(emptyAudioIndex);
        }));
    }

    private IEnumerator LoadSoundAsync(string address, Action<AudioClip> callback)
    {
        var op = Resources.LoadAsync<AudioClip>("Sound/" + address);

        yield return op;

        if (!op.isDone)
        {
            callback(null);
            yield break;
        }

        callback(op.asset as AudioClip);
    }

    #region Link Sounds with Enums
    public enum Sounds
    {
        KnowHowShow,
        KnowHowSelect,
        Swing,
        Smash,
        ThrowStone,
        HitStone,
        Jump,
        AccidentShow,
        RoundOver,
        GameOver
    }

    private class SoundSettings
    {
        public string name;
        private float _volume;
        public float volume
        {
            get { return _volume; }
            set { _volume = Mathf.Clamp01(value); }
        }

        public SoundSettings(string name, float volume)
        {
            this.name = name;
            this.volume = volume;
        }
        public SoundSettings(string name)
        {
            this.name = name;
            this.volume = 1;
        }
    }

    private SoundSettings GetSoundSettings(Sounds soundEnum) =>
        soundEnum switch
        {
            Sounds.KnowHowShow => new SoundSettings("KnowHowShow"),
            Sounds.KnowHowSelect => new SoundSettings("KnowHowShow"),
            Sounds.Swing => new SoundSettings("KnowHowShow"),
            Sounds.Smash => new SoundSettings("KnowHowShow"),
            Sounds.ThrowStone => new SoundSettings("KnowHowShow"),
            Sounds.HitStone => new SoundSettings("KnowHowShow"),
            Sounds.Jump => new SoundSettings("KnowHowShow"),
            Sounds.AccidentShow => new SoundSettings("KnowHowShow"),
            Sounds.RoundOver => new SoundSettings("KnowHowShow"),
            Sounds.GameOver => new SoundSettings("KnowHowShow"),
            _ => throw new ArgumentOutOfRangeException()
        };
    #endregion
}