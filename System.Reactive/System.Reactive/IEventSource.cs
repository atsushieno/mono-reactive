using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	public interface IEventSource<T>
	{
		event Action<T> OnNext;
	}
}
