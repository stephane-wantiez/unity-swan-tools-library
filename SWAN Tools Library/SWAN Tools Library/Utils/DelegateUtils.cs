namespace swantiez.unity.tools.utils
{
    public static class DelegateUtils
    {
        public delegate void OnSimpleEvent();
        public delegate void OnBooleanEvent(bool value);
        public delegate void OnIntegerEvent(int value);
        public delegate void OnFloatEvent(float value);
    }
}