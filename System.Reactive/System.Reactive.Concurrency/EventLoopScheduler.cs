using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
#if REACTIVE_2_0
	public sealed class EventLoopScheduler : LocalScheduler, IDisposable
#else
	public sealed class EventLoopScheduler : IScheduler, IDisposable
#endif
	{
		public EventLoopScheduler ()
			: this ((ts) => new Thread (ts) { IsBackground = true })
		{
		}
		
		public EventLoopScheduler (Func<ThreadStart, Thread> threadFactory)
		{
			if (threadFactory == null)
				throw new ArgumentNullException ("threadFactory");
			thread_factory = threadFactory;
		}
		
		Func<ThreadStart, Thread> thread_factory;
		CompositeDisposable disposables = new CompositeDisposable ();
		
		public void Dispose ()
		{
			disposables.Dispose ();
		}
		
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
			return Schedule<TState> (state, dueTime - Now, action);
		}
#endif
		
#if REACTIVE_2_0
		public override IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#else
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#endif
		{
			var dis = new SingleAssignmentDisposable ();
			bool cancel = false;
			var th = thread_factory (() => {
				Thread.Sleep (Scheduler.Normalize (dueTime));
				if (!cancel)
					dis.Disposable = action (this, state);
				});
			th.Start ();
			// The thread is not aborted even if it's at work (ThreadAbortException is not caught inside the action).
			var ret = Disposable.Create (() => { cancel = true; dis.Dispose (); disposables.Remove (dis); });
			disposables.Add (ret);
			return ret;
		}
		
#if REACTIVE_2_0
		public override IStopwatch StartStopwatch ()
		{
			throw new NotImplementedException ();
		}
#endif
	}
}
