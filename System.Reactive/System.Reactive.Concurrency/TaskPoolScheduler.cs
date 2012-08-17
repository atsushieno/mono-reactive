using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
#if REACTIVE_2_0
	public sealed class TaskPoolScheduler : LocalScheduler, ISchedulerLongRunning, ISchedulerPeriodic
#else
	public sealed class TaskPoolScheduler : IScheduler
#endif
	{
		static readonly TaskPoolScheduler instance = new TaskPoolScheduler (new TaskFactory ());
#if REACTIVE_2_0
		public
#else
		internal
#endif
		static TaskPoolScheduler Default {
			get { return instance; }
		}
		
		TaskFactory factory;

		public TaskPoolScheduler (TaskFactory taskFactory)
		{
			if (taskFactory == null)
				throw new ArgumentNullException ("taskFactory");
			this.factory = taskFactory;
		}
		
#if !REACTIVE_2_0
		public DateTimeOffset Now {
			get { return Scheduler.Now; }
		}
		
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, dueTime - Now, action);
		}
#endif

#if REACTIVE_2_0
		public override IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#else
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#endif
		{
			var cancel = new CancellationTokenSource ();
			var dis = new MultipleAssignmentDisposable ();
			dis.Disposable = new CancellationDisposable (cancel);
			Task task = null;
			task = factory.StartNew<Unit> (() => {
				Thread.Sleep (Scheduler.Normalize (dueTime));
				if (!task.IsCanceled)
					dis.Disposable = action (this, state);
				return Unit.Default;
				}, cancel.Token);
			return dis;
		}
		
#if REACTIVE_2_0
		public override IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
#else
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
#endif
		{
			return Schedule (state, TimeSpan.Zero, action);
		}
		
#if REACTIVE_2_0
		public override IStopwatch StartStopwatch ()
		{
			throw new NotImplementedException ();
		}

		public IDisposable ScheduleLongRunning<TState> (TState state, Action<TState, ICancelable> action)
		{
			throw new NotImplementedException ();
		}

		public IDisposable SchedulePeriodic<TState> (TState state, TimeSpan period, Func<TState, TState> action)
		{
			throw new NotImplementedException ();
		}
#endif
	}
}
