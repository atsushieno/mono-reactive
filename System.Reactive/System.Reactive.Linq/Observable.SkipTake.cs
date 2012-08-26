using System;
using System.Collections;
using System.Collections.Concurrent;
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
using System.Threading.Tasks;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		public static IObservable<TSource> Skip<TSource> (
			this IObservable<TSource> source,
			int count)
		{
			return source.SkipWhile ((s, i) => i < count);
		}

#if REACTIVE_2_0		
		public static IObservable<TSource> Skip<TSource> (
			this IObservable<TSource> source,
			TimeSpan duration)
		{
			return source.Skip (duration, Scheduler.Default);
		}
		
		public static IObservable<TSource> Skip<TSource> (
			this IObservable<TSource> source,
			TimeSpan duration, IScheduler scheduler)
		{
			DateTimeOffset start = scheduler.Now;
			duration = Scheduler.Normalize (duration);
			return source.SkipWhile (s => scheduler.Now - start < duration);
		}
#endif
		
		public static IObservable<TSource> SkipLast<TSource> (
			this IObservable<TSource> source,
			int count)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (count < 0)
				throw new ArgumentOutOfRangeException ("count");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			var q = new Queue<TSource> ();
			return source.Subscribe ((s) => {
				q.Enqueue (s);
				if (count > 0)
					count--;
				else
					sub.OnNext (q.Dequeue ());
			}, ex => {
				q.Clear ();
				sub.OnError (ex);
			}, () => {
				q.Clear ();
				sub.OnCompleted ();
				});
			// ----
			}, DefaultColdScheduler);
		}
		
#if REACTIVE_2_0
		
		public static IObservable<TSource> SkipLast<TSource> (
			this IObservable<TSource> source,
			TimeSpan duration)
		{
			return source.SkipLast (duration, Scheduler.Default);
		}
		
		public static IObservable<TSource> SkipLast<TSource> (
			this IObservable<TSource> source,
			TimeSpan duration, IScheduler scheduler)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			duration = Scheduler.Normalize (duration);
			
			return new ColdObservableEach<TSource> (sub => {
				// ----
				DateTimeOffset start = scheduler.Now;
				var q = new CompositeDisposable ();
				return source.Subscribe (Observer.Create<TSource> (s => {
					IDisposable task = null;
					task = scheduler.Schedule (duration, () => { sub.OnNext (s); q.Remove (task); });
					q.Add (task);
				}, ex => {
					q.Dispose (); // cancel all existing tasks.
					sub.OnError (ex);
				}, () => {
					q.Dispose (); // cancel all existing tasks.
					sub.OnCompleted ();
				}));
				// ----
			}, scheduler);
		}
#endif
		
		static IObservable<TSource> SwitchUntil<TSource, TOther> (this IObservable<TSource> source, IObservable<TOther> other, bool initValue)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (other == null)
				throw new ArgumentNullException ("other");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			bool enabled = initValue;
			var dis = new SingleAssignmentDisposable ();
			var odis = new SingleAssignmentDisposable ();
			odis.Disposable = other.Subscribe (v => { enabled = !initValue; odis.Dispose (); }, ex => odis.Dispose (), () => odis.Dispose ());
			dis.Disposable = source.Subscribe (v => { if (enabled) sub.OnNext (v); }, ex => sub.OnError (ex), () => sub.OnCompleted ());
			return new CompositeDisposable (odis, dis);
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> SkipUntil<TSource, TOther> (
			this IObservable<TSource> source,
			IObservable<TOther> other)
		{
			return source.SwitchUntil (other, false);
		}

		public static IObservable<TSource> SkipWhile<TSource> (
			this IObservable<TSource> source,
			Func<TSource, bool> predicate)
		{
			return source.SkipWhile ((s, i) => predicate (s));
		}
		
		public static IObservable<TSource> SkipWhile<TSource> (
			this IObservable<TSource> source,
			Func<TSource, int, bool> predicate)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (predicate == null)
				throw new ArgumentNullException ("predicate");

			bool skipDone = false;
			return source.Where ((s, i) => skipDone || (skipDone = !predicate (s, i)));
		}
		
#if REACTIVE_2_0
		public static IObservable<TSource> SkipUntil<TSource> (
			this IObservable<TSource> source,
			DateTimeOffset startTime)
		{
			return source.SkipUntil (startTime, Scheduler.Default);
		}
		
		public static IObservable<TSource> SkipUntil<TSource> (
			this IObservable<TSource> source,
			DateTimeOffset startTime,
			IScheduler scheduler)
		{
			return source.SkipWhile (s => scheduler.Now < startTime);
		}
