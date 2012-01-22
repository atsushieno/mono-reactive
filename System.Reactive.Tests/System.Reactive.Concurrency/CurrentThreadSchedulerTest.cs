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
	public class CurrentThreadSchedulerTest
	{
		[Test]
		public void Concurrency ()
		{
			// this is to test some race condition in scheduling (which used to fail)
			for (int i = 0; i < 100; i++)
				RunConcurrencyTest ();
		}
		
		void RunConcurrencyTest ()
		{
			var stream = Observable.Range (0, 9).Do (TextWriter.Null.WriteLine); // ... is done on CurrentThreadScheduler.
			var source = stream.ObserveOn (Scheduler.ThreadPool);
			bool done = false;
			var dis = source.Subscribe (TextWriter.Null.WriteLine, () => done = true);
			SpinWait.SpinUntil (() => done == true, 1000);
			dis.Dispose ();
		}
		
		[Test]
		public void Concurrency2 ()
		{
			for (int i = 0; i < 100; i++)
				RunConcurrencyTest2 ();
		}
		
		void RunConcurrencyTest2 ()
		{
			var stream = Observable.Range (0, 9).Do (TextWriter.Null.WriteLine); // ... is done on CurrentThreadScheduler.
			var source = stream.ObserveOn (Scheduler.CurrentThread); // Unlike another one, this test runs totally on CurrentThread.
			bool done = false;
			var dis = source.Subscribe (TextWriter.Null.WriteLine, () => done = true);
			SpinWait.SpinUntil (() => done == true, 1000);
			dis.Dispose ();
		}
	}
}