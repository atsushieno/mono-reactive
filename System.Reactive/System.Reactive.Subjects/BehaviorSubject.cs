using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace System.Reactive.Subjects
{
	// see http://leecampbell.blogspot.com/2010/05/intro-to-rx.html
	public sealed class BehaviorSubject<T>
		: ISubject<T>, ISubject<T, T>, IObserver<T>, IObservable<T>, IDisposable
	{
		public BehaviorSubject (T value)
		{
			this.value = value;
		}

		bool has_value;
		bool disposed;
		bool done;
		T value;

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
			if (!done) {
				done = true;
				var n = Notification.CreateOnCompleted<T> ();
				observers.ForEach ((o) => n.Accept (o));
			}
		}
		
		public void OnError (Exception error)
		{
			CheckDisposed ();
			if (!done) {
				done = true;
				var n = Notification.CreateOnError <T> (error);
				observers.ForEach ((o) => n.Accept (o));
			}
		}
		
		public void OnNext (T value)
		{
			CheckDisposed ();
			has_value = true;
			if (!done) {
				var n = Notification.CreateOnNext<T> (value);
				observers.ForEach ((o) => n.Accept (o));
				this.value = value;
			}
		}
		
		List<IObserver<T>> observers = new List<IObserver<T>> ();
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			if (observer == null)
				throw new ArgumentNullException ("observer");
			CheckDisposed ();
			observers.Add (observer);
			
			if (!has_value)
				OnNext (value);

			return Disposable.Create (() => observers.Remove (observer));
		}
	}
}
