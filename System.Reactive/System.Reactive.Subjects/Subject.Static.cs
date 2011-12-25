using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;

namespace System.Reactive.Subjects
{
	public static class Subject
	{
		public static ISubject<TSource, TResult> Create<TSource, TResult> (IObserver<TSource> observer, IObservable<TResult> observable)
		{
			return new DefaultVariantSubject<TSource, TResult> (observer, observable);
		}
		
		public static ISubject<TSource, TResult> Synchronize<TSource, TResult> (ISubject<TSource, TResult> subject)
		{
			throw new NotImplementedException ();
		}
		
		public static ISubject<TSource, TResult> Synchronize<TSource, TResult> (ISubject<TSource, TResult> subject, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}

		internal class DefaultVariantSubject<TSource, TResult> : ISubject<TSource, TResult>
		{
			public DefaultVariantSubject (IObserver<TSource> observer, IObservable<TResult> observable)
			{
				if (observer == null)
					throw new ArgumentNullException ("observer");
				if (observable == null)
					throw new ArgumentNullException ("observable");
				this.observer = observer;
				this.observable = observable;
			}
			
			IObserver<TSource> observer;
			IObservable<TResult> observable;

			IDisposable IObservable<TResult>.Subscribe (IObserver<TResult> observer)
			{
				return observable.Subscribe (observer);
			}
			
			void IObserver<TSource>.OnCompleted ()
			{
				observer.OnCompleted ();
			}
			
			void IObserver<TSource>.OnError (Exception error)
			{
				observer.OnError (error);
			}
			
			void IObserver<TSource>.OnNext (TSource value)
			{
				observer.OnNext (value);
			}
		}
	}
}
