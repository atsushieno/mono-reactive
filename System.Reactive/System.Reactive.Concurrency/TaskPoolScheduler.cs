using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public sealed class TaskPoolScheduler : IScheduler
	{
		TaskFactory factory;

		public TaskPoolScheduler (TaskFactory taskFactory)
		{
			if (taskFactory == null)
				throw new ArgumentNullException ("taskFactory");
			this.factory = taskFactory;
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
			IDisposable disposable = null;
			var task = factory.StartNew<Unit> (() => {
				var sleep = Scheduler.Normalize (dueTime - Now);
				Thread.Sleep (sleep);
				var dis = action (this, state);
				dis.Dispose ();
				return Unit.Default;
				});
			return Disposable.Create (() => (disposable ?? Disposable.Empty).Dispose ());
		}
		
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, Scheduler.Now + Scheduler.Normalize (dueTime), action);
		}
	}
}
