using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Peg.Util
{
    /// <summary>
    /// Circular buffer of a fixed size that provides the illusion of inifite size. Useful for
    /// handling large amounts of array data that only needs to be streamed a little at a time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FixedCircularBuffer<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 5;
        //private readonly int Capacity;
        private T[] Buffer;
        //private int Count;
        private int End;
        private int Start;

        public FixedCircularBuffer() : this(DefaultCapacity)
        {
        }

        public FixedCircularBuffer(int capacity)
        {
            Capacity = capacity;
            Buffer = new T[capacity];
            Start = End = 0;
        }

        public FixedCircularBuffer(IEnumerable<T> collection) : this(collection.Count())
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        public bool IsFull
        {
            get { return Count == Capacity; }
        }

        public int Count
        {
            get;
            private set;
        }

        public int Capacity
        {
           get; private set;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Add the specified item to the end of the buffer.
        /// </summary>
        /// <param name='item'>
        /// Item.
        /// </param>
        public void Add(T item)
        {
            if (IsFull)
            {
                End = Start;
                if (Start == Capacity - 1)
                {
                    Start = Capacity - Start - 1;
                }
                else
                {
                    Start++;
                }
                Buffer[End] = item;
            }
            else
            {
                if (++End >= Capacity)
                {
                    End = End - Capacity;
                }

                Buffer[End] = item;
                Count++;
            }
        }

        /// <summary>
        /// Pushes the specified item onto the front of the buffer and
        /// pushes all other items back by one.
        /// </summary>
        /// <param name='item'>
        /// Item.
        /// </param>
        public void Push(T item)
        {
            if(--Start < 0) Start = Start + Capacity;
            if(IsFull)
            {
                End = Start+1;
                if(End >= Capacity) End -= Capacity;
            }

            Buffer[Start] = item;
            Count++;
        }

        /// <summary>
        /// Returns the first item on the buffer and then removes that item from the buffer.
        /// </summary>
        /// <exception cref='InvalidOperationException'>
        /// Is thrown when the invalid operation exception.
        /// </exception>
        public T Get()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Buffer is Empty");
            }

            Count--;
            T item = Buffer[Start];
            Buffer[Start] = default(T);

            if (Start == Capacity - 1)
            {
                Start = Capacity - Start - 1;
            }
            else
            {
                Start++;
            }

            if (IsEmpty)
            {
                End = Start;
            }

            return item;
        }

        public T Peek(int index)
        {
            return Buffer[index];
        }

        public T PeekFront()
        {
            return Buffer[Start];
        }

        public T PeekBack()
        {
            return Buffer[End];
        }

        public T PeekOldest()
        {
            return Buffer[Start];
        }

        public T PeekNewest()
        {
            return Buffer[End];
        }

        public void Clear()
        {
            Buffer = new T[Capacity];
            Start = End = 0;
        }

        public bool Contains(T item)
        {
            return Buffer.Contains(item);
        }

        #region IEnumerable Members

        public IEnumerator<T> GetEnumerator()
        {
            return Buffer.Where(item => !item.Equals(default(T))).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

