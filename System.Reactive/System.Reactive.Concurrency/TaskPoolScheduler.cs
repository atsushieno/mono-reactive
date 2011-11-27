using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Reactive.Concurrency
{
	public sealed class TaskPoolScheduler : IScheduler
	{
		public TaskPoolScheduler (TaskFactory taskFactory)
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
			throw new NotImplementedException ();
		}
		
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, Scheduler.Now + Scheduler.Normalize (dueTime), action);
		}
	}
}
