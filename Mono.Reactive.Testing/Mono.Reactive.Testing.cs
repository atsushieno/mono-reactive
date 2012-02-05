using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NUnit.Framework;

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
	
	internal class TestableObservable<T> : ITestableObservable<T>
	{
		IList<Recorded<Notification<T>>> messages;
		IList<Subscription> subscriptions = new List<Subscription> ();
		ISubject<T> subject;
		bool hot;
		TestScheduler scheduler;

		public TestableObservable (TestScheduler scheduler, bool hot, Recorded<Notification<T>> [] messages)
		{
			this.scheduler = scheduler;
			this.hot = hot;
			this.messages = messages;
			subject = hot ? (ISubject<T>) new Subject<T> () : new ReplaySubject<T> ();
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			var subscription = ReactiveTest.Subscribe (scheduler.Clock);
			subscriptions.Add (subscription);
			// FIXME: I wonder if Subscription records the actual disposal time. If so, this should return IDisposable that involves setting disposal time on the subscription instance.
			return subject.Subscribe (observer);
		}

		public IList<Recorded<Notification<T>>> Messages {
			get { return messages; }
		}
		
		public IList<Subscription> Subscriptions {
			get { return subscriptions; }
		}
	}
	
	internal class TestableObserver<T> : ITestableObserver<T>
	{
		TestScheduler scheduler;
		
		public TestableObserver (TestScheduler scheduler)
		{
			this.scheduler = scheduler;
			Messages = new List<Recorded<Notification<T>>> ();
		}

		public IList<Recorded<Notification<T>>> Messages { get; private set; }
		public void OnNext (T value)
		{
			Messages.Add (ReactiveTest.OnNext<T> (scheduler.Clock, value));
		}

		public void OnError (Exception exception)
		{
			Messages.Add (ReactiveTest.OnError<T> (scheduler.Clock, exception));
		}

		public void OnCompleted ()
		{
			Messages.Add (ReactiveTest.OnCompleted<T> (scheduler.Clock));
		}
	}
	
	public static class ReactiveAssert
	{
		public static void AreElementsEqual<T> (IEnumerable<T> expected, IEnumerable<T> actual)
		{
			AreElementsEqual (expected, actual, "Enumerated items are not equal");
		}
		
		public static void AreElementsEqual<T> (IObservable<T> expected, IObservable<T> actual)
		{
			AreElementsEqual (expected, actual, "Observed items are not equal");
		}
		
		public static void AreElementsEqual<T> (IEnumerable<T> expected, IEnumerable<T> actual, string message)
		{
			if (expected == null) {
				if (actual != null)
					throw new ArgumentNullException ("expected");
				else
					return;
			}
			var ee = expected.GetEnumerator ();
			var ae = actual.GetEnumerator ();
			int i = 0;
			for (; ee.MoveNext (); i++) {
				if (!ae.MoveNext ())
					Assert.Fail (String.Format ("{0} (Insufficient items, ended at index {1})", message, i));
				Assert.AreEqual (ee.Current, ae.Current, String.Format ("{0} (items at index {1})", message, i));
			}
			if (ae.MoveNext ())
				Assert.Fail (String.Format ("{0} (Extra items, after index {1})", message, i));
		}
		
		struct Indexed<T>
		{
			public Indexed (int i, T value)
			{
				index = i;
				this.value = value;
			}
			int index;
			T value;
			
			public int Index { get { return index; } }
			public T Value { get { return value; } }
		}
		
		public static void AreElementsEqual<T> (IObservable<T> expected, IObservable<T> actual, string message)
		{
			if (expected == null) {
				if (actual != null)
					throw new ArgumentNullException ("expected");
				else
					return;
			}

			int ie = 0, ia = 0, endE = 0, endA = 0;
			var iex = expected.Select (e => new Indexed<T> (ie++, e)).Finally (() => endE = ie);
			var iac = actual.Select (e => new Indexed<T> (ia++, e)).Finally (() => endA = ia);
			var source = iex.Zip (iac, (e, a) => { Assert.AreEqual (e.Value, a.Value, String.Format ("{0} (Items differ at index {1})", message, e.Index)); return Unit.Default; });
			var dis = new SingleAssignmentDisposable ();
			dis.Disposable = source.Finally<Unit> (() => dis.Dispose ()).Subscribe (v => {}, () => Assert.AreEqual (endE, endA, String.Format ("{0} (Items counts differ: expected {1} but got {2})", endE, endA)));
		}
		
		public static void Throws<TException> (Action action)
			where TException : Exception
		{
			Throws<TException> (action, "Should raise " + typeof (TException));
		}
		
		public static void Throws<TException> (Action action, string message)
			where TException : Exception
		{
			try {
				action ();
				Assert.Fail (message);
			} catch (Exception ex) {
				// FIXME: should this be IsAssignableFrom() ?
				Assert.AreEqual (typeof (TException), ex.GetType (), message);
			}
		}
		
		public static void Throws<TException> (TException exception, Action action)
			where TException : Exception
		{
			Throws<TException> (exception, action, "Should raise " + typeof (TException));
		}
		
		public static void Throws<TException> (TException exception, Action action, string message)
			where TException : Exception
		{
			try {
				action ();
				Assert.Fail (message);
			} catch (Exception ex) {
				Assert.AreEqual (exception, ex, message);
			}
		}
	}
	
	public class ReactiveTest
	{
		public const long Created = 100;
		public const long Disposed = 1000;
		public const long Subscribed = 200;

		public static Recorded<Notification<T>> OnCompleted<T> (long ticks)
		{
			return new Recorded<Notification<T>> (ticks, Notification.CreateOnCompleted<T> ());
		}
		
		public static Recorded<Notification<T>> OnError<T> (long ticks, Exception exception)
		{
			return new Recorded<Notification<T>> (ticks, Notification.CreateOnError<T> (exception));
		}
		
		public static Recorded<Notification<T>> OnNext<T> (long ticks, T value)
		{
			return new Recorded<Notification<T>> (ticks, Notification.CreateOnNext<T> (value));
		}
		
		public static Subscription Subscribe (long start)
		{
			return new Subscription (start);
		}
		
		public static Subscription Subscribe (long start, long end)
		{
			return new Subscription (start, end);
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
		// VirtualTimeScheduler members.
		
		protected override long Add (long absolute, long relative)
		{
			return absolute + relative;
		}
		
		protected override DateTimeOffset ToDateTimeOffset (long absolute)
		{
			return new DateTimeOffset (DateTime.MinValue.AddTicks (absolute), TimeSpan.Zero);
		}
		
		protected override long ToRelative (TimeSpan timeSpan)
		{
			return timeSpan.Ticks;
		}
		
		// TestScheduler specific.
		
		public ITestableObservable<T> CreateColdObservable<T> (params Recorded<Notification<T>> [] messages)
		{
			return new TestableObservable<T> (this, false, messages);
		}
		
		public ITestableObservable<T> CreateHotObservable<T> (params Recorded<Notification<T>> [] messages)
		{
			return new TestableObservable<T> (this, true, messages);
		}
		
		public ITestableObserver<T> CreateObserver<T> ()
		{
			return new TestableObserver<T> (this);
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
	}
}
