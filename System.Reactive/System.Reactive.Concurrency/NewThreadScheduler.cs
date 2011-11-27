using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Reactive.Concurrency
{
	public sealed class NewThreadScheduler : IScheduler
	{
		public NewThreadScheduler ()
			: this ((t => new Thread (t)))
		{
		}
		
		public NewThreadScheduler (Func<ThreadStart, Thread> threadFactory)
		{
			if (threadFactory == null)
				throw new ArgumentNullException ("threadFactory");
			this.thread_factory = threadFactory;
		}
		
		Func<ThreadStart, Thread> thread_factory;
		
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
