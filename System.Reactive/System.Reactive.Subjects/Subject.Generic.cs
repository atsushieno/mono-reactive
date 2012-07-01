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
		
		Queue<Notification<T>> notifications = new Queue<Notification<T>> ();
		
		public void OnCompleted ()
		{
			CheckDisposed ();
			if (subscribed.Count > 0) {
				if (!done)
					// ToArray is to avoid InvalidOperationException when OnCompleted() unsubscribes item itself from the list.
					foreach (var s in subscribed.ToArray ())
						s.OnCompleted ();
				done = true;
			} else {
				if (!notifications.Any (n => n.Kind == NotificationKind.OnCompleted))
					notifications.Enqueue (Notification.CreateOnCompleted<T> ());
			}
		}
		
		public void OnError (Exception error)
		{
			CheckDisposed ();
			if (subscribed.Count > 0) {
				if (!done)
					// ToArray is to avoid InvalidOperationException when OnError() unsubscribes item itself from the list.
					foreach (var s in subscribed.ToArray ())
						s.OnError (error);
				done = true;
			} else {
				if (!notifications.Any (n => n.Kind == NotificationKind.OnError))
					notifications.Enqueue (Notification.CreateOnError<T> (error));
			}
		}
		
		public void OnNext (T value)
		{
			CheckDisposed ();
			if (subscribed.Count > 0) {
				if (!done)
					// ToArray is to avoid InvalidOperationException when OnNext() unsubscribes item itself from the list.
					foreach (var s in subscribed.ToArray ())
						s.OnNext (value);
			} else {
				var n = Notification.CreateOnNext<T> (value);
				notifications.Enqueue (n);
			}
		}
		
		List<IObserver<T>> subscribed = new List<IObserver<T>> ();
		
		// The returned IDisposable unregisters the observer when Dispose() is invoked.
		public IDisposable Subscribe (IObserver<T> observer)
		{
			CheckDisposed ();

			// If there were registered events (OnCompleted/OnError/OnNext), they are dequeued and handled here.
			if (notifications.Count > 0)
				lock (notifications)
					while (notifications.Count > 0)
						notifications.Dequeue ().Accept (observer);
			subscribed.Add (observer);
			return Disposable.Create (() => subscribed.Remove (observer));
		}
	}
}
