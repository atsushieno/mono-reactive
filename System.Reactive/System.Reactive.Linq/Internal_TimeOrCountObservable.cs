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
	class TimeOrCountObservable : IObservable<Unit>
	{
		ISubject<Unit> subject = new Subject<Unit> ();
		TimeSpan interval;
		IScheduler scheduler;
		bool started, stop;
		AutoResetEvent wait;
		IObservable<Unit> counter;
		int threshold_count;
		int current_count;
		
		public TimeOrCountObservable (TimeSpan interval, IObservable<Unit> counter, int count, IScheduler scheduler)
		{
			this.interval = interval;
			this.counter = counter;
			this.threshold_count = count;
			this.scheduler = scheduler;
		}
		
		public IDisposable Subscribe (IObserver<Unit> observer)
		{
			var dis = new CompositeDisposable ();
			dis.Add (subject.Subscribe (observer));

			if (started)
				return dis;
			started = true;
			dis.Add (scheduler.Schedule (() => {
				wait = new AutoResetEvent (false);
				dis.Add (counter.Subscribe (Observer.Create<Unit> (u => { if (++current_count == threshold_count) wait.Set (); }, ex => subject.OnError (ex))));
				Tick ();
			}));
			return Disposable.Create (() => {
				stop = true;
				if (wait != null)
					wait.Set ();
				dis.Dispose ();
			});
		}
		
		void SubmitNext ()
		{
			subject.OnNext (Unit.Default);
			current_count = 0;
		}
		
		void Tick ()
		{
			wait.WaitOne (interval);
			if (stop)
				return;
			SubmitNext ();
			Tick (); // repeat
		}
	}
}
