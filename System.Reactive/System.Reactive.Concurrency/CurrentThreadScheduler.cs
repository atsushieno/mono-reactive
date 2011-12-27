using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reactive.Disposables;

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
			return Schedule<TState> (state, Scheduler.Now, action);
		}
		
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, dueTime - Now, action);
		}
		
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			IDisposable dis = null;
			// FIXME: this should not depend on SynchronizationContext.Current
			if (SynchronizationContext.Current == null)
				SynchronizationContext.SetSynchronizationContext (new SynchronizationContext ());
			SynchronizationContext.Current.Post ((stat) => {
				Thread.Sleep ((int) Scheduler.Normalize (dueTime).TotalMilliseconds);
				dis = action (this, (TState) stat);
				}, default (TState));
			return Disposable.Create (() => { if (dis != null) dis.Dispose (); });
		}
	}
}
