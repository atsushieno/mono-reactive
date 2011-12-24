using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;

namespace System.Reactive.Subjects
{
	public sealed class Subject<T>
		: ISubject<T>, ISubject<T, T>, IObserver<T>, IObservable<T>, IDisposable
	{
		public void Dispose ()
		{
			foreach (var s in subscribed) {
				var d = s as IDisposable;
				if (d != null)
					d.Dispose ();
			}
		}
		
		public void OnCompleted ()
		{
			foreach (var s in subscribed)
				s.OnCompleted ();
		}
		
		public void OnError (Exception error)
		{
			foreach (var s in subscribed)
				s.OnError (error);
		}
		
		public void OnNext (T value)
		{
			foreach (var s in subscribed)
				s.OnNext (value);
		}
		
		ConcurrentQueue<IObserver<T>> subscribed = new ConcurrentQueue<IObserver<T>> ();
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			subscribed.Enqueue (observer);
			throw new NotImplementedException ();
		}
	}
}
