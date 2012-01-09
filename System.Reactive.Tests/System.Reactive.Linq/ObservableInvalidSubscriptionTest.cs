using System;
using NUnit.Framework;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace System.Reactive.Linq.Tests
{
	// This test fixture is a collection for invalid subscription check
	// i.e. they check invalid attempts to subscribe when it should not.
	// It is useful to see if things do not enter eager evaluation.
	//
	// They make used of ExceptionalObservable<T>.
	
	[TestFixture]
	public class ObservableInvalidSubscriptionTest
	{
		void Check<T> (Func<IObservable<T>, IObservable<T>> action, Action wait)
		{
			Check<T,T> (action, wait);
		}
		
		// The argument of the func is ExceptionalObservable, and the return value will be subscribed Console.WriteLine.
		void Check<TSource, TResult> (Func<IObservable<TSource>, IObservable<TResult>> action, Action wait)
		{
			var ret = action (new ExceptionalObservable<TSource> ());
			try {
				ret.Subscribe (Console.WriteLine);
				if (wait != null)
					wait ();
				Assert.Fail ("expected to throw ExceptionalObservableException");
			} catch (ExceptionalObservableException) {
			}
		}

		[Test]
		public void Concat ()
		{
			Check<int> (o => o.Concat (Observable.Return (5)), null);
		}

		[Test]
		public void Timestamp ()
		{
			Check<int, Timestamped<int>> (o => o.Timestamp (Scheduler.Immediate), null);
		}
	}
}
