using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
#if REACTIVE_2_0
	public
#endif
	abstract class ScheduledItem<TAbsolute> : IScheduledItem<TAbsolute>, IComparable<ScheduledItem<TAbsolute>>
		where TAbsolute : IComparable<TAbsolute>
	{
		IComparer<TAbsolute> comparer;
		
		protected ScheduledItem (TAbsolute dueTime, IComparer<TAbsolute> comparer)
		{
			DueTime = dueTime;
			this.comparer = comparer;
		}
		
		public int CompareTo (ScheduledItem<TAbsolute> other)
		{
			if (other == null)
				throw new ArgumentNullException ("other");
			return comparer.Compare (DueTime, other.DueTime);
		}
		
		public TAbsolute DueTime { get; private set; }
		
		public bool IsCanceled { get; private set; }
		
		public void Cancel ()
		{
			IsCanceled = true;
		}

		public void Invoke ()
		{
			if (!IsCanceled)
				InvokeCore ();
		}
		
		protected abstract IDisposable InvokeCore ();
		
		// FIXME: this should be examined
		public override bool Equals (object obj)
		{
			return base.Equals (obj);
		}

		public override int GetHashCode ()
		{
			return DueTime.GetHashCode ();
		}

		public static bool operator == (ScheduledItem<TAbsolute> left, ScheduledItem<TAbsolute> right)
		{
			return object.ReferenceEquals (left, null) ? object.ReferenceEquals (right, null) : left.Equals (right);
		}

		public static bool operator != (ScheduledItem<TAbsolute> left, ScheduledItem<TAbsolute> right)
		{
			return object.ReferenceEquals (left, null) ? !object.ReferenceEquals (right, null) : !left.Equals (right);
		}

		public static bool operator > (ScheduledItem<TAbsolute> left, ScheduledItem<TAbsolute> right)
		{
			return object.ReferenceEquals (left, null) ? false : left.CompareTo (right) > 0;
		}
		
		public static bool operator >= (ScheduledItem<TAbsolute> left, ScheduledItem<TAbsolute> right)
		{
			return object.ReferenceEquals (left, null) ? object.ReferenceEquals (right, null) : left.CompareTo (right) >= 0;
		}

		public static bool operator < (ScheduledItem<TAbsolute> left, ScheduledItem<TAbsolute> right)
		{
			return object.ReferenceEquals (right, null) ? false : right.CompareTo (left) > 0;
		}
		
		public static bool operator <= (ScheduledItem<TAbsolute> left, ScheduledItem<TAbsolute> right)
		{
			return object.ReferenceEquals (right, null) ? object.ReferenceEquals (left, null) : right.CompareTo (left) >= 0;
		}
	}
	
#if REACTIVE_2_0
	public
#endif
	sealed class ScheduledItem<TAbsolute,TValue> : ScheduledItem<TAbsolute>
		where TAbsolute : IComparable<TAbsolute>
	{
		public ScheduledItem (IScheduler scheduler, TValue state, Func<IScheduler,TValue,IDisposable> action, TAbsolute dueTime)
			: this (scheduler, state, action, dueTime, Comparer<TAbsolute>.Default)
		{
		}
		
		public ScheduledItem (IScheduler scheduler, TValue state, Func<IScheduler,TValue,IDisposable> action, TAbsolute dueTime, IComparer<TAbsolute> comparer)
			: base (dueTime, comparer)
		{
			throw new NotImplementedException ();
		}
		
		protected override IDisposable InvokeCore ()
		{
			throw new NotImplementedException ();
		}
	}
}
