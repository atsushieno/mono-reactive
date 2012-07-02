using System;

namespace System.Reactive
{
#if REACTIVE_2_0
	public
#endif
	sealed class AnonymousObserver<T> : ObserverBase<T>
	{
		Action<T> onNext;
		Action<Exception> onError;
		Action onCompleted;
		
		public AnonymousObserver (Action<T> onNext)
			: this (onNext, () => {})
		{
		}
		
		public AnonymousObserver (Action<T> onNext, Action onCompleted)
			: this (onNext, ex => { throw ex; }, onCompleted)
		{
		}
		
		public AnonymousObserver (Action<T> onNext, Action<Exception> onError)
			: this (onNext, onError, () => {})
		{
		}
		
		public AnonymousObserver (Action<T> onNext, Action<Exception> onError, Action onCompleted)
		{
			if (onNext == null)
				throw new ArgumentNullException ("onNext");
			if (onError == null)
				throw new ArgumentNullException ("onError");
			if (onCompleted == null)
				throw new ArgumentNullException ("onCompleted");
			this.onNext = onNext;
			this.onError = onError;
			this.onCompleted = onCompleted;
		}

		#region implemented abstract members of System.Reactive.ObserverBase[T]
		protected override void OnCompletedCore ()
		{
			onCompleted ();
		}

		protected override void OnErrorCore (Exception error)
		{
			onError (error);
		}

		protected override void OnNextCore (T value)
		{
			onNext (value);
		}
		#endregion
	}
}

