using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
	internal abstract class AbstractObservable<T> : IObservable<T>
	{
		List<IObserver<T>> observers = new List<IObserver<T>> ();
		
		bool disposed;

		public IEnumerable<IObserver<T>> Observers {
			get { return observers; }
		}

		public virtual void Dispose ()
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
	
	internal class HotObservable<T> : IObservable<T>
	{
		Func<T> func;
		IDisposable scheduler_disposable;
		ReplaySubject<T> subject;

		public HotObservable (Func<T> func, IScheduler scheduler)
		{
			subject = new ReplaySubject<T> (scheduler);
			
			scheduler_disposable = scheduler.Schedule (() => {
				try {
					var ret = func ();
					subject.OnNext (ret);
					subject.OnCompleted ();
				} catch (Exception ex) {
					subject.OnError (ex);
				}
			});
		}
		
		public void Dispose ()
		{
			if (scheduler_disposable != null) {
				scheduler_disposable.Dispose ();
				scheduler_disposable = null;
			}
			subject.Dispose ();
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			return subject.Subscribe (observer);
		}
	}
	
	/*
	internal class TimerObservable : AbstractObservable<long>
	{
		IScheduler scheduler;

		public TimerObservable (DateTimeOffset dueTime, IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			this.scheduler = scheduler;
		}
		
		public IScheduler Scheduler {
			get { return scheduler; }
		}
	}
	*/
	
	internal class TimeIntervalObservable<T> : AbstractObservable<TimeInterval<T>>
	{
		public TimeIntervalObservable (IObservable<T> source, IScheduler scheduler)
		{
		}
	}
}
