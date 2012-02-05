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
	public class EventSchedulerTest
	{
		[Test] // This test is (inevitably) processing speed dependent...
		public void Cancellation ()
		{
			bool raised = false;
			var s = new EventLoopScheduler ();
			var dis = s.Schedule<object> (null, TimeSpan.FromMilliseconds (300), (sch, stat) => {
				raised = true;
				return Disposable.Empty;
			});
			dis.Dispose (); // immediately dispose event => action should not run.
			Thread.Sleep (600);
			Assert.IsFalse (raised, "#1");
		}
	}
}
