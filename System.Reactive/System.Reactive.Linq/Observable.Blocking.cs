using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Joins;
using System.Reactive.Subjects;
using System.Threading;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		public static TSource First<TSource> (this IObservable<TSource> source)
		{
			return First<TSource> (source, (s) => true);
		}
		
		public static TSource First<TSource> (this IObservable<TSource> source, Func<TSource, bool> predicate)
		{
			return InternalFirstOrDefault<TSource> (source, predicate, true);
		}
		
		public static TSource FirstOrDefault<TSource> (this IObservable<TSource> source)
		{
			return FirstOrDefault (source, (s) => true);
		}
		
		public static TSource FirstOrDefault<TSource> (this IObservable<TSource> source, Func<TSource, bool> predicate)
		{
			return InternalFirstOrDefault<TSource> (source, predicate, false);
		}
		
		// The callers (First/FirstOrDefault) are blocking methods.
		static TSource InternalFirstOrDefault<TSource> (this IObservable<TSource> source, Func<TSource, bool> predicate, bool throwError)
		{
			// FIXME: should we use SpinWait or create some hybrid one?
			var wait = new ManualResetEvent (false);
			TSource ret = default (TSource);
			bool got = false;
			IDisposable dis = null;
			dis = source.Subscribe (
				// the first "if (!got) check is required because the source may send next values before unsubscribing this action by dis.Dispose().
				(s) => { if (!got && predicate (s)) { got = true; ret = s; dis.Dispose (); wait.Set (); } },
				() => { if (!got) wait.Set (); }
				);
			wait.WaitOne ();
			if (!got && throwError)
				throw new InvalidOperationException ();
			return ret;
		}
		
		public static void ForEach<TSource> (this IObservable<TSource> source, Action<TSource> onNext)
		{
			var wait = new ManualResetEvent (false);
			Exception error = null;
			var dis = source.Subscribe (v => onNext (v), ex => { error = ex; wait.Set (); }, () => wait.Set ());
			wait.WaitOne ();
			dis.Dispose ();
			if (error != null)
				throw error;
		}
		
		public static TSource Last<TSource> (this IObservable<TSource> source)
		{
			return Last<TSource> (source, s => true);
		}
		
		public static TSource Last<TSource> (this IObservable<TSource> source, Func<TSource, bool> predicate)
		{
			return InternalLastOrDefault<TSource> (source, predicate, true);
		}
		
		public static TSource LastOrDefault<TSource> (this IObservable<TSource> source)
		{
			return LastOrDefault<TSource> (source, s => true);
		}
		
		public static TSource LastOrDefault<TSource> (this IObservable<TSource> source, Func<TSource, bool> predicate)
		{
			return InternalLastOrDefault<TSource> (source, predicate, false);
		}
		
		// The callers (Last/LastOrDefault) are blocking methods.
		static TSource InternalLastOrDefault<TSource> (this IObservable<TSource> source, Func<TSource, bool> predicate, bool throwError)
		{
			// FIXME: should we use SpinWait or create some hybrid one?
			var wait = new ManualResetEvent (false);
			TSource ret = default (TSource);
			bool got = false;
			IDisposable dis = null;
			dis = source.Subscribe (
				// the first "if (!got) check is required because the source may send next values before unsubscribing this action by dis.Dispose().
				(s) => { if (predicate (s)) { got = true; ret = s; } },
				() => { wait.Set (); }
				);
			wait.WaitOne ();
			dis.Dispose ();
			if (!got && throwError)
				throw new InvalidOperationException ();
			return ret;
		}
		
		public static TSource Single<TSource> (
			this IObservable<TSource> source,
			Func<TSource, bool> predicate)
		{
			return InternalSingleOrDefault<TSource> (source, predicate, true);
		}
		
		public static TSource SingleOrDefault<TSource> (this IObservable<TSource> source)
		{
			return SingleOrDefault<TSource> (source, s => true);
		}
		
		public static TSource SingleOrDefault<TSource> (
			this IObservable<TSource> source,
			Func<TSource, bool> predicate)
		{
			return InternalSingleOrDefault<TSource> (source, predicate, false);
		}
		
		// The callers (Single/SingleOrDefault) are blocking methods.
		static TSource InternalSingleOrDefault<TSource> (this IObservable<TSource> source, Func<TSource, bool> predicate, bool throwError)
		{
			// FIXME: should we use SpinWait or create some hybrid one?
			var wait = new ManualResetEvent (false);
			TSource ret = default (TSource);
			bool got = false, error = false;
			IDisposable dis = null;
			dis = source.Subscribe (
				// the first "if (!got) check is required because the source may send next values before unsubscribing this action by dis.Dispose().
				(s) => { if (predicate (s)) {
					if (got)
						error = true;
					got = true;
					ret = s;
					}
				},
				() => { wait.Set (); }
				);
			wait.WaitOne ();
			dis.Dispose ();
			if (error)
				throw new InvalidOperationException ("Observed that there was more than one item in the target object");
			if (!got && throwError)
				throw new InvalidOperationException ();
			return ret;
		}
	}
}
