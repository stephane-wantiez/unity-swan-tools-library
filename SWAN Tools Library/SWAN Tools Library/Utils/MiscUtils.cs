using System.Linq;
using swantiez.unity.tools.camera;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace swantiez.unity.tools.utils
{
    public static class MiscUtils
    {
        public static List<T> Get2DObjectsOfTypeInArea<T>(Vector3 origin, float radius) where T : Component
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(origin, radius);
            List<T> objectsToReturn = new List<T>();

            foreach (Collider2D collider in colliders)
            {
                T colliderComponent = collider.GetComponent<T>();

                if ((colliderComponent != null) && !objectsToReturn.Contains(colliderComponent))
                {
                    objectsToReturn.Add(colliderComponent);
                }
            }

            return objectsToReturn;
        }

        public static bool IsPointOverObject(Vector2 point, GameObject objectToCheck)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(point, Vector2.zero);
            //Debug.Log("Clicked at point " + point + ", touched " + hits.Length + " colliders");
            return Array.Exists(hits, h => h.collider.gameObject == objectToCheck);
        }

        public static bool IsMouseOverObject(GameObject objectToCheck)
        {
            Vector3 mousePointInWorld;
            CameraUtils.GetWorldPointFromScreenAtPosZ(Input.mousePosition, objectToCheck.transform.position.z, out mousePointInWorld);
            return IsPointOverObject(mousePointInWorld, objectToCheck);
        }

        public static Vector2 GetCollisionImpulseForce(Rigidbody2D body, Collision2D collision)
        {
            float colliderMass = (collision.rigidbody != null) && !collision.rigidbody.isKinematic ? collision.rigidbody.mass : 1000f;
            Vector2 collisionVelocityDelta = colliderMass * collision.relativeVelocity / (body.mass + colliderMass);
            Vector2 collisionImpulse = collisionVelocityDelta * body.mass;
            return collisionImpulse;
        }

        public static Vector2 GetCollisionImpulseForce(Rigidbody body, Collision collision)
        {
            float colliderMass = (collision.rigidbody != null) && !collision.rigidbody.isKinematic ? collision.rigidbody.mass : 1000f;
            Vector2 collisionVelocityDelta = colliderMass * collision.relativeVelocity / (body.mass + colliderMass);
            Vector2 collisionImpulse = collisionVelocityDelta * body.mass;
            return collisionImpulse;
        }

        public static void EnableObjectColliders(GameObject obj, bool enabled, bool includeChildren)
        {
            foreach (Collider2D coll in obj.GetComponents<Collider2D>())
            {
                coll.enabled = enabled;
            }

            if (includeChildren)
            {
                foreach (Collider2D coll in obj.GetComponentsInChildren<Collider2D>())
                {
                    coll.enabled = enabled;
                }
            }
        }

        public static GameObject InstantiateObject(GameObject objPrefab, Transform objParent, Vector3 objPosition, Quaternion objRotation)
        {
            if (objPrefab == null) return null;
            GameObject obj = UnityEngine.Object.Instantiate(objPrefab, objPosition, objRotation) as GameObject;
            if (objParent != null) obj.transform.parent = objParent;
            return obj;
        }

        public static T InstantiateObject<T>(T objPrefab, Transform objParent, Vector3 objPosition, Quaternion objRotation) where T : MonoBehaviour
        {
            GameObject obj = InstantiateObject(objPrefab.gameObject, objParent, objPosition, objRotation);
            if (obj == null) return null;
            return obj.GetComponent<T>();
        }

        public static Vector3 GetMeanPositionFrom(IEnumerable<Vector3> pos)
        {
            if (pos == null) throw new ArgumentNullException("pos");
            return pos.Aggregate((p1, p2) => p1 + p2)/pos.Count();
        }

        public static Vector3 GetMeanPositionFrom(List<Transform> objects)
        {
            return GetMeanPositionFrom(objects.Select(o => o.position));
        }

        public static Vector3 GetMeanPositionFrom<T>(List<T> objects) where T : MonoBehaviour
        {
            return GetMeanPositionFrom(objects.Select(o => o.transform.position));
        }

        private static Vector3 getPositionForProvidedMethod(IEnumerable<Vector3> pos, Func<float, float, float> method)
        {
            if (pos == null) throw new ArgumentNullException("pos");
            if (!pos.Any()) throw new ArgumentException("pos is empty");
            Vector3 validPos = Vector3.zero;
            bool firstElemChecked = false;
            foreach (Vector3 currentPos in pos)
            {
                if (firstElemChecked)
                {
                    validPos.x = method(validPos.x, currentPos.x);
                    validPos.y = method(validPos.y, currentPos.y);
                    validPos.z = method(validPos.z, currentPos.z);
                }
                else
                {
                    validPos = currentPos;
                    firstElemChecked = true;
                }
            }
            return validPos;
        }

        public static Vector3 GetMinPositionFrom(IEnumerable<Vector3> pos)
        {
            return getPositionForProvidedMethod(pos, Mathf.Min);
        }

        public static Vector3 GetMinPositionFrom(List<Transform> objects)
        {
            return GetMeanPositionFrom(objects.Select(o => o.position));
        }

        public static Vector3 GetMinPositionFrom<T>(List<T> objects) where T : MonoBehaviour
        {
            return GetMeanPositionFrom(objects.Select(o => o.transform.position));
        }

        public static Vector3 GetMaxPositionFrom(IEnumerable<Vector3> pos)
        {
            return getPositionForProvidedMethod(pos, Mathf.Max);
        }

        public static Vector3 GetMaxPositionFrom(List<Transform> objects)
        {
            return GetMeanPositionFrom(objects.Select(o => o.position));
        }

        public static Vector3 GetMaxPositionFrom<T>(List<T> objects) where T : MonoBehaviour
        {
            return GetMeanPositionFrom(objects.Select(o => o.transform.position));
        }

        public static List<T> GetComponentsInObjectOfType<T>(GameObject obj) where T : class
        {
            MonoBehaviour[] objScripts = obj.GetComponents<MonoBehaviour>();
            if (objScripts.IsEmpty()) return null;
            List<T> objTScripts = new List<T>();
            Array.ForEach(objScripts, s =>
            {
                T ss = s as T;
                if (ss != null) objTScripts.Add(ss);
            });
            return objTScripts;
        }
    }
}