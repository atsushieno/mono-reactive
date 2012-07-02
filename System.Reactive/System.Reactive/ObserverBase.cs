using System;

namespace System.Reactive
{
#if REACTIVE_2_0
	public
#endif
	abstract class ObserverBase<T> : IObserver<T>, IDisposable
	{
		protected ObserverBase ()
		{
		}
		
		public void Dispose ()
		{
			Dispose (true);
		}
		
		protected virtual void Dispose (bool disposing)
		{
		}

		public void OnCompleted ()
		{
			OnCompletedCore ();
		}

		public void OnError (Exception error)
		{
			OnErrorCore (error);
		}

		public void OnNext (T value)
		{
			OnNext (value);
		}

		protected abstract void OnCompletedCore ();

		protected abstract void OnErrorCore (Exception error);

		protected abstract void OnNextCore (T value);
	}
}

