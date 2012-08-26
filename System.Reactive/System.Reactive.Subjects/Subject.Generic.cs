using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace System.Reactive.Subjects
{
	// see http://leecampbell.blogspot.com/2010/05/intro-to-rx.html
	public sealed class Subject<T>
		: ISubject<T>, ISubject<T, T>, IObserver<T>, IObservable<T>, IDisposable
	{
		bool done;
		bool disposed;

		public void Dispose ()
		{
			disposed = true;
		}
		
		void CheckDisposed ()
		{
			if (disposed)
				throw new ObjectDisposedException ("subject");
		}
		
		public void OnCompleted ()
		{
			CheckDisposed ();
			if (!done)
				// ToArray is to avoid InvalidOperationException when OnCompleted() unsubscribes item itself from the list.
				foreach (var s in subscribed.ToArray ())
					s.OnCompleted ();
			done = true;
		}
		
		public void OnError (Exception error)
		{
			CheckDisposed ();
			if (!done)
				// ToArray is to avoid InvalidOperationException when OnError() unsubscribes item itself from the list.
				foreach (var s in subscribed.ToArray ())
					s.OnError (error);
			done = true;
		}
		
		public void OnNext (T value)
		{
			CheckDisposed ();
			if (!done)
				// ToArray is to avoid InvalidOperationException when OnNext() unsubscribes item itself from the list.
				foreach (var s in subscribed.ToArray ())
					s.OnNext (value);
		}
		
		List<IObserver<T>> subscribed = new List<IObserver<T>> ();
		
		// The returned IDisposable unregisters the observer when Dispose() is invoked.
		public IDisposable Subscribe (IObserver<T> observer)
		{
			CheckDisposed ();

			// If there were registered events (OnCompleted/OnError/OnNext), they are dequeued and handled here.
			subscribed.Add (observer);
			return Disposable.Create (() => subscribed.Remove (observer));
		}
	}
}
