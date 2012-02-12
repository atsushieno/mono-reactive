using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public sealed class CurrentThreadScheduler : IScheduler
	{
		internal CurrentThreadScheduler ()
		{
		}
		
		public bool ScheduleRequired {
			get { return busy > 0; }
		}
		
		public DateTimeOffset Now {
			get { return Scheduler.Now; }
		}
		
		int busy = 0;

		List<ScheduledItem<DateTimeOffset>> tasks = new List<ScheduledItem<DateTimeOffset>> ();
		
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
	}
}
