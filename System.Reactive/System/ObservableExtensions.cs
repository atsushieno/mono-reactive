namespace System
{
	public static class ObservableExtensions
	{
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source, Action<TSource> onNext)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source, Action<TSource> onNext, Action<Exception> onError)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source, Action<TSource> onNext, Action onCompleted)
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable Subscribe<TSource> (this IObservable<TSource> source, Action<TSource> onNext, Action<Exception> onError, Action onCompleted)
		{
			throw new NotImplementedException ();
		}
	}
}
