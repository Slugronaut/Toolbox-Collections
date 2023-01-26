/*
 * Borrowed from the Unity community wiki.
 */

//Maybe not the best idea to use this namespace, but at least it's easy to remember.
namespace System.Collections
{
    /// <summary>
    /// Interface that must be exposed by all heap-able objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHeapItem<T> : IComparable<T>
    {
        int HeapIndex { get; set; }
    }

    /// <summary>
    /// Data-structure that produces a partially sorted tree.
    /// Useful for data that needs to be frequently inserted,
    /// removed, and compared.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Heap<T> where T : IHeapItem<T>
    {
        T[] items;
        int CurrentCount;
        int capacity;
        public int Capacity { get { return capacity; } }
        public int Count { get { return CurrentCount; } }


        /// <summary>
        /// Constructs an instance of a heap with a give maximum number of elements it can contain.
        /// </summary>
        /// <param name="maxHeapSize"></param>
        public Heap(int maxHeapSize)
        {
            items = new T[maxHeapSize];
            capacity = maxHeapSize;
        }

        /// <summary>
        /// Clears all items from the heap. Internally, all allocated memory remains.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < CurrentCount; i++)
                items[i].HeapIndex = 0;
            CurrentCount = 0;
        }

        /// <summary>
        /// Adds a new item to the heap.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            item.HeapIndex = CurrentCount;
            items[CurrentCount] = item;
            SortUp(item);
            CurrentCount++;
        }

        /// <summary>
        /// Removes the top item of the heap.
        /// </summary>
        /// <returns>The item removed.</returns>
        public T RemoveFirst()
        {
            T firstItem = items[0];
            CurrentCount--;
            items[0] = items[CurrentCount];
            items[0].HeapIndex = 0;
            SortDown(items[0]);
            return firstItem;
        }

        /// <summary>
        /// Checks for the existance of an item in the heap.
        /// </summary>
        /// <param name="item"></param>
        /// <returns><c>true</c> if the item exists in this heap, <c>false</c> otherwise.</returns>
        public bool Contains(T item)
        {
            return Equals(items[item.HeapIndex], item);
        }

        /// <summary>
        /// Ensures the the item is properly sorted within the heap.
        /// </summary>
        /// <param name="item"></param>
        public void UpdateItem(T item)
        {
            SortUp(item);
        }

        /// <summary>
        /// Helper method for pushing an item down the heap in order to maintain heap-property.
        /// </summary>
        /// <param name="item"></param>
        void SortDown(T item)
        {
            while (true)
            {
                int childIndexLeft = item.HeapIndex * 2 + 1;
                int childIndexRight = item.HeapIndex * 2 + 2;
                int swapIndex = 0;

                if (childIndexLeft < CurrentCount)
                {
                    swapIndex = childIndexLeft;
                    if (childIndexRight < CurrentCount)
                    {
                        if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                        {
                            swapIndex = childIndexRight;
                        }
                    }
                    if (item.CompareTo(items[swapIndex]) < 0)
                        Swap(item, items[swapIndex]);
                    else
                        return;
                }
                else
                    return;
            }
        }

        /// <summary>
        /// Helper method for pushing an item up the heap in order to maintain heap-property.
        /// </summary>
        /// <param name="item"></param>
        void SortUp(T item)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;
            while (true)
            {
                T parentItem = items[parentIndex];
                if (item.CompareTo(parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else
                    break;
                parentIndex = (item.HeapIndex - 1) / 2;
            }
        }

        /// <summary>
        /// Helper method for swapping two items in this heap.
        /// </summary>
        /// <param name="itemA"></param>
        /// <param name="itemB"></param>
        void Swap(T itemA, T itemB)
        {
            items[itemA.HeapIndex] = itemB;
            items[itemB.HeapIndex] = itemA;
            int itemAHeapIndex = itemA.HeapIndex;
            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAHeapIndex;
        }
    }

}
