using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Reactive.Concurrency
{
	public static class Scheduler
	{
		public static CurrentThreadScheduler CurrentThread {
			get { throw new NotImplementedException (); }
		}
		public static ImmediateScheduler Immediate {
			get { throw new NotImplementedException (); }
		}
		public static NewThreadScheduler NewThread {
			get { throw new NotImplementedException (); }
		}
		public static DateTimeOffset Now {
			get { throw new NotImplementedException (); }
		}
		public static TaskPoolScheduler TaskPool {
			get { throw new NotImplementedException (); }
		}
		public static ThreadPoolScheduler ThreadPool {
			get { throw new NotImplementedException (); }
		}
		
		public static TimeSpan Normalize (TimeSpan timeSpan)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, Action action)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, Action<Action> action)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, DateTimeOffset dueTime, Action action)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, DateTimeOffset dueTime, Action<Action> action)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, TimeSpan dueTime, Action action)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Schedule (this IScheduler scheduler, TimeSpan dueTime, Action<Action> action)
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
		
		public static IDisposable Schedule<TState> (this IScheduler scheduler, TState state, TimeSpan dueTime, Action<TState, Action<TState, TimeSpan>> action)
		{
			throw new NotImplementedException ();
		}
	}
}
