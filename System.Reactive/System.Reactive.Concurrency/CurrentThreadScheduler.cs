using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Reactive.Concurrency
{
	public sealed class CurrentThreadScheduler : IScheduler
	{
		internal CurrentThreadScheduler ()
		{
		}
		
		public bool ScheduleRequired {
			get { throw new NotImplementedException (); }
		}
		
		public DateTimeOffset Now {
			get { return Scheduler.Now; }
		}
		
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
		}
		
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
		}
		
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
		}
	}
}
