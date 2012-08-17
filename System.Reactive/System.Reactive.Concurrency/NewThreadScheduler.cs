using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
#if REACTIVE_2_0
	public sealed class NewThreadScheduler : LocalScheduler, ISchedulerLongRunning, ISchedulerPeriodic
#else
	public sealed class NewThreadScheduler : IScheduler
#endif
	{
		static readonly NewThreadScheduler instance = new NewThreadScheduler ();
#if REACTIVE_2_0
		public
#else
		internal
#endif
		static NewThreadScheduler Default {
			get { return instance; }
		}

		public NewThreadScheduler ()
			: this ((t => new Thread (t)))
		{
		}
		
		public NewThreadScheduler (Func<ThreadStart, Thread> threadFactory)
		{
			if (threadFactory == null)
				throw new ArgumentNullException ("threadFactory");
			this.thread_factory = threadFactory;
		}
		
		Func<ThreadStart, Thread> thread_factory;
		
#if !REACTIVE_2_0
		public DateTimeOffset Now {
			get { return Scheduler.Now; }
		}
		
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, TimeSpan.Zero, action);
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
			bool cancel = false;
			var dis = new CompositeDisposable ();
			dis.Add (Disposable.Create (() => cancel = true));
			thread_factory (() => {
				Thread.Sleep ((int) Scheduler.Normalize (dueTime).TotalMilliseconds);
				if (!cancel)
					dis.Add (action (this, (TState) state));
				}).Start ();
			return dis;
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

		public IDisposable SchedulePeriodic<TState> (TState state, TimeSpan period, Func<TState, TState> action)
		{
			throw new NotImplementedException ();
		}
#endif
	}
}
