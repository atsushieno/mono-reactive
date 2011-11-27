using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	internal class WrappedObserver<T> : IObserver<T>
	{
		IObserver<T> wrapped;
		
		public WrappedObserver (IObserver<T> wrapped)
		{
			this.wrapped = wrapped;
		}
		
		public void OnCompleted ()
		{
			wrapped.OnCompleted ();
		}
		
		public void OnError (Exception error)
		{
			wrapped.OnError (error);
		}
		
		public void OnNext (T value)
		{
			wrapped.OnNext (value);
		}
	}
}
