using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using NUnit.Framework;

namespace System.Reactive.Concurrency.Tests
{
	[TestFixture]
	public class SchedulerTest
	{
		[Test]
		public void SimpleAction ()
		{
			var sch = Scheduler.CurrentThread;
			int i = 0;
			var dis = sch.Schedule (() => i += 5);
			Thread.Sleep (100);
			Assert.AreEqual (5, i, "#1"); // at least, do not repeat.
			dis.Dispose ();
		}
		
		[Test]
		public void SimpleActionDateTimeOffset ()
		{
			var sch = Scheduler.CurrentThread;
			int i = 0;
			var dis = sch.Schedule (DateTimeOffset.Now, () => i += 5);
			Thread.Sleep (100);
			Assert.AreEqual (5, i, "#1"); // at least, do not repeat.
			dis.Dispose ();
		}
		
		[Test]
		public void SimpleActionTimeSpan ()
		{
			var sch = Scheduler.CurrentThread;
			int i = 0;
			var dis = sch.Schedule (TimeSpan.Zero, () => i += 5);
			Thread.Sleep (100);
			Thread.Sleep (100);
			Assert.AreEqual (5, i, "#1"); // at least, do not repeat.
			dis.Dispose ();
		}
		
		[Test]
		public void RecursiveAction ()
		{
			var sch = Scheduler.CurrentThread;
			int i = 0;
			bool done = true;
			var dis = sch.Schedule ((Action a) => { i++; if (i < 10) a (); else done = true; });
			SpinWait.SpinUntil (() => done, 1000);
			Assert.AreEqual (10, i, "#1");
			Assert.IsTrue (done, "#2");
			dis.Dispose ();
		}
		
		[Test]
		public void RecursiveActionTimeSpan ()
		{
			var sch = Scheduler.CurrentThread;
			int i = 0;
			bool done = true;
			var dueTime = TimeSpan.FromMilliseconds (20);
			var dis = sch.Schedule (dueTime, (Action<TimeSpan> a) => { i++; if (i < 10) a (dueTime); else done = true; });
			SpinWait.SpinUntil (() => done, 1000);
			Assert.AreEqual (10, i, "#1");
			dis.Dispose ();
		}

		[Test]
		public void RecursiveActionTimeSpan2 ()
		{
			int i = 0;
			var scheduler = new HistoricalScheduler ();
			var span = TimeSpan.FromMilliseconds (50);
			scheduler.Schedule<object> (null, span, (object obj,Action<object,TimeSpan> a) => { i++; a (obj, span); });
			scheduler.AdvanceBy (TimeSpan.FromSeconds (1));
			Assert.AreEqual (20, i, "#1");
		}
		
		[Test]
		public void RecursiveActionDateTimeOffset ()
		{
			var sch = Scheduler.CurrentThread;
			int i = 0;
			bool done = true;
			DateTimeOffset dueTime = DateTimeOffset.Now.AddMilliseconds (20);
			var dis = sch.Schedule (dueTime, (Action<DateTimeOffset> a) => { i++; if (i < 10) a (dueTime); else done = true; });
			SpinWait.SpinUntil (() => done, 1000);
			Assert.AreEqual (10, i, "#1");
			dis.Dispose ();
		}

#if REACTIVE_2_0
		[Test]
		public void AsLongRunning ()
		{
			Assert.IsNull (Scheduler.AsLongRunning (Scheduler.CurrentThread), "#1");
			Assert.IsNotNull (Scheduler.AsLongRunning (Scheduler.Default), "#2");
			Assert.IsNull (Scheduler.AsLongRunning (Scheduler.Immediate), "#3");
			Assert.IsNotNull (Scheduler.AsLongRunning (Scheduler.NewThread), "#4");
			Assert.IsNotNull (Scheduler.AsLongRunning (Scheduler.TaskPool), "#5");
			Assert.IsNotNull (Scheduler.AsLongRunning (Scheduler.ThreadPool), "#6");
		}

		[Test]
		public void AsPeriodic ()
		{
			Assert.IsNull (Scheduler.AsPeriodic (Scheduler.CurrentThread), "#1");
			Assert.IsNotNull (Scheduler.AsPeriodic (Scheduler.Default), "#2");
			Assert.IsNull (Scheduler.AsPeriodic (Scheduler.Immediate), "#3");
			Assert.IsNotNull (Scheduler.AsPeriodic (Scheduler.NewThread), "#4");
			Assert.IsNotNull (Scheduler.AsPeriodic (Scheduler.TaskPool), "#5");
			Assert.IsNotNull (Scheduler.AsPeriodic (Scheduler.ThreadPool), "#6");
		}
#endif
	}
}
