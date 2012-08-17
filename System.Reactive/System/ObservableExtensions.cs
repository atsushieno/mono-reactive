using System.ComponentModel;
using System.Reactive;

namespace System
{
	public static class ObservableExtensions
	{
		public static IDisposable Subscribe<T> (this IObservable<T> source)
		{
			return Subscribe (source, (tsrc) => {});
		}
		
		public static IDisposable Subscribe<T> (this IObservable<T> source, Action<T> onNext)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			return source.Subscribe (Observer.Create (onNext));
		}
		
		public static IDisposable Subscribe<T> (this IObservable<T> source, Action<T> onNext, Action<Exception> onError)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			return source.Subscribe (Observer.Create (onNext, onError));
		}
		
		public static IDisposable Subscribe<T> (this IObservable<T> source, Action<T> onNext, Action onCompleted)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			return source.Subscribe (Observer.Create (onNext, onCompleted));
		}
		
		public static IDisposable Subscribe<T> (this IObservable<T> source, Action<T> onNext, Action<Exception> onError, Action onCompleted)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			return source.Subscribe (Observer.Create (onNext, onError, onCompleted));
		}
		
#if REACTIVE_2_0
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		public static IDisposable SubscribeSafe<T> (this IObservable<T> source, IObserver<T> observer)
		{
			throw new NotImplementedException ();
		}
#endif
	}
}
