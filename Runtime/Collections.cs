using System;
using System.Collections.Generic;

namespace Toolbox.Collections
{
    /// <summary>
    /// Provides several extension methods for C# collections.
    /// </summary>
    public static class Collections
    {
        /*
        private static readonly List<GCHandle> _arrayHandles = new();
        public T* pin<T>(T[] array) where T : unmanaged
        {
            if (array == null || array.Length == 0)
                throw new Exception("Array must be non-null and non-empty");
            GCHandle gcHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            _arrayHandles.Add(gcHandle);
            return (T*)gcHandle.AddrOfPinnedObject();
        }

        //EXAMPLE OF USE
        private unsafe struct ConfigureInstanceDataJob : IJob 
        {
            [NativeDisableUnsafePtrRestriction] private Matrix4x4* pointMatrices;
        }

        //EXAMPLE TO RELEASE
        foreach(GCHandle pinned in _arrayHandles)
            pinned.Free();
        _arrayHandles.Clear();
        */


        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="element"></param>
        public static T[] RemoveElement<T>(this T[] array, T elementToRemove)
        {
            int index = Array.IndexOf(array, elementToRemove);
            if (index < 0) return array;

            T[] result = new T[array.Length - 1];
            if (index > 0) Array.Copy(array, 0, result, 0, index);
            if (index < array.Length - 1) Array.Copy(array, index + 1, result, index, array.Length - index - 1);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="elementToAdd"></param>
        /// <returns></returns>
        public static T[] AddElement<T>(this T[] array, T elementToAdd)
        {
            T[] result = new T[array.Length + 1];
            Array.Copy(array, result, array.Length);
            result[array.Length] = elementToAdd;
            return result;
        }

        /// <summary>
        /// Adds a value to a dictionary. If the key already exists, the value is inserted into the value's list.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        public static void AddListed<TKey, TValue>(this Dictionary<TKey, List<TValue>> dic, TKey key, TValue val)
        {
            List<TValue> list = null;
            if (dic.TryGetValue(key, out list))
                list.Add(val);
            else dic.Add(key, new List<TValue> { val });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="val"></param>
        public static bool RemoveListed<TKey, TValue>(this Dictionary<TKey, List<TValue>> dic, TKey key, TValue val)
        {
            List<TValue> list = null;
            if (dic.TryGetValue(key, out list))
            {
                if (list == null)
                {
                    dic.Remove(key);
                    return false;
                }
                else
                {
                    var result = list.Remove(val);
                    if (list.Count < 1) dic.Remove(key);
                    return result;
                }
            }
            else return false;
        }

        /// <summary>
        /// Removes the first instance of the value found in any list in the dictionary's value List<>.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="val"></param>
        public static bool RemoveListed<TKey, TValue>(this Dictionary<TKey, List<TValue>> dic, TValue val)
        {
            foreach (var kvp in dic)
            {
                var list = kvp.Value;
                if (list == null)
                {
                    dic.Remove(kvp.Key);
                    return false;
                }
                else
                {
                    var result = kvp.Value.Remove(val);
                    if (kvp.Value.Count < 1) dic.Remove(kvp.Key);
                    return result;
                }
            }

            return false;
        }
    }
}