using System;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
#if REACTIVE_2_0
	public sealed class ThreadPoolScheduler : LocalScheduler, ISchedulerLongRunning
#else
	public sealed class ThreadPoolScheduler : IScheduler
#endif
	{
		static readonly ThreadPoolScheduler instance = new ThreadPoolScheduler ();
#if REACTIVE_2_0
		public
#else
		internal
#endif
		static ThreadPoolScheduler Instance {
			get { return instance; }
		}

		internal ThreadPoolScheduler ()
		{
		}
		
#if !REACTIVE_2_0
		public DateTimeOffset Now {
			get { return Scheduler.Now; }
		}
		
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (TState state, dueTime - Now, Action);
		}

		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, Now + Scheduler.Normalize (dueTime), action);
		}
#endif

#if REACTIVE_2_0
		public override IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#else
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#endif
		{
			bool cancel = false;
			var dis = new CompositeDisposable ();
			dis.Add (Disposable.Create (() => cancel = true));
			ThreadPool.QueueUserWorkItem (s => {
				Thread.Sleep (Scheduler.Normalize (dueTime));
				if (!cancel)
					dis.Add (action (this, state));
			});
			return dis;
		}
		
#if REACTIVE_2_0
		public override IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
#else
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
#endif
		{
			return Schedule<TState> (state, TimeSpan.Zero, action);
		}
		
#if REACTIVE_2_0
		public override IStopwatch StartStopwatch ()
		{
			throw new NotImplementedException ();
		}

		public IDisposable ScheduleLongRunning<TState> (TState state, Action<TState, ICancelable> action)
		{
			throw new NotImplementedException ();
		}
#endif
	}
}
