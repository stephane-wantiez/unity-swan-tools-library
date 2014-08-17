using UnityEngine;

namespace swantiez.unity.tools.utils
{
    public static class VectorUtils
    {
        public static float GetSquareDistanceBetween(ref Vector3 pos1, ref Vector3 pos2)
        {
            return (pos2 - pos1).sqrMagnitude;
        }

        public static float GetSquareDistanceBetween(Vector3 pos1, Vector3 pos2)
        {
            return GetSquareDistanceBetween(ref pos1, ref pos2);
        }

        public static float GetSquareDistanceBetween(Transform elem1, Transform elem2)
        {
            return GetSquareDistanceBetween(elem1.position, elem2.position);
        }

        public static float GetSquareDistanceWith(this Vector3 currentPos, Vector3 position)
        {
            return GetSquareDistanceBetween(currentPos, position);
        }

        public static Vector3 GetVectorOfAxisDistancesBetween(ref Vector3 pos1, ref Vector3 pos2)
        {
            Vector3 axisDistances;
            axisDistances.x = Mathf.Abs(pos1.x - pos2.x);
            axisDistances.y = Mathf.Abs(pos1.y - pos2.y);
            axisDistances.z = Mathf.Abs(pos1.z - pos2.z);
            return axisDistances;
        }

        public static Vector3 GetVectorOfAxisDistancesBetween(Vector3 pos1, Vector3 pos2)
        {
            return GetVectorOfAxisDistancesBetween(ref pos1, ref pos2);
        }

        public static Vector3 GetVectorOfAxisDistancesBetween(Transform elem1, Transform elem2)
        {
            return GetVectorOfAxisDistancesBetween(elem1.position, elem2.position);
        }

        public static Vector3 GetVectorOfAxisDistancesWith(this Vector3 currentPos, Vector3 position)
        {
            return GetVectorOfAxisDistancesBetween(currentPos, position);
        }

        public static Vector2 GetRotated2DVector(Vector2 baseVector, float angleOfRotationInDegrees)
        {
            Quaternion rotationQuaternion = Quaternion.AngleAxis(angleOfRotationInDegrees, Vector3.forward);
            return rotationQuaternion * baseVector;
        }

        public static Vector2 RotateOfAngle(this Vector2 currentVector, float angleOfRotationInDegrees)
        {
            return GetRotated2DVector(currentVector, angleOfRotationInDegrees);
        }

        public static bool IsVectorSizeZero(Vector2 value)
        {
            return Mathf.Approximately(value.sqrMagnitude, 0f);
        }

        public static bool HasMagnitudeZero(this Vector2 value)
        {
            return Mathf.Approximately(value.sqrMagnitude, 0f);
        }

        public static bool IsVectorSizeNotZero(Vector2 value)
        {
            return !Mathf.Approximately(value.sqrMagnitude, 0f);
        }

        public static bool HasMagnitudeNotZero(this Vector2 value)
        {
            return !Mathf.Approximately(value.sqrMagnitude, 0f);
        }

        public static bool IsVectorSizeZero(Vector3 value)
        {
            return Mathf.Approximately(value.sqrMagnitude, 0f);
        }

        public static bool HasMagnitudeZero(this Vector3 value)
        {
            return Mathf.Approximately(value.sqrMagnitude, 0f);
        }

        public static bool IsVectorSizeNotZero(Vector3 value)
        {
            return !Mathf.Approximately(value.sqrMagnitude, 0f);
        }

        public static bool HasMagnitudeNotZero(this Vector3 value)
        {
            return !Mathf.Approximately(value.sqrMagnitude, 0f);
        }

        public static bool AreVectorPreciselyEquals(Vector3 v1, Vector3 v2)
        {
            return Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y) && Mathf.Approximately(v1.z, v2.z);
        }

        public static bool AreVectorEqualsWithDelta(Vector3 v1, Vector3 v2, float delta)
        {
            return Vector3.Distance(v1, v2) < delta;
        }

        public static bool AreVectorEqualsWithDeltaSqr(Vector3 v1, Vector3 v2, float deltaSqr)
        {
            return GetSquareDistanceBetween(v1,v2) < deltaSqr;
        }

        public static bool IsPreciselyEqualTo(this Vector3 currentVector, Vector3 otherVector)
        {
            return AreVectorPreciselyEquals(currentVector, otherVector);
        }

        public static bool IsPreciselyEqualTo(this Vector3 currentVector, Vector3? otherVector)
        {
            if (otherVector == null) return false;
            return AreVectorPreciselyEquals(currentVector, (Vector3) otherVector);
        }

        public static bool IsEqualWithDeltaTo(this Vector3 currentVector, Vector3 otherVector, float delta)
        {
            return AreVectorEqualsWithDelta(currentVector, otherVector, delta);
        }

        public static bool IsEqualWithDeltaTo(this Vector3 currentVector, Vector3? otherVector, float delta)
        {
            if (otherVector == null) return false;
            return AreVectorEqualsWithDelta(currentVector, (Vector3) otherVector, delta);
        }

        public static bool IsEqualWithDeltaSqrTo(this Vector3 currentVector, Vector3 otherVector, float deltaSqr)
        {
            return AreVectorEqualsWithDeltaSqr(currentVector, otherVector, deltaSqr);
        }

        public static bool IsEqualWithDeltaSqrTo(this Vector3 currentVector, Vector3? otherVector, float deltaSqr)
        {
            if (otherVector == null) return false;
            return AreVectorEqualsWithDeltaSqr(currentVector, (Vector3)otherVector, deltaSqr);
        }
    }
}