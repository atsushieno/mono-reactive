using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public class HistoricalScheduler : HistoricalSchedulerBase
	{
		List<ScheduledItem<DateTimeOffset>> tasks = new List<ScheduledItem<DateTimeOffset>> ();
		
		protected override IScheduledItem<DateTimeOffset> GetNext ()
		{
			return tasks.FirstOrDefault ();
		}
		
		public override IDisposable ScheduleAbsolute<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			ScheduledItemImpl<DateTimeOffset> t = null;
			t = new ScheduledItemImpl<DateTimeOffset> (dueTime, () => { tasks.Remove (t); return action (this, state); });
			
			Scheduler.AddTask (tasks, t);
			
			return new CompositeDisposable (Disposable.Create (() => tasks.Remove (t)), t);
		}
	}
}
