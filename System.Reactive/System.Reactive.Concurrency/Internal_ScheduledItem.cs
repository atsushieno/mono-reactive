using System;
using System.Linq;
using System.Reactive;

namespace System.Reactive.Concurrency
{
	internal class ScheduledItem<TAbsolute> : IScheduledItem<TAbsolute>
	{
		TAbsolute due_time;
		Action action;
		bool invoked;
		
		public ScheduledItem (TAbsolute dueTime, Action action)
		{
		}
		
		public TAbsolute DueTime {
			get { return due_time; }
		}
		
		public void Invoke ()
		{
			if (invoked)
				throw new InvalidOperationException ("This scheduled item was already invoked.");
			invoked = true;
			action ();
		}
	}
}

