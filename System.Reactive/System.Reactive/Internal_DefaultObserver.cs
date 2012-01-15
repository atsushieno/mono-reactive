using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	internal class DefaultObserver<T> : IObserver<T>
	{
		Action<T> on_next;
		Action<Exception> on_error;
		Action on_completed;

		public DefaultObserver (Action<T> onNext, Action<Exception> onError, Action onCompleted)
		{
			on_next = onNext;
			on_error = onError;
			on_completed = onCompleted;
		}
		
		public void OnCompleted ()
		{
			on_completed ();
		}
		
		public void OnError (Exception error)
		{
			on_error (error);
		}
		
		public void OnNext (T value)
		{
			on_next (value);
		}
	}
}
