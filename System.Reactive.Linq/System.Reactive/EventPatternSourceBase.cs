using System;

namespace System.Reactive
{
	public abstract class EventPatternSourceBase<TSender,TEventArgs> where TEventArgs : EventArgs
	{
		protected EventPatternSourceBase (IObservable<EventPattern<TSender,TEventArgs>> source, Action<Action<TSender,TEventArgs>, EventPattern<TSender,TEventArgs>> invokeHandler)
		{
			throw new NotImplementedException ();
		}

		protected void Add (Delegate handler, Action<TSender,TEventArgs> invoke)
		{
			throw new NotImplementedException ();
		}

		protected void Remove (Delegate handler)
		{
			throw new NotImplementedException ();
		}
	}
}

