using System;

namespace System.Reactive.Concurrency
{
	public sealed class DefaultScheduler : LocalScheduler
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
	}
}

