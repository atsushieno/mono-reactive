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
		
		class Task : IScheduledItem<DateTimeOffset>
		{
			public Task (Func<IDisposable> action, DateTimeOffset dueTime)
			{
				Action = action;
				DueTime = dueTime;
			}
			
			public Func<IDisposable> Action;
			public DateTimeOffset DueTime { get; private set; }

			public void Invoke ()
			{
				Thread.Sleep (Scheduler.Normalize (DueTime - Scheduler.CurrentThread.Now));
				var dis = Action ();
				dis.Dispose ();
			}
		}
		
		public class TaskComparer : IComparer<IScheduledItem<DateTimeOffset>>
		{
			public int Compare (IScheduledItem<DateTimeOffset> i1, IScheduledItem<DateTimeOffset> i2)
			{
				return Comparer<DateTimeOffset>.Default.Compare (i1.DueTime, i2.DueTime);
			}
		}
		
		int busy = 0;

		ISet<IScheduledItem<DateTimeOffset>> tasks = new SortedSet<IScheduledItem<DateTimeOffset>> (new TaskComparer ());
		
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, Scheduler.Now, action);
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
			var task = new Task (() => action (this, state), dueTime);
			if (Interlocked.CompareExchange (ref busy, 1, busy) == 1) {
				tasks.Add (task);
				return Disposable.Create (() => tasks.Remove (task));
			} else {
				try {
					task.Invoke ();
					while (tasks.Count > 0) {
						var t = (Task) tasks.First ();
						tasks.Remove (t);
						t.Invoke ();
					}
				} finally {
					busy = 0;
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
