using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public abstract class VirtualTimeScheduler<TAbsolute, TRelative>
		: VirtualTimeSchedulerBase<TAbsolute, TRelative>
	{
		protected VirtualTimeScheduler ()
		{
			tasks = new SortedSet<IScheduledItem<TAbsolute>> ();
		}
		
		protected VirtualTimeScheduler (TAbsolute initialClock, IComparer<TAbsolute> comparer)
			: base (initialClock, comparer)
		{
			tasks = new SortedSet<IScheduledItem<TAbsolute>> (new ScheduledItem<TAbsolute>.Comparer (comparer));
		}
		
		SortedSet<IScheduledItem<TAbsolute>> tasks;
		
		protected override IScheduledItem<TAbsolute> GetNext ()
		{
			return tasks.FirstOrDefault ();
		}
		
		public override IDisposable ScheduleAbsolute<TState> (TState state, TAbsolute dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			ScheduledItem<TAbsolute> t = null;
			t = new ScheduledItem<TAbsolute> (dueTime, () => { tasks.Remove (t); return action (this, state); });
			tasks.Add (t);
			return Disposable.Create (() => { tasks.Remove (t); t.Dispose (); });
		}
	}
}
