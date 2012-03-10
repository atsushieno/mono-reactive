using System;
using System.Reactive.Concurrency;

#if !REACTIVE_2_0
using TSender = System.Object;
#endif

namespace System.Reactive
{
#if REACTIVE_2_0
	public class EventPattern<TEventArgs> : EventPattern<object, TEventArgs>
	{
		public EventPattern (object sender, TEventArgs e)
			: base (sender, e)
		{
		}
	}
	
	public class EventPattern<TSender,TEventArgs> : IEventPattern<TSender, TEventArgs>, IEquatable<EventPattern<TSender,TEventArgs>>
#else
	public class EventPattern<TEventArgs>
		where TEventArgs : EventArgs
#endif
	{
		TSender sender;
		TEventArgs e;
		
		public EventPattern (TSender sender, TEventArgs e)
		{
			this.sender = sender;
			this.e = e;
		}
		
		public TEventArgs EventArgs {
			get { return e; }
		}
		
		public TSender Sender {
			get { return sender; }
		}
		
		public override bool Equals (object obj)
		{
			var ep = obj as EventPattern<TEventArgs>;
			return ep != null && Equals (ep);
		}
		
#if REACTIVE_2_0
		public bool Equals (EventPattern<TSender, TEventArgs> other)
#else
		public bool Equals (EventPattern<TEventArgs> other)
#endif
		{
			if ((object) other == null)
				return false;
			return ((TSender) sender == null ? (TSender) other.sender == null : sender.Equals (other.sender)) && e.Equals (other.e);
		}
		
#if REACTIVE_2_0
		public static bool operator == (EventPattern<TSender,TEventArgs> first, EventPattern<TSender,TEventArgs> second)
#else
		public static bool operator == (EventPattern<TEventArgs> first, EventPattern<TEventArgs> second)
#endif
		{
			return (object) first == null ? (object) second == null : first.Equals (second);
		}
		
#if REACTIVE_2_0
		public static bool operator != (EventPattern<TSender,TEventArgs> first, EventPattern<TSender,TEventArgs> second)
#else
		public static bool operator != (EventPattern<TEventArgs> first, EventPattern<TEventArgs> second)
#endif
		{
			return (object) first == null ? (object) second != null : !first.Equals (second);
		}
		
		public override int GetHashCode ()
		{
			return ((TSender) sender != null ? sender.GetHashCode () : 0) ^ 7 + e.GetHashCode ();
		}
	}
}
