using System;

namespace System.Reactive.Interfaces
{
	public interface ISchedulerPeriodic
	{
		IDisposable SchedulePeriodic<TState> (TState state, TimeSpan period, Func<TState,TState> action);
	}
}

