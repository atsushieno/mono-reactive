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

		IComparer<IScheduledItem<DateTimeOffset>> comparer = new ScheduledItem<DateTimeOffset>.Comparer (Comparer<DateTimeOffset>.Default);
		List<IScheduledItem<DateTimeOffset>> tasks = new List<IScheduledItem<DateTimeOffset>> ();
		
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
			var task = new ScheduledItem<DateTimeOffset> (dueTime, () => action (this, state));
			if (Interlocked.CompareExchange (ref busy, busy, busy + 1) > 0) {
				tasks.Add (task);
				tasks.Sort (comparer);
				return Disposable.Create (() => tasks.Remove (task));
			} else {
				try {
					task.Invoke ();
					while (true) {
						ScheduledItem<DateTimeOffset> t;
						lock (tasks) {
							t = (ScheduledItem<DateTimeOffset>) tasks.FirstOrDefault ();
							if (t == null)
								break;
							tasks.Remove (t);
						}
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
