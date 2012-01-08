using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		public static IEnumerable<TSource> Latest<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			
			return new LatestEnumerable<TSource> (source);
		}
		
		internal struct LatestEnumerable<TSource> : IEnumerable<TSource>
		{
			IObservable<TSource> source;
			
			internal LatestEnumerable (IObservable<TSource> source)
			{
				this.source = source;
			}
			
			IEnumerator IEnumerable.GetEnumerator ()
			{
				return new LatestEnumerator<TSource> (source);
			}
			
			public IEnumerator<TSource> GetEnumerator ()
			{
				return new LatestEnumerator<TSource> (source);
			}
		}
		
		internal class LatestEnumerator<TSource> : IEnumerator<TSource>, IEnumerator
		{
			int index = 0;
			TSource cur = default (TSource), snapshot = default (TSource);
			ManualResetEvent wait = new ManualResetEvent (false);
			bool running = true;
			Exception error = null;
			IDisposable dis;

			public LatestEnumerator (IObservable<TSource> source)
			{
				dis = source.Subscribe (v => { cur = v; index++; wait.Set (); }, ex => { error = ex; running = false; }, () => running = false);
			}

			object IEnumerator.Current {
				get { return cur; }
			}

			public TSource Current {
				get { return snapshot; }
			}
			
			public void Dispose ()
			{
				dis.Dispose ();
			}

			public bool MoveNext ()
			{
				if (!running)
					return false;
				
				wait.WaitOne ();
				if (error != null)
					throw error;
				if (!running)
					return false;
				wait.Reset ();
				snapshot = cur;
				return true;
			}
			
			public void Reset ()
			{
				throw new InvalidOperationException ();
			}
		}
	}
}
