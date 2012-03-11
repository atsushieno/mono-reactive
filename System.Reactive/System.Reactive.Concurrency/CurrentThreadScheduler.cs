using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
#if REACTIVE_2_0
	public sealed class CurrentThreadScheduler : LocalScheduler
#else
	public sealed class CurrentThreadScheduler : IScheduler
#endif
	{
		static readonly CurrentThreadScheduler instance = new CurrentThreadScheduler ();
#if REACTIVE_2_0
		public
#else
		internal
#endif
		static CurrentThreadScheduler Instance {
			get { return instance; }
		}
		
		internal CurrentThreadScheduler ()
		{
		}
		
		public bool ScheduleRequired {
			get { return busy > 0; }
		}
		
		int busy = 0;

		List<ScheduledItem<TimeSpan>> tasks = new List<ScheduledItem<TimeSpan>> ();
		
#if !REACTIVE_2_0
		public DateTimeOffset Now {
			get { return Scheduler.Now; }
		}
		
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, TimeSpan.Zero, action);
		}
		
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, dueTime - Now, action);
		}
#endif
		
#if REACTIVE_2_0
		public override IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#else
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#endif
		{
			// FIXME: this sort of needs to handle correct cancellation. (Though there's not likely many chances to "cancel" it...)
			var task = new ScheduledItemImpl<TimeSpan> (dueTime, () => action (this, state));
			if (Interlocked.CompareExchange (ref busy, busy, busy + 1) > 0) {
				AddTask (tasks, task);
				return Disposable.Create (() => tasks.Remove (task));
			} else {
				try {
					Thread.Sleep (Scheduler.Normalize (task.DueTime));
					task.Invoke ();
					while (true) {
						ScheduledItem<TimeSpan> t;
						lock (tasks) {
							t = (ScheduledItem<TimeSpan>) tasks.FirstOrDefault ();
							if (t == null)
								break;
							tasks.Remove (t);
						}
						Thread.Sleep (Scheduler.Normalize (t.DueTime));
						t.Invoke ();
					}
				} finally {
					busy--;
				}
				return Disposable.Empty;
			}
		}
		static void AddTask (IList<ScheduledItem<TimeSpan>> tasks, ScheduledItem<TimeSpan> task)
		{
			// It is most likely appended in order, so don't use ineffective List.Sort(). Simple comparison makes it faster.
			// Also, it is important that events are processed *in order* when they are scheduled at the same moment.
			int pos = -1;
			TimeSpan dueTime = task.DueTime;
			for (int i = tasks.Count - 1; i >= 0; i--) {
				if (dueTime >= tasks [i].DueTime) {
					tasks.Insert (i + 1, task);
					pos = i;
					break;
				}
			}
			if (pos < 0)
				tasks.Insert (0, task);
		}
	}
}
