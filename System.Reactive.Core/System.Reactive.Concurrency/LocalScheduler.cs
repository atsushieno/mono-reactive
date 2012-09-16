using System;
using System.Reactive.PlatformServices;

namespace System.Reactive.Concurrency
{
	public abstract class LocalScheduler : IScheduler, IStopwatchProvider
#if REACTIVE_2_0
		, IServiceProvider
#endif
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

#if REACTIVE_2_0
		static PeriodicTimerSystemClockMonitor timer_clock_monitor = new PeriodicTimerSystemClockMonitor (TimeSpan.FromSeconds (10));

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
#endif
	}
}

