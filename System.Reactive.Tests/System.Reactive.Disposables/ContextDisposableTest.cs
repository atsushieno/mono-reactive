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
	public class ContextDisposableTest
	{
		[Test]
		public void DisposeInAsync ()
		{
			int i = 0;
			// This tests that ContextDisposable.Dispose() is not done in async mode.
			var d = new ContextDisposable (new SynchronizationContext (), Disposable.Create (() => { Thread.Sleep (200); i++; }));
			d.Dispose ();
			Assert.IsTrue (d.IsDisposed, "#1");
			Assert.AreEqual (0, i, "#2"); // not (very likely) yet
			Assert.IsTrue (SpinWait.SpinUntil (() => i != 0, 1000), "#3");
		}
	}
}
