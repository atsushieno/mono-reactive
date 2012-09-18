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
#if REACTIVE_2_0
		static volatile IScheduler new_thread;
		static volatile IScheduler task_pool;
		static volatile IScheduler thread_pool;
#endif
		
		public static CurrentThreadScheduler CurrentThread {
			get { return CurrentThreadScheduler.Instance; }
		}
		public static ImmediateScheduler Immediate {
			get { return ImmediateScheduler.Instance; }
		}
#if REACTIVE_2_0

		public static DefaultScheduler Default {
			get { return DefaultScheduler.Instance; }
		}

		// Those properties are [Obsolete] only in non-portable build...

		[Obsolete ("This property is no longer supported. Use System.Reactive.Concurrency.NewThreadScheduler in System.Reactive.PlatformServices.dll")]
		public static IScheduler NewThread {
			get {
				if (new_thread == null)
					new_thread = (IScheduler) Type.GetType ("System.Reactive.Concurrency.NewThreadScheduler, System.Reactive.PlatformServices").GetProperty ("Default").GetValue (null, null);
				return new_thread;
			}
		}
		[Obsolete ("This property is no longer supported. Use System.Reactive.Concurrency.TaskPoolScheduler in System.Reactive.PlatformServices.dll")]
		public static IScheduler TaskPool {
			get {
				if (task_pool == null)
					task_pool = (IScheduler) Type.GetType ("System.Reactive.Concurrency.TaskPoolScheduler, System.Reactive.PlatformServices").GetProperty ("Default").GetValue (null, null);
				return task_pool;
			}
		}
		[Obsolete ("This property is no longer supported. Use System.Reactive.Concurrency.ThreadPoolScheduler in System.Reactive.PlatformServices.dll")]
		public static IScheduler ThreadPool {
			get {
				if (thread_pool == null)
					thread_pool = (IScheduler) Type.GetType ("System.Reactive.Concurrency.ThreadPoolScheduler, System.Reactive.PlatformServices").GetProperty ("Instance").GetValue (null, null);
				return thread_pool;
			}
		}
#else
		public static NewThreadScheduler NewThread {
			get { return NewThreadScheduler.Default; }
		}
		public static TaskPoolScheduler TaskPool {
			get { return TaskPoolScheduler.Default; }
		}
		
		public static ThreadPoolScheduler ThreadPool {
			get { return ThreadPoolScheduler.Instance; }
		}
#endif

		public static DateTimeOffset Now {
			get { return DateTimeOffset.UtcNow; }
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
		
#if REACTIVE_2_0
		public static ISchedulerLongRunning AsLongRunning (this IScheduler scheduler)
		{
			// is this method really just like this...?
			return scheduler as ISchedulerLongRunning;
		}

		public static ISchedulerPeriodic AsPeriodic (this IScheduler scheduler)
		{
			// is this method really just like this...?
			return scheduler as ISchedulerPeriodic;
		}

		public static IStopwatchProvider AsStopwatchProvider (this IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static IScheduler Catch<TException> (this IScheduler scheduler, Func<TException,bool> handler)
			where TException : Exception
		{
			throw new NotImplementedException ();
		}
		
		public static IScheduler DisableOptimizations (this IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static IScheduler DisableOptimizations (this IScheduler scheduler, params Type [] optimizationInterfaces)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable ScheduleLongRunning (this ISchedulerLongRunning scheduler, Action<ICancelable> action)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable SchedulePeriodic (this IScheduler scheduler, TimeSpan period, Action action)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable SchedulePeriodic<TState> (this IScheduler scheduler, TState state, TimeSpan period, Action<TState> action)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable SchedulePeriodic<TState> (this IScheduler scheduler, TState state, TimeSpan period, Func<TState,TState> action)
		{
			throw new NotImplementedException ();
		}
		
		public static IStopwatch StartStopwatch (this IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
#endif
	}
}
