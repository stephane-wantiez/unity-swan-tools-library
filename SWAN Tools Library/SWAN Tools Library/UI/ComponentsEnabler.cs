using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace swantiez.unity.tools.ui
{
    public class ComponentsEnabler : MonoBehaviour
    {
        public Component[] componentsToDisable;
        public Component[] componentsToEnable;

        public void OnEnableComponents()
        {
            foreach(Component componentToDisable in componentsToDisable)
            {
                componentToDisable.gameObject.SetActive(false);
            }
            foreach (Component componentToEnable in componentsToEnable)
            {
                componentToEnable.gameObject.SetActive(true);
            }
        }
    }
}
