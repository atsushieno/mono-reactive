using System;

namespace System.Reactive
{
#if REACTIVE_2_0
	public
#endif
	sealed class AnonymousObservable<T> : ObservableBase<T>
	{
		Func<IObserver<T>, IDisposable> subscribe;
		
		public AnonymousObservable (Func<IObserver<T>, IDisposable> subscribe)
		{
			if (subscribe == null)
				throw new ArgumentNullException ("subscribe");
			this.subscribe = subscribe;
		}
		
		protected override IDisposable SubscribeCore (IObserver<T> observer)
		{
			return subscribe (observer);
		}
	}
}

