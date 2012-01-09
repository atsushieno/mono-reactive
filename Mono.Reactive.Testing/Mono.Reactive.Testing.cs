using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Mono.Reactive.Testing
{
	public interface ITestableObservable<T> : IObservable<T>
	{
		IList<Recorded<Notification<T>>> Messages { get; }
		IList<Subscription> Subscriptions { get; }
	}

	public interface ITestableObserver<T> : IObserver<T>
	{
		IList<Recorded<Notification<T>>> Messages { get; }
	}
	
	public static class ReactiveAssert
	{
		public static void AreElementsEqual<T> (IEnumerable<T> expected, IEnumerable<T> actual)
		{
			throw new NotImplementedException ();
		}
		
		public static void AreElementsEqual<T> (IObservable<T> expected, IObservable<T> actual)
		{
			throw new NotImplementedException ();
		}
		
		public static void AreElementsEqual<T> (IEnumerable<T> expected, IEnumerable<T> actual, string message)
		{
			throw new NotImplementedException ();
		}
		
		public static void AreElementsEqual<T> (IObservable<T> expected, IObservable<T> actual, string message)
		{
			throw new NotImplementedException ();
		}
		
		public static void Throws<TException> (Action action)
			where TException : Exception
		{
			throw new NotImplementedException ();
		}
		
		public static void Throws<TException> (Action action, string message)
			where TException : Exception
		{
			throw new NotImplementedException ();
		}
		
		public static void Throws<TException> (TException exception, Action action)
			where TException : Exception
		{
			throw new NotImplementedException ();
		}
		
		public static void Throws<TException> (TException exception, Action action, string message)
			where TException : Exception
		{
			throw new NotImplementedException ();
		}
	}
	
	public class ReactiveTest
	{
		public const long Created = 100;
		public const long Disposed = 1000;
		public const long Subscribed = 200;

		public static Recorded<Notification<T>> OnCompleted<T> (long ticks)
		{
			throw new NotImplementedException ();
		}
		
		public static Recorded<Notification<T>> OnError<T> (long ticks, Exception exception)
		{
			throw new NotImplementedException ();
		}
		
		public static Recorded<Notification<T>> OnNext<T> (long ticks, T value)
		{
			throw new NotImplementedException ();
		}
		
		public static Subscription Subscribe (long start)
		{
			throw new NotImplementedException ();
		}
		
		public static Subscription Subscribe (long start, long end)
		{
			throw new NotImplementedException ();
		}
	}

	[SerializableAttribute]
	public struct Recorded<T> : IEquatable<Recorded<T>>
	{
		public Recorded (long time, T value)
		{
			this.time = time;
			this.value = value;
		}
		
		long time;
		T value;
		
		public long Time { get { return time; } }
		public T Value { get { return value; } }
		
		public override bool Equals (object obj)
		{
			return obj is Recorded<T> && Equals ((Recorded<T>) obj);
		}
		
		public bool Equals (Recorded<T> other)
		{
			return time == other.time && value.Equals (other.value);
		}
		
		public override int GetHashCode ()
		{
			return (int) time + value.GetHashCode ();
		}
		
		public override string ToString ()
		{
			return value + "@" + time;
		}

		public static bool operator == (Recorded<T> left, Recorded<T> right)
		{
			return left.Equals (right);
		}
		
		public static bool operator != (Recorded<T> left, Recorded<T> right)
		{
			return !left.Equals (right);
		}
	}

	[SerializableAttribute]
	public struct Subscription : IEquatable<Subscription>
	{
		public const long Infinite = long.MaxValue;

		public Subscription (long subscribe)
			: this (subscribe, Infinite)
		{
		}
		
		public Subscription (long subscribe, long unsubscribe)
		{
			sub = subscribe;
			unsub = unsubscribe;
		}
		
		long sub, unsub;
		
		public long Subscribe { get { return sub; } }
		
		public long Unsubscribe { get { return unsub; } }
		
		public override bool Equals (object obj)
		{
			return obj is Subscription && Equals ((Subscription) obj);
		}
		
		public bool Equals (Subscription other)
		{
			return sub == other.sub && unsub == other.unsub;
		}
		
		public override int GetHashCode ()
		{
			return (int) ((sub << 17) + unsub);
		}
		
		public override string ToString ()
		{
			return String.Format ("({0}, {1})", sub, unsub == Infinite ? "Infinite" : unsub.ToString (CultureInfo.InvariantCulture));
		}
		
		public static bool operator == (Subscription left, Subscription right)
		{
			return left.Equals (right);
		}
		
		public static bool operator != (Subscription left, Subscription right)
		{
			return !left.Equals (right);
		}
	}

	public class TestScheduler : VirtualTimeScheduler<long, long>
	{
		protected override long Add (long absolute, long relative)
		{
			return absolute + relative;
		}
		
		public ITestableObservable<T> CreateColdObservable<T> (params Recorded<Notification<T>> [] messages)
		{
			throw new NotImplementedException ();
		}
		
		public ITestableObservable<T> CreateHotObservable<T> (params Recorded<Notification<T>> [] messages)
		{
			throw new NotImplementedException ();
		}
		
		public ITestableObserver<T> CreateObserver<T> ()
		{
			throw new NotImplementedException ();
		}
		
		public override IDisposable ScheduleAbsolute<TState> (TState state, long dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			throw new NotImplementedException ();
		}
		
		public ITestableObserver<T> Start<T> (Func<IObservable<T>> create)
		{
			throw new NotImplementedException ();
		}
		
		public ITestableObserver<T> Start<T> (Func<IObservable<T>> create, long disposed)
		{
			throw new NotImplementedException ();
		}
		
		public ITestableObserver<T> Start<T> (Func<IObservable<T>> create, long created, long subscribed, long disposed)
		{
			throw new NotImplementedException ();
		}
		
		protected override DateTimeOffset ToDateTimeOffset (long absolute)
		{
			return new DateTimeOffset (new DateTime (absolute));
		}
		
		protected override long ToRelative (TimeSpan timeSpan)
		{
			return timeSpan.Ticks;
		}
	}
}
