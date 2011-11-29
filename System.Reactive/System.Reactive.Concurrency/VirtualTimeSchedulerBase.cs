using System;
using System.Collections.Generic;

namespace System.Reactive.Concurrency
{
	public abstract class VirtualTimeSchedulerBase<TAbsolute, TRelative>
		: IScheduler
	{
		protected VirtualTimeSchedulerBase ()
			: this (default (TAbsolute), Comparer<TAbsolute>.Default)
		{
		}
		
		protected VirtualTimeSchedulerBase (TAbsolute initialClock, IComparer<TAbsolute> comparer)
		{
			if (comparer == null)
				throw new ArgumentNullException ("comparer");
			Clock = initialClock;
			Comparer = comparer;
		}
		
		public TAbsolute Clock { get; protected set; }
		
		protected IComparer<TAbsolute> Comparer { get; private set; }
		
		protected bool IsEnabled {
			get { throw new NotImplementedException (); }
			private set { throw new NotImplementedException (); }
		}
		
		public DateTimeOffset Now {
			get { return Scheduler.Now; }
		}
		
		protected abstract TAbsolute Add (TAbsolute absolute, TRelative relative);
		
		public void AdvanceBy (TRelative time)
		{
			Clock = Add (Clock, time);
		}
		
		public void AdvanceTo (TAbsolute time)
		{
			Clock = time;
		}
		
		protected abstract IScheduledItem<TAbsolute> GetNext ();
		
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, TimeSpan.Zero, action);
		}
		
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule<TState> (state, dueTime - DateTimeOffset.Now, action);
		}
		
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return ScheduleRelative<TState> (state, ToRelative (Scheduler.Normalize (dueTime)), action);
		}
		
		public abstract IDisposable ScheduleAbsolute<TState> (TState state, TAbsolute dueTime, Func<IScheduler, TState, IDisposable> action);
		
		public IDisposable ScheduleRelative<TState> (TState state, TRelative dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return ScheduleAbsolute<TState> (state, Add (Clock, dueTime), action);
		}
		
		public void Start ()
		{
			throw new NotImplementedException ();
		}
		
		public void Stop ()
		{
			throw new NotImplementedException ();
		}
		
		protected abstract DateTimeOffset ToDateTimeOffset (TAbsolute absolute);
		
		protected abstract TRelative ToRelative (TimeSpan timeSpan);
	}
}
