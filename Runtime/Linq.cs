using System;

namespace Toolbox.Linq
{
    public static class ArrayUtil
    {
        public static void AddElement<T>(ref T[] array, T element)
        {
            int len = array.Length;
            T[] temp = new T[len + 1];
            array.CopyTo(temp, 0);
            temp[len] = element;
            array = temp;
        }

        public static void InsertElementByRef<T>(ref T[] array, ref T element, int index)
        {
            if(index > array.Length + 1 || index < 0) throw new ArgumentOutOfRangeException("index");

            int len = array.Length;
            T[] temp = new T[len+1];

            Array.Copy(array, 0, temp, 0, index);                       //copy part before insertion
            temp[index] = element;                                      //insert value
            Array.Copy(array, index, temp, index+1, len-index);         //copy part after insertion
            array = temp;
        }

        public static void RemoveElement<T>(ref T[] array, T element) where T : IComparable<T>
        {
            int len = array.Length;
            if (len == 0) return;

            int index = -1;
            for (int i = 0; i < len; i++)
            {
                var elm = array[i];
                if ((elm == null && element == null) || (elm != null && elm.Equals(element)))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1) return;
            if (len == 1)
            {
                array = new T[0];
                return;
            }
            T[] temp = new T[len - 1];

            int j = 0;
            for (int i = 0; i < index; i++, j++)
                temp[j] = array[i];

            for (int i = index + 1; i < len; i++, j++)
                temp[j] = array[i];

            array = temp;
        }
    }
}
