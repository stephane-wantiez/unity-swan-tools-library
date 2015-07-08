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
            if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = enable;
            if (GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = enable;
        }

        private bool isValid(GameObject otherObj)
        {
            if (onlyWithActiveRenderer && !GetComponent<Renderer>().enabled) return false;
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
            bool killZoneEnabled = !onlyWithActiveRenderer || GetComponent<Renderer>().enabled;

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
