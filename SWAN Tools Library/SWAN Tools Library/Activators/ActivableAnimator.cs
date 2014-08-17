using UnityEngine;

namespace swantiez.unity.tools.activators
{
    public class ActivableAnimator : MonoBehaviour, IActivable
    {
        [Tooltip("Specify the animator containing the animation to launch (the animator of the current object will be taken by default if nothing specified)")]
        public Animator specificAnimator;
        [Tooltip("Specify the name of the trigger animation parameter when the object is activated, if any")]
        public string nameOfTriggerAnimVariableForActivation;
        [Tooltip("Specify the name of the trigger animation parameter when the object is deactivated, if any")]
        public string nameOfTriggerAnimVariableForDeactivation;
        [Tooltip("Specify the name of the animation flag to set to true or false when the object is activated or deactivated")]
        public string nameOfAnimFlag;

        void Awake()
        {
            if (specificAnimator == null) specificAnimator = GetComponent<Animator>();
            if (specificAnimator == null) Debug.LogError("Missing animator");
        }

        private void setAnimParameter(bool _activated)
        {
            if (nameOfAnimFlag != "") specificAnimator.SetBool(nameOfAnimFlag, _activated);
            if (_activated && (nameOfTriggerAnimVariableForActivation != "")) specificAnimator.SetTrigger(nameOfTriggerAnimVariableForActivation);
            if (!_activated && (nameOfTriggerAnimVariableForDeactivation != "")) specificAnimator.SetTrigger(nameOfTriggerAnimVariableForDeactivation);
        }

        public void link(AbstractActivator activator)
        {
            // nothing to do
        }

        public void activate(AbstractActivator activator)
        {
            setAnimParameter(true);
        }

        public void deactivate(AbstractActivator activator)
        {
            setAnimParameter(false);
        }
    }
}