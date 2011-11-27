using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	public class EventPattern<TEventArgs>
		where TEventArgs : EventArgs
	{
		object sender;
		TEventArgs e;
		
		public EventPattern (object sender, TEventArgs e)
		{
			this.sender = sender;
			this.e = e;
		}
		
		public TEventArgs EventArgs {
			get { return e; }
		}
		
		public object Sender {
			get { return sender; }
		}
		
		public override bool Equals (object obj)
		{
			var ep = obj as EventPattern<TEventArgs>;
			return ep != null && Equals (ep);
		}
		
		public bool Equals (EventPattern<TEventArgs> other)
		{
			if ((object) other == null)
				return false;
			return ((object) sender == null ? (object) other.sender == null : sender.Equals (other.sender)) && e.Equals (other.e);
		}
		
		public static bool operator == (EventPattern<TEventArgs> first, EventPattern<TEventArgs> second)
		{
			return (object) first == null ? (object) second == null : first.Equals (second);
		}
		
		public static bool operator != (EventPattern<TEventArgs> first, EventPattern<TEventArgs> second)
		{
			return (object) first == null ? (object) second != null : !first.Equals (second);
		}
		
		public override int GetHashCode ()
		{
			return ((object) sender != null ? sender.GetHashCode () : 0) ^ 7 + e.GetHashCode ();
		}
	}
}
