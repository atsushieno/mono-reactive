using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public sealed class CompositeDisposable
		: ICollection<IDisposable>, IEnumerable<IDisposable>, IEnumerable, IDisposable
	{
		// FIXME: not sure if simple stupid List is applicable...
		List<IDisposable> items;
		
		public CompositeDisposable (IEnumerable<IDisposable> disposables)
		{
			items = new List<IDisposable> (disposables);
		}
		
		public CompositeDisposable (params IDisposable[] disposables)
		{
			if (disposables == null)
				throw new ArgumentNullException ("disposables");
			if (disposables.Any (d => d == null))
				throw new ArgumentNullException ("disposables", "Argument disposable parameter contains null");
			items = new List<IDisposable> (disposables);
		}
		
		public CompositeDisposable (int capacity)
		{
			items = new List<IDisposable> (capacity);
		}
		
		public int Count {
			get { return items.Count (); }
		}
		
		public bool IsReadOnly {
			get { throw new NotImplementedException (); }
		}
		
		IEnumerator IEnumerable.GetEnumerator ()
		{
			foreach (var i in items)
				yield return i;
		}
		
		public void Add (IDisposable item)
		{
			if (item == null)
				throw new ArgumentNullException ("item");
			if (disposed)
				item.Dispose ();
			else
				items.Add (item);
		}
		
		public void Clear ()
		{
			items.Clear ();
		}
		
		public bool Contains (IDisposable item)
		{
			return items.Contains (item);
		}
		
		public void CopyTo (IDisposable [] array, int arrayIndex)
		{
			items.CopyTo (array, arrayIndex);
		}
		
		bool disposed;
		
		public void Dispose ()
		{
			if (disposed)
				return;
			disposed = true;
			foreach (var item in items)
				item.Dispose ();
			items.Clear ();
		}
		
		public IEnumerator<IDisposable> GetEnumerator ()
		{
			foreach (var i in items)
				yield return i;
		}
		
		public bool Remove (IDisposable item)
		{
			return items.Remove (item);
		}
	}
}
