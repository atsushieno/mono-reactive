using System;

namespace System.Reactive
{
	class SynchronizedObservable<T> : IObservable<T>
	{
		public SynchronizedObservable (IObservable<T> source, object gate)
		{
			this.source = source;
			this.gate = gate;
		}
		IObservable<T> source;
		object gate;
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			return source.Subscribe (new SynchronizedObserver<T> (observer, gate));
		}
	}
	
	class SynchronizedObserver<T> : IObserver<T>
	{
		IObserver<T> observer;
		object gate;
		
		public SynchronizedObserver (IObserver<T> observer, object gate)
		{
			this.observer = observer;
			this.gate = gate;
		}
		
		public void OnNext (T value)
		{
			lock (gate)
				observer.OnNext (value);
		}
		
		public void OnError (Exception error)
		{
			lock (gate)
				observer.OnError (error);
		}
		
		public void OnCompleted ()
		{
			lock (gate)
				observer.OnCompleted ();
		}
	}
}

