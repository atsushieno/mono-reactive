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
	
	internal class ColdObservableEach<T> : IObservable<T>
	{
		IScheduler scheduler;
		Action<ISubject<T>> work;
		
		public ColdObservableEach (Action<ISubject<T>> work, IScheduler scheduler)
		{
			this.work = work;
			this.scheduler = scheduler;
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			var sub = new Subject<T> ();
			sub.Subscribe (observer);
			scheduler.Schedule (() => work (sub));
			return Disposable.Create (() => {
				sub.Dispose ();
			});
		}
		
		public void Dispose ()
		{
		}
	}
	
	/*
	internal class ColdObservableOnce<T> : IObservable<T>
	{
		IObservable<T> inner;
		Action start;
		IScheduler scheduler;
		bool started;

		public ColdObservableOnce (IObservable<T> inner, Action start, IScheduler scheduler)
		{
			this.inner = inner;
			this.start = start;
			this.scheduler = scheduler;
		}
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			if (!started) {
				started = true;
				scheduler.Schedule (start);
			}
			return inner.Subscribe (observer);
		}
	}
	*/
	
	internal class WrappedSubject<T> : ISubject<T>
	{
		ISubject<T> inner;
		IDisposable disposable;
		
		public WrappedSubject (ISubject<T> inner, IDisposable disposable)
		{
			this.inner = inner;
			this.disposable = disposable;
		}
		
		public void Dispose ()
		{
			disposable.Dispose ();
		}
		
		public void OnNext (T value)
		{
			inner.OnNext (value);
		}
		
		public void OnError (Exception ex)
		{
			inner.OnError (ex);
		}
		
		public void OnCompleted ()
		{
			inner.OnCompleted ();
		}
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			return inner.Subscribe (observer);
		}
	}

	// subscription and unsubscription is handled on the specified scheduler.
	internal class SchedulerBoundObservable<T> : IObservable<T>
	{
		IObservable<T> source;
		IScheduler scheduler;
		
		public SchedulerBoundObservable (IObservable<T> source, IScheduler scheduler)
		{
			this.source = source;
			this.scheduler = scheduler;
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			IDisposable dis = null;
			scheduler.Schedule (() => dis = source.Subscribe (observer));
			return Disposable.Create (() => { if (dis != null) scheduler.Schedule (() => dis.Dispose ()); });
		}
	}
}
