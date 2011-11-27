using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	public static class Observer
	{
		public static IObserver<T> AsObserver<T> (this IObserver<T> observer)
		{
			throw new NotImplementedException ();
		}
		
		public static IObserver<T> Create<T> (Action<T> onNext)
		{
			throw new NotImplementedException ();
		}
		
		public static IObserver<T> Create<T> (Action<T> onNext, Action onCompleted)
		{
			throw new NotImplementedException ();
		}
		
		public static IObserver<T> Create<T> (Action<T> onNext, Action <Exception> onError)
		{
			throw new NotImplementedException ();
		}
		
		public static IObserver<T> Create<T> (Action<T> onNext, Action<Exception> onError, Action onCompleted)
		{
			throw new NotImplementedException ();
		}
		
		public static IObserver<T> Synchronize<T> (IObserver<T> observer)
		{
			throw new NotImplementedException ();
		}
		
		public static IObserver<T> Synchronize<T> (IObserver<T> observer, Object gate)
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