#endif
		
		public static IObservable<TSource> Take<TSource> (
			this IObservable<TSource> source,
			int count)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			return new ColdObservableEach<TSource> (sub => {
			// ----
			int idx = 0;
			bool done = false;
			return source.Subscribe (
				v => {
					if (!done) {
						if (idx < count)
							sub.OnNext (v);
						if (++idx == count)
							sub.OnCompleted ();
					}
				},
				ex => {
					if (!done)
						sub.OnError (ex);
					done = true;
				},
				() => {
					if (!done)
						sub.OnCompleted ();
					done = true;
				});
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> TakeLast<TSource> (
			this IObservable<TSource> source,
			int count)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (count < 0)
				throw new ArgumentOutOfRangeException ("count");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			var q = new Queue<TSource> ();
			return source.Subscribe ((s) => {
				q.Enqueue (s);
				if (count > 0)
					count--;
				else
					q.Dequeue ();
				}, ex => sub.OnError (ex), () => {
				while (q.Count > 0)
					sub.OnNext (q.Dequeue ());
				sub.OnCompleted ();
				});
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> TakeUntil<TSource, TOther> (
			this IObservable<TSource> source,
			IObservable<TOther> other)
		{
			return source.SwitchUntil (other, true);
		}

#if REACTIVE_2_0
		public static IObservable<TSource> Take<TSource> (
			this IObservable<TSource> source,
			TimeSpan duration)
		{
			return source.Take (duration, Scheduler.Default);
		}
		
		public static IObservable<TSource> Take<TSource> (
			this IObservable<TSource> source,
			TimeSpan duration,
			IScheduler scheduler)
		{
			var start = scheduler.Now;
			return source.TakeWhile (s => scheduler.Now - start < duration);
		}
		
		public static IObservable<TSource> TakeLast<TSource> (
			this IObservable<TSource> source,
			TimeSpan duration)
		{
			return source.TakeLast (duration, Scheduler.Default);
		}
		
		public static IObservable<TSource> TakeLast<TSource> (
			this IObservable<TSource> source,
			TimeSpan duration,
			IScheduler scheduler)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			
			return new ColdObservableEach<TSource> (sub => {
				// ----
				DateTimeOffset start = scheduler.Now;
				var q = new Queue<KeyValuePair<DateTimeOffset,TSource>> ();
				bool done = false;
				return source.Subscribe (Observer.Create<TSource> (s => {
					if (done)
						return;
					q.Enqueue (new KeyValuePair<DateTimeOffset,TSource> (scheduler.Now, s));
					while (q.Count > 0) {
						var p = q.Peek ();
						if (scheduler.Now - p.Key < duration)
							break;
						q.Dequeue ();
					}
				}, ex => {
					while (q.Count > 0)
						sub.OnNext (q.Dequeue ().Value);
					sub.OnError (ex);
					done = true;
				}, () => {
					while (q.Count > 0)
						sub.OnNext (q.Dequeue ().Value);
					sub.OnCompleted ();
					done = true;
				}));
				// ----
			}, scheduler);
		}
		
		public static IObservable<TSource> TakeUntil<TSource> (
			this IObservable<TSource> source,
			DateTimeOffset duration)
		{
			return source.TakeUntil (duration, Scheduler.Default);
		}
		
		public static IObservable<TSource> TakeUntil<TSource> (
			this IObservable<TSource> source,
			DateTimeOffset duration,
			IScheduler scheduler)
		{
			var start = scheduler.Now;
			return source.TakeWhile (s => scheduler.Now < duration);
		}
#endif
		
		public static IObservable<TSource> TakeWhile<TSource> (
			this IObservable<TSource> source,
			Func<TSource, bool> predicate)
		{
			if (predicate == null)
				throw new ArgumentNullException ("predicate");

			return source.TakeWhile ((s, i) => predicate (s));
		}
		
		public static IObservable<TSource> TakeWhile<TSource> (
			this IObservable<TSource> source,
			Func<TSource, int, bool> predicate)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (predicate == null)
				throw new ArgumentNullException ("predicate");
			return new ColdObservableEach<TSource> (sub => {
				// ----
				int idx = 0;
				bool done = false;
				return source.Subscribe (s => {
					if (!done) {
						if (predicate (s, idx++))
							sub.OnNext (s);
						else {
							done = true;
							sub.OnCompleted ();
						}
					}
				}, ex => {
					if (!done)
						sub.OnError (ex);
					done = true;
				}, () => {
					sub.OnCompleted ();
					done = true;
				});
			}, DefaultColdScheduler);
		}		
	}
}
