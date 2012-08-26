using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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

		[Test]
		public void ToEnumerableWithInterval ()
		{
			var obs = Observable.Interval (TimeSpan.FromMilliseconds (100)).Take (5);
			obs.Subscribe (v => {}); // should not affect ToEnumerable() startup.
			int i = 0;
			Thread.Sleep (200);
			DateTime start = DateTime.Now;
			var e = obs.ToEnumerable ();
			foreach (var v in e)
				i ++;
			Assert.AreEqual (5, i, "#1");
			Assert.IsTrue (DateTime.Now - start > TimeSpan.FromMilliseconds (500), "#2"); // if it enumerates in incorrect time, it will result in false.
		}
		
		[Test]
		public void Materialize ()
		{
			var expected = new NotificationKind [] {
				NotificationKind.OnNext,
				NotificationKind.OnNext,
				NotificationKind.OnError };
			var source = Observable.Range (0, 2).Concat (Observable.Throw<int> (new Exception ("failure")));
			var l = new List<NotificationKind> ();
			bool done = false;
			var dis = source.Materialize ().Subscribe (v => l.Add (v.Kind), () => done = true); // test that Materialize() yields OnCompleted event after yielding OnError.
			Assert.IsTrue (SpinWait.SpinUntil (() => done, TimeSpan.FromSeconds (1)), "#1");
			Assert.AreEqual (expected, l.ToArray (), "#3");
			dis.Dispose ();
		}

		public class MyObservable<T> : IObservable<T>
		{
			public IDisposable Subscribe (IObserver<T> observer)
			{
				throw new MyException ();
			}
		}

		[Test]
		[ExpectedException (typeof (MyException))]
		public void ErrorFlow ()
		{
			// throw error on main
			new MyObservable<int> ().Timestamp ().Subscribe (v => {});
			Assert.Fail ("should not reach here");
		}
		
		[Test]
		public void ErrorSubscription ()
		{
			bool done = false;
			bool shouldNotPass = false;
			var o = Observable.Create<int> (observer => { try { throw new Exception (); return Disposable.Empty; } finally { done = true; } });
			var dis = new SingleAssignmentDisposable ();
			try {
				dis.Disposable = o.SubscribeOn (Scheduler.ThreadPool).Subscribe (v => {}, ex => shouldNotPass = true);
			} finally {
				dis.Dispose ();
			}
			SpinWait.SpinUntil (() => done, 1000);
			Assert.IsTrue (done, "#1");
			Assert.IsFalse (shouldNotPass, "#2");
			// the exception does not occur in *this* thread, so it passes here.
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
		[ExpectedException (typeof (MyException))]
		public void AllPredicateError ()
		{
			var source = Observable.Range (0, 20).All (i => { throw new MyException (); });
			source.Subscribe (v => {} , ex => Assert.Fail ("Should not reach OnError"), () => Assert.Fail ("Should not reach OnCompleted"));
		}
		
		[Test]
		public void Amb ()
		{
			var scheduler = new HistoricalScheduler ();
			var s1 = Observable.Range (1, 3).Delay (TimeSpan.FromMilliseconds (500), scheduler);
			var s2 = Observable.Range (4, 3);
			var e = s1.Amb (s2).ToEnumerable ().ToArray ();
			scheduler.AdvanceBy (TimeSpan.FromSeconds (1));
			Assert.AreEqual (new int [] {4, 5, 6}, e, "#1");
		}
		
		[Test]
		[Ignore ("NotWorking")]
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
		[ExpectedException (typeof (MyException))]
		public void AnyPredicateError ()
		{
			var source = Observable.Range (0, 20).Any (i => { throw new MyException (); });
			source.Subscribe (v => {} , ex => Assert.Fail ("Should not reach OnError"), () => Assert.Fail ("Should not reach OnCompleted"));
		}
		
		[Test]
		public void BufferCountAndSkip ()
		{
			var sw = new StringWriter () { NewLine = "\n" };
			var source = Observable.Range (0, 20).Buffer (3, 5);
			source.Subscribe (v => sw.WriteLine (String.Concat (v)), () => sw.Write ("done"));
			string expected = "012\n567\n101112\n151617\ndone".Replace ("\r", "");
			Assert.AreEqual (expected, sw.ToString (), "#1");
		}
		
		[Test]
		public void BufferCountAndSkip2 ()
		{
			// It has to work for overlapped range
			var sw = new StringWriter () { NewLine = "\n" };
			var source = Observable.Range (0, 10).Buffer (5, 3);
			source.Subscribe (v => sw.WriteLine (String.Concat (v)), () => sw.Write ("done"));
			var expected = "01234\n34567\n6789\n9\ndone".Replace ("\r", "");
			Assert.AreEqual (expected, sw.ToString (), "#1");
		}

		[Test]
		public void BufferTimeSpans ()
		{
			var scheduler = new HistoricalScheduler ();
			var interval = TimeSpan.FromMilliseconds (100);
			var span = TimeSpan.FromMilliseconds (300);
			var shift = TimeSpan.FromMilliseconds (500);
			// This makes notable corner case ... FromMilliseconds (3100) passes the test, while FromSeconds (3) does not. It is because the source is cut at that state.
			var total = TimeSpan.FromSeconds (3);
			
			var sw = new StringWriter () { NewLine = "\n" };
			var source = Observable.Interval (interval, scheduler).Take (30).Buffer (span, shift, scheduler);
			source.Subscribe (v => sw.WriteLine (String.Concat (v)), () => sw.Write ("done"));
			scheduler.AdvanceBy (total);
			var expected = "01\n456\n91011\n141516\n192021\n242526\n29\ndone".Replace ("\r", "");
			Assert.AreEqual (expected, sw.ToString (), "#1");
		}

		[Test]
		public void BufferTimeSpans2 ()
		{
			var scheduler = new HistoricalScheduler ();
			var interval = TimeSpan.FromMilliseconds (100);
			var span = TimeSpan.FromMilliseconds (500);
			var shift = TimeSpan.FromMilliseconds (300);
			var total = TimeSpan.FromSeconds (2);
			
			var sw = new StringWriter () { NewLine = "\n" };
			var source = Observable.Interval (interval, scheduler).Take (10).Buffer (span, shift, scheduler);
			source.Subscribe (v => sw.WriteLine (String.Concat (v)), () => sw.Write ("done"));
			scheduler.AdvanceBy (total);
			// this overlaps the list
			var expected = "0123\n23456\n56789\n89\ndone".Replace ("\r", "");
			Assert.AreEqual (expected, sw.ToString (), "#1");
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
			Assert.IsTrue (SpinWait.SpinUntil (() => done == true, 2000), "#5");
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

			source = Observable.Range (0, 4).Concat (Observable.Throw<int> (new MyException ()));
			try {
				source.ToEnumerable ().All (v => true);
				Assert.Fail ("should not complete");
			} catch (MyException) {
			}
		}
		
		[Test]
		public void Concat2 ()
		{
			var expected = new NotificationKind [] {
				NotificationKind.OnNext,
				NotificationKind.OnNext,
				NotificationKind.OnError };
			var source = Observable.Range (0, 2).Concat (Observable.Throw<int> (new Exception ("failure")));
			var arr = from n in source.Materialize ().ToEnumerable () select n.Kind;
			Assert.AreEqual (expected, arr.ToArray (), "#1");
		}
		
		[Test]
		public void Concat3 ()
		{
			var expected = new NotificationKind [] {
				NotificationKind.OnNext,
				NotificationKind.OnNext,
				NotificationKind.OnNext,
				NotificationKind.OnNext,
				NotificationKind.OnCompleted };
			var scheduler = new HistoricalScheduler ();
			var source = Observable.Range (1, 3).Concat (Observable.Return (2).Delay (TimeSpan.FromMilliseconds (50), scheduler));
			bool done = false;
			var l = new List<NotificationKind> ();
			source.Materialize ().Subscribe (v => l.Add (v.Kind), () => done = true);
			scheduler.AdvanceBy (TimeSpan.FromMilliseconds (50));
			Assert.AreEqual (expected, l.ToArray (), "#1");
			Assert.IsTrue (done, "#2");
		}

		IEnumerable<IObservable<long>> IntervalSelectTakeDoEnumerable (HistoricalScheduler scheduler, List<string> l, TextWriter sw)
		{
			DateTimeOffset start = scheduler.Now;
			for (int i = 0; i < 5; i++) {
				int x = i;
				scheduler.AdvanceBy (TimeSpan.FromMilliseconds (150));
				yield return Observable.Interval (TimeSpan.FromMilliseconds (100), scheduler)
#if false
					.Select (v => x * 10 + v).Take (5)
#else
					// FIXME: this select should work, but it does not give expected "i" or "x" values, likely due to some mcs bug regarding local variable volatility...
					// ... So I used out-of-scope value comparison here.
					.Select (v => (long) ((scheduler.Now - start).TotalMilliseconds / 20) + v).Take (5)
#endif
					.Do (v => l.Add (String.Format ("{0:ss.fff}: {1} {2}", scheduler.Now, i, v)), () => sw.WriteLine ());
				scheduler.AdvanceBy (TimeSpan.FromMilliseconds (50));
			}
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void FirstOrDefault ()
		{
			var source = Observable.Throw<int> (new MyException ());
			var ret = source.FirstOrDefault ();
		}

		[Test] // some practical test
		public void IntervalSelectTakeDo ()
		{
			var scheduler = new HistoricalScheduler ();
			var l = new List<string> ();
			var sw = new StringWriter () { NewLine = "\n" };
			foreach (var o in IntervalSelectTakeDoEnumerable (scheduler, l, TextWriter.Null))
				o.Subscribe (v => {}); // dummy
			scheduler.AdvanceBy (TimeSpan.FromSeconds (3));
			l.Sort ();
			foreach (var s in l)
				sw.WriteLine (s);

			string expected = @"00.250: 1 12
				00.350: 1 18
				00.450: 2 22
				00.450: 2 24
				00.550: 2 28
				00.550: 2 30
				00.650: 3 32
				00.650: 3 34
				00.650: 3 36
				00.750: 3 38
				00.750: 3 40
				00.850: 4 42
				00.850: 4 44
				00.850: 4 46
				00.950: 4 48
				00.950: 4 50
				01.050: 5 52
				01.050: 5 54
				01.050: 5 56
				01.150: 5 58
				01.150: 5 60
				01.250: 5 64
				01.250: 5 66
				01.350: 5 70
				01.450: 5 76
				".Replace ("\r", "").Replace ("\t", "");
			Assert.AreEqual (expected, sw.ToString (), "#1");
		}
		
		[Test]
		public void Concat4 ()
		{
			var scheduler = new HistoricalScheduler ();

			var sw = new StringWriter () { NewLine = "\n" };
			var source = IntervalSelectTakeDoEnumerable (scheduler, new List<string> (), sw).ToObservable ().Concat ();
			source.Subscribe (v => sw.Write ("{0} ", v), () => sw.WriteLine ());
			scheduler.AdvanceBy (TimeSpan.FromMilliseconds (3000));
			var expected = @"12 18 24 30 36 
				37 43 49 55 61 
				62 68 74 80 86 
				87 93 99 105 111 
				112 118 124 130 136 
				
				".Replace ("\r", "").Replace ("\t", "");
			Assert.AreEqual (expected, sw.ToString (), "#1");
		}

		[Test]
		public void Delay ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = Observable.Return (2).Delay (TimeSpan.FromMilliseconds (50), scheduler).Materialize ();
			var l = new List<NotificationKind> ();
			bool done = false;
			source.Subscribe (v => l.Add (v.Kind), () => done = true);
			scheduler.AdvanceBy (TimeSpan.FromMilliseconds (50));
			Assert.IsTrue (done, "#2");
			Assert.AreEqual (new NotificationKind [] {
				NotificationKind.OnNext,
				NotificationKind.OnCompleted }, l.ToArray (), "#3");
		}
		
		[Test]
		public void Delay2 ()
		{
			// github issue #12
			var scheduler = new HistoricalScheduler ();
			var source = Observable.Interval (TimeSpan.FromSeconds (1), scheduler).Take (5).Timestamp ();
			var delayed = source.Delay (TimeSpan.FromSeconds (4), scheduler).Timestamp ();
			bool done = false;
			delayed.Subscribe (item => Assert.IsTrue (item.Value.Timestamp < item.Timestamp, "not delayed"), () => done = true);
			scheduler.AdvanceBy (TimeSpan.FromSeconds (3));
			Assert.IsFalse (done, "#1");
			scheduler.AdvanceBy (TimeSpan.FromSeconds (10));
			Assert.IsTrue (done, "#2");
		}
		
		[Test]
		public void Distinct ()
		{
			var source = new int [] {0, 1, 1, 3, 3, 1}.ToObservable ().Distinct ();
			string s = null;
			source.Subscribe (i => s += i, ex => s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("013done", s, "#1");
		}
		
		[Test]
		public void DistinctError ()
		{
			var source = new int [] {0, 1, 1, 3, 3, 1}.ToObservable ().Concat (Observable.Throw<int> (new SystemException ())).Distinct ();
			string s = null;
			source.Subscribe (i => s += i, ex => s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("013error:System.SystemException", s, "#1");
		}
		
		[Test]
		public void DistinctUntilChanged ()
		{
			var l = new List<int> ();
			var source = new int [] {3, 3, 5, 5, 4, 3}.ToObservable ().DistinctUntilChanged<int,int> (i => i);
			bool done = false;
			source.Subscribe (v => l.Add (v), () => done = true);
			SpinWait.SpinUntil (() => done, 1000);
			Assert.IsTrue (done, "#1");
			Assert.AreEqual (new int [] {3, 5, 4, 3}, l.ToArray (), "#2");
		}
		
		[Test]
		public void DistinctUntilChangedErrorSequence ()
		{
			string s = null;
			var source = new int [] {3, 3, 5, 5, 4, 3}.ToObservable ().Concat (Observable.Throw<int> (new SystemException ())).DistinctUntilChanged<int,int> (i => i);
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => Assert.Fail ("Should not reach OnCompleted"));
			Assert.AreEqual ("3543error:System.SystemException", s, "#1");
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void DistinctUntilChangedErrorSelector ()
		{
			var source = new int [] {3, 3, 5, 5, 4, 3}.ToObservable ().DistinctUntilChanged<int,int> (i => { throw new MyException (); });
			source.Subscribe (v => {} , ex => Assert.Fail ("Should not reach OnError"), () => Assert.Fail ("Should not reach OnCompleted"));
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
		
		[Test]
		public void Generate ()
		{
			var source = Observable.Generate (-1, x => x < 5, x => x + 1, x => x);
			int i = 0;
			var dis = new CompositeDisposable ();
			int done = 0;
			// test multiple subscription
			foreach (var iter in Enumerable.Range (0, 5))
				dis.Add (source.Subscribe (v => i += v, () => done++));
			Assert.IsTrue (SpinWait.SpinUntil (() => done == 5, 1000), "#1");
			dis.Dispose ();
			Assert.AreEqual (45, i, "#2");
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void GenerateErrorSelector ()
		{
			var source = Observable.Generate<int,int> (-1, x => x < 5, x => x + 1, x => { throw new MyException (); });
			source.Subscribe (v => {} , ex => Assert.Fail ("Should not reach OnError"), () => Assert.Fail ("Should not reach OnCompleted"));
		}
		
		[Test]
		public void GroupBy ()
		{
			var dic = new Dictionary<int, List<int>> ();
			var source = Observable.Range (0, 20).GroupBy (i => i / 5);
			bool done = false;
			var dis = new CompositeDisposable ();
			dis.Add (source.Subscribe (g => dis.Add (g.Subscribe (v => {
				List<int> l;
				if (!dic.TryGetValue (g.Key, out l)) {
					l = new List<int> ();
					dic [g.Key] = l;
				}
				l.Add (v);
				})), () => done = true));
			SpinWait.SpinUntil (() => done, 1000);
			Assert.IsTrue (done, "#1");
			Assert.AreEqual (new int [] {0, 1, 2, 3, 4}, dic [0].ToArray (), "#2");
			Assert.AreEqual (new int [] {15, 16, 17, 18, 19}, dic [3].ToArray (), "#3");
			Assert.AreEqual (4, dic.Count, "#4");
		}
		
		[Test]
		public void GroupBySequenceError ()
		{
			var source = new int[] { 0, 1, 1, 3, 3, 1 }.ToObservable ().Concat (Observable.Throw<int> (new SystemException ())).GroupBy (k => k);
			string s = null;
			source.Subscribe (i => i.ToEnumerable ().Select (j => s += j), ex=> s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void GroupBySelectorError ()
		{
			var source = Observable.Range (0, 20).GroupBy<int,int> (i => { throw new MyException (); });
			source.Subscribe (v => {} , ex => Assert.Fail ("Should not reach OnError"), () => Assert.Fail ("Should not reach OnCompleted"));
		}
		
		[Test]
		public void GroupByUntilSequenceError ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = new int[] { 0, 1, 1, 3, 3, 1 }.ToObservable ().Concat (Observable.Throw<int> (new SystemException ())).GroupByUntil (k => k, x => Observable.Interval (TimeSpan.FromSeconds (1), scheduler).Take (1));
			string s = null;
			source.Subscribe (i => i.ToEnumerable ().Select (j => s += j), ex=> s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}
		
		[Test]
		public void GroupByUntilDurationSequenceError ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = new int[] { 0, 1, 1, 3, 3, 1 }.ToObservable ().GroupByUntil (k => k, x => Observable.Throw<int> (new SystemException ()));
			string s = null;
			source.Subscribe (i => i.ToEnumerable ().Select (j => s += j), ex=> s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}

		[Test]
		public void GroupJoin ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = Observable.GroupJoin (
				Observable.Interval (TimeSpan.FromMilliseconds (500), scheduler).Take (10),
				Observable.Interval (TimeSpan.FromMilliseconds (800), scheduler).Delay (TimeSpan.FromSeconds (1), scheduler),
				l => Observable.Interval (TimeSpan.FromMilliseconds (1500), scheduler),
				r => Observable.Interval (TimeSpan.FromMilliseconds (1600), scheduler),
				(l, rob) => new { Left = l, Rights = rob }
			);
			bool done = false;
			bool [,] results = new bool [10, 10];
			bool [] groupDone = new bool [10];
			source.Subscribe (
				v => v.Rights.Subscribe (
					r => results [v.Left, r] = true,
					() => groupDone [v.Left] = true),
				() => done = true);
			scheduler.AdvanceBy (TimeSpan.FromSeconds (15));

			Assert.IsTrue (done, "#1");
			Assert.AreEqual (-1, Array.IndexOf (groupDone, false), "#2");
			int [] rstarts = new int [] {0, 0, 0, 0, 0, 0, 1, 1, 2, 3};
			int [] rends = new int [] {0, 0, 1, 2, 2, 3, 3, 4, 5, 5};
			for (int l = 0; l < 10; l++)
				for (int r = 0; r < 10; r++)
					Assert.AreEqual (rstarts [l] <= r && r <= rends [l], results [l, r], String.Format ("({0}, {1})", l, r));
		}
		
		[Test]
		public void GroupJoin2 ()
		{
			// almost identical to the previous one, but without delay. And I only test some corner case that could result in difference.
			var scheduler = new HistoricalScheduler ();
			var source = Observable.GroupJoin (
				Observable.Interval (TimeSpan.FromMilliseconds (500), scheduler).Take (10),
				Observable.Interval (TimeSpan.FromMilliseconds (800), scheduler),
				l => Observable.Interval (TimeSpan.FromMilliseconds (1500), scheduler),
				r => Observable.Interval (TimeSpan.FromMilliseconds (1600), scheduler),
				(l, rob) => new { Left = l, Rights = rob }
			);
			
			bool done = false;
			bool [,] results = new bool [10, 10];
			bool [] groupDone = new bool [10];
			source.Subscribe (
				v => v.Rights.Subscribe (
					r => results [v.Left, r] = true,
					() => groupDone [v.Left] = true),
				() => done = true);
			scheduler.AdvanceBy (TimeSpan.FromSeconds (15));

			Assert.IsTrue (done, "#1");
			// this value could be published *IF* unsubscription is
			// handled *after* 7 is published as a left value.
			// Rx does not publish this, likely means a right value
			// at the end moment of rightDuration is *not* published
			// ... so we do that too.
			Assert.IsFalse (results [7, 2], "#2");
		}

		[Test]
		public void GroupJoinLeftSequenceError ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = Observable.GroupJoin (
				Observable.Throw<int> (new SystemException ()),
				Observable.Interval (TimeSpan.FromMilliseconds (800), scheduler).Delay (TimeSpan.FromSeconds (1), scheduler),
				l => Observable.Interval (TimeSpan.FromMilliseconds (1500), scheduler),
				r => Observable.Interval (TimeSpan.FromMilliseconds (1600), scheduler),
				(l, rob) => new { Left = l, Rights = rob }
			);
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			scheduler.AdvanceBy (TimeSpan.FromSeconds (15));
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}

		[Test]
		public void GroupJoinRightSequenceError ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = Observable.GroupJoin (
				Observable.Interval (TimeSpan.FromMilliseconds (800), scheduler).Delay (TimeSpan.FromSeconds (1), scheduler),
				Observable.Throw<int> (new SystemException ()),
				l => Observable.Interval (TimeSpan.FromMilliseconds (1500), scheduler),
				r => Observable.Interval (TimeSpan.FromMilliseconds (1600), scheduler),
				(l, rob) => new { Left = l, Rights = rob }
			);
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			scheduler.AdvanceBy (TimeSpan.FromSeconds (15));
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}

		[Test]
		public void GroupJoinLeftDurationError ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = Observable.GroupJoin (
				Observable.Interval (TimeSpan.FromMilliseconds (500), scheduler).Delay (TimeSpan.FromSeconds (1), scheduler),
				Observable.Interval (TimeSpan.FromMilliseconds (800), scheduler).Delay (TimeSpan.FromSeconds (1), scheduler),
				l => Observable.Throw<int> (new SystemException ()),
				r => Observable.Interval (TimeSpan.FromMilliseconds (1600), scheduler),
				(l, rob) => new { Left = l, Rights = rob }
			);
			string s = null;
			source.Subscribe (v => {}, ex => s += "error:" + ex.GetType (), () => s += "done");
			scheduler.AdvanceBy (TimeSpan.FromSeconds (15));
			// LAMESPEC: shouldn't this also raise OnError() ? GroupByUntil() does so.
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}

		[Test]
		public void GroupJoinRightDurationError ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = Observable.GroupJoin (
				Observable.Interval (TimeSpan.FromMilliseconds (500), scheduler).Delay (TimeSpan.FromSeconds (1), scheduler),
				Observable.Interval (TimeSpan.FromMilliseconds (800), scheduler).Delay (TimeSpan.FromSeconds (1), scheduler),
				l => Observable.Interval (TimeSpan.FromMilliseconds (1600), scheduler),
				r => Observable.Throw<int> (new SystemException ()),
				(l, rob) => new { Left = l, Rights = rob }
			);
			string s = null;
			source.Subscribe (v => {}, ex => s += "error:" + ex.GetType (), () => s += "done");
			scheduler.AdvanceBy (TimeSpan.FromSeconds (15));
			// LAMESPEC: shouldn't this also raise OnError() ? GroupByUntil() does so.
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}
		
		[Test] // this test is processing-speed dependent, but (unlike other tests) I think testing this with default (ThreadPool) scheduler should make sense...
		public void Interval ()
		{
			var interval = Observable.Interval (TimeSpan.FromMilliseconds (100)).Take (6);
			long v1 = 0, v2 = 0;
			int done = 0;
			long diff = 0;
			var sub1 = interval.Subscribe (v => v1++, () => { done++; diff = v1 - v2; });
			Thread.Sleep (400);
			var sub2 = interval.Subscribe (v => v2++, () => done++);
			Assert.IsTrue (v1 != v2, "#1"); // at arbitrary time
			SpinWait.SpinUntil (() => done == 2, 1000);
			Assert.AreEqual (2, done, "#2");
			// test that two sequences runs in different time, same speed.
			Assert.IsTrue (diff > 2, "#3");
			sub1.Dispose ();
			sub2.Dispose ();
		}
		
		[Test]
		public void LongCountErrorSequence ()
		{
			var source = Observable.Range (0, 3).Concat (Observable.Throw<int> (new SystemException ())).LongCount ();
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}
		
		[Test]
		public void MaxByErrorSequence ()
		{
			var source = Observable.Range (0, 3).Concat (Observable.Throw<int> (new SystemException ())).MaxBy (v => v);
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void MaxByErrorSelector ()
		{
			var source = Observable.Range (0, 3).MaxBy<int,int> (v => { throw new MyException (); });
			string s = null;
			source.Subscribe (v => s += v, ex => Assert.Fail ("should not reach OnError"), () => Assert.Fail ("should not reach OnCompleted"));
		}
		
		[Test]
		public void MinByErrorSequence ()
		{
			var source = Observable.Range (0, 3).Concat (Observable.Throw<int> (new SystemException ())).MinBy (v => v);
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void MinByErrorSelector ()
		{
			var source = Observable.Range (0, 3).MaxBy<int,int> (v => { throw new MyException (); });
			string s = null;
			source.Subscribe (v => s += v, ex => Assert.Fail ("should not reach OnError"), () => Assert.Fail ("should not reach OnCompleted"));
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void RangeErrorScheduler ()
		{
			var source = Observable.Range (0, 3, new ErrorScheduler ());
			source.Subscribe (v => {} , ex => Assert.Fail ("Should not reach OnError"), () => Assert.Fail ("Should not reach OnCompleted"));
		}
		
		[Test]
		public void Retry ()
		{
			var source = Observable.Range (0, 4).Concat (Observable.Throw<int> (new Exception ("failure"))).Retry (2);
			var i = 0;
			bool done = false, error = false;
			var dis = source.Subscribe (
				v => i += v,
				ex => { error = true; done = true; Assert.AreEqual ("failure", ex.Message, "#1"); },
				() => Assert.Fail ("should not complete"));
			
			Assert.IsTrue (SpinWait.SpinUntil (() => done, 500), "#2");
			
			dis.Dispose ();
			Assert.IsTrue (error, "#3");
			Assert.AreEqual (12, i, "#4");
		}
		
		[Test]
		public void RetryZero ()
		{
			var source = Observable.Range (0, 4).Concat (Observable.Throw<int> (new Exception ("failure"))).Retry (0);
			bool done = false;
			var dis = source.Subscribe (
				v => Assert.Fail ("should not increment", "#1"),
				ex => Assert.Fail ("should not fail", "#2"),
				() => done = true
				);
			
			Assert.IsTrue (SpinWait.SpinUntil (() => done, 500), "#3");
			Assert.IsTrue (done, "#4");
			dis.Dispose ();
		}
		
		[Test]
		public void Sample ()
		{
			var l = new List<long> ();
			var l2 = new List<long> ();
			var scheduler = new HistoricalScheduler ();
			var source = Observable.Interval (TimeSpan.FromMilliseconds (300), scheduler).Delay (TimeSpan.FromSeconds (2), scheduler);
			source.Subscribe (v => l.Add (v));
			var sampler = Observable.Interval (TimeSpan.FromMilliseconds (1000), scheduler).Take (10);
			var o = source.Sample (sampler);
			bool done = false;
			o.Subscribe (v => l2.Add (v), () => done = true);
			for (int i = 0; i < 50; i++)
				scheduler.AdvanceBy (TimeSpan.FromMilliseconds (300));
			//scheduler.AdvanceBy (TimeSpan.FromMilliseconds (15000));
			Assert.AreEqual (43, l.Count, "#1");
			Assert.AreEqual (new long [] {2, 5, 8, 12, 15, 18, 22, 25}, l2.ToArray (), "#2");
			Assert.IsFalse (done, "#3"); // while sampler finishes, sample observable never does.
		}
		
		[Test]
		public void SelectErrorSequence ()
		{
			var source = Observable.Range (0, 3).Concat (Observable.Throw<int> (new SystemException ())).Select (v => v);
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("012error:System.SystemException", s, "#1");
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void SelectErrorSelector ()
		{
			var source = Observable.Range (0, 3).Select<int,int> (v => { throw new MyException (); });
			string s = null;
			source.Subscribe (v => s += v, ex => Assert.Fail ("should not reach OnError"), () => Assert.Fail ("should not reach OnCompleted"));
		}
		
		[Test]
		public void SelectManyObservable ()
		{
			var scheduler = new HistoricalScheduler ();
			Func<int,IObservable<int>> f = x => Observable.Return (x).Delay (TimeSpan.FromSeconds (1), scheduler);
			var source = Observable.Range (0, 5).SelectMany (n => f (n));
			var l = new List<int> ();
			bool done = false;
			var dis = source.Subscribe (v => l.Add (v), () => done = true);
			Assert.IsFalse (done ,"#1");
			scheduler.AdvanceBy (TimeSpan.FromSeconds (1));
			Assert.AreEqual (new int [] {0, 1, 2, 3, 4}, l.ToArray (), "#2");
			Assert.IsTrue (done, "#3");
		}

#if REACTIVE_2_0		
		[Test]
		public void SkipDuration ()
		{
			var l = new List<long> ();
			var scheduler = new HistoricalScheduler ();
			Observable.Interval (TimeSpan.FromMilliseconds (100), scheduler).Take (10).Skip (TimeSpan.FromMilliseconds (300), scheduler).Subscribe (v => l.Add (v));
			scheduler.AdvanceBy (TimeSpan.FromSeconds (1));
			var array = Enumerable.Range (2, 8).ToArray ();
			Assert.AreEqual (array, l.ToArray ());
		}
		
		[Test]
		public void SkipLastDuration ()
		{
			var l = new List<long> ();
			var scheduler = new HistoricalScheduler ();
			Observable.Interval (TimeSpan.FromMilliseconds (100), scheduler).Take (10).SkipLast (TimeSpan.FromMilliseconds (300), scheduler).Subscribe (v => l.Add (v));
			scheduler.AdvanceBy (TimeSpan.FromSeconds (1));
			var array = Enumerable.Range (0, 7).ToArray ();
			Assert.AreEqual (array, l.ToArray ());
		}

		[Test]
		public void SkipUntil ()
		{
			var l = new List<long> ();
			var scheduler = new HistoricalScheduler ();
			Observable.Interval (TimeSpan.FromMilliseconds (100), scheduler).Take (10).SkipUntil (scheduler.Now.AddMilliseconds (300), scheduler).Subscribe (v => l.Add (v));
			scheduler.AdvanceBy (TimeSpan.FromSeconds (1));
			var array = Enumerable.Range (2, 8).ToArray ();
			Assert.AreEqual (array, l.ToArray ());
		}
#endif
		
		[Test]
		public void SkipLastErrorSequence ()
		{
			var source = Observable.Range (0, 3).Concat (Observable.Throw<int> (new SystemException ())).SkipLast (3); // note that this could still result in OnError
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}
		
		[Test]
		public void Start ()
		{
			bool next = false;
			try {
				Observable.Start (() => { Thread.Sleep (200); throw new MyException (); }); // run it in another thread.
				next = true;
			} catch (MyException) {
				Assert.IsTrue (next, "#1");
			}
		}
		
		[Test]
		public void TakeLastErrorSequence ()
		{
			var source = Observable.Range (0, 3).Concat (Observable.Throw<int> (new SystemException ())).TakeLast (3);
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("error:System.SystemException", s, "#1");
		}
		
		[Test]
		public void TakeWhile ()
		{
			var source = Observable.Range (0, 5).TakeWhile (i => i < 3);
			string s = null;
			source.Subscribe (i => s += i, () => s += "done");
			Assert.AreEqual ("012done", s, "#1");
		}
		
		[Test]
		public void TakeWhileErrorSequence ()
		{
			var source = Observable.Range (0, 3).Concat (Observable.Throw<int> (new SystemException ())).TakeWhile (i => i < 3);
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("012error:System.SystemException", s, "#1");
		}
		
		[Test]
		[ExpectedException (typeof (MyException))]
		public void TakeWhileErrorSelector ()
		{
			var source = Observable.Range (0, 3).Concat (Observable.Throw<int> (new SystemException ())).TakeWhile (i => { throw new MyException (); });
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
		}
		
		[Test]
		public void Throttle ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = Observable.Range (1, 3).Concat (Observable.Return (2).Delay (TimeSpan.FromMilliseconds (100), scheduler)).Throttle (TimeSpan.FromMilliseconds (50), scheduler);
			bool done = false;
			var l = new List<int> ();
			var dis = source.Subscribe (v => l.Add (v), () => done = true);
			scheduler.AdvanceBy (TimeSpan.FromSeconds (1));
			Assert.IsTrue (done, "#1");
			Assert.AreEqual (new int [] {3, 2}, l.ToArray (), "#2");
			dis.Dispose ();
		}
		
		[Test]
		public void Throttle2 ()
		{
			var subject = new Subject<string> ();
			var scheduler = new HistoricalScheduler ();
			var input = (from text in subject select text).Throttle (TimeSpan.FromSeconds (0.5), scheduler).Timestamp (scheduler);
			var sw = new StringWriter ();
			input.Subscribe (
				v => sw.WriteLine ("THR: {0} at {1:ss.fff} timer:{2:ss.fff}", v.Value, v.Timestamp, scheduler.Now),
				() => sw.WriteLine ("THR: completed: {0:ss.fff}", scheduler.Now));

			int [] vals = {100, 600, 300, 600, 400, 900, 500, 800};
			for (int i = 0; i < 10; i++) {
				var val = vals [i % vals.Length];
				scheduler.AdvanceBy (TimeSpan.FromMilliseconds (val));
				subject.OnNext (val.ToString ());
			}
			subject.OnCompleted ();
			string expected = @"THR: 100 at 00.600 timer:00.600
THR: 300 at 01.500 timer:01.500
THR: 400 at 02.500 timer:02.500
THR: 900 at 03.400 timer:03.400
THR: 500 at 03.900 timer:03.900
THR: 100 at 04.800 timer:04.800
THR: 600 at 04.900 timer:04.900
THR: completed: 04.900
".Replace ("\r\n", "\n");
			Assert.AreEqual (expected, sw.ToString ().Replace ("\r\n", "\n"), "#1");
		}
		
		[Test]
		public void TimeoutInTime ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = Observable.Range (0, 3).Timeout (TimeSpan.FromSeconds (1), scheduler);
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("012done", s, "#1");
		}
		
		[Test]
		public void TimeoutOutOfTime ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = Observable.Interval (TimeSpan.FromSeconds (1), scheduler).Take (2).Timeout (TimeSpan.FromSeconds (1), scheduler);
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			scheduler.AdvanceBy (TimeSpan.FromSeconds (5));
			Assert.AreEqual ("error:System.TimeoutException", s, "#1");
		}
		
		[Test]
		public void TimeoutErrorSelector ()
		{
			var scheduler = new HistoricalScheduler ();
			var source = Observable.Range (0, 3).Concat (Observable.Throw<int> (new SystemException ())).Timeout (TimeSpan.FromSeconds (1), scheduler);
			string s = null;
			source.Subscribe (v => s += v, ex => s += "error:" + ex.GetType (), () => s += "done");
			Assert.AreEqual ("012error:System.SystemException", s, "#1");
		}
		
		class Resource : IDisposable
		{
			public bool Disposed;
			
			public Resource ()
			{
			}
			
			public void Dispose ()
			{
				Disposed = true;
			}
			
			public IObservable<int> GetObservable ()
			{
				return Observable.Range (0, 3);
			}
		}
		
		[Test]
		public void Using ()
		{
			var res = new Resource ();
			var ro = Observable.Using<int,Resource> (() => res, r => r.GetObservable ());
			Assert.IsFalse (res.Disposed, "#1");
			int i = 0;
			bool done = false;
			var dis = ro.Subscribe (v => i += v, () => done = true);
			Assert.IsTrue (SpinWait.SpinUntil (() => done, TimeSpan.FromSeconds (1)), "#2");
			Assert.IsFalse (res.Disposed, "#2");
			dis.Dispose ();
			Assert.IsTrue (res.Disposed, "#3");
		}
		
		[Test]
		public void Where ()
		{
			var source = Observable.Range (0, 5).Where (i => i < 3);
			string s = null;
			source.Subscribe (i => s += i, () => s += "done");
			Assert.AreEqual ("012done", s, "#1");
		}

		[Test]
		public void WindowCounts ()
		{
			string expected = "(0,0)(1,0)(2,0)(3,0)(3,1)(4,0)(4,1)(5,1)(6,1)(6,2)(7,1)(7,2)(8,2)(9,2)(9,3)(10,2)(10,3)(11,3)(12,3)(12,4)(13,3)(13,4)(14,4)(15,4)(15,5)(16,4)(16,5)(17,5)(18,5)(18,6)(19,5)(19,6)done";
			WindowCounts (5, 3, 7, expected);
		}
		
		[Test]
		public void WindowCounts2 ()
		{
			string expected = "(0,0)(1,0)(2,0)(5,1)(6,1)(7,1)(10,2)(11,2)(12,2)(15,3)(16,3)(17,3)done";
			WindowCounts (3, 5, 4, expected);
		}
		
		void WindowCounts (int count, int skip, int windowCount, string expected)
		{
			var scheduler = new HistoricalScheduler ();
			var sources = Observable.Range (0, 20).Window (count, skip);
			int windows = 0;
			var sw = new StringWriter () { NewLine = "\n" };
			bool [] windowDone = new bool [windowCount];
			sources.Subscribe (source => {
				int w = windows++;
				source.Subscribe (v => sw.Write ("({0},{1})", v, w), () => windowDone [w] = true);
			}, () => sw.Write ("done"));
			Assert.AreEqual (expected, sw.ToString (), "#1");
			Assert.AreEqual (-1, Array.IndexOf (windowDone, false), "#2");
		}

		[Test]
		public void WindowTimeSpans ()
		{
			var scheduler = new HistoricalScheduler ();
			var interval = TimeSpan.FromMilliseconds (100);
			var span = TimeSpan.FromMilliseconds (300);
			var shift = TimeSpan.FromMilliseconds (500);
			var total = TimeSpan.FromMilliseconds (1500);
			
			var sw = new StringWriter () { NewLine = "\n" };
			var sources = Observable.Interval (interval, scheduler).Take (15).Window (span, shift, scheduler);
			int windows = 0;
			bool [] windowDone = new bool [4];
			sources.Subscribe (source => {
				int w = windows++;
				source.Subscribe (v => sw.WriteLine("{0:ss.fff} {1} [{2}]", scheduler.Now, v, w), () => windowDone [w] = true);
				}, () => sw.WriteLine ("done"));
			scheduler.AdvanceBy (total);
			string expected = @"00.100 0 [0]
				00.200 1 [0]
				00.500 4 [1]
				00.600 5 [1]
				00.700 6 [1]
				01.000 9 [2]
				01.100 10 [2]
				01.200 11 [2]
				01.500 14 [3]
				done
				".Replace ("\t", "").Replace ("\r", "");
			Assert.AreEqual (expected, sw.ToString (), "#1");
			Assert.AreEqual (-1, Array.IndexOf (windowDone, false), "#2");
		}


		[Test]
		public void WindowTimeSpans2 ()
		{
			var scheduler = new HistoricalScheduler ();
			var interval = TimeSpan.FromMilliseconds (100);
			var span = TimeSpan.FromMilliseconds (500);
			var shift = TimeSpan.FromMilliseconds (300);
			var total = TimeSpan.FromMilliseconds (1500);
			
			var sw = new StringWriter () { NewLine = "\n" };
			var sources = Observable.Interval (interval, scheduler).Take (15).Window (span, shift, scheduler);
			int windows = 0;
			bool [] windowDone = new bool [6];
			sources.Subscribe (source => {
				int w = windows++;
				source.Subscribe (v => sw.WriteLine("{0:ss.fff} {1} [{2}]", scheduler.Now, v, w), () => windowDone [w] = true);
				}, () => sw.WriteLine ("done"));
			scheduler.AdvanceBy (total);
			
			string expected = @"00.100 0 [0]
				00.200 1 [0]
				00.300 2 [0]
				00.300 2 [1]
				00.400 3 [0]
				00.400 3 [1]
				00.500 4 [1]
				00.600 5 [1]
				00.600 5 [2]
				00.700 6 [1]
				00.700 6 [2]
				00.800 7 [2]
				00.900 8 [2]
				00.900 8 [3]
				01.000 9 [2]
				01.000 9 [3]
				01.100 10 [3]
				01.200 11 [3]
				01.200 11 [4]
				01.300 12 [3]
				01.300 12 [4]
				01.400 13 [4]
				01.500 14 [4]
				01.500 14 [5]
				done
				".Replace ("\t", "").Replace ("\r", "");
			Assert.AreEqual (expected, sw.ToString (), "#1");
			Assert.AreEqual (-1, Array.IndexOf (windowDone, false), "#2");
		}
	}
}
