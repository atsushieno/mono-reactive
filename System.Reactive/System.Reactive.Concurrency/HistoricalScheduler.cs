using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public class HistoricalScheduler : HistoricalSchedulerBase, IServiceProvider, IStopwatchProvider
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
			
			Scheduler.InternalAddTask (tasks, t);
			
			return new CompositeDisposable (Disposable.Create (() => tasks.Remove (t)), t);
		}
	}
}
