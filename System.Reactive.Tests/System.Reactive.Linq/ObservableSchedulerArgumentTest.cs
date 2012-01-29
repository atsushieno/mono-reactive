using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using NUnit.Framework;

namespace System.Reactive.Linq.Tests
{
	[TestFixture]
	public class ObservableSchedulerArgumentTest
	{
		class MyException : Exception
		{
		}
		
		class ErrorScheduler : IScheduler
		{
			public DateTimeOffset Now { get { return DateTimeOffset.Now; } }
			public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
			{
				throw new MyException ();
			}
			public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
			{
				throw new MyException ();
			}
			public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
			{
				throw new MyException ();
			}
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void BufferScheduler ()
		{
			var o = Observable.Range (1, 3).Buffer (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void BufferSchedulerEmpty ()
		{
			var o = Observable.Range (0, 0).Buffer (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void DelayScheduler ()
		{
			var o = Observable.Range (1, 3).Delay (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void DelaySchedulerEmpty ()
		{
			var o = Observable.Empty<int> ().Delay (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void EmptyScheduler ()
		{
			var o = Observable.Empty<int> (new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void GenerateScheduler ()
		{
			var o = Observable.Generate<int,int> (0, i => i < 5, i => i + 1, i => i, new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void GenerateSchedulerEmpty ()
		{
			var o = Observable.Generate<int,int> (0, i => i < 0, i => i + 1, i => i, new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void IntervalScheduler ()
		{
			var o = Observable.Interval (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void MergeScheduler ()
		{
			var o = Observable.Range (0, 3).Merge (Observable.Range (4, 3), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void MergeSchedulerEmpty ()
		{
			var o = Observable.Empty<int> ().Merge (Observable.Empty<int> (), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void ObserveOnScheduler ()
		{
			var o = Observable.Range (0, 3).ObserveOn (new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void ObserveOnSchedulerEmpty ()
		{
			// empty, still schedules.
			var o = Observable.Empty<int> ().ObserveOn (new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void RangeScheduler ()
		{
			var o = Observable.Range (0, 3, new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void RangeSchedulerEmpty ()
		{
			var o = Observable.Range (0, 0, new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void RepeatScheduler ()
		{
			var o = Observable.Repeat (0, new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		/*
		[Test]
		[ExpectedException (typeof (MyException))]
		public void ReplayScheduler ()
		{
			var o = Observable.Range (0, 3).Replay (2, new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		*/

		[Test]
		[ExpectedException (typeof (MyException))]
		public void ReturnScheduler ()
		{
			var o = Observable.Return (0, new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void SampleScheduler ()
		{
			var o = Observable.Range (0, 3).Sample (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void SampleSchedulerEmpty ()
		{
			var o = Observable.Empty<int> ().Sample (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void StartScheduler ()
		{
			var o = Observable.Start (() => {}, new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void StartWithScheduler ()
		{
			var o = Observable.Range (0, 3).StartWith (new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void SubscribeOnScheduler ()
		{
			var o = Observable.Range (0, 3).StartWith (new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void ThrottleScheduler ()
		{
			var o = Observable.Range (0, 3).Throttle (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		public void ThrottleSchedulerEmpty ()
		{
			// empty causes no subscription.
			var o = Observable.Empty<int> ().Throttle (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void ThrowScheduler ()
		{
			var o = Observable.Throw<int> (new Exception (), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test] // does not schedule
		public void TimeIntervalScheduler ()
		{
			var o = Observable.Range (0, 3).TimeInterval (new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void TimeoutScheduler ()
		{
			var o = Observable.Range (0, 3).Timeout (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void TimeoutSchedulerEmpty ()
		{
			var o = Observable.Range (0, 0).Timeout (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void TimeoutSchedulerTimeout ()
		{
			// timeout with no value, still raises an error.
			var o = Observable.Interval (TimeSpan.FromMilliseconds (100)).Take (5).Timeout (TimeSpan.FromMilliseconds (10), new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void TimerScheduler ()
		{
			var o = Observable.Timer (DateTimeOffset.Now, new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
		
		[Test] // does not schedule
		public void TimestampScheduler ()
		{
			var o = Observable.Range (1, 3).Timestamp (new ErrorScheduler ()); // This ensures that Timestamp() does *not* use IScheduler to *schedule* tasks.
			var a = from t in o.ToEnumerable () select t.Value;
			Assert.AreEqual (new int [] {1, 2, 3}, a.ToArray (), "#1");
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void ToAsyncScheduler ()
		{
			var o = Observable.ToAsync (() => {}, new ErrorScheduler ());
			foreach (var i in o ().ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void ToObservableScheduler ()
		{
			var o = Enumerable.Range (0, 3).ToObservable (new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void ToObservableSchedulerEmpty ()
		{
			var o = Enumerable.Range (0, 0).ToObservable (new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void WindowScheduler ()
		{
			var o = Observable.Range (0, 3).Window (TimeSpan.FromMilliseconds (10), 2, new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void WindowSchedulerEmpty ()
		{
			var o = Observable.Range (0, 0).Window (TimeSpan.FromMilliseconds (10), 2, new ErrorScheduler ());
			foreach (var i in o.ToEnumerable ())
				;
		}
	}
}
