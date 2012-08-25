using System;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Threading;

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
		
		public static IObserver<T> Checked<T> (this IObserver<T> observer)
		{
			throw new NotImplementedException ();
		}

		public static IObserver<T> Create<T> (Action<T> onNext)
		{
			return Create (onNext, () => {});
		}
		
		public static IObserver<T> Create<T> (Action<T> onNext, Action onCompleted)
		{
			return Create (onNext, (ex) => { throw ex; }, onCompleted);
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
			return Synchronize (observer, false);
		}

		public static IObserver<T> Synchronize<T> (IObserver<T> observer, bool preventReentrancy)
		{
			if (preventReentrancy)
				throw new NotImplementedException ();
			return Synchronize (observer, new object ());
		}
		
		public static IObserver<T> Synchronize<T> (IObserver<T> observer, object gate)
		{
			return new SynchronizedObserver<T> (observer, gate);
		}

#if REACTIVE_2_0		
		public static IObserver<T> Synchronize<T> (IObserver<T> observer, AsyncLock gate)
		{
			throw new NotImplementedException ();
		}
#endif

		public static Action<Notification<T>> ToNotifier<T> (this IObserver<T> observer)
		{
			if (observer == null)
				throw new ArgumentNullException ("observer");

			return delegate (Notification<T> n) {
				switch (n.Kind) {
				case NotificationKind.OnCompleted:
					observer.OnCompleted ();
					break;
				case NotificationKind.OnError:
					observer.OnError (n.Exception);
					break;
				case NotificationKind.OnNext:
					observer.OnNext (n.Value);
					break;
				}
			};
		}
		
		public static IObserver<T> ToObserver<T> (this Action<Notification<T>> handler)
		{
			if (handler == null)
				throw new ArgumentNullException ("handler");
			return new NotifiedObserver<T> (handler);
		}

		internal class NotifiedObserver<T> : IObserver<T>
		{
			Action<Notification<T>> handler;
			
			public NotifiedObserver (Action<Notification<T>> handler)
			{
				this.handler = handler;
			}
			
			public void OnCompleted ()
			{
				handler (Notification.CreateOnCompleted<T> ());
			}
			
			public void OnError (Exception ex)
			{
				handler (Notification.CreateOnError<T> (ex));
			}
			
			public void OnNext (T value)
			{
				handler (Notification.CreateOnNext <T> (value));
			}
		}

#if REACTIVE_2_0
		public static IObserver<T> NotifyOn<T> (this IObserver<T> observer, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}

		public static IObserver<T> NotifyOn<T> (this IObserver<T> observer, SynchronizationContext context)
		{
			throw new NotImplementedException ();
		}
#endif
	}
}
