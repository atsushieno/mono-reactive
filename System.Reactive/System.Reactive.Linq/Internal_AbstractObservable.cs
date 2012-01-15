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
		
		bool disposed;
		
		public void Dispose ()
		{
			if (disposed)
				return;
			disposed = true;
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
	
	internal class ColdObservableEach<T> : IObservable<T>
	{
		IScheduler scheduler;
		Func<ISubject<T>, IDisposable> work;
		
		public ColdObservableEach (Func<ISubject<T>, IDisposable> work, IScheduler scheduler)
		{
			this.work = work;
			this.scheduler = scheduler;
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			var sub = new ReplaySubject<T> ();
			var subdis = sub.Subscribe (observer);
			var workdis = new SingleAssignmentDisposable ();
			var dis = scheduler.Schedule (() => workdis.Disposable = work (sub));
			return Disposable.Create (() => {
				workdis.Dispose ();
				dis.Dispose ();
				subdis.Dispose ();
			});
		}
	}
	
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
			var dis = new SingleAssignmentDisposable ();
			scheduler.Schedule (() => dis.Disposable = source.Subscribe (observer));
			return new ScheduledDisposable (scheduler, dis);
		}
	}

	// observations are handled on the specified scheduler.
	internal class SchedulerBoundSubject<T> : ISubject<T>
	{
		IScheduler scheduler;
		ISubject<T> sub = new Subject<T> ();
		
		public SchedulerBoundSubject (IScheduler scheduler)
		{
			this.scheduler = scheduler;
		}

		public void OnNext (T value)
		{
			var dis = new SingleAssignmentDisposable ();
			dis.Disposable = scheduler.Schedule (() => { sub.OnNext (value); dis.Dispose (); });
		}

		public void OnError (Exception error)
		{
			var dis = new SingleAssignmentDisposable ();
			dis.Disposable = scheduler.Schedule (() => { sub.OnError (error); dis.Dispose (); });
		}

		public void OnCompleted ()
		{
			var dis = new SingleAssignmentDisposable ();
			dis.Disposable = scheduler.Schedule (() => { sub.OnCompleted (); dis.Dispose (); });
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			return sub.Subscribe (observer);
		}
	}
}
