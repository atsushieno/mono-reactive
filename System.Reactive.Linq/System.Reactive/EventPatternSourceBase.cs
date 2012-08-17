using System;

namespace System.Reactive
{
	public class EventPatternSourceBase<TSender,TEventArgs>
	{
		public EventPatternSourceBase (IObservable<EventPattern<TSender,TEventArgs>> source, Action<Action<TSender,TEventArgs>, EventPattern<TSender,TEventArgs>> action)
		{
			throw new NotImplementedException ();
		}

		public void Add (Delegate handler, Action<TSender,TEventArgs> invoke)
		{
			throw new NotImplementedException ();
		}

		public void Remove (Delegate handler)
		{
			throw new NotImplementedException ();
		}
	}
}

