using swantiez.unity.tools.utils;
using UnityEngine;

namespace swantiez.unity.tools.activators
{
    public class KillableActivable : AbstractActivator, IActivable, IKillable
    {
        public float health;
        public enum ActionType { None, Damage, Kill }
        public ActionType actionAtActivation;
        public ActionType actionAtDeactivation;
        public float activationDamageValue;
        public float deactivationDamageValue;
        public Action actionWhenDead;

        public event DelegateUtils.OnFloatEvent OnDamageEvents;
        public event DelegateUtils.OnSimpleEvent OnKillEvents;

        void Start()
        {
            init();
        }

        public void link(AbstractActivator activator)
        {
            // nothing to do
        }

        public float getHealth()
        {
            return health;
        }

        public bool isAlive()
        {
            return health.IsPreciselyNotZero();
        }

        public bool isDead()
        {
            return health.IsPreciselyZero();
        }

        public void damage(float damageValue)
        {
            float realDamageValue = Mathf.Min(damageValue, health);
            health -= realDamageValue;

            if (isDead())
            {
                kill();
            }
            else
            {
                if (OnDamageEvents != null) OnDamageEvents(realDamageValue);
            }
        }

        public void kill()
        {
            health = 0f;
            if (OnKillEvents != null) OnKillEvents();
            applyActionOnObjects(actionWhenDead);
        }

        private void applyAction(ActionType actionType, float damageValue)
        {
            switch (actionType)
            {
                case ActionType.Damage:
                    damage(damageValue);
                    break;
                case ActionType.Kill:
                    kill();
                    break;
            }
        }

        public void activate(AbstractActivator activator)
        {
            applyAction(actionAtActivation, activationDamageValue);
        }

        public void deactivate(AbstractActivator activator)
        {
            applyAction(actionAtDeactivation, deactivationDamageValue);
        }
    }
}