using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reactive.Linq
{
	public class ListObservable<T> : IList<T>, IObservable<T>
	{
		public ListObservable ()
		{
		}

		public IEnumerator GetEnumerator ()
		{
			throw new System.NotImplementedException ();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator ()
		{
			throw new System.NotImplementedException ();
		}

		public void Add (T item)
		{
			throw new System.NotImplementedException ();
		}

		public void Clear ()
		{
			throw new System.NotImplementedException ();
		}

		public bool Contains (T item)
		{
			throw new System.NotImplementedException ();
		}

		public void CopyTo (T[] array, int arrayIndex)
		{
			throw new System.NotImplementedException ();
		}

		public bool Remove (T item)
		{
			throw new System.NotImplementedException ();
		}

		public int Count {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public bool IsReadOnly {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public int IndexOf (T item)
		{
			throw new System.NotImplementedException ();
		}

		public void Insert (int index, T item)
		{
			throw new System.NotImplementedException ();
		}

		public void RemoveAt (int index)
		{
			throw new System.NotImplementedException ();
		}

		public T this [int index] {
			get {
				throw new System.NotImplementedException ();
			}
			set {
				throw new System.NotImplementedException ();
			}
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			throw new System.NotImplementedException ();
		}
	}
}

