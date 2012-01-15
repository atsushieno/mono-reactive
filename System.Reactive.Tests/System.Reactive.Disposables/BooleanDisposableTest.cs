using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NUnit.Framework;

namespace System.Reactive.Disposables.Tests
{
	[TestFixture]
	public class BooleanDisposableTest
	{
		[Test]
		public void DisposeSimple ()
		{
			var b = new BooleanDisposable ();
			Assert.IsFalse (b.IsDisposed, "#1");
			b.Dispose ();
			Assert.IsTrue (b.IsDisposed, "#2");
			b.Dispose (); // call multiple time
			Assert.IsTrue (b.IsDisposed, "#2");
		}
	}
}
