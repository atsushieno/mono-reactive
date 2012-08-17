using System;

namespace System.Reactive.Linq
{
	public interface IQbservable<out T> : IQbservable, IObservable<T>
	{
	}
}

