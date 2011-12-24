using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace System.Reactive.Linq
{
	internal abstract class AbstractObservable<T> : IObservable<T>
	{
		IScheduler scheduler;
		List<IObserver<T>> observers = new List<IObserver<T>> ();
		
		protected AbstractObservable (IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			this.scheduler = scheduler;
		}

		public IScheduler Scheduler {
			get { return scheduler; }
		}
		
		bool disposed;

		public void Dispose ()
		{
			disposed = true;
		}
		
		void CheckDisposed ()
		{
			if (disposed)
				throw new ObjectDisposedException ("observable");
		}
		
		public virtual IDisposable Subscribe (IObserver<T> observer)
		{
			CheckDisposed ();
			observers.Add (observer);
			return Disposable.Create (() => observers.Remove (observer));
		}
	}
	
	internal class TimerObservable : AbstractObservable<long>
	{
		public TimerObservable (DateTimeOffset dueTime, IScheduler scheduler)
			: base (scheduler)
		{
		}
	}
	
	internal class TimeIntervalObservable<T> : AbstractObservable<TimeInterval<T>>
	{
		public TimeIntervalObservable (IObservable<T> source, IScheduler scheduler)
			: base (scheduler)
		{
		}
	}
}
