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
	internal class ColdObservableEach<T> : IObservable<T>
	{
		IScheduler scheduler;
		Func<ISubject<T>, IDisposable> work;
		Func<ISubject<T>> subject_creator;
		
		public ColdObservableEach (Func<ISubject<T>, IDisposable> work, IScheduler scheduler)
			: this (work, scheduler, () => new Subject<T> ())
		{
		}
		
		public ColdObservableEach (Func<ISubject<T>, IDisposable> work, IScheduler scheduler, Func<ISubject<T>> subjectCreator)
		{
			this.work = work;
			this.scheduler = scheduler;
			subject_creator = subjectCreator;
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			var sub = subject_creator ();
			var dis = new CompositeDisposable ();
			dis.Add (sub.Subscribe (observer));
			dis.Add (scheduler.Schedule (() => dis.Add (work (sub))));
			return dis;
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
		ISubject<T> sub = new ReplaySubject<T> ();
		
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
