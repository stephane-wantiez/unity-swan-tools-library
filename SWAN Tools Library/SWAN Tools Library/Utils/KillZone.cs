using System;
using System.Linq;
using UnityEngine;

namespace swantiez.unity.tools.utils
{
    public class KillZone : MonoBehaviour
    {
        public bool onlyWithActiveRenderer;
        public string[] restringToTags;

        private bool zoneColliderEnabled;

        void Start()
        {
            enableZoneCollider(true);
        }

        private void enableZoneCollider(bool enable)
        {
            zoneColliderEnabled = enable;
            if (collider != null) collider.enabled = enable;
            if (collider2D != null) collider2D.enabled = enable;
        }

        private bool isValid(GameObject otherObj)
        {
            if (onlyWithActiveRenderer && !renderer.enabled) return false;
            if ((restringToTags == null) || (restringToTags.Length == 0)) return true;
            return Array.Exists(restringToTags, otherObj.CompareTag);
        }

        private void killObject(GameObject otherObj)
        {
            MonoBehaviour[] otherScripts = otherObj.GetComponents<MonoBehaviour>();
            foreach (IKillable killable in otherScripts.OfType<IKillable>())
            {
                killable.kill();
                return;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (isValid(other.gameObject))
            {
                killObject(other.gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (isValid(other.gameObject))
            {
                killObject(other.gameObject);
            }
        }

        void Update()
        {
            bool killZoneEnabled = !onlyWithActiveRenderer || renderer.enabled;

            if (!zoneColliderEnabled && killZoneEnabled)
            {
                enableZoneCollider(true);
            }
            else if (zoneColliderEnabled && !killZoneEnabled)
            {
                enableZoneCollider(false);
            }
        }
    }
}
