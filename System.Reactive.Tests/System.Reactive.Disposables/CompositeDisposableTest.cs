using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NUnit.Framework;

namespace System.Reactive.Disposables.Tests
{
	[TestFixture]
	public class CompositeDisposableTest
	{
		[Test]
		[ExpectedException (typeof (ArgumentNullException))] // MS Rx fails to detect this...
		public void ConstructorNullArgs ()
		{
			new CompositeDisposable ((IDisposable) null);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void ConstructorNullArgs2 ()
		{
			new CompositeDisposable ((IDisposable []) null);
		}
		
		[Test]
		public void ConstructorWithList ()
		{
			var l = new List<IDisposable> ();
			var d = new CompositeDisposable (l);
			l.Add (Disposable.Empty);
			Assert.AreEqual (0, d.Count, "#1");
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void AddNull ()
		{
			new CompositeDisposable ().Add (null);
		}

		[Test]
		public void AddTwice ()
		{
			int i = 0;
			var d = new CompositeDisposable ();
			var item = Disposable.Create (() => i++);
			d.Add (item);
			d.Add (item);
			// this results in two items registered.
			Assert.AreEqual (2, d.Count, "#1");
			d.Dispose ();
			// though, since the first disposal takes effect, it never invokes Dispose() on item twice.
			Assert.AreEqual (1, i, "#2");
		}
		
		[Test]
		public void CountAndDispose ()
		{
			var d = new CompositeDisposable (Disposable.Empty);
			Assert.AreEqual (1, d.Count, "#1");
			d.Dispose ();
			Assert.AreEqual (0, d.Count, "#2");
		}
		
		[Test]
		public void AddAfterDisposal ()
		{
			int i1 = 0, i2 = 0;
			var d = new CompositeDisposable ();
			d.Add (Disposable.Create (() => i1++));
			d.Dispose ();
			Assert.AreEqual (1, i1, "#1");
			d.Dispose ();
			Assert.AreEqual (1, i1, "#2"); // no further addition

			d.Add (Disposable.Create (() => i2++));
			Assert.AreEqual (1, i2, "#3");
			d.Dispose ();
			Assert.AreEqual (1, i2, "#4"); // no further addition

			d.Add (Disposable.Create (() => i2++)); // should be immediately disposed
			Assert.AreEqual (2, i2, "#5");
		}

		[Test]
		public void Clear ()
		{
			var d = new CompositeDisposable (Disposable.Empty);
			Assert.AreEqual (1, d.Count, "#1");
			d.Clear ();
			Assert.AreEqual (0, d.Count, "#2");
		}
		
		[Test]
		public void Remove ()
		{
			var d = new CompositeDisposable ();
			d.Remove (Disposable.Empty); // no effect, no error.
		}
		
		[Test]
		public void GetEnumerator ()
		{
			var d = new CompositeDisposable ();
			var e = d.GetEnumerator ();
			e.MoveNext ();
			d.Add (Disposable.Empty);
			e.MoveNext (); // looks like it does not result in InvalidOperationException.
		}
	}
}
