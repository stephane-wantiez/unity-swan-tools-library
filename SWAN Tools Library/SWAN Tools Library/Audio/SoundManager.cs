using System.Collections.Generic;
using UnityEngine;

namespace swantiez.unity.tools.audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;
        public static SoundManager Instance { get { return _instance; } }

        public const bool DEFAULT_SOUND_ENABLED = true;
        public const float DEFAULT_SOUND_VOLUME = 1f;

        public AudioSource specificAudioSource;

        private bool _soundEnabled;
        private bool _soundEnabledInitialized;
        private float _soundVolume;
        private bool _soundVolumeInitialized;

        public delegate void OnSoundEnabledChange(bool enabled);
        public event OnSoundEnabledChange OnSoundEnabledChangeEvents;

        private readonly List<AudioSource> audioSources = new List<AudioSource>();

        public bool SoundEnabled
        {
            get
            {
                if (!_soundEnabledInitialized)
                {
                    _soundEnabledInitialized = true;
                    enableSources(_soundEnabled);
                }
                return _soundEnabled;
            }
            set
            {
                if (!_soundEnabledInitialized || (_soundEnabled != value))
                {
                    _soundEnabled = value;
                    _soundEnabledInitialized = true;
                    enableSources(value);
                    if (OnSoundEnabledChangeEvents != null) OnSoundEnabledChangeEvents(value);
                }
            }
        }

        public float SoundVolume
        {
            get
            {
                if (!_soundVolumeInitialized)
                {
                    _soundVolumeInitialized = true;
                    specificAudioSource.volume = _soundVolume;
                }
                return _soundVolume;
            }
            set
            {
                if (!_soundVolumeInitialized || !Mathf.Approximately(_soundVolume, value))
                {
                    _soundVolume = value;
                    _soundVolumeInitialized = true;
                    specificAudioSource.volume = value;
                }
            }
        }

        void Awake()
        {
            _instance = this;
            audioSources.Clear();
            if (specificAudioSource == null) specificAudioSource = audio;
            if (specificAudioSource == null)
            {
                Debug.LogError("Missing audio source in SourceManager! There won't be any sound!");
            }
            else
            {
                audioSources.Add(specificAudioSource);
            }
        }

        private void registerAudioSource(AudioSource audioSource)
        {
            if (!audioSources.Contains(audioSource))
            {
                audioSources.Add(audioSource);
            }

            enableSources(SoundEnabled);
        }

        private void enableSources(bool _enabled)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource != null)
                {
                    audioSource.enabled = _enabled;
                }
            }
        }

        public void switchSoundEnabled()
        {
            SoundEnabled = !SoundEnabled;
        }

        public static void stopSoundLoop(AudioSource audioSource)
        {
            if ((audioSource != null) && (audioSource.enabled))
            {
                //Debug.Log("Stopping sound loop on source " + audioSource);
                audioSource.Stop();
                audioSource.time = 0;
            }
        }

        public void stopSoundLoop()
        {
            stopSoundLoop(specificAudioSource);
        }

        private static void playSoundLoop(SoundOrMusic sound, AudioSource audioSource)
        {
            if ((sound != null) && (audioSource != null) && (audioSource.enabled))
            {
                stopSoundLoop(audioSource);
                audioSource.clip = sound.clip;
                audioSource.loop = true;
                audioSource.volume = sound.soundVolume;
                audioSource.Play();
            }
        }

        private static void playSoundFX(SoundOrMusic sound, AudioSource audioSource)
        {
            if ((sound != null) && (audioSource != null) && (audioSource.enabled))
            {
                audioSource.PlayOneShot(sound.clip, sound.soundVolume);
            }
        }

        public void playSoundOnSource(SoundOrMusic soundOrMusic, AudioSource audioSource)
        {
            if ((soundOrMusic != null) && (audioSource != null) && (audioSource.enabled))
            {
                registerAudioSource(audioSource);
                if (soundOrMusic.loop) playSoundLoop(soundOrMusic, audioSource);
                else playSoundFX(soundOrMusic, audioSource);
            }
        }

        public void playSound(SoundOrMusic soundOrMusic)
        {
            if (soundOrMusic != null)
            {
                playSoundOnSource(soundOrMusic, specificAudioSource);
            }
        }
    }
}