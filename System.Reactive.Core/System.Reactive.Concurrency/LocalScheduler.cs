using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.PlatformServices;

namespace System.Reactive.Concurrency
{
#if REACTIVE_2_0
	public abstract class LocalScheduler : IScheduler, IStopwatchProvider, IServiceProvider
	{
		// I wondered if this had better become a static field, but since there is no way to unsubscribe
		// event handlers and those instances are rather likely created only once for each class (as
		// static Scheduler properties), I leave them as is.
		PeriodicTimerSystemClockMonitor timer_clock_monitor;

		protected LocalScheduler ()
		{
			timer_clock_monitor = new PeriodicTimerSystemClockMonitor (TimeSpan.FromSeconds (10));
			timer_clock_monitor.SystemClockChanged += (o, e) => {
				var l = new List<ScheduledItem<DateTimeOffset>> ();
				// TODO: cancel all existing tasks and re-register everything with new dueTime.
				throw new NotImplementedException ();
			};
		}

		public virtual IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, TimeSpan.Zero, action);
		}
		
		public virtual IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			ScheduledItem<DateTimeOffset> task = null;
			Func<IScheduler, TState, IDisposable> funcRemovingTask = (sch, stat) => { tasks.Remove (task); return action (sch, stat); };
			task = new ScheduledItemImpl<DateTimeOffset> (dueTime, () => funcRemovingTask (this, state));
			tasks.Add (task);
			var reldis = Schedule (state, dueTime - Now, funcRemovingTask);
			return Disposable.Create (() => { tasks.Remove (task); reldis.Dispose (); });
		}
		
		public abstract IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action);
		
		public DateTimeOffset Now {
			get { return SystemClock.UtcNow; }
		}

		public virtual IStopwatch StartStopwatch ()
		{
			throw new NotImplementedException ();
		}

		object IServiceProvider.GetService (Type serviceType)
		{
			return GetService (serviceType);
		}

		protected object GetService (Type serviceType)
		{
			if (serviceType == typeof (INotifySystemClockChanged))
				return timer_clock_monitor;

			return null;
		}

		List<ScheduledItem<DateTimeOffset>> tasks = new List<ScheduledItem<DateTimeOffset>> ();
	}
#else
	public abstract class LocalScheduler : IScheduler, IStopwatchProvider
	{
		protected LocalScheduler ()
		{
		}

		public virtual IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, TimeSpan.Zero, action);
		}
	
		public virtual IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, dueTime - Now, action);
		}
	
		public abstract IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action);
	
		public DateTimeOffset Now {
			get { return DateTimeOffset.Now; }
		}

		public virtual IStopwatch StartStopwatch ()
		{
			throw new NotImplementedException ();
		}
	}
#endif
}
