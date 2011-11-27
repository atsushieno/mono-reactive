using System;

namespace System.Reactive.Linq
{
	public interface IGroupedObservable<out TKey, out TElement> : IObservable<TElement>
	{
		TKey Key { get; }
	}
}
