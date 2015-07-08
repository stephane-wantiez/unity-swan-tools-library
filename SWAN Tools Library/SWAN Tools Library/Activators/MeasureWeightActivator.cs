using System.Linq;
using swantiez.unity.tools.utils;
using UnityEngine;

namespace swantiez.unity.tools.activators
{
    [RequireComponent(typeof(CollisionActivator))]
    public class MeasureWeightActivator : AbstractActivator, IActivable
    {
        public float minWeightForActivation;
        public bool deactivateWhenUnderMinWeight;

        private float currentMeasuredWeight;
        private bool currentIsActivated;

        void Awake()
        {
            init();
            currentIsActivated = false;
            currentMeasuredWeight = 0f;
        }

        private void setCurrentMeasuredWeight(float value)
        {
            if (Mathf.Approximately(currentMeasuredWeight, value)) return;
            currentMeasuredWeight = value;

            if (FloatUtils.IsFirstFloatPreciselyGreaterOrEqualToSecond(currentMeasuredWeight, minWeightForActivation))
            {
                if (currentIsActivated) return;
                currentIsActivated = true;
                activateObjects();
            }
            else
            {
                if (!currentIsActivated) return;
                currentIsActivated = false;
                if (deactivateWhenUnderMinWeight) deactivateObjects();
            }
        }

        private float getEffectiveWeightFromBody(Rigidbody body3D, Rigidbody2D body2D)
        {
            if (!body2D && !body3D) return 0f;
            Vector3 velocity = body2D ? (Vector3) body2D.velocity : body3D.velocity;
            float mass = body2D ? body2D.mass : body3D.mass;
            bool colliderIsNull = body2D ? !body2D.GetComponent<Collider2D>() : !body3D.GetComponent<Collider>();
            bool colliderIsTrigger = !colliderIsNull && (body2D ? body2D.GetComponent<Collider2D>().isTrigger : body3D.GetComponent<Collider>().isTrigger);
            if (FloatUtils.IsFirstFloatPreciselySmallerOrEqualToSecond(Mathf.Abs(velocity.y), 0.1f) && !colliderIsNull && !colliderIsTrigger)
            {
                return mass;
            }
            return 0;
        }

        private float getEffectiveWeightOfMeasuredBodies(CollisionActivator collisionActivator)
        {
            return collisionActivator.getCollidingObjects()
                .Where(collidingObject => (collidingObject.GetComponent<Rigidbody>() != null) || (collidingObject.GetComponent<Rigidbody2D>() != null))
                .Sum(collidingObject => getEffectiveWeightFromBody(collidingObject.GetComponent<Rigidbody>(), collidingObject.GetComponent<Rigidbody2D>()));
        }

        private void checkWeight(AbstractActivator activator)
        {
            CollisionActivator collisionActivator = activator as CollisionActivator;
            if (collisionActivator != null)
            {
                setCurrentMeasuredWeight(getEffectiveWeightOfMeasuredBodies(collisionActivator));
            }
        }

        public void link(AbstractActivator activator)
        {
            // nothing to do
        }

        public void activate(AbstractActivator activator)
        {
            checkWeight(activator);
        }

        public void deactivate(AbstractActivator activator)
        {
            checkWeight(activator);
        }
    }
}