using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using NUnit.Framework;

namespace System.Reactive.Disposables.Tests
{
	[TestFixture]
	public class ScheduledDisposableTest
	{
		[Test]
		public void DisposeOnScheduler ()
		{
			int i = 0;
			var d = new ScheduledDisposable (Scheduler.ThreadPool, Disposable.Create (() => { Thread.Sleep (200); i++; }));
			d.Dispose ();
			Assert.AreEqual (0, i, "#1");
			Assert.IsTrue (d.IsDisposed, "#2");
			Assert.IsTrue (SpinWait.SpinUntil (() => i != 0, 1000), "#3");

			d = new ScheduledDisposable (Scheduler.Immediate, Disposable.Create (() => { Thread.Sleep (200); i++; }));
			d.Dispose ();
			Assert.AreEqual (2, i, "#4"); // disposes immediately, so it must be already 2.
		}
	}
}
