﻿using UnityEngine;

namespace swantiez.unity.tools.physics
{
    public static class Rigidbody2DExtension
    {
        /// <summary>
        /// Add an explosion force to the 2D rigidbody.
        /// </summary>
        /// <remarks>
        /// See following discussion for more info: http://forum.unity3d.com/threads/need-rigidbody2d-addexplosionforce.212173/
        /// </remarks>
        public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
        {
            var dir = (body.transform.position - explosionPosition);
            float wearoff = 1 - (dir.magnitude / explosionRadius);
            body.AddForce(dir.normalized * explosionForce * wearoff, ForceMode2D.Impulse);
        }

        /// <summary>
        /// Add an explosion force to the 2D rigidbody.
        /// </summary>
        /// <remarks>
        /// See following discussion for more info: http://forum.unity3d.com/threads/need-rigidbody2d-addexplosionforce.212173/
        /// </remarks>
        public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
        {
            var dir = (body.transform.position - explosionPosition);
            float wearoff = 1 - (dir.magnitude / explosionRadius);
            Vector3 baseForce = dir.normalized * explosionForce * wearoff;
            body.AddForce(baseForce, ForceMode2D.Impulse);

            float upliftWearoff = 1 - upliftModifier / explosionRadius;
            Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
            body.AddForce(upliftForce, ForceMode2D.Impulse);
        }

        /// <summary>
        /// Get the force generated by the impact of the collision on the current body.
        /// </summary>
        /// <remarks>
        /// See following discussion for more info: http://forum.unity3d.com/threads/getting-impact-force-not-just-velocity.23746/
        /// </remarks>
        public static float GetImpactForce(this Rigidbody2D body, Collision2D collision)
        {
            return Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity)) * body.mass;
        }
    }
}