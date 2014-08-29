using UnityEngine;

namespace swantiez.unity.tools.utils
{
    public static class FloatUtils
    {
        public static bool IsPreciselyZero(this float value)
        {
            return Mathf.Approximately(value, 0f);
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

        public static bool IsPreciselyStrictlyPositive(this float currentValue)
        {
            return IsFirstFloatPreciselyStrictlyGreaterThanSecond(currentValue, 0f);
        }

        public static bool IsPreciselyPositiveOrZero(this float currentValue)
        {
            return IsFirstFloatPreciselyGreaterOrEqualToSecond(currentValue, 0f);
        }

        public static bool IsPreciselyStrictlyNegative(this float currentValue)
        {
            return IsFirstFloatPreciselyStrictlySmallerThanSecond(currentValue, 0f);
        }

        public static bool IsPreciselyNegativeOrZero(this float currentValue)
        {
            return IsFirstFloatPreciselySmallerOrEqualToSecond(currentValue, 0f);
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