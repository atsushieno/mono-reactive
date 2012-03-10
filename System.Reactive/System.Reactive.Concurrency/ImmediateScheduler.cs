using System;
using System.Reactive.Disposables;
using System.Threading;

namespace System.Reactive.Concurrency
{
#if REACTIVE_2_0
	public sealed class ImmediateScheduler : LocalScheduler
#else
	public sealed class ImmediateScheduler : IScheduler
#endif
	{
		static readonly ImmediateScheduler instance = new ImmediateScheduler ();
#if REACTIVE_2_0
		public
#else
		internal
#endif
		static ImmediateScheduler Instance {
			get { return instance; }
		}
		
		internal ImmediateScheduler ()
		{
		}
		
#if !REACTIVE_2_0
		public DateTimeOffset Now {
			get { return Scheduler.Now; }
		}
		
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, dueTime - Now, action);
		}
#endif
		
#if REACTIVE_2_0
		public override IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#else
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#endif
		{
			Thread.Sleep ((int) Scheduler.Normalize (dueTime).TotalMilliseconds);
			return action (this, state);
		}
		
#if REACTIVE_2_0
		public override IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
#else
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
#endif
		{
			return Schedule (state, TimeSpan.Zero, action);
		}
	}
}
