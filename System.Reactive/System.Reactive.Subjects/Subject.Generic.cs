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
		
		ConcurrentQueue<Notification<T>> notifications = new ConcurrentQueue<Notification<T>> ();
		
		public void OnCompleted ()
		{
			CheckDisposed ();
			if (subscribed.Count > 0) {
				if (!done)
					foreach (var s in subscribed)
						s.OnCompleted ();
				done = true;
			} else {
				var n = Notification.CreateOnCompleted<T> ();
				if (!notifications.Contains (n))
					notifications.Enqueue (n);
			}
		}
		
		public void OnError (Exception error)
		{
			CheckDisposed ();
			if (subscribed.Count > 0) {
				if (!done)
					foreach (var s in subscribed)
						s.OnError (error);
				done = true;
			} else {
				var n = Notification.CreateOnError<T> (error);
				if (!notifications.Contains (n))
					notifications.Enqueue (n);
			}
		}
		
		public void OnNext (T value)
		{
			CheckDisposed ();
			if (subscribed.Count > 0) {
				if (!done)
					foreach (var s in subscribed)
						s.OnNext (value);
				done = true;
			} else {
				var n = Notification.CreateOnNext<T> (value);
				if (!notifications.Contains (n))
					notifications.Enqueue (n);
			}
		}
		
		List<IObserver<T>> subscribed = new List<IObserver<T>> ();
		
		// The returned IDisposable unregisters the observer when Dispose() is invoked.
		public IDisposable Subscribe (IObserver<T> observer)
		{
			CheckDisposed ();

			// If there was registered events (OnCompleted/OnError/OnNext), they are dequeued and handled here.
			while (notifications.Count > 0) {
				Notification<T> n;
				if (notifications.TryDequeue (out n))
					n.Accept (observer);
			}

			subscribed.Add (observer);
			return Disposable.Create (() => subscribed.Remove (observer));
		}
	}
}
