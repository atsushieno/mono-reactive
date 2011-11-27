using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	public interface IEventPatternSource<TEventArgs>
		where TEventArgs : EventArgs
	{
		event EventHandler<TEventArgs> OnNext;
	}
}
