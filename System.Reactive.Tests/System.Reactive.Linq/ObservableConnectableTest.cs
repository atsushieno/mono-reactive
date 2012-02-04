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
	public class ObservableConnectableTest
	{
		[Test]
		public void Publish ()
		{
			var source = Observable.Range (2, 3, Scheduler.Immediate);

			int result = 1;
			bool started = false;
			var published = source.Publish ();
			var pdis1 = published.Subscribe (i => { started = true; result *= i; });
			Assert.IsFalse (started, "#1");
			var cdis = published.Connect ();
			Assert.IsTrue (started, "#2");
			Assert.AreEqual (24, result, "#3");
			var pdis2 = published.Subscribe (i => { started = true; result *= i; });
			Assert.AreEqual (24, result, "#3"); // but it should not happen.
			cdis.Dispose (); // disconnect
			pdis1.Dispose ();
			pdis2.Dispose ();
		}

		[Test]
		public void PublishConnectTwice ()
		{
			var source = Observable.Range (2, 3, Scheduler.Immediate);

			int result = 1;
			bool started = false;
			var published = source.Publish ();
			var pdis1 = published.Subscribe (i => { started = true; result *= i; });
			Assert.IsFalse (started, "#1");
			var cdis1 = published.Connect ();
			var cdis2 = published.Connect (); // no error
			Assert.AreEqual (cdis1, cdis2, "#2");
			pdis1.Dispose ();
			cdis1.Dispose ();
		}
		
		[Test]
		public void PublishReconnect ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = Observable.Interval (TimeSpan.FromMilliseconds (50), scheduler);

			int result = 0;
			var published = source.Publish ();
			var pdis1 = published.Subscribe (i => result++);
			Assert.AreEqual (0, result, "#0");
			var cdis1 = published.Connect ();
			scheduler.AdvanceBy (TimeSpan.FromMilliseconds (200)); // should be enough to receive some events
			Assert.IsTrue (result > 0, "#1");
			pdis1.Dispose ();
			cdis1.Dispose (); // disconnect
			int oldResult = result;
			scheduler.AdvanceBy (TimeSpan.FromMilliseconds (200)); // should be enough to raise interval event if it were active (which should *not*)
			Assert.AreEqual (oldResult, result, "#2");
			var cdis2 = published.Connect ();
			scheduler.AdvanceBy (TimeSpan.FromMilliseconds (400)); // should be enough to receive some events
			Assert.IsTrue (result > oldResult, "#3");
			cdis2.Dispose ();
		}

		[Test]
		public void PublishLast ()
		{
			var scheduler = new HistoricalScheduler ();
			var hot = Observable.Interval (TimeSpan.FromMilliseconds (20), scheduler).Skip (4).Take (2).PublishLast ();
			hot.Connect ();
			var observable = hot.Replay ();
			observable.Connect ();
			long result = 0;
			var dis = observable.Subscribe (i => result += i);
			scheduler.AdvanceBy (TimeSpan.FromSeconds (1));
			Assert.AreEqual (5, result, "#1");
			dis.Dispose ();
			var dis2 = observable.Subscribe (i => result += i);
			Assert.AreEqual (10, result, "#2");
			dis2.Dispose ();
		}

		[Test]
		public void RefCount ()
		{
			int side = 0;
			var scheduler = new HistoricalScheduler ();
			var source = Observable.Interval (TimeSpan.FromMilliseconds (50), scheduler).Do (i => side++);

			int result = 0;
			var published = source.Publish ();
			var connected = published.RefCount ();
			var cdis1 = connected.Subscribe (i => result++);
			scheduler.AdvanceBy (TimeSpan.FromMilliseconds (400));
			Assert.IsTrue (result > 0, "#1");
			cdis1.Dispose (); // also disconnects.

			int oldSide = side;
			scheduler.AdvanceBy (TimeSpan.FromMilliseconds (400));
			Assert.AreEqual (oldSide, side, "#2"); // no advance in sequence

			var cdis2 = connected.Subscribe (i => result++);
			scheduler.AdvanceBy (TimeSpan.FromSeconds (1));
			Assert.IsTrue (side > oldSide, "#3");
			cdis2.Dispose ();
		}

		[Test]
		public void Replay ()
		{
			var scheduler = new HistoricalScheduler ();
			var hot = Observable.Interval (TimeSpan.FromMilliseconds (20), scheduler).Take (5).Publish ();
			hot.Connect ();
			var observable = hot.Replay ();
			observable.Connect ();
			int result = 0;
			var dis1 = observable.Subscribe (i => result++);
			scheduler.AdvanceBy (TimeSpan.FromSeconds (1));
			Assert.AreEqual (5, result, "#1");
			var dis2 = observable.Subscribe (i => result++);
			Assert.AreEqual (10, result, "#1");
			dis1.Dispose ();
			dis2.Dispose ();
		}
	}
}
