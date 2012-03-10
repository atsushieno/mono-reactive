using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
#if REACTIVE_2_0
	public
#endif
	class ScheduledItem<TAbsolute> : IScheduledItem<TAbsolute>, IDisposable
	{
		public class Comparer : IComparer<IScheduledItem<TAbsolute>>
		{
			IComparer<TAbsolute> comparer;
			
			public Comparer (IComparer<TAbsolute> comparer)
			{
				this.comparer = comparer;
			}

			public int Compare (IScheduledItem<TAbsolute> i1, IScheduledItem<TAbsolute> i2)
			{
				return comparer.Compare (i1.DueTime, i2.DueTime);
			}
		}

		public ScheduledItem (TAbsolute dueTime, Func<IDisposable> action)
		{
			this.action = action;
			DueTime = dueTime;
		}
		
		Func<IDisposable> action;
		SingleAssignmentDisposable dis = new SingleAssignmentDisposable ();
		
		public TAbsolute DueTime { get; private set; }
		
		public void Dispose ()
		{
			dis.Dispose ();
		}
		
		public void Invoke ()
		{
			dis.Disposable = action ();
		}
	}
}
