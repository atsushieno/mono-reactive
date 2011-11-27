using System;
using System.Collections.Generic;

namespace System.Reactive.Concurrency
{
	public abstract class VirtualTimeScheduler<TAbsolute, TRelative>
		: VirtualTimeSchedulerBase<TAbsolute, TRelative>
	{
		protected VirtualTimeScheduler ()
		{
			throw new NotImplementedException ();
		}
		
		protected VirtualTimeScheduler (TAbsolute initialClock, IComparer<TAbsolute> comparer)
		{
			throw new NotImplementedException ();
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
