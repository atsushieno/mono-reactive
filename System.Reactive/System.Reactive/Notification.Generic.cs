using System;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;

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

		// These were added against the documentation (they lack but they should exist)
		public override int GetHashCode ()
		{
			return
				(int) Kind +
				((Exception != null ? Exception.GetHashCode () : 0) << 9) +
				(HasValue ? Value.GetHashCode () : 0);
		}

		public IObservable<T> ToObservable ()
		{
			var sub = new ReplaySubject<T> ();
			ApplyToSubject (sub);
			return sub;
		}
		
		public IObservable<T> ToObservable (IScheduler scheduler)
		{
			var sub = new ReplaySubject<T> (scheduler);
			ApplyToSubject (sub);
			return sub;
		}
		
		void ApplyToSubject (ISubject<T> sub)
		{
			switch (Kind) {
			case NotificationKind.OnNext: sub.OnNext (Value); break;
			case NotificationKind.OnError: sub.OnError (Exception); break;
			case NotificationKind.OnCompleted: sub.OnCompleted (); break;
			}
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
				return other is OnCompleted;
			}

			public override string ToString ()
			{
				return "OnCompleted()";
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

			public override string ToString ()
			{
				return "OnError(" + error + ")";
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

			public override string ToString ()
			{
				return "OnNext(" + value + ")";
			}
		}
	}
}
