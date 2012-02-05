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

namespace System.Reactive.Concurrency.Tests
{
	[TestFixture]
	public class HistoricalSchedulerTest
	{
		class MyHistoricalScheduler : HistoricalScheduler
		{
			public DateTimeOffset PublicAdd (TimeSpan time)
			{
				return Add (Clock, time);
			}
			
			public TimeSpan PublicToRelative (TimeSpan time)
			{
				return ToRelative (time);
			}
			
			public IScheduledItem<DateTimeOffset> PublicGetNext ()
			{
				return GetNext ();
			}
		}
	
		[Test]
		public void Clock ()
		{
			var scheduler = new HistoricalScheduler ();
			Assert.AreEqual (DateTimeOffset.MinValue, scheduler.Clock, "#1"); // default
			scheduler.AdvanceBy (TimeSpan.FromDays (1));
			Assert.AreEqual (DateTimeOffset.MinValue.AddDays (1), scheduler.Clock, "#2");
			scheduler.AdvanceTo (new DateTimeOffset (2012, 1, 1, 0, 0, 0, TimeSpan.Zero));
			Assert.AreEqual (2012, scheduler.Clock.Year, "#3");
		}
		
		[Test]
		public void AdvanceByRaisesEvent ()
		{
			var scheduler = new MyHistoricalScheduler ();
			Assert.AreEqual (DateTimeOffset.MinValue.AddDays (1), scheduler.PublicAdd (TimeSpan.FromDays (1)), "#0");
			Assert.AreEqual (TimeSpan.FromDays (1), scheduler.PublicToRelative (TimeSpan.FromDays (1)), "#0-2");
			var source = Observable.Interval (TimeSpan.FromDays (1), scheduler);
			int x = 0;
			var dis = source.Subscribe (v => x++);
			Assert.IsNotNull (scheduler.PublicGetNext (), "#1");
			Assert.AreEqual (0, x, "#2");
			scheduler.AdvanceBy (TimeSpan.FromHours (1));
			Assert.AreEqual (0, x, "#3");
			scheduler.AdvanceBy (TimeSpan.FromDays (1));
			var item = scheduler.PublicGetNext ();
			Assert.IsNotNull (item, "#5");
			Assert.AreEqual (1, x, "#5");
			dis.Dispose ();
		}
		
		[Test]
		public void AdvanceByRaisesEvent2 ()
		{
			// This is actually very complicated pattern of error, caused by all of:
			// - Concat() when it internally uses SerialDisposable instead of CompositeDisposable.
			// - Delay() with non-zero duration.
			// - Delay() with non-CurrentThreadScheduler.
			var scheduler = new HistoricalScheduler ();
			var o = Observable.Empty<int> (scheduler).Delay (TimeSpan.FromSeconds (1), scheduler);
			bool done = false;
			Observable.Range (0, 3).Concat (o).Subscribe (v => {}, () => done = true);
			scheduler.AdvanceBy (TimeSpan.FromSeconds (2));
			Assert.IsTrue (done, "#1");
		}
	}
}
