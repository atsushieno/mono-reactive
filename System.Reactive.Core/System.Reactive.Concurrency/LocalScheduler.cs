using System;

namespace System.Reactive.Concurrency
{
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
}

