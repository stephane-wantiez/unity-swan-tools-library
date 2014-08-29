using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace swantiez.unity.tools.utils
{
    public static class CollectionUtils
    {
        public static bool IsCollFilled<T>(ICollection<T> array)
        {
            return (array != null) && (array.Count != 0);
        }

        public static bool IsFilled<T>(this ICollection<T> array)
        {
            return IsCollFilled(array);
        }

        public static bool IsEmpty<T>(this ICollection<T> array)
        {
            return !IsCollFilled(array);
        }

        public static string CollToString<T>(IEnumerable<T> coll)
        {
            if (coll == null) return "NULL";
            StringBuilder str = new StringBuilder("{");
            bool firstElem = true;
            foreach (T elem in coll)
            {
                if (!firstElem) str.Append(", ");
                str.Append("" + elem);
                firstElem = false;
            }
            str.Append("}");
            return str.ToString();
        }

        public static string GetAsString<T>(this IEnumerable<T> coll)
        {
            return CollToString(coll);
        }

        public static T[] MergeArrays<T>(T[] array1, T[] array2)
        {
            if ((array1 == null) || (array1.Length == 0)) return array2;
            if ((array2 == null) || (array2.Length == 0)) return array1;

            T[] newArray = new T[array1.Length + array2.Length];
            Array.Copy(array1, 0, newArray, 0, array1.Length);
            Array.Copy(array2, 0, newArray, array1.Length, array2.Length);

            return newArray;
        }

        public static T[] MergeWith<T>(this T[] array1, T[] array2)
        {
            return MergeArrays(array1, array2);
        }

        public static T[] AddToArray<T>(T[] array, params T[] elems)
        {
            return MergeArrays(array, elems);
        }

        public static T[] AddElements<T>(this T[] array, params T[] elems)
        {
            return MergeArrays(array, elems);
        }

        public static T[] RemoveAllFromArray<T>(T[] array, Predicate<T> predicate)
        {
            if ((array == null) || (array.Length == 0)) return null;
            List<T> arrayList = array.ToList();
            arrayList.RemoveAll(predicate);
            return arrayList.Count == 0 ? null : arrayList.ToArray();
        }

        public static T[] RemoveFromArray<T>(T[] array, T elem)
        {
            return RemoveAllFromArray(array, t => Equals(t, elem));
        }

        public static T[] RemoveAll<T>(this T[] array, Predicate<T> predicate)
        {
            return array = RemoveAllFromArray(array, predicate);
        }

        public static T[] Remove<T>(this T[] array, T elem)
        {
            return array = RemoveAllFromArray(array, t => Equals(t, elem));
        }

        public static ICollection<T> RemoveAllNull<T>(this ICollection<T> coll) where T : UnityEngine.Object
        {
            while (coll.Contains(null)) coll.Remove(null);
            return coll;
        }

        public static List<T> GetValuesOfEnum<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");
            return Enum.GetValues(typeof(T)).Cast<T>().ToList<T>();
        }

        public static int GetNbValuesInEnum<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");
            return Enum.GetValues(typeof(T)).Length;
        }

        public static void ShuffleFY<T>(this IList<T> list)
        {
            for (int i = list.Count; i > 1; i--)
            {
                // Pick random element to swap.
                int j = UnityEngine.Random.Range(0, i); // 0 <= j <= i-1
                // Swap.
                T tmp = list[j];
                list[j] = list[i - 1];
                list[i - 1] = tmp;
            }
        }
    }
}