using UnityEngine;
using System.Collections;

namespace swantiez.unity.tools.utils
{
    public class TimedDestruction : MonoBehaviour
    {
        public float timeBeforeDestroyInSec;
        public bool onlyDeactivate;

        void Start()
        {
            Invoke("doDestroy", timeBeforeDestroyInSec);
        }

        private void doDestroy()
        {
            if (onlyDeactivate)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}