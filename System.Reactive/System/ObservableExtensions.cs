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
			return source.Subscribe (Observer.Create (onNext));
		}
		
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source, Action<TSource> onNext, Action<Exception> onError)
		{
			return source.Subscribe (Observer.Create (onNext, onError));
		}
		
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source, Action<TSource> onNext, Action onCompleted)
		{
			return source.Subscribe (Observer.Create (onNext, onCompleted));
		}
		
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source, Action<TSource> onNext, Action<Exception> onError, Action onCompleted)
		{
			return source.Subscribe (Observer.Create (onNext, onError, onCompleted));
		}
	}
}
