using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace System.Reactive.Subjects
{
	// see http://leecampbell.blogspot.com/2010/05/intro-to-rx.html
	public sealed class ReplaySubject<T>
		: ISubject<T>, ISubject<T, T>, IObserver<T>, IObservable<T>, IDisposable
	{
		const int default_buffer_size = 10;
		
		public ReplaySubject ()
			: this (default_buffer_size)
		{
		}

		public ReplaySubject (int bufferSize)
			: this (bufferSize, TimeSpan.Zero)
		{
		}

		public ReplaySubject (TimeSpan window)
			: this (default_buffer_size, window)
		{
		}

		public ReplaySubject (IScheduler scheduler)
			: this (TimeSpan.Zero, scheduler)
		{
		}

		public ReplaySubject (int bufferSize, IScheduler scheduler)
			: this (bufferSize, TimeSpan.Zero, scheduler)
		{
		}

		public ReplaySubject (int bufferSize, TimeSpan window)
		{
			notifications = new List<Notification<T>> (bufferSize);
			this.window = window;
		}

		public ReplaySubject (TimeSpan window, IScheduler scheduler)
			: this (default_buffer_size, window, scheduler)
		{
		}

		public ReplaySubject (int bufferSize, TimeSpan window, IScheduler scheduler)
			: this (bufferSize, window)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			this.scheduler = scheduler;
		}

		bool disposed;
		bool done;
		TimeSpan window;
		// For use of CurrentThread, see http://social.msdn.microsoft.com/Forums/en-AU/rx/thread/e032b40a-019b-496e-bb11-64c8fcc94410
		IScheduler scheduler = Scheduler.CurrentThread;

		public void Dispose ()
		{
			disposed = true;
		}
		
		void CheckDisposed ()
		{
			if (disposed)
				throw new ObjectDisposedException ("subject");
		}
		
		List<Notification<T>> notifications;
		
		public void OnCompleted ()
		{
			CheckDisposed ();
			if (!done) {
				done = true;
				var n = Notification.CreateOnCompleted<T> ();
				notifications.Add (n);
				Schedule (() => observers.ForEach ((o) => n.Accept (o)));
			}
		}
		
		public void OnError (Exception error)
		{
			CheckDisposed ();
			if (!done) {
				done = true;
				var n = Notification.CreateOnError<T> (error);
				notifications.Add (n);
				Schedule (() => observers.ForEach ((o) => n.Accept (o)));
			}
		}
		
		public void OnNext (T value)
		{
			CheckDisposed ();
			if (!done) {
				var n = Notification.CreateOnNext<T> (value);
				notifications.Add (n);
				Schedule (() => observers.ForEach ((o) => n.Accept (o)));
			}
		}
		
		List<IObserver<T>> observers = new List<IObserver<T>> ();
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			CheckDisposed ();
			observers.Add (observer);
			Schedule (() => notifications.ForEach ((n) => n.Accept (observer)));
			return Disposable.Create (() => observers.Remove (observer));
		}

		void Schedule (Action action)
		{
			// FIXME: use window. Maybe with Observable.Window().
			scheduler.Schedule (action);
		}
	}
}
