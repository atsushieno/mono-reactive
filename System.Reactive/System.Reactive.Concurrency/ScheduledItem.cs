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
		
		public ScheduledItem (TAbsolute dueTime, IComparer<TAbsolute> comparer)
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
		
		public bool IsCancelled { get; private set; }
		
		public void Cancel ()
		{
			IsCancelled = true;
		}

		public void Invoke ()
		{
			if (!IsCancelled)
				InvokeCore ();
		}
		
		protected abstract IDisposable InvokeCore ();
	}
}
