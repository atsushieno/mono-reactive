using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	[SerializableAttribute]
	public abstract class Notification<T> : IEquatable<Notification<T>>
	{
		public abstract Exception Exception { get; }
		public abstract bool HasValue { get; }
		public abstract NotificationKind Kind { get; }
		public abstract T Value { get; }

		public abstract void Accept (IObserver<T> observer);
		public abstract void Accept (Action<T> onNext, Action<Exception> onError, Action onCompleted);
		public abstract TResult Accept<TResult> (Func<T, TResult> onNext, Func<Exception, TResult> onError, Func<TResult> onCompleted);

		public override bool Equals (object obj)
		{
			var n = obj as Notification<T>;
			return n != null && Equals (n);
		}
		
		public abstract bool Equals (Notification<T> other);

		public static bool operator == (Notification<T> left, Notification<T> right)
		{
			return (object) left == null ? (object) right == null : left.Equals (right);
		}

		public static bool operator != (Notification<T> left, Notification<T> right)
		{
			return (object) left == null ? (object) right != null : !left.Equals (right);
		}

		public IObservable<T> ToObservable ()
		{
			throw new NotImplementedException ();
		}
		
		public IObservable<T> ToObservable (IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
	}
}
