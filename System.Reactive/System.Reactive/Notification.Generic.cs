using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	[SerializableAttribute]
	public abstract class Notification<T> : IEquatable<Notification<T>>
	{
		internal Notification ()
		{
		}

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

		// It is public in Microsoft.Phone.Reactive
		public class OnCompleted : Notification<T>
		{
			public override Exception Exception {
				get { return null; }
			}
			public override bool HasValue {
				get { return false; }
			}
			public override NotificationKind Kind {
				get { return NotificationKind.OnCompleted; }
			}
			public override T Value {
				get { return default (T); }
			}
			
			public override void Accept (IObserver<T> observer)
			{
				if (observer == null)
					throw new ArgumentNullException ("observer");
				observer.OnCompleted ();
			}
			
			public override void Accept (Action<T> onNext, Action<Exception> onError, Action onCompleted)
			{
				onCompleted ();
			}
			
			public override TResult Accept<TResult> (Func<T, TResult> onNext, Func<Exception, TResult> onError, Func<TResult> onCompleted)
			{
				return onCompleted ();
			}
			
			public override bool Equals (Notification<T> other)
			{
				return object.ReferenceEquals (this, other);
			}
		}

		// It is public in Microsoft.Phone.Reactive
		public class OnError : Notification<T>
		{
			Exception error;
			
			public OnError (Exception error)
			{
				this.error = error;
			}
			
			public override Exception Exception {
				get { return error; }
			}
			public override bool HasValue {
				get { return false; }
			}
			public override NotificationKind Kind {
				get { return NotificationKind.OnError; }
			}
			public override T Value {
				get { return default (T); }
			}
			
			public override void Accept (IObserver<T> observer)
			{
				if (observer == null)
					throw new ArgumentNullException ("observer");
				observer.OnError (error);
			}
			
			public override void Accept (Action<T> onNext, Action<Exception> onError, Action onCompleted)
			{
				onError (error);
			}
			
			public override TResult Accept<TResult> (Func<T, TResult> onNext, Func<Exception, TResult> onError, Func<TResult> onCompleted)
			{
				return onError (error);
			}
			
			public override bool Equals (Notification<T> other)
			{
				var e = other as OnError;
				return (object) e != null && error.Equals (e.error);
			}
		}

		// It is public in Microsoft.Phone.Reactive
		public class OnNext : Notification<T>
		{
			T value;
			
			public OnNext (T value)
			{
				this.value = value;
			}
			
			public override Exception Exception {
				get { return null; }
			}
			public override bool HasValue {
				get { return true; }
			}
			public override NotificationKind Kind {
				get { return NotificationKind.OnNext; }
			}
			public override T Value {
				get { return value; }
			}
			
			public override void Accept (IObserver<T> observer)
			{
				if (observer == null)
					throw new ArgumentNullException ("observer");
				observer.OnNext (value);
			}
			
			public override void Accept (Action<T> onNext, Action<Exception> onError, Action onCompleted)
			{
				onNext (value);
			}
			
			public override TResult Accept<TResult> (Func<T, TResult> onNext, Func<Exception, TResult> onError, Func<TResult> onCompleted)
			{
				return onNext (value);
			}
			
			public override bool Equals (Notification<T> other)
			{
				var n = other as OnNext;
				return (object) n != null && value.Equals (n.value);
			}
		}
	}
}
