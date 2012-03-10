using System;

namespace System.Reactive
{
	public interface IEventPattern<out TSender, out TEventArgs>
	{
		TSender Sender { get; }
		TEventArgs EventArgs { get; }
	}
}

