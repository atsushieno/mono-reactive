using System.Reactive;

namespace System
{
	public static class ObservableExtensions
	{
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source)
		{
			return Subscribe (source, (tsrc) => {});
		}
		
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source, Action<TSource> onNext)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			return source.Subscribe (Observer.Create (onNext));
		}
		
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source, Action<TSource> onNext, Action<Exception> onError)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			return source.Subscribe (Observer.Create (onNext, onError));
		}
		
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source, Action<TSource> onNext, Action onCompleted)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			return source.Subscribe (Observer.Create (onNext, onCompleted));
		}
		
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source, Action<TSource> onNext, Action<Exception> onError, Action onCompleted)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			return source.Subscribe (Observer.Create (onNext, onError, onCompleted));
		}
		
#if REACTIVE_2_0
		public static IDisposable SubscribeSafe<T> (this IObservable<T> source, IObserver<T> observer)
		{
			throw new NotImplementedException ();
		}
#endif
	}
}
