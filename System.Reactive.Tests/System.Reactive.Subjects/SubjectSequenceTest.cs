using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using NUnit.Framework;

namespace System.Reactive.Subjects.Tests
{
	[TestFixture]
	public class SubjectSequenceTest
	{
		[Flags]
		enum Subjects
		{
			Simple = 1,
			Replay = 2,
			ProcessAll = 3,
			Behavior = 4,
			Async = 8,
			All = 15,
		}
		
		void RunTest<T> (Subjects target, Action<ISubject<T>> action)
		{
			if ((target & Subjects.Simple) != 0)
				action (new Subject<T> ());
			if ((target & Subjects.Replay) != 0)
				action (new ReplaySubject<T> ());
			if ((target & Subjects.Behavior) != 0)
				action (new BehaviorSubject<T> (default (T)));
			if ((target & Subjects.Async) != 0)
				action (new AsyncSubject<T> ());
		}

		[Test]
		public void NoOnNextAfterOnCompleted ()
		{
			RunTest<int> (Subjects.ProcessAll | Subjects.Async, sub => {
				sub.Subscribe (v => Assert.Fail ("should not raise OnNext : " + sub));
				sub.OnCompleted ();
				sub.OnNext (0);
			});
		}

		[Test]
		public void NoOnNextAfterOnError ()
		{
			RunTest<int> (Subjects.ProcessAll | Subjects.Async, sub => {
				bool error = false;
				sub.Subscribe (v => Assert.Fail ("should not raise OnNext : " + sub), ex => error = true, () => Assert.Fail ("should not complete : " + sub));
				sub.OnError (new Exception ());
				sub.OnNext (0);
				Assert.IsTrue (error, "#1: " + sub);
			});
		}

		[Test]
		public void OnNextAfterOnCompletedIgnored ()
		{
			RunTest<int> (Subjects.ProcessAll, sub => {
				int i = 0;
				sub.Subscribe (v => i += v);
				sub.OnNext (3);
				sub.OnNext (4);
				sub.OnCompleted ();
				sub.OnNext (5);
				Assert.AreEqual (7, i, "#1 " + sub);
			});
		}

		[Test]
		public void NoOnCompletedAfterOnCompleted ()
		{
			RunTest<int> (Subjects.All, sub => {
				bool completed = false;
				sub.Subscribe (v => {}, () => {if (completed) Assert.Fail ("Should not complete twice : " + sub); else completed = true; });
				sub.OnCompleted ();
				sub.OnCompleted ();
			});
		}

		[Test]
		public void NoOnErrorAfterOnCompleted ()
		{
			RunTest<int> (Subjects.All, sub => {
				sub.Subscribe (v => {}, ex => Assert.Fail ("Should not raise error after OnCompleted : " + sub));
				sub.OnCompleted ();
				sub.OnError (new Exception ("foo"));
			});
		}

		[Test]
		public void NoOnErrorAfterOnError ()
		{
			RunTest<int> (Subjects.All, sub => {
				bool error = false;
				sub.Subscribe (v => {}, ex => { if (error) Assert.Fail ("Should not raise error after OnCompleted"); else error = true; }, () => Assert.Fail ("should not complete : " + sub));
				sub.OnError (new Exception ("foo"));
				sub.OnError (new Exception ("foo"));
			});
		}

		[Test]
		public void NoOnCompleteAfterOnError ()
		{
			RunTest<int> (Subjects.All, sub => {
				bool error = false;
				sub.Subscribe (v => {}, ex => error = true, () => Assert.Fail ("should not complete : " + sub));
				sub.OnError (new Exception ("foo"));
				sub.OnError (new Exception ("foo"));
				Assert.IsTrue (error, "#1 : " + sub);
			});
		}
	}
}

