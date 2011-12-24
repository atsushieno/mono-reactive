using System;
using System.Reactive.Disposables;
using System.Threading;

namespace System.Reactive.Concurrency
{
	public sealed class ImmediateScheduler : IScheduler
	{
		internal ImmediateScheduler ()
		{
		}
		
		public DateTimeOffset Now {
			get { return Scheduler.Now; }
		}
		
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, Scheduler.Now, action);
		}
		
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, dueTime - Now, action);
		}
		
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			Thread.Sleep ((int) Scheduler.Normalize (dueTime).TotalMilliseconds);
			return action (this, state);
		}
	}
}
