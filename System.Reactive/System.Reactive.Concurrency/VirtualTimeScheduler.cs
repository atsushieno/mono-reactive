using System;
using System.Collections.Generic;

namespace System.Reactive.Concurrency
{
	public abstract class VirtualTimeScheduler<TAbsolute, TRelative>
		: VirtualTimeSchedulerBase<TAbsolute, TRelative>
	{
		protected VirtualTimeScheduler ()
		{
		}
		
		protected VirtualTimeScheduler (TAbsolute initialClock, IComparer<TAbsolute> comparer)
			: base (initialClock, comparer)
		{
		}
		
		protected override IScheduledItem<TAbsolute> GetNext ()
		{
			throw new NotImplementedException ();
		}
		
		public override IDisposable ScheduleAbsolute<TState> (TState state, TAbsolute dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
		}
	}
}
