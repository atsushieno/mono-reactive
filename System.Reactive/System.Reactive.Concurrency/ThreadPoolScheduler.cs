using System;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public sealed class ThreadPoolScheduler : IScheduler
	{
		internal ThreadPoolScheduler ()
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
			var dis = new SingleAssignmentDisposable ();
			ThreadPool.QueueUserWorkItem ((s) => {
				Thread.Sleep (Scheduler.Normalize (dueTime));
				dis.Disposable = action (this, (TState) s);
			});
			return dis;
		}
	}
}
