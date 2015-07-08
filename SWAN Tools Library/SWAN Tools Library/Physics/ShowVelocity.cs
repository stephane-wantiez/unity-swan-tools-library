using UnityEngine;

namespace swantiez.unity.tools.physics
{
    public class ShowVelocity : MonoBehaviour
    {
        public Vector3 velocity;

        void FixedUpdate()
        {
            velocity = GetComponent<Rigidbody>() ? GetComponent<Rigidbody>().velocity : (GetComponent<Rigidbody2D>() ? (Vector3)GetComponent<Rigidbody2D>().velocity : Vector3.zero);
        }
    }
}