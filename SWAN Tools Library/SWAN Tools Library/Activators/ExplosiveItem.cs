using swantiez.unity.tools.audio;
using swantiez.unity.tools.physics;
using System.Collections;
using UnityEngine;

namespace swantiez.unity.tools.activators
{
    public class ExplosiveItem : MonoBehaviour, IActivable
    {
        public float force;
        public float radius;
        public float timerDelayInSec;
        public GameObject instantiateAtExplosion;
        public GameObject activateAtExplosion;
        public GameObject deactivateAtExplosion;
        public bool destroyOnExplosion = true;
        public float destructionDelayInSec;
        public bool invertActivable;
        public SoundOrMusic explosionSound;
        public bool startSoundAtCountdown;

        private bool explosionTimerLaunched = false;

        public void explode()
        {
            if (instantiateAtExplosion != null) Instantiate(instantiateAtExplosion, transform.position, Quaternion.identity);
            if (activateAtExplosion != null) activateAtExplosion.SetActive(true);
            if (deactivateAtExplosion != null) deactivateAtExplosion.SetActive(false);
            if (!startSoundAtCountdown) explosionSound.play();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D c in colliders)
            {
                if (c.GetComponent<Rigidbody2D>() != null)
                {
                    //Debug.Log("Found collider with rigidbody2D named '" + c.gameObject.name + "'");
                    c.GetComponent<Rigidbody2D>().AddExplosionForce(force, transform.position, radius);
                }
            }

            if (destroyOnExplosion) Destroy(gameObject, destructionDelayInSec);
        }

        private IEnumerator launchExplosionTimer()
        {
            if (!explosionTimerLaunched)
            {
                explosionTimerLaunched = true;
                if (startSoundAtCountdown) explosionSound.play();
                yield return new WaitForSeconds(timerDelayInSec);
                explode();
            }
        }

        public void link(AbstractActivator activator)
        {
            explosionTimerLaunched = false;
        }

        public void activate(AbstractActivator activator)
        {
            if (!invertActivable) StartCoroutine(launchExplosionTimer());
        }

        public void deactivate(AbstractActivator activator)
        {
            if (invertActivable) StartCoroutine(launchExplosionTimer());
        }
    }
}
