using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	public static class Observer
	{
		public static IObserver<T> AsObserver<T> (this IObserver<T> observer)
		{
			if (observer == null)
				throw new ArgumentNullException ("observer");
			return new WrappedObserver<T> (observer);
		}
		
		public static IObserver<T> Create<T> (Action<T> onNext)
		{
			return Create (onNext, () => {});
		}
		
		public static IObserver<T> Create<T> (Action<T> onNext, Action onCompleted)
		{
			return Create (onNext, (ex) => {}, onCompleted);
		}
		
		public static IObserver<T> Create<T> (Action<T> onNext, Action <Exception> onError)
		{
			return Create (onNext, onError, () => {});
		}
		
		public static IObserver<T> Create<T> (Action<T> onNext, Action<Exception> onError, Action onCompleted)
		{
			if (onNext == null)
				throw new ArgumentNullException ("onNext");
			if (onError == null)
				throw new ArgumentNullException ("onError");
			if (onCompleted == null)
				throw new ArgumentNullException ("onCompleted");
			return new DefaultObserver<T> (onNext, onError, onCompleted);
		}
		
		public static IObserver<T> Synchronize<T> (IObserver<T> observer)
		{
			return Synchronize (observer, new object ());
		}
		
		public static IObserver<T> Synchronize<T> (IObserver<T> observer, object gate)
		{
			throw new NotImplementedException ();
		}
		
		public static Action<Notification<T>> ToNotifier<T> (this IObserver<T> observer)
		{
			throw new NotImplementedException ();
		}
		
		public static IObserver<T> ToObserver<T> (this Action<Notification<T>> handler)
		{
			throw new NotImplementedException ();
		}
	}
}
