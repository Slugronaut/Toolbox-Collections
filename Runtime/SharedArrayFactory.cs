using UnityEngine;
using System.Collections.Generic;
using System;


namespace Toolbox.Collections
{
    /// <summary>
    /// Factory class for arrays of objects that are commonly used as temporary variables.
    /// All data should be considered non-persistent, volitile, and non-thread safe.
    /// 
    /// Also contains several pre-allocated collider and raycast hit arrays.
    /// </summary>
    public static class SharedArrayFactory
    {
        static Dictionary<Type, Dictionary<int, object[]>> Arrays = new Dictionary<Type, Dictionary<int, object[]>>();
        static Dictionary<Type, object> TempLists = new Dictionary<Type, object>();

        public static Collider Col;
        public static Collider[] Col1 = new Collider[1];
        public static Collider[] Col3 = new Collider[3];
        public static Collider[] Col5 = new Collider[5];
        public static Collider[] Col10 = new Collider[10];
        public static Collider[] Col20 = new Collider[20];
        //public static Collider[] Col50 = new Collider[50];

        public static Collider2D Col2d;
        public static Collider2D[] Col2d1 = new Collider2D[1];
        public static Collider2D[] Col2d3 = new Collider2D[3];
        public static Collider2D[] Col2d5 = new Collider2D[5];
        public static Collider2D[] Col2d10 = new Collider2D[10];

        public static RaycastHit Hit;
        public static RaycastHit[] Hit1 = new RaycastHit[1];
        public static RaycastHit[] Hit3 = new RaycastHit[3];
        public static RaycastHit[] Hit5 = new RaycastHit[5];
        public static RaycastHit[] Hit10 = new RaycastHit[10];
        public static RaycastHit[] Hit20 = new RaycastHit[20];
        public static RaycastHit[] Hit25 = new RaycastHit[25];
        public static RaycastHit[] Hit50 = new RaycastHit[50];

        public static RaycastHit2D Hit2d;
        public static RaycastHit2D[] Hit2d1 = new RaycastHit2D[1];
        public static RaycastHit2D[] Hit2d3 = new RaycastHit2D[3];
        public static RaycastHit2D[] Hit2d5 = new RaycastHit2D[5];
        public static RaycastHit2D[] Hit2d10 = new RaycastHit2D[10];


        /// <summary>
        /// Helper for checking if an array contains a reference to any element.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool ContainsElement<T>(T[] array, T element)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    if (element == null) return true;
                    else return false;
                }
                else if(array[i].Equals(element)) 
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Helper for checking if an array contains a reference to any element.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static int IndexOf<T>(T[] array, T element)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    if (element == null) return i;
                    else return -1;
                }
                else if (array[i].Equals(element))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns an array of a requested size.
        /// 
        /// This array should not be stored for later use
        /// or be expected to hold any persistent, non-volitle
        /// data beyond the lifetime of the function in which
        /// it was requested. This array is not thread safe.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="size"></param>
        /// <returns></returns>
        public static T[] RequestTempArray<T>(int size)
        {
            Dictionary<int, object[]> sub = null;
            if(Arrays.TryGetValue(typeof(T), out sub))
            {
                object[] arr = null;
                if (sub.TryGetValue(size, out arr))
                    return arr as T[];
                else
                {
                    T[] subArr = new T[size];
                    sub.Add(size, subArr as object[]);
                    return subArr as T[];
                }
            }
            else
            {
                sub = new Dictionary<int, object[]>();
                T[] arr = new T[size];
                sub.Add(size, arr as object[]);
                Arrays.Add(typeof(T), sub);
                return arr as T[];
            }

        }
        
        /// <summary>
        /// Returns a generic List of the desired datatype.
        /// This list may have pre-allocated space within it
        /// but will be cleared just before being returned.
        /// Do not cache or thread-share this list! It is temporary and volitle!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> RequestTempList<T>()
        {
            if (TempLists.TryGetValue(typeof(T), out object list))
            {
                List<T> output = list as List<T>;
                output.Clear();
                return output;
            }
            else
            {
                List<T> output = new();
                TempLists.Add(typeof(T), output);
                output.Clear();
                return output;
            }
        }

    }
}
