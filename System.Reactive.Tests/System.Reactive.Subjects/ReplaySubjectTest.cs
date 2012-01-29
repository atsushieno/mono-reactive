using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using NUnit.Framework;

namespace System.Reactive.Subjects.Tests
{
	[TestFixture]
	public class ReplaySubjectTest
	{
		class MyException : Exception
		{
		}
		
		class ErrorScheduler : IScheduler
		{
			public DateTimeOffset Now { get { return DateTimeOffset.Now; } }
			public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
			{
				throw new MyException ();
			}
			public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
			{
				throw new MyException ();
			}
			public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
			{
				throw new MyException ();
			}
		}
		
		[Test]
		public void OnNextThenSubscription ()
		{
			var sub = new ReplaySubject<string> ();
			sub.OnNext ("X");
			string s1 = null, s2 = null, s3 = null;
			sub.Subscribe (s => s1 += s);
			sub.OnNext ("Y");
			sub.Subscribe (s => s2 += s);
			sub.OnNext ("Z");
			sub.OnCompleted ();
			sub.Subscribe (s => s3 += s);
			Assert.AreEqual ("XYZ", s1, "#1");
			Assert.AreEqual ("XYZ", s2, "#2");
			Assert.AreEqual ("XYZ", s3, "#3");
		}

		[Test]
		public void OnErrorThenOnNextAndSubscription ()
		{
			var sub = new ReplaySubject<string> ();
			sub.OnNext ("X");
			string s1 = null, s2 = null;
			bool error = false;
			sub.Subscribe (s => s1 += s, ex => error = true);
			sub.OnError (new Exception ());
			sub.OnNext ("Y"); // ignored.
			Assert.IsTrue (error, "#1");
			sub.Subscribe (s => s2 += s, ex => error = true);
			Assert.AreEqual ("X", s1, "#2");
			Assert.AreEqual ("X", s2, "#3");
		}
		
		[Test]
		public void SchedulerUsage ()
		{
			var sub = new ReplaySubject<int> (new ErrorScheduler ());
			sub.Subscribe (Console.WriteLine);
		}
	}
}
