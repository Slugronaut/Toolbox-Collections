using System.Collections.Generic;


namespace Toolbox.Collections
{
    /// <summary>
    /// Provides several extension methods for C# collections.
    /// </summary>
    public static class Collections
    {
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