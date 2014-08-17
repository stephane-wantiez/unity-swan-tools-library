using System;
using UnityEngine;

namespace swantiez.unity.tools.activators
{
    public class ActivableScript : MonoBehaviour, IActivable
    {
        [Tooltip("The 2D Joint components to enable/disable at object (de)activation")]
        public Joint2D joint2DScript;
        [Tooltip("The 3D Joint components to enable/disable at object (de)activation")]
        public Joint joint3DScript;
        [Tooltip("The Renderer components to enable/disable at object (de)activation")]
        public Renderer renderScript;
        [Tooltip("The 2D Collider components to enable/disable at object (de)activation")]
        public Collider2D collider2DScript;
        [Tooltip("The 3D Collider components to enable/disable at object (de)activation")]
        public Collider collider3DScript;

        public enum ScriptAction { None, Enable, Disable };
        [Tooltip("The action to perform on specified script/component at object activation by parent activator")]
        public ScriptAction actionOnScriptWhenActivated;
        [Tooltip("The action to perform on specified script/component at object deactivation by parent activator")]
        public ScriptAction actionOnScriptWhenDeactivated;

        private static void applyActionOnScript<T>(ScriptAction action, T script, Action<T,bool> enableScriptAction) where T : Component
        {
            if (script == null) return;
            if (action == ScriptAction.None) return;

            T[] scripts = script.GetComponents<T>();
            bool enableScript = action == ScriptAction.Enable;
            Array.ForEach(scripts, s => enableScriptAction(s, enableScript));
        }
        
        private void treatActivation(bool activated)
        {
            ScriptAction action = activated ? actionOnScriptWhenActivated : actionOnScriptWhenDeactivated;
            applyActionOnScript(action, joint2DScript, (s, v) => s.enabled = v);
            applyActionOnScript(action, joint3DScript, (s, v) => s.active = v);
            applyActionOnScript(action, renderScript, (s, v) => s.enabled = v);
            applyActionOnScript(action, collider2DScript, (s, v) => s.enabled = v);
            applyActionOnScript(action, collider3DScript, (s, v) => s.enabled = v);
        }

        public void link(AbstractActivator activator)
        {
            // nothing to do
        }

        public void activate(AbstractActivator activator)
        {
            treatActivation(true);
        }

        public void deactivate(AbstractActivator activator)
        {
            treatActivation(false);
        }
    }
}