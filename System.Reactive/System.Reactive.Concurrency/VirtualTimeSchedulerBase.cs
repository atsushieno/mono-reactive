using System;
using System.Collections.Generic;

namespace System.Reactive.Concurrency
{
	public abstract class VirtualTimeSchedulerBase<TAbsolute, TRelative>
		: IScheduler
	{
		protected VirtualTimeSchedulerBase ()
		{
			throw new NotImplementedException ();
		}
		
		protected VirtualTimeSchedulerBase (TAbsolute initialClock, IComparer<TAbsolute> comparer)
		{
			throw new NotImplementedException ();
		}
		
		public TAbsolute Clock {
			get { throw new NotImplementedException (); }
			protected set { throw new NotImplementedException (); }
		}
		
		protected IComparer<TAbsolute> Comparer {
			get { throw new NotImplementedException (); }
			private set { throw new NotImplementedException (); }
		}
		
		protected bool IsEnabled {
			get { throw new NotImplementedException (); }
			private set { throw new NotImplementedException (); }
		}
		
		public DateTimeOffset Now {
			get { throw new NotImplementedException (); }
		}
		
		protected abstract TAbsolute Add (TAbsolute absolute, TRelative relative);
		
		public void AdvanceBy (TRelative time)
		{
			throw new NotImplementedException ();
		}
		
		public void AdvanceTo (TAbsolute time)
		{
			throw new NotImplementedException ();
		}
		
		protected abstract IScheduledItem<TAbsolute> GetNext ();
		
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
		}
		
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
		}
		
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
		}
		
		public abstract IDisposable ScheduleAbsolute<TState> (TState state, TAbsolute dueTime, Func<IScheduler, TState, IDisposable> action);
		
		public IDisposable ScheduleRelative<TState> (TState state, TRelative dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
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
