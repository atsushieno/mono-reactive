using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public sealed class CompositeDisposable
		: ICollection<IDisposable>, IEnumerable<IDisposable>, IEnumerable, IDisposable
	{
		public CompositeDisposable (IEnumerable<IDisposable> disposables)
		{
			throw new NotImplementedException ();
		}
		
		public CompositeDisposable (params IDisposable[] disposables)
		{
			throw new NotImplementedException ();
		}
		
		public CompositeDisposable (int capacity)
		{
			throw new NotImplementedException ();
		}
		
		public int Count {
			get { throw new NotImplementedException (); }
		}
		
		public bool IsReadOnly {
			get { throw new NotImplementedException (); }
		}
		
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}
		
		public void Add (IDisposable item)
		{
			throw new NotImplementedException ();
		}
		
		public void Clear ()
		{
			throw new NotImplementedException ();
		}
		
		public bool Contains (IDisposable item)
		{
			throw new NotImplementedException ();
		}
		
		public void CopyTo (IDisposable [] array, int arrayIndex)
		{
			throw new NotImplementedException ();
		}
		
		public void Dispose ()
		{
			throw new NotImplementedException ();
		}
		
		public IEnumerator<IDisposable> GetEnumerator ()
		{
			throw new NotImplementedException ();
		}
		
		public bool Remove (IDisposable item)
		{
			throw new NotImplementedException ();
		}
	}
}
