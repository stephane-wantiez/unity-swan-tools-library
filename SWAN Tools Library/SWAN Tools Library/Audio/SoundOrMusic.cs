using System;
using UnityEngine;

namespace swantiez.unity.tools.audio
{
    [Serializable]
    public class SoundOrMusic
    {
        public AudioClip clip;
        public AudioSource specificSource;
        public bool loop = false;
        public float soundVolume = 1f;
        public SoundPitch pitch;
        public bool disabled;

        private AudioSource playSource;

        public void play()
        {
            if (specificSource != null)
            {
                playOnSource(specificSource);
            }
            else if (!disabled && (clip != null))
            {
                //Debug.Log("Sound/Music element " + clip.name + " - PLAY");
                SoundManager.Instance.playSound(this);
                playSource = null;
            }
        }

        public void playOnSource(AudioSource audioSource)
        {
            if (!disabled && (clip != null))
            {
                //Debug.Log("Sound/Music element " + clip.name + " - PLAY ON SOURCE " + audioSource.name);
                pitch.updatePitchAtPlay(audioSource);
                SoundManager.Instance.playSoundOnSource(this, audioSource);
                playSource = audioSource;
            }
        }

        public void stop()
        {
            if (loop && !disabled && (clip != null))
            {
                if (playSource != null)
                {
                    playSource.Stop();
                }
                else if (specificSource != null)
                {
                    specificSource.Stop();
                }
                else
                {
                    SoundManager.Instance.stopSoundLoop();
                }
            }
        }
    }
}