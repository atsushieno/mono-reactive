using System;
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
	public class NewThreadSchedulerTest
	{
		[Test]
		public void Cancellation ()
		{
			bool raised = false;
			var dis = new NewThreadScheduler ().Schedule<object> (null, TimeSpan.FromMilliseconds (300), (sch, stat) => raised = true);
			Assert.IsFalse (raised, "#1");
			dis.Dispose (); // immediately, to not raise event.
			Thread.Sleep (400);
			Assert.IsFalse (raised, "#2");
		}
	}
}