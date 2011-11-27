using System;

namespace System.Reactive.Concurrency
{
	public class HistoricalScheduler : HistoricalSchedulerBase
	{
		protected override IScheduledItem<DateTimeOffset> GetNext ()
		{
			throw new NotImplementedException ();
		}
		
		public override IDisposable ScheduleAbsolute<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
		}
	}
}
