using System;
using System.Collections.Generic;
using swantiez.unity.tools.utils;
using UnityEngine;

namespace swantiez.unity.tools.activators
{
    public class CollisionActivator : AbstractActivator
    {
        public Action collisionEnterAction;
        public Action collisionExitAction;
        public bool applyAlsoOnCollidingObject;
        public string[] restrictToCollidersWithTags;
        public bool restrictToCollidersWithRigidBody;
        public float minimumCollisionForce;

        private bool checkCollisionForces;
        private float minimumCollisionForceSqr;
        private readonly List<GameObject> collidingObjects = new List<GameObject>();

        void Awake()
        {
            init();
            collidingObjects.Clear();
            checkCollisionForces = !Mathf.Approximately(minimumCollisionForce, 0f);
            minimumCollisionForceSqr = minimumCollisionForce * minimumCollisionForce;
        }

        public List<GameObject> getCollidingObjects()
        {
            return collidingObjects;
        }

        private bool isColliderValid(GameObject colliderToCheck, Component colliderRigidBody, float collisionForceSqr)
        {
            bool colliderIsValid = true;
            bool colliderHasRigidBody = colliderRigidBody != null;

            if (CollectionUtils.IsCollFilled(restrictToCollidersWithTags))
            {
                colliderIsValid = Array.Exists(restrictToCollidersWithTags, colliderToCheck.CompareTag);
            }

            if (restrictToCollidersWithRigidBody)
            {
                colliderIsValid = colliderIsValid && colliderHasRigidBody;
            }

            if (checkCollisionForces)
            {
                colliderIsValid = colliderIsValid && collisionForceSqr.IsPreciselyGreaterOrEqualTo(minimumCollisionForceSqr);
            }

            //Debug.Log("CollisionActivator " + gameObject.name + " - Force² = " + collisionForceSqr + " - min collision force² = " + minimumCollisionForceSqr);
            //Debug.Log("CollisionActivator " + gameObject.name + " - Has rigidbody2d? " + colliderHasRigidBody + " - restricted to colliders with tags: " + restrictToCollidersWithTags.GetAsString());
            //Debug.Log("CollisionActivator " + gameObject.name + " -> Collision valid? " + colliderIsValid);

            return colliderIsValid;
        }

        private void checkCollision(GameObject collidingObject, Component colliderRigidBody, bool enterCollision, float collisionForceSqr)
        {
            //Debug.Log("CollisionActivator " + gameObject.name + " - Collision " + (enterCollision ? "started" : "ended") + " with " + collidingObject + " - force² = " + collisionForceSqr);

            if (enterCollision && isColliderValid(collidingObject, colliderRigidBody, collisionForceSqr))
            {
                collidingObjects.Add(collidingObject);
            }
            else if (!enterCollision && collidingObjects.Contains(collidingObject))
            {
                collidingObjects.Remove(collidingObject);
            }
            else
            {
                return;
            }

            Action action = enterCollision ? collisionEnterAction : collisionExitAction;
            //Debug.Log("CollisionActivator " + gameObject.name + " - Applying action " + action + " on objects: " + activableObjects.GetAsString());
            applyActionOnObjects(action);

            if (applyAlsoOnCollidingObject)
            {
                //Debug.Log("CollisionActivator " + gameObject.name + " - Applying also on colliding object " + collidingObject);
                applyActionOnObjects(getActivablesFromObject(collidingObject), action);
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            float collisionImpulsionForceSqr = checkCollisionForces ? MiscUtils.GetCollisionImpulseForce(rigidbody2D, other).sqrMagnitude : 0f;
            checkCollision(other.gameObject, other.rigidbody, true, collisionImpulsionForceSqr);
        }

        void OnCollisionEnter(Collision other)
        {
            float collisionImpulsionForceSqr = checkCollisionForces ? MiscUtils.GetCollisionImpulseForce(rigidbody, other).sqrMagnitude : 0f;
            checkCollision(other.gameObject, other.rigidbody, true, collisionImpulsionForceSqr);
        }

        void OnCollisionExit2D(Collision2D other)
        {
            checkCollision(other.gameObject, other.rigidbody, false, 0f);
        }

        void OnCollisionExit(Collision other)
        {
            checkCollision(other.gameObject, other.rigidbody, false, 0f);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            checkCollision(other.gameObject, other.rigidbody, true, 0f);
        }

        void OnTriggerEnter(Collider other)
        {
            checkCollision(other.gameObject, other.rigidbody, true, 0f);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            checkCollision(other.gameObject, other.rigidbody, false, 0f);
        }

        void OnTriggerExit(Collider other)
        {
            checkCollision(other.gameObject, other.rigidbody, false, 0f);
        }

        void Update()
        {
            collidingObjects.RemoveAll(c => c == null);
        }
    }
}