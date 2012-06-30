using System;

namespace System.Reactive.Concurrency
{
	public class SchedulerQueue<TAbsolute>
		where TAbsolute : IComparable<TAbsolute>
	{
		public SchedulerQueue ()
			: this (0x1000) // not verified at all
		{
		}
		
		public SchedulerQueue (int capacity)
		{
			throw new NotImplementedException ();
		}
		
		public int Count {
			get { throw new NotImplementedException (); }
		}
		
		public ScheduledItem<TAbsolute> Dequeue ()
		{
			throw new NotImplementedException ();
		}
		
		public void Enqueue (ScheduledItem<TAbsolute> scheduledItem)
		{
			throw new NotImplementedException ();
		}
		
		public ScheduledItem<TAbsolute> Peek ()
		{
			throw new NotImplementedException ();
		}
		
		public bool Remove (ScheduledItem<TAbsolute> scheduledItem)
		{
			throw new NotImplementedException ();
		}
	}
}

