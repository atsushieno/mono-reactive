using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading;

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
		IDisposable scheduler_disposable;
		ReplaySubject<T> subject;

		public ReplaySubject<T> Subject {
			get { return subject; }
		}
		
		public HotObservable (Action<ReplaySubject<T>> work, IScheduler scheduler)
		{
			subject = new ReplaySubject<T> (scheduler);
			scheduler_disposable = scheduler.Schedule (() => work (subject));
		}
		
		public void Dispose ()
		{
			if (scheduler_disposable != null)
				scheduler_disposable.Dispose ();
			var dis = Subject as IDisposable;
			if (dis != null)
				dis.Dispose ();
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			return Subject.Subscribe (observer);
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
	
	/* Observable implementation for Interval() :
		- cold observable
		- Notifies "current count" to *each* observer i.e. this
		  observable holds different count numbers to the observers.

		example of different counts:
		
		var interval = Observable.Interval(TimeSpan.FromMilliseconds(250));
		Thread.Sleep(3000);
		interval.Subscribe(Console.WriteLine);
		Thread.Sleep(3000);
		interval.Subscribe((s) => Console.WriteLine ("x " + s)); 

	*/
	internal class IntervalObservable : IObservable<long>
	{
		List<KeyValuePair<IObserver<long>, ISubject<long>>> subjects = new List<KeyValuePair<IObserver<long>, ISubject<long>>> ();
		
		TimeSpan interval;
		IScheduler scheduler;
		
		public IntervalObservable (TimeSpan interval, IScheduler scheduler)
		{
			this.interval = interval;
			this.scheduler = scheduler;
		}

		public IDisposable Subscribe (IObserver<long> observer)
		{
			var sub = new Subject<long> ();
			sub.Subscribe (observer);
			subjects.Add (new KeyValuePair<IObserver<long>, ISubject<long>> (observer, sub));
			scheduler.Schedule (() => {
				try {
					int count = 0;
					while (true) {
						Thread.Sleep (interval);
						sub.OnNext (count++);
					}
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				});
			return Disposable.Create (() => {
				subjects.Remove (subjects.First (p => p.Value == sub));
				sub.Dispose ();
			});
		}
		
		public void Dispose ()
		{
			subjects.Clear ();
		}
	}
}
