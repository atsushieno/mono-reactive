using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public class HistoricalScheduler
#if REACTIVE_2_0
		: HistoricalSchedulerBase, IServiceProvider, IStopwatchProvider
#else
		: HistoricalSchedulerBase
#endif
	{
#if REACTIVE_2_0
		public HistoricalScheduler ()
		{
		}

		public HistoricalScheduler (DateTimeOffset initialClock)
			: base (initialClock, Comparer<DateTimeOffset>.Default)
		{
		}

		public HistoricalScheduler (DateTimeOffset initialClock, IComparer<DateTimeOffset> comparer)
			: base (initialClock, comparer)
		{
		}
#endif
		
		List<ScheduledItem<DateTimeOffset>> tasks = new List<ScheduledItem<DateTimeOffset>> ();
		
		protected override IScheduledItem<DateTimeOffset> GetNext ()
		{
			return tasks.FirstOrDefault ();
		}

		public override IDisposable ScheduleAbsolute<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			ScheduledItemImpl<DateTimeOffset> t = null;
			t = new ScheduledItemImpl<DateTimeOffset> (dueTime, () => { tasks.Remove (t); return action (this, state); });
			
			InternalAddTask (tasks, t);
			
			return new CompositeDisposable (Disposable.Create (() => tasks.Remove (t)), t);
		}
		
		internal static void InternalAddTask (IList<ScheduledItem<DateTimeOffset>> tasks, ScheduledItem<DateTimeOffset> task)
		{
			// It is most likely appended in order, so don't use ineffective List.Sort(). Simple comparison makes it faster.
			// Also, it is important that events are processed *in order* when they are scheduled at the same moment.
			int pos = -1;
			DateTimeOffset dueTime = task.DueTime;
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
