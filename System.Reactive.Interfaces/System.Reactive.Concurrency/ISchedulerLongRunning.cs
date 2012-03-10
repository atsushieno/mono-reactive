using System;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public interface ISchedulerLongRunning
	{
		IDisposable ScheduleLongRunning<TState> (TState state, Action<TState, ICancelable> action);
	}
}

