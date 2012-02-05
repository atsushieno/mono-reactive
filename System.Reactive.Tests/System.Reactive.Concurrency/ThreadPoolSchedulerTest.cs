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
	public class ThreadPoolSchedulerTest
	{
		[Test]
		public void ScheduleErrorneousAction ()
		{
			var s = Scheduler.ThreadPool;
			bool done = false;
			s.Schedule (() => { try { throw new Exception (); } finally { done = true; } });
			SpinWait.SpinUntil (() => done, 1000);
			Assert.IsTrue (done, "#1");
			// the exception does not occur in *this* thread, so it passes here.
		}
		
		[Test]
		public void Order ()
		{
			// It is time-dependent test (i.e. lengthy and inconsistent), which is not very good but we cannot use HistoricalScheduler to test it...
			var s = Scheduler.ThreadPool;
			var l = new List<int> ();
			var dis = new CompositeDisposable ();
			try {
				dis.Add (s.Schedule (() => { Thread.Sleep (1500); l.Add (1); }));
				dis.Add (s.Schedule (() => { Thread.Sleep (1000); l.Add (2); }));
				dis.Add (s.Schedule (() => { Thread.Sleep (50); l.Add (3); }));
				SpinWait.SpinUntil (() => l.Count == 3, 2000);
				Assert.AreEqual (new int [] {3, 2, 1}, l.ToArray (), "#1");
			} finally {
				dis.Dispose ();
			}
		}
	}
}