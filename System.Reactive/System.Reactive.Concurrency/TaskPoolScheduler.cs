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
			return Schedule (state, Now, action);
		}
		
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			var cancel = new CancellationTokenSource ();
			var dis = new MultipleAssignmentDisposable ();
			dis.Disposable = new CancellationDisposable (cancel);
			Task task = null;
			task = factory.StartNew<Unit> (() => {
				Thread.Sleep (Scheduler.Normalize (dueTime - Now));
				if (!task.IsCanceled)
					dis.Disposable = action (this, state);
				return Unit.Default;
				}, cancel.Token);
			return dis;
		}
		
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, Now + Scheduler.Normalize (dueTime), action);
		}
	}
}
