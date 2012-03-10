using System;

namespace System.Reactive.Linq
{
	public interface IQbservable<out TSource> : IQbservable, IObservable<TSource>
	{
	}
}

