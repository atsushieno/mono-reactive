using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using NUnit.Framework;

namespace System.Reactive.Linq.Tests
{
	[TestFixture]
	public class ObservableTest
	{
		// first, test some basic functionality used by every other tests.
		[Test]
		public void ToEnumerable ()
		{
			var e = Observable.Range (0, 3).ToEnumerable ();
			var ee = e.GetEnumerator ();
			Assert.IsTrue (ee.MoveNext (), "#1");
			Assert.AreEqual (0, ee.Current, "#2");
			Assert.IsTrue (ee.MoveNext (), "#3");
			Assert.AreEqual (1, ee.Current, "#4");
			Assert.IsTrue (ee.MoveNext (), "#5");
			Assert.AreEqual (2, ee.Current, "#6");
			Assert.IsFalse (ee.MoveNext (), "#7");
		}

		// tests for individual method follow...

		[Test]
		public void Aggregate ()
		{
			int i = 0, j = 0, k = 0;
			var source = Observable.Range (1, 4).Aggregate ((v1, v2) => v1 + v2);
			source.Subscribe (v => { i += v; k++; }, () => j++);
			Assert.IsTrue (SpinWait.SpinUntil (() => j != 0, 1000), "#1");
			Assert.AreEqual (10, i, "#2");
			Assert.AreEqual (1, k, "#3");
		}
		
		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void AggregateEmpty ()
		{
			Observable.Empty<int> ().Aggregate ((v1, v2) => v1 + v2).Subscribe (TextWriter.Null.WriteLine);
		}
		
		[Test]
		public void AggregateWithSeed ()
		{
			int i = 0, j = 0, k = 0;
			var source = Observable.Range (1, 4).Aggregate (5, (v1, v2) => v1 + v2);
			source.Subscribe (v => { i += v; k++; }, () => j++);
			Assert.IsTrue (SpinWait.SpinUntil (() => j != 0, 1000), "#1");
			Assert.AreEqual (15, i, "#2");
			Assert.AreEqual (1, k, "#3");
		}
		
		[Test]
		// Note that this overload does not result in error.
		public void AggregateEmptyWithSeed ()
		{
			int i = 0, j = 0, k = 0;
			var source = Observable.Empty<int> ().Aggregate (5, (v1, v2) => v1 + v2).Do (v => k++);
			source.Subscribe (v => i += v, () => j++);
			Assert.IsTrue (SpinWait.SpinUntil (() => j != 0, 1000), "#1");
			Assert.AreEqual (5, i, "#2");
			Assert.AreEqual (1, k, "#3");
		}
		
		[Test]
		public void All ()
		{
			Assert.IsFalse (Observable.Empty<int> ().All (v => true).ToEnumerable ().First (), "#1");
			Assert.IsTrue (Observable.Return<int> (1).All (v => true).ToEnumerable ().First (), "#2");
			Assert.IsFalse (Observable.Range (1, 3).All (v => v % 2 == 1).ToEnumerable ().First (), "#3");
		}
		
		[Test]
		public void Amb ()
		{
			var s1 = Observable.Range (1, 3).Delay (TimeSpan.FromMilliseconds (500));
			var s2 = Observable.Range (4, 3);
			var e = s1.Amb (s2).ToEnumerable ().ToArray ();
			Assert.AreEqual (new int [] {4, 5, 6}, e, "#1");
		}
		
		[Test]
		public void AndThenWhen ()
		{
			var s1 = Observable.Range (1, 3);
			var s2 = Observable.Range (4, 4); // extra element is ignored.
			var e = Observable.When<int> (s1.And (s2).Then ((v1, v2) => v1 + v2)).ToEnumerable ().ToArray ();
			Assert.AreEqual (new int [] {5, 7, 9}, e, "#1");
		}
		
		[Test]
		public void Any ()
		{
			Assert.IsFalse (Observable.Empty<int> ().Any (v => true).ToEnumerable ().First (), "#1");
			Assert.IsTrue (Observable.Return<int> (1).Any (v => true).ToEnumerable ().First (), "#2");
			Assert.IsTrue (Observable.Range (1, 3).Any (v => v % 2 == 0).ToEnumerable ().First (), "#3");
		}
		
		[Test]
		public void BufferTimeAndCount ()
		{
			// This emites events as: 0 <0ms> 1 <100ms> 2 ... 5 <500ms>
			var o1 = Observable.Generate<int, int> (0, i => i < 6, i => { Thread.Sleep (i * 50); return i + 1; }, i => i);
			var source = o1.Buffer (TimeSpan.FromMilliseconds (500), 3);
			bool done = false;
			DateTime start = DateTime.Now;
			int iter = 0;
			var dis = source.Subscribe (l => {
				if (iter == 0) {
					Assert.IsTrue (DateTime.Now - start <= TimeSpan.FromMilliseconds (500), "#1");
					Assert.AreEqual (3, l.Count, "#2");
				} else if (iter == 1) {
					Assert.IsTrue (l.Count < 3, "#3");
					Assert.IsTrue (DateTime.Now - start >= TimeSpan.FromMilliseconds (500), "#4");
				}
				else
					Assert.Fail ("Unexpected Generate() iteration");
				iter++;
				}, () => done = true);
			SpinWait.SpinUntil (() => done == true, 2000);
			dis.Dispose ();
		}
		
		[Test]
		public void Concat ()
		{
			int i = 0, j = 0;
			var source = Observable.Range (0, 5).Concat (Observable.Range (11, 3));
			source.Subscribe (v => i += v, () => j++);
			Assert.IsTrue (SpinWait.SpinUntil (() => j != 0, 1000), "#1");
			Assert.AreEqual (46, i, "#2");
		}
		
		[Test]
		public void Do ()
		{
			int i = 0, j = 0, k = 0;
			var source = Observable.Range (0, 5).Do (v => k += v);
			source.Subscribe (v => i += v, () => j++);
			Assert.IsTrue (SpinWait.SpinUntil (() => j != 0, 1000), "#1");
			Assert.AreEqual (10, i, "#2");
			Assert.AreEqual (10, k, "#3");
		}
	}
}
