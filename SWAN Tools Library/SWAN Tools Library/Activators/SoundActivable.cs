using swantiez.unity.tools.audio;
using UnityEngine;

namespace swantiez.unity.tools.activators
{
    public class SoundActivable : MonoBehaviour, IActivable
    {
        public SoundOrMusic soundForActivation;
        public SoundOrMusic soundForDeactivation;

        public void link(AbstractActivator activator)
        {
            // nothing to do
        }

        public void activate(AbstractActivator activator)
        {
            soundForActivation.play();
        }

        public void deactivate(AbstractActivator activator)
        {
            soundForDeactivation.play();
        }		
    }
}