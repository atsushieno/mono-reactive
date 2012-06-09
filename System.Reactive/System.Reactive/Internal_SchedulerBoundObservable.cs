using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace System.Reactive
{
	class SchedulerBoundObservable<T> : IObservable<T>
	{
		public SchedulerBoundObservable (IObservable<T> source, IScheduler scheduler)
		{
			this.source = source;
			this.scheduler = scheduler;
		}
		IObservable<T> source;
		IScheduler scheduler;
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			return source.Subscribe (new SchedulerBoundObserver<T> (observer, scheduler));
		}
	}
	
	class SchedulerBoundObserver<T> : IObserver<T>
	{
		IObserver<T> observer;
		IScheduler scheduler;
		
		public SchedulerBoundObserver (IObserver<T> observer, IScheduler scheduler)
		{
			this.observer = observer;
			this.scheduler = scheduler;
		}
		
		public void OnNext (T value)
		{
			var dis = new SingleAssignmentDisposable ();
			dis.Disposable = scheduler.Schedule (() => { observer.OnNext (value); dis.Dispose (); });
		}
		
		public void OnError (Exception error)
		{
			var dis = new SingleAssignmentDisposable ();
			dis.Disposable = scheduler.Schedule (() => { observer.OnError (error); dis.Dispose (); });
		}
		
		public void OnCompleted ()
		{
			var dis = new SingleAssignmentDisposable ();
			dis.Disposable = scheduler.Schedule (() => { observer.OnCompleted (); dis.Dispose (); });
		}
	}
}

