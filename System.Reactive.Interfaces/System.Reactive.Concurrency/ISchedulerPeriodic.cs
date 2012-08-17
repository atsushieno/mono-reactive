using System;

namespace System.Reactive.Concurrency
{
	public interface ISchedulerPeriodic
	{
		IDisposable SchedulePeriodic<TState> (TState state, TimeSpan period, Func<TState,TState> action);
	}
}

