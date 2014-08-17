using UnityEngine;

namespace swantiez.unity.tools.physics
{
    public class ShowVelocity : MonoBehaviour
    {
        public Vector3 velocity;

        void FixedUpdate()
        {
            velocity = rigidbody ? rigidbody.velocity : (rigidbody2D ? (Vector3) rigidbody2D.velocity : Vector3.zero);
        }
    }
}