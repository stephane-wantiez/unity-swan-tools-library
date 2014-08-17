using System.Collections.Generic;
using swantiez.unity.tools.utils;
using UnityEngine;

namespace swantiez.unity.tools.activators
{
    [RequireComponent(typeof(KillableActivable))]
    public class KillableHealthObserver : AbstractActivator
    {
        public float maxHealth;
        public Action actionWhenValueReached;
        public GameObject specificKillable;

        private IKillable killable;
        private bool actionApplied;

        void Start()
        {
            init();
            if (specificKillable == null) specificKillable = gameObject;
            killable = getKillable(specificKillable);
            if (killable == null)
            {
                Debug.LogError("No killable object found");
            }
            else
            {
                killable.OnDamageEvents += onDamage;
                onDamage(0f);
            }
        }

        private IKillable getKillable(GameObject obj)
        {
            List<IKillable> killables = MiscUtils.GetComponentsInObjectOfType<IKillable>(obj);
            return killables.IsEmpty() ? null : killables[0];
        }

        private void onDamage(float damageValue)
        {
            if (!actionApplied && (killable.getHealth().IsPreciselySmallerOrEqualTo(maxHealth)))
            {
                actionApplied = true;
                applyActionOnObjects(actionWhenValueReached);
            }
        }
    }
}
