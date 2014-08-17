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
        /// Get the signed angle between two vectors - if v1 is "above" v2, it will be positive, and negative otherwise.
        /// </summary>
        /// <param name="v1">The first vector to check the angle from</param>
        /// <param name="v2">The second vector to compare for angle</param>
        /// <returns>The angle between the two provided vectors</returns>
        public static float GetSignedAngleBetween2DVectors(Vector2 v1, Vector2 v2)
        {
            float angle = Vector2.Angle(v1, v2);
            Vector3 planeNormal = Vector3.forward;
            Vector3 cross = Vector3.Cross(v1, v2);
            if (Vector3.Dot(planeNormal, cross) < 0)
            {
                angle = -angle;
            }
            if (angle <= -180f) angle += 360f;
            return angle;
        }

        public static float GetSignedAngleFromVector(this Vector2 currentVector, Vector2 vector)
        {
            return GetSignedAngleBetween2DVectors(vector, currentVector);
        }
    }
}