using System;
using System.Collections.Generic;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
	class ScheduledItemImpl<TAbsolute> : ScheduledItem<TAbsolute>, IDisposable
		where TAbsolute : IComparable<TAbsolute>
	{
		public ScheduledItemImpl (TAbsolute dueTime, Func<IDisposable> action)
			: base (dueTime, Comparer<TAbsolute>.Default)
		{
			this.action = action;
		}
		
		SingleAssignmentDisposable disposable = new SingleAssignmentDisposable ();
		
		public void Dispose ()
		{
			disposable.Dispose ();
		}
		
		Func<IDisposable> action;
		
		protected override IDisposable InvokeCore ()
		{
			disposable.Disposable = action ();
			return disposable;
		}
	}
}