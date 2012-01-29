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

namespace System.Reactive.Linq.Tests
{
	[TestFixture]
	public class ObservableConnectableTest
	{
		[Test]
		public void Publish ()
		{
			var source = Observable.Range (2, 3);

			int result = 1;
			bool started = false;
			var published = source.Publish ();
			var pdis1 = published.Subscribe (i => { started = true; result *= i; });
			Assert.IsFalse (started, "#1");
			var cdis = published.Connect ();
			Assert.IsTrue (started, "#2");
			Thread.Sleep (100); // should be enough to finish publishing source.
			Assert.AreEqual (24, result, "#3");
			var pdis2 = published.Subscribe (i => { started = true; result *= i; });
			Thread.Sleep (50); // should be enough to make some change (if it were to happen).
			Assert.AreEqual (24, result, "#3"); // but it should not happen.
			cdis.Dispose (); // disconnect
			pdis1.Dispose ();
			pdis2.Dispose ();
		}

		[Test]
		public void PublishConnectTwice ()
		{
			var source = Observable.Range (2, 3);

			int result = 1;
			bool started = false;
			var published = source.Publish ();
			var pdis1 = published.Subscribe (i => { started = true; result *= i; });
			Assert.IsFalse (started, "#1");
			var cdis1 = published.Connect ();
			var cdis2 = published.Connect (); // no error
			Assert.AreEqual (cdis1, cdis2, "#2");
			cdis1.Dispose ();
		}
	}
}
