using System;
using swantiez.unity.tools.utils;
using UnityEngine;

namespace swantiez.unity.tools.activators
{
    public class KillActivable : MonoBehaviour, IActivable
    {
        public enum ActionType { Activation, Deactivation }

        public IKillable[] killables;
        public ActionType killLaunchedCharactersAt;

        public void link(AbstractActivator activator)
        {
            // nothing to do
        }

        private void kill()
        {
            Array.ForEach(killables, k => { if ((k != null) && k.isAlive()) k.kill(); });
        }

        public void activate(AbstractActivator activator)
        {
            if (killLaunchedCharactersAt == ActionType.Activation)
            {
                kill();
            }
        }

        public void deactivate(AbstractActivator activator)
        {
            if (killLaunchedCharactersAt == ActionType.Deactivation)
            {
                kill();
            }
        }		
    }
}