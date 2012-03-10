using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	public interface IEventSource<out T>
	{
		event Action<T> OnNext;
	}
}
