using System;

namespace System.Reactive.Concurrency
{
	public sealed class DefaultScheduler : LocalScheduler, ISchedulerPeriodic
	{
		static readonly DefaultScheduler instance = new DefaultScheduler ();

		public static DefaultScheduler Instance {
			get { return instance; }
		}
		
		internal DefaultScheduler ()
		{
		}

		public override IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, TimeSpan.Zero, action);
		}
	
		public override IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
		}

#if REACTIVE_2_0
		public IDisposable SchedulePeriodic<TState> (TState state, TimeSpan period, Func<TState, TState> action)
		{
			throw new NotImplementedException ();
		}

		public object GetService (Type serviceType)
		{
			throw new NotImplementedException ();
		}
#endif
	}
}

