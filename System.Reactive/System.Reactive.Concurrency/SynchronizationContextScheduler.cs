using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public class SynchronizationContextScheduler : IScheduler
	{
		public SynchronizationContextScheduler (SynchronizationContext context)
		{
			if (context == null)
				throw new ArgumentNullException ("context");
			this.context = context;
		}
		
		SynchronizationContext context;
		
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
			context.Post (stat => {
				Thread.Sleep ((int) Scheduler.Normalize (dueTime).TotalMilliseconds);
				dis.Disposable = action (this, (TState) stat);
				}, default (TState));
			return dis;
		}
	}
}
