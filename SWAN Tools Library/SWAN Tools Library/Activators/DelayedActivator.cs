using UnityEngine;

namespace swantiez.unity.tools.activators
{
    /// <summary>
    /// Allow to delay the activation of some activable objects when activated by a parent Activator. Delayed activation can be canceled in case of deactivation by this latest.
    /// </summary>
    public class DelayedActivator : AbstractActivator, IActivable
    {
        [Tooltip("Should the delayed activation happen at the start of the level")]
        public bool launchCountdownAtStartup;
        [Tooltip("Time to wait (in sec) before firing activation of linked objects")]
        public float timeBeforeActivationInSec;
        [Tooltip("Should the delayed activation happen even if the parent activator has deactivated this component?")]
        public bool noDelayedActivationWhenDeactivated;

        void Start()
        {
            init();

            if (launchCountdownAtStartup)
            {
                Invoke("delayedActivation", timeBeforeActivationInSec);
            }
        }

        public void link(AbstractActivator activator)
        {
            // nothing to do
        }

        public void activate(AbstractActivator activator)
        {
            Invoke("delayedActivation", timeBeforeActivationInSec);
        }

        public void deactivate(AbstractActivator activator)
        {
            if (noDelayedActivationWhenDeactivated) CancelInvoke("delayedActivation");
        }

        private void delayedActivation()
        {
            activateObjects();
        }
    }
}