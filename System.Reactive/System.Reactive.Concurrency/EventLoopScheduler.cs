using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public sealed class EventLoopScheduler : IScheduler, IDisposable
	{
		public EventLoopScheduler ()
			: this ((ts) => new Thread (ts))
		{
		}
		
		public EventLoopScheduler (Func<ThreadStart, Thread> threadFactory)
		{
			if (threadFactory == null)
				throw new ArgumentNullException ("threadFactory");
			thread_factory = threadFactory;
		}
		
		Func<ThreadStart, Thread> thread_factory;
		
		public void Dispose ()
		{
			throw new NotImplementedException ();
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
			IDisposable dis = null;
			var th = thread_factory (() => {
				Thread.Sleep (Scheduler.Normalize (dueTime - Now));
				dis = action (this, state);
				});
			th.Start ();
			// The thread is not aborted even if it's at work (ThreadAbortException is not caught inside the action).
			// FIXME: this should *always* dispose "dis" instance that is returned by the action even after disposable of this instance (action starts regardless of this).
			return Disposable.Create (() => { if (dis != null) dis.Dispose (); });
		}
		
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, Scheduler.Now + dueTime, action);
		}
	}
}
