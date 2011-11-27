using System;
using System.Reactive.Disposables;

namespace System.Reactive.Linq
{
	internal class SimpleActionObservable<T> : IObservable<T>
	{
		Func<IObserver<T>, Action> subscribe;

		public SimpleActionObservable (Func<IObserver<T>, Action> subscribe)
		{
			this.subscribe = subscribe;
		}
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			return Disposable.Create (subscribe (observer));
		}
	}
}
