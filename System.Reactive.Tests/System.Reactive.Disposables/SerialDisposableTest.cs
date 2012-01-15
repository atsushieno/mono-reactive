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
	public class SerialDisposableTest
	{
		[Test]
		public void AssignMultipleTimes ()
		{
			int i = 0, j = 0, k = 0;
			var d = new SerialDisposable ();
			d.Disposable = Disposable.Create (() => i++);
			d.Disposable = Disposable.Create (() => j++);
			Assert.AreEqual (1, i, "#1");
			d.Dispose ();
			d.Disposable = Disposable.Create (() => k++); // immediately disposed
			Assert.AreEqual (1, j, "#2");
			Assert.AreEqual (1, k, "#3");
			d.Dispose (); // invoke once more
			Assert.AreEqual (1, i, "#4");
			Assert.AreEqual (1, j, "#5");
			Assert.AreEqual (1, k, "#6");
		}

		[Test]
		public void AssignNull ()
		{
			int i = 0;
			var d = new SerialDisposable ();
			d.Disposable = Disposable.Create (() => i++);
			d.Disposable = null; // this disposes previous disposable.
			d.Dispose ();
			Assert.IsTrue (d.IsDisposed, "#1");
			Assert.AreEqual (1, i, "#2");
		}
	}
}
