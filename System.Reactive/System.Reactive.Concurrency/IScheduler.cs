using System;
using System.Collections.Generic;

namespace System.Reactive.Concurrency
{
	public interface IScheduler
	{
		DateTimeOffset Now { get; }
		IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action);
		IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action);
		IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action);
	}
}
