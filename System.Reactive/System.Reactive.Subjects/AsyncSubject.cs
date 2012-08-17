using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace System.Reactive.Subjects
{
	// see http://leecampbell.blogspot.com/2010/05/intro-to-rx.html
	public sealed class AsyncSubject<T>
		: ISubject<T>, ISubject<T, T>, IObserver<T>, IObservable<T>, IDisposable
	{

		bool disposed;
		bool done;

		public void Dispose ()
		{
			disposed = true;
		}
		
		void CheckDisposed ()
		{
			if (disposed)
				throw new ObjectDisposedException ("subject");
		}
		
		Notification<T> n;
		
		public void OnCompleted ()
		{
			CheckDisposed ();
			if (!done) {
				done = true;
				if (n != null)
					observers.ForEach ((o) => n.Accept (o));
				var cmp = Notification.CreateOnCompleted<T> ();
				observers.ForEach ((o) => cmp.Accept (o));
			}
		}
		
		public void OnError (Exception error)
		{
			CheckDisposed ();
			if (!done) {
				done = true;
				n = Notification.CreateOnError<T> (error);
				observers.ForEach ((o) => n.Accept (o));
			}
		}
		
		public void OnNext (T value)
		{
			CheckDisposed ();
			if (!done)
				n = Notification.CreateOnNext<T> (value);
		}
		
		List<IObserver<T>> observers = new List<IObserver<T>> ();
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			if (observer == null)
				throw new ArgumentNullException ("observer");
			CheckDisposed ();
			observers.Add (observer);

			if (n != null && done)
				n.Accept (observer);

			return Disposable.Create (() => observers.Remove (observer));
		}

#if REACTIVE_2_0
		public T GetResult ()
		{
			throw new NotImplementedException ();
		}

		public bool IsCompleted {
			get { throw new NotImplementedException (); }
		}
#endif
	}
}
