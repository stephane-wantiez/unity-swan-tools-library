using swantiez.unity.tools.utils;
using System;
using System.Collections;
using UnityEngine;

namespace swantiez.unity.tools.activators
{
    public class ObjectsActivable : MonoBehaviour, IActivable
    {
        public GameObject[] objectsToEnableAtActivation;
        public GameObject[] objectsToEnableAtDeactivation;
        public GameObject[] objectsToDisableAtActivation;
        public GameObject[] objectsToDisableAtDeactivation;
        public GameObject[] objectsToInstantiateAtActivation;
        public GameObject[] objectsToInstantiateAtDeactivation;
        public GameObject[] objectsToDestroyAtActivation;
        public GameObject[] objectsToDestroyAtDeactivation;
        public Transform instantiationPositionAndRotation;
        public Transform instantiatedParent;
        public float instantiationDelayInSec;
        public float destructionDelayInSec;

        public void link(AbstractActivator activator)
        {
            // nothing to do
        }

        private void executeForEachObjectOfArray(GameObject[] objectsArray, Action<GameObject> action)
        {
            if (objectsArray != null)
            {
                Array.ForEach(objectsArray, action);
            }
        }

        private IEnumerator instantiateObject(GameObject objPrefab)
        {
            if (instantiationDelayInSec.IsPreciselyNotZero()) yield return new WaitForSeconds(instantiationDelayInSec);
            MiscUtils.InstantiateObject(objPrefab, instantiatedParent, instantiationPositionAndRotation.position, instantiationPositionAndRotation.rotation);
        }

        private void updateObjectsForActivation(GameObject[] objectsToEnable, GameObject[] objectsToDisable, GameObject[] objectsToInstantiate, GameObject[] objectsToDestroy)
        {
            executeForEachObjectOfArray(objectsToEnable, obj => obj.SetActive(true));
            executeForEachObjectOfArray(objectsToDisable, obj => obj.SetActive(false));
            executeForEachObjectOfArray(objectsToInstantiate, obj => StartCoroutine(instantiateObject(obj)));
            executeForEachObjectOfArray(objectsToDestroy, obj => Destroy(obj, destructionDelayInSec));
        }

        public void activate(AbstractActivator activator)
        {
            updateObjectsForActivation(objectsToEnableAtActivation, objectsToDisableAtActivation, objectsToInstantiateAtActivation, objectsToDestroyAtActivation);
        }

        public void deactivate(AbstractActivator activator)
        {
            updateObjectsForActivation(objectsToEnableAtDeactivation, objectsToDisableAtDeactivation, objectsToInstantiateAtDeactivation, objectsToDestroyAtDeactivation);
        }
    }
}