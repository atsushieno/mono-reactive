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

namespace Mono.Reactive.Testing.Tests
{
	[TestFixture]
	public class TestSchedulerTest
	{
		class MyTestScheduler : TestScheduler
		{
			public long PublicAdd (long time)
			{
				return Add (Clock, time);
			}
			
			public long PublicToRelative (TimeSpan time)
			{
				return ToRelative (time);
			}
			
			public IScheduledItem<long> PublicGetNext ()
			{
				return GetNext ();
			}
		}
	
		[Test]
		public void Clock ()
		{
			var scheduler = new TestScheduler ();
			Assert.AreEqual (0, scheduler.Clock, "#1"); // default
			scheduler.AdvanceBy (TimeSpan.FromDays (1).Ticks);
			Assert.AreEqual (TimeSpan.FromDays (1).Ticks, scheduler.Clock, "#2");
			var dt = new DateTimeOffset (2012, 1, 1, 0, 0, 0, TimeSpan.Zero);
			scheduler.AdvanceTo (dt.Ticks);
			Assert.AreEqual (dt, new DateTimeOffset (scheduler.Clock, TimeSpan.Zero), "#3");
		}
		
		[Test]
		public void AdvanceByRaisesEvent ()
		{
			var scheduler = new MyTestScheduler ();
			Assert.AreEqual (TimeSpan.FromDays (1).Ticks, scheduler.PublicAdd (TimeSpan.FromDays (1).Ticks), "#0");
			Assert.AreEqual (TimeSpan.FromDays (1).Ticks, scheduler.PublicToRelative (TimeSpan.FromDays (1)), "#0-2");
			var source = Observable.Interval (TimeSpan.FromDays (1), scheduler);
			int x = 0;
			var dis = source.Subscribe (v => x++);
			Assert.IsNotNull (scheduler.PublicGetNext (), "#1");
			Assert.AreEqual (0, x, "#2");
			scheduler.AdvanceBy (TimeSpan.FromHours (1).Ticks);
			Assert.AreEqual (0, x, "#3");
			scheduler.AdvanceBy (TimeSpan.FromDays (1).Ticks);
			var item = scheduler.PublicGetNext ();
			Assert.IsNotNull (item, "#5");
			Assert.AreEqual (1, x, "#5");
			dis.Dispose ();
		}
	}
}
