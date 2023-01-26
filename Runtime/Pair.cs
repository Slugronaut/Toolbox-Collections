using System;
using UnityEngine;


namespace Toolbox.Collections
{
    /// <summary>
    /// Very simple class for storing paired values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    [Serializable]
	public class PairObject<T, U> 
	{
		public PairObject() 
		{
		}
		
		public PairObject(T first, U second) 
		{
			this._First = first;
			this._Second = second;
		}

        [SerializeField]
        private T _First;
        public T First { get { return _First; } set { _First = value; } }

        [SerializeField]
        private U _Second;
        public U Second { get { return _Second; } set { _Second = value; } }
	};


    /// <summary>
    /// Very simple struct for storing paired values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    [Serializable]
    public struct ImmutablePairValue<T, U>
    {
        public ImmutablePairValue(T first, U second)
        {
            this._First = first;
            this._Second = second;
        }

        [SerializeField]
        private T _First;
        public T First { get { return _First; } set { _First = value; } }

        [SerializeField]
        private U _Second;
        public U Second { get { return _Second; } set { _Second = value; } }

    };


    /// <summary>
    /// Very simple struct for storing paired values. Supports mutation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    [Serializable]
    public struct PairValue<T, U>
    {
        public PairValue(T first, U second)
        {
            this._First = first;
            this._Second = second;
        }

        [SerializeField]
        private T _First;
        public T First { get { return _First; } set { _First = value; } }

        [SerializeField]
        private U _Second;
        public U Second { get { return _Second; } set { _Second = value; } }

        public T ChangeFirst(T first)
        {
            _First = first;
            return _First;
        }

        public U ChangeSecond(U second)
        {
            _Second = second;
            return _Second;
        }

        public void Change(T first, U second)
        {
            _First = first;
            _Second = second;
        }
    };
}

