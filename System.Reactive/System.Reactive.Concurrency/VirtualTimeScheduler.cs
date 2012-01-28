using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public abstract class VirtualTimeScheduler<TAbsolute, TRelative>
		: VirtualTimeSchedulerBase<TAbsolute, TRelative>
	{
		IComparer<IScheduledItem<TAbsolute>> comparer = new ScheduledItem<TAbsolute>.Comparer (Comparer<TAbsolute>.Default);

		protected VirtualTimeScheduler ()
		{
			tasks = new List<IScheduledItem<TAbsolute>> ();
		}
		
		protected VirtualTimeScheduler (TAbsolute initialClock, IComparer<TAbsolute> comparer)
			: base (initialClock, comparer)
		{
			tasks = new List<IScheduledItem<TAbsolute>> ();
		}
		
		List<IScheduledItem<TAbsolute>> tasks;
		
		protected override IScheduledItem<TAbsolute> GetNext ()
		{
			return tasks.FirstOrDefault ();
		}
		
		public override IDisposable ScheduleAbsolute<TState> (TState state, TAbsolute dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			ScheduledItem<TAbsolute> t = null;
			t = new ScheduledItem<TAbsolute> (dueTime, () => { tasks.Remove (t); return action (this, state); });
			tasks.Add (t);
			tasks.Sort (comparer);
			return Disposable.Create (() => { tasks.Remove (t); t.Dispose (); });
		}
	}
}
