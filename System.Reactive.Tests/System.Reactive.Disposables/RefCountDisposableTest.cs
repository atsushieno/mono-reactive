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
	public class RefCountDisposableTest
	{
		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void ConstructorNullArg ()
		{
			new RefCountDisposable (null);
		}
		
		[Test]
		public void GetDisposableDisposeFirst ()
		{
			int i = 0;
			var src = Disposable.Create (() => i++);
			var d = new RefCountDisposable (src);
			var des = d.GetDisposable ();
			Assert.AreNotSame (src, des, "#1");
			d.Dispose (); // triggers final disposable, up to refcount.
			Assert.AreEqual (0, i, "#2");
			des.Dispose ();
			Assert.AreEqual (1, i, "#3");
			Assert.IsTrue (d.IsDisposed);
			
			des = d.GetDisposable ();
			des.Dispose ();
			Assert.AreEqual (1, i, "#4");
		}
		
		[Test]
		public void GetDisposableDisposeLater ()
		{
			int i = 0;
			var src = Disposable.Create (() => i++);
			var d = new RefCountDisposable (src);
			var des = d.GetDisposable ();
			Assert.AreNotSame (src, des, "#1");
			Assert.AreEqual (0, i, "#2");
			des.Dispose ();
			Assert.AreEqual (0, i, "#3");
			Assert.IsFalse (d.IsDisposed);
			
			des = d.GetDisposable ();
			des.Dispose ();
			Assert.AreEqual (0, i, "#4");
			d.Dispose (); // finally, it calls src dispose
			Assert.AreEqual (1, i, "#5");
		}
	}
}

