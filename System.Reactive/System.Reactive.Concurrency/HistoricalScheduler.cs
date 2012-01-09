using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public class HistoricalScheduler : HistoricalSchedulerBase
	{
		SortedSet<IScheduledItem<DateTimeOffset>> tasks = new SortedSet<IScheduledItem<DateTimeOffset>> ();
		
		protected override IScheduledItem<DateTimeOffset> GetNext ()
		{
			return tasks.FirstOrDefault ();
		}
		
		public override IDisposable ScheduleAbsolute<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			ScheduledItem<DateTimeOffset> t = null;
			t = new ScheduledItem<DateTimeOffset> (dueTime, () => { tasks.Remove (t); return action (this, state); });
			tasks.Add (t);
			return new CompositeDisposable (Disposable.Create (() => tasks.Remove (t)), t);
		}
	}
}
