using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Reactive.Concurrency
{
	public static class Scheduler
	{
		// FIXME: implement
		internal static IScheduler GetDefault<T> (IObservable<T> source)
		{
			return Scheduler.CurrentThread;
		}
		
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
			return Schedule (scheduler, Now, action);
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, Action<Action> action)
		{
			return Schedule (scheduler, Now, action);
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, DateTimeOffset dueTime, Action action)
		{
			return Schedule (scheduler, dueTime, (a) => action ());
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, DateTimeOffset dueTime, Action<Action> action)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Schedule<TState> (this IScheduler scheduler, TState state, Action<TState, Action<TState>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Schedule<TState> (this IScheduler scheduler, TState state, DateTimeOffset dueTime, Action<TState, Action<TState, DateTimeOffset>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, TimeSpan dueTime, Action action)
		{
			return Schedule (scheduler, Now + Normalize (dueTime), action);
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, TimeSpan dueTime, Action<Action> action)
		{
			return Schedule (scheduler, Now + Normalize (dueTime), action);
		}
		
		public static IDisposable Schedule<TState> (this IScheduler scheduler, TState state, TimeSpan dueTime, Action<TState, Action<TState, TimeSpan>> action)
		{
			throw new NotImplementedException ();
		}
	}
}
