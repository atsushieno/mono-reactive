using System;
using System.Reactive.Disposables;

namespace System.Reactive.Linq
{
	internal class SimpleDisposableObservable<T> : IObservable<T>
	{
		Func<IObserver<T>, IDisposable> subscribe;

		public SimpleDisposableObservable (Func<IObserver<T>, IDisposable> subscribe)
		{
			this.subscribe = subscribe;
		}
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			return subscribe (observer);
		}
	}
}
