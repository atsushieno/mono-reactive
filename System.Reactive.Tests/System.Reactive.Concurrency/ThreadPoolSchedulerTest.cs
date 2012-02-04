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
			// FIXME: not very good, time-dependent test (i.e. lengthy and inconsistent).
			var s = Scheduler.ThreadPool;
			var l = new List<int> ();
			s.Schedule (() => { Thread.Sleep (1000); l.Add (1); });
			s.Schedule (() => { Thread.Sleep (500); l.Add (2); });
			s.Schedule (() => { Thread.Sleep (100); l.Add (3); });
			SpinWait.SpinUntil (() => l.Count == 3, 1000);
			Assert.AreEqual (new int [] {3, 2, 1}, l.ToArray (), "#1");
		}
	}
}