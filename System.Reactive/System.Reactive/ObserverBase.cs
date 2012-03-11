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
		
		public virtual void Dispose ()
		{
		}

		public void OnCompleted ()
		{
			Completed ();
		}

		public void OnError (Exception error)
		{
			Error (error);
		}

		public void OnNext (T value)
		{
			OnNext (value);
		}

		protected abstract void Completed ();

		protected abstract void Error (Exception error);

		protected abstract void Next (T value);
	}
}

