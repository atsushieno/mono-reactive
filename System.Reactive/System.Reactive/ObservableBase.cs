using System;

namespace System.Reactive
{
#if REACTIVE_2_0
	public
#endif
	abstract class ObservableBase<T> : IObservable<T>
	{
		protected ObservableBase ()
		{
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			return SubscribeCore (observer);
		}

		protected abstract IDisposable SubscribeCore (IObserver<T> observer);
	}
}

