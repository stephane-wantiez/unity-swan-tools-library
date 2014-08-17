using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace swantiez.unity.tools.activators
{
    public abstract class AbstractActivator : MonoBehaviour
    {
        public enum Action { None, Activate, Deactivate }

        [Tooltip("The objects containing all the Activable components to (de)activate")]
        public MonoBehaviour[] activableObjects;
        [Tooltip("Activate or deactivate the linked objects at startup?")]
        public Action actionAtStartup;

        private readonly List<IActivable> allActivableItems = new List<IActivable>();

        public void init()
        {
            foreach (MonoBehaviour activableObject in activableObjects)
            {
                allActivableItems.AddRange(getActivablesFromObject(activableObject.gameObject));
            }

            allActivableItems.ForEach(a => a.link(this));

            applyActionOnObjects(actionAtStartup);
        }

        public List<IActivable> getActivablesFromObject(GameObject obj)
        {
            MonoBehaviour[] scriptsInActivableObject = obj.GetComponents<MonoBehaviour>();
            return scriptsInActivableObject.OfType<IActivable>().ToList();
        }

        public void activateObjects(List<IActivable> _activableObjects)
        {
            _activableObjects.ForEach(a => a.activate(this));
        }

        public void activateObjects()
        {
            activateObjects(allActivableItems);
        }

        public void deactivateObjects(IList<IActivable> _activableObjects)
        {
            foreach (IActivable activableObject in _activableObjects)
            {
                activableObject.deactivate(this);
            }
        }

        public void deactivateObjects()
        {
            deactivateObjects(allActivableItems);
        }

        public void applyActionOnObjects(IList<IActivable> _activableObjects, Action _action)
        {
            foreach (IActivable activableObject in _activableObjects)
            {
                switch (_action)
                {
                    case Action.Activate:
                        activableObject.activate(this);
                        break;
                    case Action.Deactivate:
                        activableObject.deactivate(this);
                        break;
                }
            }
        }

        public void applyActionOnObjects(Action _action)
        {
            applyActionOnObjects(allActivableItems, _action);
        }
    }
}