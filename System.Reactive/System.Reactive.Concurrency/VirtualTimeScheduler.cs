using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public abstract class VirtualTimeScheduler<TAbsolute, TRelative>
		: VirtualTimeSchedulerBase<TAbsolute, TRelative>
		where TAbsolute : IComparable<TAbsolute> // strictly to say, this is not in Rx1, but it must be anyways.
	{
		IComparer<TAbsolute> comparer = Comparer<TAbsolute>.Default;

		protected VirtualTimeScheduler ()
		{
			tasks = new List<ScheduledItem<TAbsolute>> ();
		}
		
		protected VirtualTimeScheduler (TAbsolute initialClock, IComparer<TAbsolute> comparer)
			: base (initialClock, comparer)
		{
			tasks = new List<ScheduledItem<TAbsolute>> ();
		}
		
		List<ScheduledItem<TAbsolute>> tasks;
		
		protected override IScheduledItem<TAbsolute> GetNext ()
		{
			return tasks.FirstOrDefault ();
		}
		
		public override IDisposable ScheduleAbsolute<TState> (TState state, TAbsolute dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			ScheduledItemImpl<TAbsolute> t = null;
			t = new ScheduledItemImpl<TAbsolute> (dueTime, () => { tasks.Remove (t); return action (this, state); });
			AddTask (tasks, t);
			return Disposable.Create (() => { tasks.Remove (t); t.Dispose (); });
		}

		void AddTask (IList<ScheduledItem<TAbsolute>> tasks, ScheduledItem<TAbsolute> task)
		{
			// It is most likely appended in order, so don't use ineffective List.Sort(). Simple comparison makes it faster.
			// Also, it is important that events are processed *in order* when they are scheduled at the same moment.
			int pos = -1;
			TAbsolute dueTime = task.DueTime;
			for (int i = tasks.Count - 1; i >= 0; i--) {
				if (comparer.Compare (dueTime, tasks [i].DueTime) >= 0) {
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
