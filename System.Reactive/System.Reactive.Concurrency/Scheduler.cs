using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	public static class Scheduler
	{
		static object lock_obj = new object ();
		static volatile CurrentThreadScheduler current_thread;
		static volatile ImmediateScheduler immediate;
		static volatile NewThreadScheduler new_thread;
		static volatile TaskPoolScheduler task_pool;
		static volatile ThreadPoolScheduler thread_pool;
		
		public static CurrentThreadScheduler CurrentThread {
			get {
				if (current_thread == null)
					lock (lock_obj)
						if (current_thread == null)
							 current_thread = new CurrentThreadScheduler ();
				return current_thread;
			}
		}
		public static ImmediateScheduler Immediate {
			get {
				if (immediate == null)
					lock (lock_obj)
						if (immediate == null)
							 immediate = new ImmediateScheduler ();
				return immediate;
			}
		}
		public static NewThreadScheduler NewThread {
			get {
				if (new_thread == null)
					lock (lock_obj)
						if (new_thread == null)
							 new_thread = new NewThreadScheduler ();
				return new_thread;
			}
		}
		public static TaskPoolScheduler TaskPool {
			get {
				if (task_pool == null)
					lock (lock_obj)
						if (task_pool == null)
							task_pool = new TaskPoolScheduler (new TaskFactory ());
				return task_pool;
			}
		}
		
		public static ThreadPoolScheduler ThreadPool {
			get {
				if (thread_pool == null)
					lock (lock_obj)
						if (thread_pool == null)
							thread_pool = new ThreadPoolScheduler ();
				return thread_pool;
			}
		}

		public static DateTimeOffset Now {
			get { return DateTimeOffset.Now; }
		}
		
		// returns non-negative TimeSpan.
		public static TimeSpan Normalize (TimeSpan timeSpan)
		{
			return timeSpan >= TimeSpan.Zero ? timeSpan : TimeSpan.Zero;
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, Action action)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			return Schedule (scheduler, scheduler.Now, action);
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, Action<Action> action)
		{
			return Schedule (scheduler, TimeSpan.Zero, a => action (() => a (TimeSpan.Zero)));
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, DateTimeOffset dueTime, Action action)
		{
			return Schedule (scheduler, dueTime, a => action ());
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, DateTimeOffset dueTime, Action<Action<DateTimeOffset>> action)
		{
			return Schedule<object> (scheduler, new object (), dueTime, (stat, act) => action (dt => act (stat, dt)));
		}
		
		public static IDisposable Schedule<TState> (this IScheduler scheduler, TState state, Action<TState, Action<TState>> action)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			return Schedule (scheduler, state, scheduler.Now, (stat, stdtact) => action (stat, (st) => stdtact (st, scheduler.Now)));
		}
		
		public static IDisposable Schedule<TState> (this IScheduler scheduler, TState state, DateTimeOffset dueTime, Action<TState, Action<TState, DateTimeOffset>> action)
		{
			// invoke IScheduler.Schedule<TState> (TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)
			Func<IScheduler,TState,IDisposable> f = null;
			f = (sch, stat) => {
				var dis = new SingleAssignmentDisposable ();
				action (stat, (st, dt) => { if (!dis.IsDisposed) dis.Disposable = sch.Schedule (st, dt, f); });
				return dis;
			};
			return scheduler.Schedule<TState> (state, dueTime, f);
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, TimeSpan dueTime, Action action)
		{
			return Schedule (scheduler, dueTime, a => action ());
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, TimeSpan dueTime, Action<Action<TimeSpan>> action)
		{
			return Schedule<object> (scheduler, new object (), dueTime, (stat, act) => action (ts => act (stat, ts)));
		}
		
		public static IDisposable Schedule<TState> (this IScheduler scheduler, TState state, TimeSpan dueTime, Action<TState, Action<TState, TimeSpan>> action)
		{
			// invoke IScheduler.Schedule<TState> (TState, TimeSpan, Func<IScheduler, TState, IDisposable>)
			Func<IScheduler,TState,IDisposable> f = null;
			f = (sch, stat) => {
				var dis = new SingleAssignmentDisposable ();
				action (stat, (st, dt) => { if (!dis.IsDisposed) dis.Disposable = sch.Schedule (st, dt, f); });
				return dis;
			};
			return scheduler.Schedule<TState> (state, dueTime, f);
		}
	}
}
