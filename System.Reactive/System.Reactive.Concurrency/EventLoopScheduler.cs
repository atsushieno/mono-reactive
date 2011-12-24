using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Reactive.Concurrency
{
	public sealed class EventLoopScheduler : IScheduler, IDisposable
	{
		public EventLoopScheduler ()
			: this ((ts) => new Thread (ts))
		{
		}
		
		Thread th;
		
		public EventLoopScheduler (Func<ThreadStart, Thread> threadFactory)
		{
			if (threadFactory == null)
				throw new ArgumentNullException ("threadFactory");
			thread_factory = threadFactory;
		}
		
		Func<ThreadStart, Thread> thread_factory;
		Thread thread;
		
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
			throw new NotImplementedException ();
		}
		
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, Scheduler.Now + Scheduler.Normalize (dueTime), action);
		}
	}
}
