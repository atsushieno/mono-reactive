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
		public void Cancellation ()
		{
			bool raised = false;
			var dis = Scheduler.ThreadPool.Schedule<object> (null, TimeSpan.FromMilliseconds (100), (sch, stat) => raised = true);
			Assert.IsFalse (raised, "#1");
			dis.Dispose (); // immediately, to not raise event.
			Thread.Sleep (200);
			Assert.IsFalse (raised, "#2");
		}

		[Test]
		[Ignore ("This breaks NUnit execution")]
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
				dis.Add (s.Schedule (() => { Thread.Sleep (1200); l.Add (1); }));
				dis.Add (s.Schedule (() => { Thread.Sleep (800); l.Add (2); }));
				dis.Add (s.Schedule (() => { Thread.Sleep (50); l.Add (3); }));
				Thread.Sleep (1500);
				Assert.AreEqual (new int [] {3, 2, 1}, l.ToArray (), "#1");
			} finally {
				dis.Dispose ();
			}
		}
	}
}