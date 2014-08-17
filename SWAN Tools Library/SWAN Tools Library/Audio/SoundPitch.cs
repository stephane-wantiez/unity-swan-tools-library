using swantiez.unity.tools.utils;
using System;
using UnityEngine;

namespace swantiez.unity.tools.audio
{
    [Serializable]
    public class SoundPitch
    {
        public float basePitch = 1f;
        public bool increasePitchAtSuccessivePlays;
        public float pitchIncreaseAfterEachPlay;
        public float timeBeforePitchResetInSec;
        public float maxPitchValueBeforeReset;

        private DateTime pitchIncreasePeriodEndDT;
        [HideInInspector]
        public float pitch;

        public void updatePitchAtPlay(AudioSource source)
        {
            if (!increasePitchAtSuccessivePlays) return;

            if (pitchIncreasePeriodEndDT < DateTime.Now)
            {
                pitch = basePitch;
            }
            else if (pitch.IsPreciselyGreaterOrEqualTo(maxPitchValueBeforeReset))
            {
                pitch = maxPitchValueBeforeReset;
            }
            else
            {
                pitch += pitchIncreaseAfterEachPlay;
            }

            pitchIncreasePeriodEndDT = DateTime.Now.AddSeconds(timeBeforePitchResetInSec);
            source.pitch = pitch;
        }
    }
}