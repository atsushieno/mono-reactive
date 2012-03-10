using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
#if REACTIVE_2_0
	public sealed class CurrentThreadScheduler : LocalScheduler
#else
	public sealed class CurrentThreadScheduler : IScheduler
#endif
	{
		static readonly CurrentThreadScheduler instance = new CurrentThreadScheduler ();
#if REACTIVE_2_0
		public
#else
		internal
#endif
		static CurrentThreadScheduler Instance {
			get { return instance; }
		}
		
		internal CurrentThreadScheduler ()
		{
		}
		
		public bool ScheduleRequired {
			get { return busy > 0; }
		}
		
		int busy = 0;

		List<ScheduledItem<DateTimeOffset>> tasks = new List<ScheduledItem<DateTimeOffset>> ();
		
#if !REACTIVE_2_0
		public DateTimeOffset Now {
			get { return Scheduler.Now; }
		}
		
		// FIXME: this needs to be rewritten to rather use TimeSpan-based impl.
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, Now, action);
		}
		
		/* verified with:

			var dis = Scheduler.CurrentThread.Schedule<int>(0, TimeSpan.FromSeconds (3), (sch, i) => {
				Console.WriteLine("start outer");
				Thread.Sleep(2000);
				Scheduler.CurrentThread.Schedule(() => Console.WriteLine("inner action"));
				Console.WriteLine("OK");
				return Disposable.Empty;
			});
			Console.WriteLine("Started");
			Console.WriteLine("done all");

		*/
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			// FIXME: this sort of needs to handle correct cancellation. (Though there's not likely many chances to "cancel" it...)
			var task = new ScheduledItem<DateTimeOffset> (dueTime, () => action (this, state));
			if (Interlocked.CompareExchange (ref busy, busy, busy + 1) > 0) {
				Scheduler.AddTask (tasks, task);
				return Disposable.Create (() => tasks.Remove (task));
			} else {
				try {
					Thread.Sleep (Scheduler.Normalize (task.DueTime - Now));
					task.Invoke ();
					while (true) {
						ScheduledItem<DateTimeOffset> t;
						lock (tasks) {
							t = (ScheduledItem<DateTimeOffset>) tasks.FirstOrDefault ();
							if (t == null)
								break;
							tasks.Remove (t);
						}
						Thread.Sleep (Scheduler.Normalize (t.DueTime - Now));
						t.Invoke ();
					}
				} finally {
					busy--;
				}
				return Disposable.Empty;
			}
		}

		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, Now + dueTime, action);
		}
#else
		public override IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
		}
#endif
	}
}
