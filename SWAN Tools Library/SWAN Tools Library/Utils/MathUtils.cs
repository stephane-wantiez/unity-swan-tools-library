using System;
using UnityEngine;

namespace swantiez.unity.tools.utils
{
    public static class MathUtils
    {
        public static Vector2 Get2DUnitVectorForAngleInRadians(float angleInRadians)
        {
            return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
        }

        public static Vector2 Get2DUnitVectorForAngleInDegrees(float angleInDegrees)
        {
            float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
            return Get2DUnitVectorForAngleInRadians(angleInRadians);
        }

        /// <summary>
        /// Get the signed angle of a vector compared to the right vector (counter-clockwise) - between -179.99... and 180 degrees
        /// </summary>
        /// <param name="v">The vector to get the signed angle from</param>
        /// <returns>The signed angle of the provided vector</returns>
        public static float GetSignedAngleOf2DVector(Vector2 v)
        {
            float angle = Vector2.Angle(v, Vector2.right);
            if (v.y < 0f) angle = -angle;
            return angle;
        }

        public static float GetSignedAngleFromRight(this Vector2 currentVector)
        {
            return GetSignedAngleOf2DVector(currentVector);
        }

        /// <summary>
        /// Get the signed angle between two vectors while looking towards the forward axis.
        /// </summary>
        /// <param name="fromVector">The vector to check the angle from</param>
        /// <param name="toVector">The svector to compare for angle</param>
        /// <returns>The signed angle between the two provided vectors</returns>
        public static float GetSignedAngleBetweenVectors(Vector3 fromVector, Vector3 toVector)
        {
            float angle = Vector3.Angle(fromVector, toVector);
            Vector3 cromFromToVector = Vector3.Cross(fromVector, toVector);

            if (Vector3.Dot(Vector3.forward, cromFromToVector) < 0)
            {
                angle = -angle;
            }

            if (angle <= -180f) angle += 360f;
            return angle;
        }

        /// <summary>
        /// Get the signed angle from the specified vector to the current one while looking towards the forward axis.
        /// </summary>
        /// <param name="currentVector">The current vector to compare for angle</param>
        /// <param name="vector">The vector to check the angle from</param>
        /// <returns>The signed angle between the two provided vectors</returns>
        public static float GetSignedAngleFromVector(this Vector3 currentVector, Vector3 vector)
        {
            return GetSignedAngleBetweenVectors(vector, currentVector);
        }

        /// <summary>
        /// Get the signed angle from the specified vector to the current one.
        /// </summary>
        /// <param name="currentVector">The current vector to compare for angle</param>
        /// <param name="vector">The vector to check the angle from</param>
        /// <returns>The signed angle between the two provided vectors</returns>
        public static float GetSignedAngleFromVector(this Vector2 currentVector, Vector2 vector)
        {
            return GetSignedAngleBetweenVectors(vector, currentVector);
        }
    }
}