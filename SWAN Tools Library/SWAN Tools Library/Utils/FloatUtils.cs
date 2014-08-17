using UnityEngine;

namespace swantiez.unity.tools.utils
{
    public static class FloatUtils
    {
        public static bool IsFloatPreciselyZero(float value)
        {
            return Mathf.Approximately(value, 0f);
        }

        public static bool IsPreciselyZero(this float value)
        {
            return Mathf.Approximately(value, 0f);
        }

        public static bool IsFloatPreciselyNotZero(float value)
        {
            return !Mathf.Approximately(value, 0f);
        }

        public static bool IsPreciselyNotZero(this float value)
        {
            return !Mathf.Approximately(value, 0f);
        }

        public enum ComparisonResult { Smaller, Equal, Greater }

        public static ComparisonResult CompareFloatsWithPrecision(float a, float b)
        {
            if (Mathf.Approximately(a, b)) return ComparisonResult.Equal;
            return a < b ? ComparisonResult.Smaller : ComparisonResult.Greater;
        }

        public static ComparisonResult ComparePreciselyWith(this float a, float b)
        {
            return CompareFloatsWithPrecision(a, b);
        }

        public static bool IsFirstFloatPreciselyStrictlySmallerThanSecond(float first, float second)
        {
            return CompareFloatsWithPrecision(first, second) == ComparisonResult.Smaller;
        }

        public static bool IsPreciselyStrictlySmallerThan(this float currentValue, float value)
        {
            return IsFirstFloatPreciselyStrictlySmallerThanSecond(currentValue,value);
        }

        public static bool IsFirstFloatPreciselySmallerOrEqualToSecond(float first, float second)
        {
            return CompareFloatsWithPrecision(first, second) != ComparisonResult.Greater;
        }

        public static bool IsPreciselySmallerOrEqualTo(this float currentValue, float value)
        {
            return IsFirstFloatPreciselySmallerOrEqualToSecond(currentValue, value);
        }

        public static bool IsFirstFloatPreciselyStrictlyGreaterThanSecond(float first, float second)
        {
            return CompareFloatsWithPrecision(first, second) == ComparisonResult.Greater;
        }

        public static bool IsPreciselyStrictlyGreaterThan(this float currentValue, float value)
        {
            return IsFirstFloatPreciselyStrictlyGreaterThanSecond(currentValue, value);
        }

        public static bool IsFirstFloatPreciselyGreaterOrEqualToSecond(float first, float second)
        {
            return CompareFloatsWithPrecision(first, second) != ComparisonResult.Smaller;
        }

        public static bool IsPreciselyGreaterOrEqualTo(this float currentValue, float value)
        {
            return IsFirstFloatPreciselyGreaterOrEqualToSecond(currentValue, value);
        }

        public static bool IsFloatPreciselyStrictlyPositive(float n)
        {
            return IsFirstFloatPreciselyStrictlyGreaterThanSecond(n, 0f);
        }

        public static bool IsPreciselyStrictlyPositive(this float currentValue)
        {
            return IsFloatPreciselyStrictlyPositive(currentValue);
        }

        public static bool IsFloatPreciselyPositiveOrZero(float n)
        {
            return IsFirstFloatPreciselyGreaterOrEqualToSecond(n, 0f);
        }

        public static bool IsPreciselyPositiveOrZero(this float currentValue)
        {
            return IsFloatPreciselyPositiveOrZero(currentValue);
        }

        public static bool IsFloatPreciselyStrictlyNegative(float n)
        {
            return IsFirstFloatPreciselyStrictlySmallerThanSecond(n, 0f);
        }

        public static bool IsPreciselyStrictlyNegative(this float currentValue)
        {
            return IsFloatPreciselyStrictlyNegative(currentValue);
        }

        public static bool IsFloatPreciselyNegativeOrZero(float n)
        {
            return IsFirstFloatPreciselySmallerOrEqualToSecond(n, 0f);
        }

        public static bool IsPreciselyNegativeOrZero(this float currentValue)
        {
            return IsFloatPreciselyNegativeOrZero(currentValue);
        }

        public static bool IsFloatPreciselyStrictlyBetween(float value, float min, float max)
        {
            return IsFirstFloatPreciselyStrictlyGreaterThanSecond(value, min) &&
                   IsFirstFloatPreciselyStrictlySmallerThanSecond(value, max);
        }

        public static bool IsPreciselyStrictlyBetween(this float currentValue, float min, float max)
        {
            return IsFloatPreciselyStrictlyBetween(currentValue, min, max);
        }
    }
}