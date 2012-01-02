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

namespace System.Reactive.Linq
{
	// For the default scheduler in each method, see http://social.msdn.microsoft.com/Forums/en-AU/rx/thread/e032b40a-019b-496e-bb11-64c8fcc94410

	public static partial class Observable
	{
		public static IObservable<TSource> Aggregate<TSource> (
			this IObservable<TSource> source,
			Func<TSource, TSource, TSource> accumulator)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TAccumulate> Aggregate<TSource, TAccumulate> (
			this IObservable<TSource> source,
			TAccumulate seed,
			Func<TAccumulate, TSource, TAccumulate> accumulator)
		{ throw new NotImplementedException (); }
		
		public static IObservable<bool> All<TSource> (
			this IObservable<TSource> source,
			Func<TSource, bool> predicate)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (predicate == null)
				throw new ArgumentNullException ("predicate");

			var sub = new Subject<bool> ();
			IDisposable dis = null;
			bool ret = true;
			bool hasValue = false;
			dis = source.Subscribe ((s) => {
				hasValue = true;
				try {
					ret &= predicate (s);
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, () => {
				try {
					sub.OnNext (hasValue && ret);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				});
			return new WrappedSubject<bool> (sub, dis);
		}
		
		public static IObservable<TSource> Amb<TSource> (this IEnumerable<IObservable<TSource>> sources)
		{
			if (sources == null)
				throw new ArgumentNullException ("sources");
			var sub = new Subject<TSource> ();
			IObservable<TSource> first = null;

			// avoided using "from source in sources select ..." for eager evaluation.
			var dis = new List<IDisposable> ();
			foreach (var source in sources) {
				dis.Add (source.Subscribe (Observer.Create<TSource> (s => {
					if (first == null)
						first = source;
					if (first == source)
						sub.OnNext (s);
				}, ex => {
					if (first == null)
						first = source;
					if (first == source)
						sub.OnError (ex);
				}, () => {
					if (first == null)
						first = source;
					if (first == source)
						sub.OnCompleted ();
				})));
			}
			return new WrappedSubject<TSource> (sub, Disposable.Create (() => { foreach (var d in dis) d.Dispose (); sub.Dispose (); }));
		}
		
		public static IObservable<TSource> Amb<TSource> (params IObservable<TSource>[] sources)
		{
			return Amb ((IEnumerable<IObservable<TSource>>) sources);
		}
		
		public static IObservable<TSource> Amb<TSource> (this IObservable<TSource> first, IObservable<TSource> second)
		{
			return Amb (new IObservable<TSource> [] {first, second});
		}
		
		public static Pattern<TLeft, TRight> And<TLeft, TRight> (this IObservable<TLeft> left, IObservable<TRight> right)
		{
			return new Pattern<TLeft, TRight> (left, right);
		}
		
		public static IObservable<bool> Any<TSource> (this IObservable<TSource> source)
		{
			return Any<TSource> (source, (s) => true);
		}
		
		public static IObservable<bool> Any<TSource> (this IObservable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (predicate == null)
				throw new ArgumentNullException ("predicate");

			var sub = new Subject<bool> ();
			IDisposable dis = null;
			bool hit = false;
			dis = source.Subscribe ((s) => {
				try {
					if (predicate (s)) {
						hit = true;
						sub.OnNext (true);
						sub.OnCompleted ();
						dis.Dispose ();
					}
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, () => {
				try {
					if (!hit) {
						sub.OnNext (false);
						sub.OnCompleted ();
						dis.Dispose ();
					}
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				});
			return new WrappedSubject<bool> (sub, dis);
		}
		
		public static IObservable<TSource> AsObservable<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> Cast<TResult> (this IObservable<Object> source)
		{ throw new NotImplementedException (); }

		public static IObservable<TSource> Catch<TSource> (this IEnumerable<IObservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Catch<TSource> (params IObservable<TSource> [] sources)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Catch<TSource, TException> (
			this IObservable<TSource> source,
			Func<TException, IObservable<TSource>> handler)
			where TException : Exception
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Catch<TSource> (
			this IObservable<TSource> first,
			IObservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> CombineLatest<TFirst, TSecond, TResult> (
			this IObservable<TFirst> first,
			IObservable<TSecond> second,
			Func<TFirst, TSecond, TResult> resultSelector)
		{
			var sub = new Subject<TResult> ();
			TFirst fv = default (TFirst);
			TSecond sv = default (TSecond);
			bool first_started = false, second_started = false, first_completed = false, second_completed = false;
			var dis1 = first.Subscribe (
				f => { fv = f; first_started = true; if (second_started) sub.OnNext (resultSelector (fv, sv)); },
				ex => sub.OnError (ex),
				() => { first_completed = true; if (second_completed) sub.OnCompleted (); });
			var dis2 = second.Subscribe (
				s => { sv = s; second_started = true; if (first_started) sub.OnNext (resultSelector (fv, sv)); },
				ex => sub.OnError (ex),
				() => { second_completed = true; if (first_completed) sub.OnCompleted (); });
			return new WrappedSubject<TResult> (sub, new CompositeDisposable (dis1, dis2));
		}
		
		public static IObservable<TSource> Concat<TSource> (this IEnumerable<IObservable<TSource>> sources)
		{
			var sub = new Subject<TSource> ();
			var dis = new List<IDisposable> ();
			StartConcat (sources.GetEnumerator (), sub, dis);
			return new WrappedSubject<TSource> (sub, Disposable.Create (() => { foreach (var d in dis) d.Dispose (); }));
		}
		
		static bool StartConcat<TSource> (IEnumerator<IObservable<TSource>> sources, Subject<TSource> sub, List<IDisposable> dis)
		{
			if (!sources.MoveNext ())
				return true;
			dis.Add (sources.Current.Subscribe (v => sub.OnNext (v), ex => sub.OnError (ex), () => { if (StartConcat (sources, sub, dis)) sub.OnCompleted (); }));
			return false;
		}
		
		public static IObservable<TSource> Concat<TSource> (this IObservable<IObservable<TSource>> sources)
		{
			return sources.ToEnumerable ().Concat ();
		}
		
		public static IObservable<TSource> Concat<TSource> (params IObservable<TSource> [] sources)
		{
			return sources.AsEnumerable ().Concat ();
		}
		
		public static IObservable<TSource> Concat<TSource> (this IObservable<TSource> first, IObservable<TSource> second)
		{
			return new IObservable<TSource> [] {first, second}.Concat ();
		}
		
		public static IObservable<bool> Contains<TSource> (
			this IObservable<TSource> source,
			TSource value)
		{
			return Contains<TSource> (source, value, EqualityComparer<TSource>.Default);
		}
		
		public static IObservable<bool> Contains<TSource> (
			this IObservable<TSource> source,
			TSource value,
			IEqualityComparer<TSource> comparer)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (comparer == null)
				throw new ArgumentNullException ("comparer");
			return Any<TSource> (source, v => comparer.Equals (v, value));
		}
		
		public static IObservable<int> Count<TSource> (this IObservable<TSource> source)
		{
			var sub = new Subject<int> ();
			IDisposable dis = null;
			int count = 0;
			dis = source.Subscribe ((s) => count++, () => { sub.OnNext (count); sub.OnCompleted (); dis.Dispose (); });
			return sub;
		}
		
		public static IObservable<TSource> Create<TSource> (Func<IObserver<TSource>, Action> subscribe)
		{
			return Create<TSource> (observer => Disposable.Create (subscribe (observer)));
		}
		
		public static IObservable<TSource> Create<TSource> (Func<IObserver<TSource>, IDisposable> subscribe)
		{
			return new SimpleDisposableObservable<TSource> (subscribe);
		}
		
		public static IObservable<TSource> DefaultIfEmpty<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> DefaultIfEmpty<TSource> (this IObservable<TSource> source, TSource defaultValue)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TValue> Defer<TValue> (Func<IObservable<TValue>> observableFactory)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Delay<TSource> (this IObservable<TSource> source, DateTimeOffset dueTime)
		{
			return Delay<TSource> (source, dueTime, Scheduler.ThreadPool);
		}
		
		public static IObservable<TSource> Delay<TSource> (this IObservable<TSource> source, TimeSpan dueTime)
		{
			return Delay<TSource> (source, dueTime, Scheduler.ThreadPool);
		}
		
		public static IObservable<TSource> Delay<TSource> (
			this IObservable<TSource> source,
			DateTimeOffset dueTime,
			IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			return Delay<TSource> (source, dueTime - scheduler.Now, scheduler);
		}
		
		public static IObservable<TSource> Delay<TSource> (
			this IObservable<TSource> source,
			TimeSpan dueTime,
			IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			return new ColdObservable<TSource> ((sub) => {
				Thread.Sleep (Scheduler.Normalize (dueTime)); 
				source.Subscribe (sub);
				}, scheduler);
		}
		
		public static IObservable<TSource> Dematerialize<TSource> (this IObservable<Notification<TSource>> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Distinct<TSource> (this IObservable<TSource> source)
		{
			return source.Distinct (EqualityComparer<TSource>.Default);
		}
		
		public static IObservable<TSource> Distinct<TSource> (
			this IObservable<TSource> source,
			IEqualityComparer<TSource> comparer)
		{
			return Distinct<TSource, TSource> (source, s => s, comparer);
		}
		
		public static IObservable<TSource> Distinct<TSource, TKey> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector)
		{
			return source.Distinct (keySelector, EqualityComparer<TKey>.Default);
		}
		
		public static IObservable<TSource> Distinct<TSource, TKey> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (keySelector == null)
				throw new ArgumentNullException ("keySelector");
			if (comparer == null)
				throw new ArgumentNullException ("comparer");

			var sub = new Subject<TSource> ();
			IDisposable dis = null;
			var keys = new HashSet<TKey> (comparer);
			dis = source.Subscribe (Observer.Create<TSource> (
				(s) => {
					var k = keySelector (s);
					if (!keys.Contains (k)) {
						keys.Add (k);
						sub.OnNext (s);
					}
				},
				(ex) => sub.OnError (ex),
				() => {
					sub.OnCompleted ();
				}));
			return new WrappedSubject<TSource> (sub, dis);
		}
		
		public static IObservable<TSource> DistinctUntilChanged<TSource> (this IObservable<TSource> source)
		{
			return source.DistinctUntilChanged (EqualityComparer<TSource>.Default);
		}
		
		public static IObservable<TSource> DistinctUntilChanged<TSource> (
			this IObservable<TSource> source,
			IEqualityComparer<TSource> comparer)
		{
			return source.DistinctUntilChanged (k => k, comparer);
		}
		
		public static IObservable<TSource> DistinctUntilChanged<TSource, TKey> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector)
		{
			return source.DistinctUntilChanged (keySelector, EqualityComparer<TKey>.Default);
		}
		
		public static IObservable<TSource> DistinctUntilChanged<TSource, TKey> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (keySelector == null)
				throw new ArgumentNullException ("keySelector");
			if (comparer == null)
				throw new ArgumentNullException ("comparer");

			var sub = new Subject<TSource> ();
			IDisposable dis = null;
			bool hit = false;
			TKey prev = default (TKey);
			dis = source.Subscribe (Observer.Create<TSource> ((s) => {
				try {
					var k = keySelector (s);
					if (!hit) {
						hit = true;
						prev = k;
						sub.OnNext (s);
					} else if (!comparer.Equals (k, prev)) {
						prev = k;
						sub.OnNext (s);
					}
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, () => {
				try {
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}));
			return new WrappedSubject<TSource> (sub, dis);
		}

		public static IObservable<TSource> Do<TSource> (
			this IObservable<TSource> source,
			Action<TSource> onNext)
		{ throw new NotImplementedException (); }

		public static IObservable<TSource> Do<TSource> (
			this IObservable<TSource> source,
			IObserver<TSource> observer)
		{ throw new NotImplementedException (); }

		public static IObservable<TSource> Do<TSource> (
			this IObservable<TSource> source,
			Action<TSource> onNext,
			Action<Exception> onError)
		{ throw new NotImplementedException (); }

		public static IObservable<TSource> Do<TSource> (
			this IObservable<TSource> source,
			Action<TSource> onNext,
			Action onCompleted)
		{ throw new NotImplementedException (); }

		public static IObservable<TSource> Do<TSource> (
			this IObservable<TSource> source,
			Action<TSource> onNext,
			Action<Exception> onError,
			Action onCompleted)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> ElementAt<TSource> (this IObservable<TSource> source, int index)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> ElementAtOrDefault<TSource> (this IObservable<TSource> source, int index)
		{ throw new NotImplementedException (); }
		
		// see http://leecampbell.blogspot.com/2010/05/rx-part-2-static-and-extension-methods.html
		public static IObservable<TResult> Empty<TResult> ()
		{
			var sub = new ReplaySubject<TResult> ();
			sub.OnCompleted ();
			return sub;
		}
		
		public static IObservable<TResult> Empty<TResult> (IScheduler scheduler)
		{
			var sub = new ReplaySubject<TResult> (scheduler);
			sub.OnCompleted ();
			return sub;
		}
		
		public static IObservable<TSource> Finally<TSource> (this IObservable<TSource> source, Action finallyAction)
		{ throw new NotImplementedException (); }
		
		public static void ForEach<TSource> (this IObservable<TSource> source, Action<TSource> onNext)
		{ throw new NotImplementedException (); }
		
		public static Func<IObservable<Unit>> FromAsyncPattern(
			Func<AsyncCallback, Object, IAsyncResult> begin,
			Action<IAsyncResult> end)
		{
			var sub = new Subject<Unit> ();
			return () => { begin ((res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub);
				return sub;
			};
		}
		
		public static Func<IObservable<TResult>> FromAsyncPattern<TResult> (
			Func<AsyncCallback, Object, IAsyncResult> begin,
			Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return () => { begin ((res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub);
				return sub;
			};
		}

		public static IObservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector)
		{
			return Generate<TState, TResult> (initialState, condition, iterate, resultSelector, Scheduler.CurrentThread);
		}

		public static IObservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector,
			Func<TState, DateTimeOffset> timeSelector)
		{
			return Generate<TState, TResult> (initialState, condition, iterate, resultSelector, timeSelector, Scheduler.ThreadPool);
		}

		public static IObservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector,
			Func<TState, TimeSpan> timeSelector)
		{
			return Generate<TState, TResult> (initialState, condition, iterate, resultSelector, timeSelector, Scheduler.ThreadPool);
		}

		public static IObservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector,
			IScheduler scheduler)
		{
			return Generate<TState, TResult> (initialState, condition, iterate, resultSelector, (st) => TimeSpan.Zero, scheduler);
		}

		public static IObservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector,
			Func<TState, DateTimeOffset> timeSelector,
			IScheduler scheduler)
		{
			return Generate<TState, TResult> (initialState, condition, iterate, resultSelector, (st) => timeSelector (st) - scheduler.Now, scheduler);
		}

		public static IObservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector,
			Func<TState, TimeSpan> timeSelector,
			IScheduler scheduler)
		{
			if (condition == null)
				throw new ArgumentNullException ("condition");
			if (iterate == null)
				throw new ArgumentNullException ("iterate");
			if (resultSelector == null)
				throw new ArgumentNullException ("resultSelector");
			if (timeSelector == null)
				throw new ArgumentNullException ("timeSelector");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			var sub = new Subject<TResult> ();
			Action action = () => {
				try {
					for (var i = initialState; condition (i); i = iterate (i)) {
						Thread.Sleep (Scheduler.Normalize (timeSelector (i)));
						sub.OnNext (resultSelector (i));
					}
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				};
			return new ColdObservable2<TResult> (sub, action, scheduler);
		}
		
		public static IEnumerator<TSource> GetEnumerator<TSource> (this IObservable<TSource> source)
		{
			return source.ToEnumerable ().GetEnumerator ();
		}
		
		public static IObservable<IGroupedObservable<TKey, TSource>> GroupBy<TSource, TKey> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector)
		{
			return source.GroupBy (keySelector, EqualityComparer<TKey>.Default);
		}
		
		public static IObservable<IGroupedObservable<TKey, TSource>> GroupBy<TSource, TKey> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{
			return GroupBy<TSource, TKey, TSource> (source, keySelector, s => s, comparer);
		}
		
		public static IObservable<IGroupedObservable<TKey, TElement>> GroupBy<TSource, TKey, TElement> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector)
		{
			return source.GroupBy (keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}
		
		public static IObservable<IGroupedObservable<TKey, TElement>> GroupBy<TSource, TKey, TElement> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{
			IDisposable dis = null;
			var sub = new Subject<IGroupedObservable<TKey, TElement>> ();
			var dic = new Dictionary<TKey, GroupedSubject<TKey, TElement>> (comparer);
			dis = source.Subscribe ((s) => {
				try {
					var k = keySelector (s);
					GroupedSubject<TKey, TElement> g;
					if (!dic.TryGetValue (k, out g)) {
						g = new GroupedSubject<TKey, TElement> (k);
						dic.Add (k, g);
						sub.OnNext (g);
					}
					g.OnNext (elementSelector (s));
				} catch (Exception ex) {
					sub.OnError (ex);
					// FIXME: should we handle OnError() in groups too?
				}
			}, () => {
				try {
					foreach (var g in dic.Values)
						g.OnCompleted ();
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
					// FIXME: should we handle OnError() in groups too?
				}
			});
			return new WrappedSubject<IGroupedObservable<TKey, TElement>> (sub, dis);
		}
		
		public static IObservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>> durationSelector)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>> durationSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			Func<IGroupedObservable<TKey, TElement>, IObservable<TDuration>> durationSelector)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			Func<IGroupedObservable<TKey, TElement>, IObservable<TDuration>> durationSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> GroupJoin<TLeft, TRight, TLeftDuration, TRightDuration, TResult> (
			this IObservable<TLeft> left,
			IObservable<TRight> right,
			Func<TLeft, IObservable<TLeftDuration>> leftDurationSelector,
			Func<TRight, IObservable<TRightDuration>> rightDurationSelector,
			Func<TLeft, IObservable<TRight>, TResult> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> IgnoreElements<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<long> Interval (TimeSpan period)
		{
			return Interval (period, Scheduler.ThreadPool);
		}

		/* It Notifies "current count" to *each* observer i.e. this
		   observable holds different count numbers to the observers.
		
		Example of different counts:
		
		var interval = Observable.Interval(TimeSpan.FromMilliseconds(250));
		interval.Subscribe(Console.WriteLine);
		Thread.Sleep(3000);
		interval.Subscribe((s) => Console.WriteLine ("x " + s)); 

		*/
		public static IObservable<long> Interval (
			TimeSpan period,
			IScheduler scheduler)
		{
			return new ColdObservable<long> ((sub) => {
				try {
					long count = 0;
					while (true) {
						Thread.Sleep (period);
						sub.OnNext (count++);
					}
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, scheduler);
		}
		
		public static IObservable<TResult> Join<TLeft, TRight, TLeftDuration, TRightDuration, TResult>(
			this IObservable<TLeft> left,
			IObservable<TRight> right,
			Func<TLeft, IObservable<TLeftDuration>> leftDurationSelector,
			Func<TRight, IObservable<TRightDuration>> rightDurationSelector,
			Func<TLeft, TRight, TResult> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IEnumerable<TSource> Latest<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<long> LongCount<TSource> (this IObservable<TSource> source)
		{
			var sub = new Subject<long> ();
			IDisposable dis = null;
			long count = 0;
			dis = source.Subscribe ((s) => count++, () => { sub.OnNext (count); sub.OnCompleted (); dis.Dispose (); });
			return sub;
		}
		
		public static IObservable<Notification<TSource>> Materialize<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IList<TSource>> MaxBy<TSource, TKey> (this IObservable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.MaxBy (keySelector, Comparer<TKey>.Default);
		}
		
		public static IObservable<IList<TSource>> MaxBy<TSource, TKey> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IComparer<TKey> comparer)
		
		{
			TKey maxk = default (TKey);
			List<TSource> max = new List<TSource> ();
			var sub = new Subject<IList<TSource>> ();
			bool got = false;
			IDisposable dis = null;
			dis = source.Subscribe (
				(s) => {
					if (!got) {
						got = true;
						max.Add (s);
						maxk = keySelector (s);
					} else {
						var k = keySelector (s);
						var cmp = comparer.Compare (maxk, k);
						if (cmp == 0)
							max.Add (s);
						if (cmp < 0) {
							max.Clear ();
							max.Add (s);
							maxk = k;
						}
					}
				},
				() => {
					if (!got)
						sub.OnError (new InvalidOperationException ());
					else {
						sub.OnNext (max);
						sub.OnCompleted ();
					}
				});
			return new WrappedSubject<IList<TSource>> (sub, dis);
		}
		
		public static IObservable<TSource> Merge<TSource> (this IEnumerable<IObservable<TSource>> sources)
		{
			return Merge<TSource> (sources, Scheduler.Immediate);
		}
		
		public static IObservable<TSource> Merge<TSource> (this IObservable<IObservable<TSource>> sources)
		{
			return Merge<TSource> (sources, int.MaxValue);
		}
		
		public static IObservable<TSource> Merge<TSource> (params IObservable<TSource>[] sources)
		{
			return Merge<TSource> (Scheduler.Immediate, sources);
		}
		
		public static IObservable<TSource> Merge<TSource> (
			this IEnumerable<IObservable<TSource>> sources,
			int maxConcurrent)
		{
			return Merge<TSource> (sources, maxConcurrent, Scheduler.Immediate);
		}
		
		public static IObservable<TSource> Merge<TSource> (
			this IEnumerable<IObservable<TSource>> sources,
			IScheduler scheduler)
		{
			return sources.Merge (int.MaxValue, scheduler);
		}
		
		public static IObservable<TSource> Merge<TSource> (
			this IObservable<IObservable<TSource>> sources,
			int maxConcurrent)
		{
			return Merge<TSource> (sources.ToEnumerable (), maxConcurrent, Scheduler.Immediate);
		}
		
		public static IObservable<TSource> Merge<TSource> (
			this IObservable<TSource> first,
			IObservable<TSource> second)
		{
			return Merge<TSource> (first, second, Scheduler.Immediate);
		}
		
		public static IObservable<TSource> Merge<TSource> (
			IScheduler scheduler,
			params IObservable<TSource>[] sources)
		{
			return sources.Merge (scheduler);
		}
		
		public static IObservable<TSource> Merge<TSource> (
			this IEnumerable<IObservable<TSource>> sources,
			int maxConcurrent,
			IScheduler scheduler)
		{
			if (sources == null)
				throw new ArgumentNullException ("sources");
			var sub = new Subject<TSource> ();
			// avoided using "from source in sources select ..." for eager evaluation.
			var dis = new List<IDisposable> ();
			var l = new List<IObservable<TSource>> (sources);
			int index = 0;
			foreach (var source in l) {
				if (index >= maxConcurrent)
					continue;
				Func<IObservable<TSource>, IDisposable> subfunc = null;
				subfunc = ss => ss.Subscribe (Observer.Create<TSource> (s => {
					sub.OnNext (s);
				}, ex => {
					sub.OnError (ex);
				}, () => {
					sub.OnCompleted ();
					if (index < l.Count)
						dis.Add (subfunc (l [index++]));
				}));
				dis.Add (subfunc (source));
			}
			return new WrappedSubject<TSource> (sub, Disposable.Create (() => { foreach (var d in dis) d.Dispose (); sub.Dispose (); }));
		}
		
		public static IObservable<TSource> Merge<TSource> (
			this IObservable<TSource> first,
			IObservable<TSource> second,
			IScheduler scheduler)
		{
			return Merge (scheduler, new IObservable<TSource> [] {first, second});
		}
		
		public static IObservable<IList<TSource>> MinBy<TSource, TKey> (this IObservable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.MinBy (keySelector, Comparer<TKey>.Default);
		}
		
		public static IObservable<IList<TSource>> MinBy<TSource, TKey> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IComparer<TKey> comparer)
		{
			TKey mink = default (TKey);
			List<TSource> min = new List<TSource> ();
			var sub = new Subject<IList<TSource>> ();
			bool got = false;
			IDisposable dis = null;
			dis = source.Subscribe (
				(s) => {
					if (!got) {
						got = true;
						min.Add (s);
						mink = keySelector (s);
					} else {
						var k = keySelector (s);
						var cmp = comparer.Compare (mink, k);
						if (cmp == 0)
							min.Add (s);
						if (cmp > 0) {
							min.Clear ();
							min.Add (s);
							mink = k;
						}
					}
				},
				() => {
					if (!got)
						sub.OnError (new InvalidOperationException ());
					else {
						sub.OnNext (min);
						sub.OnCompleted ();
					}
				});
			return new WrappedSubject<IList<TSource>> (sub, dis);
		}

		public static IEnumerable<TSource> MostRecent<TSource> (
			this IObservable<TSource> source,
			TSource initialValue)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TResult> Multicast<TSource, TResult> (
			this IObservable<TSource> source,
			ISubject<TSource, TResult> subject)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> Multicast<TSource, TIntermediate, TResult> (
			this IObservable<TSource> source,
			Func<ISubject<TSource, TIntermediate>> subjectSelector,
			Func<IObservable<TIntermediate>, IObservable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> Never<TResult> ()
		{
			return new NeverObservable<TResult> ();
		}
		
		public static IEnumerable<TSource> Next<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> ObserveOn<TSource> (
			this IObservable<TSource> source,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> ObserveOn<TSource> (
			this IObservable<TSource> source,
			SynchronizationContext context)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> OfType<TResult> (this IObservable<Object> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> OnErrorResumeNext<TSource> (this IEnumerable<IObservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> OnErrorResumeNext<TSource> (params IObservable<TSource>[] sources)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> OnErrorResumeNext<TSource> (
			this IObservable<TSource> first,
			IObservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TSource> Publish<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TSource> Publish<TSource> (
			this IObservable<TSource> source,
			TSource initialValue)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> Publish<TSource, TResult>(
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> Publish<TSource, TResult>(
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			TSource initialValue)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TSource> PublishLast<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> PublishLast<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
		// see http://leecampbell.blogspot.com/2010/05/rx-part-2-static-and-extension-methods.html
		public static IObservable<int> Range (int start, int count)
		{
			return Range (start, count, Scheduler.CurrentThread);
		}
		
		public static IObservable<int> Range (int start, int count, IScheduler scheduler)
		{
			var sub = new ReplaySubject<int> (scheduler);
			foreach (var i in Enumerable.Range (start, count))
				sub.OnNext (i);
			sub.OnCompleted ();
			return sub;
		}
		
		public static IObservable<TSource> RefCount<TSource> (this IConnectableObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Repeat<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> Repeat<TResult> (TResult value)
		{
			return Repeat<TResult> (value, Scheduler.CurrentThread);
		}
		
		public static IObservable<TResult> Repeat<TResult> (TResult value, int repeatCount)
		{
			return Repeat<TResult> (value, repeatCount, Scheduler.CurrentThread);
		}
		
		public static IObservable<TResult> Repeat<TResult> (TResult value, IScheduler scheduler)
		{
			return Repeat (value, 1, scheduler);
		}
		
		public static IObservable<TResult> Repeat<TResult> (
			TResult value,
			int repeatCount,
			IScheduler scheduler)
		{
			var sub = new ReplaySubject<TResult> (scheduler);
			for (int i = 0; i < repeatCount; i++)
				sub.OnNext (value);
			sub.OnCompleted ();
			return sub;
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source)
		{
			return Replay<TSource, TResult> (source, Scheduler.CurrentThread);
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			int bufferSize)
		{
			return Replay<TSource, TResult> (source, bufferSize, Scheduler.CurrentThread);
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			TimeSpan window)
		{
			return Replay<TSource, TResult> (source, window, Scheduler.CurrentThread);
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			int bufferSize,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			int bufferSize,
			TimeSpan window)
		{
			return Replay<TSource, TResult> (source, bufferSize, window, Scheduler.CurrentThread);
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			TimeSpan window,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			int bufferSize,
			TimeSpan window,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector)
		{
			return Replay<TSource, TResult> (source, selector, Scheduler.CurrentThread);
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize)
		{
			return Replay<TSource, TResult> (source, selector, bufferSize, Scheduler.CurrentThread);
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			TimeSpan window)
		{
			return Replay<TSource, TResult> (source, selector, window, Scheduler.CurrentThread);
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize,
			IScheduler scheduler)
		{
			return Replay<TSource, TResult> (source, selector, bufferSize, Scheduler.CurrentThread);
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize,
			TimeSpan window)
		{
			return Replay<TSource, TResult> (source, selector, bufferSize, window, Scheduler.CurrentThread);
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			TimeSpan window,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize,
			TimeSpan window,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IObservable<TSource> Retry<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Retry<TSource> (
			this IObservable<TSource> source,
			int retryCount)
		{ throw new NotImplementedException (); }
		
		// see http://leecampbell.blogspot.com/2010/05/rx-part-2-static-and-extension-methods.html
		public static IObservable<TResult> Return<TResult> (TResult value)
		{
			return Return (value, Scheduler.Immediate);
		}
		
		public static IObservable<TResult> Return<TResult> (TResult value, IScheduler scheduler)
		{
			var sub = new ReplaySubject<TResult> (scheduler);
			sub.OnNext (value);
			sub.OnCompleted ();
			return sub;
		}
		
		public static IObservable<TSource> Sample<TSource> (
			this IObservable<TSource> source,
			TimeSpan interval)
		{
			return Sample (source, interval, Scheduler.ThreadPool);
		}
		
		public static IObservable<TSource> Sample<TSource> (
			this IObservable<TSource> source,
			TimeSpan interval,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Sample<TSource, TSample> (
			this IObservable<TSource> source,
			IObservable<TSample> sampler)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Scan<TSource> (
			this IObservable<TSource> source,
			Func<TSource, TSource, TSource> accumulator)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TAccumulate> Scan<TSource, TAccumulate> (
			this IObservable<TSource> source,
			TAccumulate seed,
			Func<TAccumulate, TSource, TAccumulate> accumulator)
		{ throw new NotImplementedException (); }

		public static IObservable<TResult> Select<TSource, TResult> (
			this IObservable<TSource> source,
			Func<TSource, TResult> selector)
		{
			return source.Select ((s, i) => selector (s));
		}
		
		public static IObservable<TResult> Select<TSource, TResult> (
			this IObservable<TSource> source,
			Func<TSource, int, TResult> selector)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (selector == null)
				throw new ArgumentNullException ("selector");

			var sub = new Subject<TResult> ();
			IDisposable dis = null;
			int idx = 0;
			dis = source.Subscribe ((s) => {
				try {
					sub.OnNext (selector (s, idx++));
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, () => {
				try {
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				});
			return new WrappedSubject<TResult> (sub, dis);
		}
		
		public static IObservable<TResult> SelectMany<TSource, TResult> (
			this IObservable<TSource> source,
			Func<TSource, IEnumerable<TResult>> selector)
		{
			var sub = new ReplaySubject<TResult> ();
			source.Subscribe ((v) => {
				foreach (var r in selector (v))
					sub.OnNext (r);
				}, (ex) => sub.OnError (ex), () => sub.OnCompleted ());
			return sub;
		}
		
		public static IObservable<TResult> SelectMany<TSource, TResult> (
			this IObservable<TSource> source,
			Func<TSource, IObservable<TResult>> selector)
		{
			var sub = new ReplaySubject<TResult> ();
			var dis = source.Subscribe (
				(v) => { var o = selector (v); o.Subscribe (vv => sub.OnNext (vv)); },
				(ex) => sub.OnError (ex),
				() => sub.OnCompleted ());
			return new WrappedSubject<TResult> (sub, dis);
		}
		
		public static IObservable<TOther> SelectMany<TSource, TOther> (
			this IObservable<TSource> source,
			IObservable<TOther> other)
		{
			var sub = new ReplaySubject<TOther> ();
			int waits = 0;
			var dis = source.Subscribe (
				v => {
					waits++;
					IDisposable d = null;
					d = other.Subscribe (
						vv => sub.OnNext (vv),
						ex => sub.OnError (ex),
						() => { waits--; if (d != null) d.Dispose (); });
				},
				ex => sub.OnError (ex),
				() => { if (waits == 0) sub.OnCompleted (); }
				);
			return new WrappedSubject<TOther> (sub, dis);
		}
		
		public static IObservable<TResult> SelectMany<TSource, TResult> (
			this IObservable<TSource> source,
			Func<TSource, IObservable<TResult>> onNext,
			Func<Exception, IObservable<TResult>> onError,
			Func<IObservable<TResult>> onCompleted)
		{
			var sub = new ReplaySubject<TResult> ();
			var dis = source.Subscribe (
				(v) => { var o = onNext (v); o.Subscribe (vv => sub.OnNext (vv)); },
				(ex) => { var o = onError (ex); o.Subscribe (vv => sub.OnNext (vv)); },
				() => { var o = onCompleted (); o.Subscribe (vv => sub.OnNext (vv)); });
			return new WrappedSubject<TResult> (sub, dis);
		}
		
		public static IObservable<TResult> SelectMany<TSource, TCollection, TResult> (
			this IObservable<TSource> source,
			Func<TSource, IEnumerable<TCollection>> collectionSelector,
			Func<TSource, TCollection, TResult> resultSelector)
		{
			var sub = new Subject<TResult> ();
			var dis = source.Subscribe (Observer.Create<TSource> (
				v => {
					var c = collectionSelector (v);
					foreach (var v2 in c)
						sub.OnNext (resultSelector (v, v2));
				},
				ex => sub.OnError (ex),
				() => sub.OnCompleted ()));
			return new WrappedSubject<TResult> (sub, dis);
		}
		
		public static IObservable<TResult> SelectMany<TSource, TCollection, TResult> (
			this IObservable<TSource> source,
			Func<TSource, IObservable<TCollection>> collectionSelector,
			Func<TSource, TCollection, TResult> resultSelector)
		{
			var sub = new Subject<TResult> ();
			int waits = 0;
			var dis = source.Subscribe (Observer.Create<TSource> (
				v => {
					waits++;
					var cc = collectionSelector (v);
					IDisposable d = null;
					d = cc.Subscribe (
						c => sub.OnNext (resultSelector (v, c)),
						ex => sub.OnError (ex),
						() => { waits--; if (d != null) d.Dispose (); });
				},
				ex => sub.OnError (ex),
				() => { if (waits == 0) sub.OnCompleted (); }));
			return new WrappedSubject<TResult> (sub, dis);
		}
		
		public static IObservable<bool> SequenceEqual<TSource> (
			this IObservable<TSource> first,
			IObservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static IObservable<bool> SequenceEqual<TSource> (
			this IObservable<TSource> first,
			IObservable<TSource> second,
			IEqualityComparer<TSource> comparer)
		{ throw new NotImplementedException (); }
		
		public static TSource Single<TSource> (this IObservable<TSource> source)
		{
			return Single<TSource> (source, s => true);
		}
		
		public static IObservable<TSource> Skip<TSource> (
			this IObservable<TSource> source,
			int count)
		{
			return source.SkipWhile ((s, i) => i < count);
		}
		
		public static IObservable<TSource> SkipLast<TSource> (
			this IObservable<TSource> source,
			int count)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (count < 0)
				throw new ArgumentOutOfRangeException ("count");

			var q = new Queue<TSource> ();
			var sub = new Subject<TSource> ();
			IDisposable dis = null;
			dis = source.Subscribe ((s) => {
				try {
					q.Enqueue (s);
					if (count > 0)
						count--;
					else
						sub.OnNext (q.Dequeue ());
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, () => {
				try {
					q.Clear ();
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				});
			return new WrappedSubject<TSource> (sub, dis);
		}
		
		public static IObservable<TSource> SkipUntil<TSource, TOther> (
			this IObservable<TSource> source,
			IObservable<TOther> other)
		{ throw new NotImplementedException (); }

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
			bool skipDone = false;
			return source.Where ((s, i) => skipDone || (skipDone = !predicate (s, i)));
		}
		
		public static IObservable<Unit> Start (Action action)
		{
			return Start (action, Scheduler.ThreadPool);
		}
		
		public static IObservable<Unit> Start (Action action, IScheduler scheduler)
		{
			return Start<Unit> (() => { action (); return Unit.Default; }, scheduler);
		}
		
		public static IObservable<TSource> Start<TSource> (Func<TSource> function)
		{
			return Start (function, Scheduler.ThreadPool);
		}
		
		public static IObservable<TSource> Start<TSource> (Func<TSource> function, IScheduler scheduler)
		{
			return new HotObservable<TSource> ((sub) => {
				try {
					var ret = function ();
					sub.OnNext (ret);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, scheduler);
		}
		
		public static IObservable<TSource> StartWith<TSource>( 
			this IObservable<TSource> source,
			params TSource [] values)
		{
			return StartWith<TSource> (source, Scheduler.Immediate, values);
		}
		
		public static IObservable<TSource> StartWith<TSource> (
			this IObservable<TSource> source,
			IScheduler scheduler,
			params TSource [] values)
		{
			return new HotObservable<TSource> ((sub) => {
				try {
					foreach (var v in values)
						sub.OnNext (v);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, scheduler);
		}
		
		public static IDisposable Subscribe<TSource> (
			this IEnumerable<TSource> source,
			IObserver<TSource> observer)
		{
			return Subscribe<TSource> (source, observer, Scheduler.CurrentThread);
		}
		
		public static IDisposable Subscribe<TSource> (
			this IEnumerable<TSource> source,
			IObserver<TSource> observer,
			IScheduler scheduler)
		{
			var o = source.ToObservable ();
			var sub = new ReplaySubject<TSource> (scheduler);
			sub.Subscribe (observer);
			var dis = o.Subscribe (Observer.Create<TSource> (s => sub.OnNext (s), ex => sub.OnError (ex), () => sub.OnCompleted ()));
			return Disposable.Create (() => { dis.Dispose (); sub.Dispose (); });
		}

		public static IObservable<TSource> SubscribeOn<TSource> (
			this IObservable<TSource> source,
			IScheduler scheduler)
		{
			return new SchedulerBoundObservable<TSource> (source, scheduler);
		}
		
		public static IObservable<TSource> SubscribeOn<TSource> (
			this IObservable<TSource> source,
			SynchronizationContext context)
		{
			return source.SubscribeOn (new SynchronizationContextScheduler (context));
		}
		
		public static IObservable<decimal> Sum (this IObservable<decimal> source)
		{
			return source.NonNullableSum ((x, y) => x + y);
		}
		
		public static IObservable<TSource> Switch<TSource> (this IObservable<IObservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Synchronize<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Synchronize<TSource> (
			this IObservable<TSource> source,
			Object gate)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> Take<TSource> (
			this IObservable<TSource> source,
			int count)
		{
			return source.Where ((s, i) => i < count);
		}
		
		public static IObservable<TSource> TakeLast<TSource> (
			this IObservable<TSource> source,
			int count)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (count < 0)
				throw new ArgumentOutOfRangeException ("count");

			var q = new Queue<TSource> ();
			var sub = new Subject<TSource> ();
			IDisposable dis = null;
			dis = source.Subscribe ((s) => {
				try {
					q.Enqueue (s);
					if (count > 0)
						count--;
					else
						q.Dequeue ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, () => {
				try {
					while (q.Count > 0)
						sub.OnNext (q.Dequeue ());
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				});
			return new WrappedSubject<TSource> (sub, dis);
		}
		
		public static IObservable<TSource> TakeUntil<TSource, TOther> (
			this IObservable<TSource> source,
			IObservable<TOther> other)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> TakeWhile<TSource> (
			this IObservable<TSource> source,
			Func<TSource, bool> predicate)
		{
			return source.TakeWhile ((s, i) => predicate (s));
		}
		
		public static IObservable<TSource> TakeWhile<TSource> (
			this IObservable<TSource> source,
			Func<TSource, int, bool> predicate)
		{
			bool stopped = false;
			return source.Where ((s, i) => !stopped && (stopped = !predicate (s, i)));
		}
		
		public static Plan<TResult> Then<TSource, TResult> (
			this IObservable<TSource> source,
			Func<TSource, TResult> selector)
		{
			return new Plan<TSource, TResult> (new Pattern<TSource> (source), selector);
		}
		
		public static IObservable<TSource> Throttle<TSource> (
			this IObservable<TSource> source,
			TimeSpan dueTime)
		{
			return Throttle (source, dueTime, Scheduler.ThreadPool);
		}
		
		public static IObservable<TSource> Throttle<TSource> (
			this IObservable<TSource> source,
			TimeSpan dueTime,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		// see http://leecampbell.blogspot.com/2010/05/rx-part-2-static-and-extension-methods.html
		public static IObservable<TResult> Throw<TResult> (Exception exception)
		{
			return Throw<TResult> (exception, Scheduler.Immediate);
		}
		
		public static IObservable<TResult> Throw<TResult> (
			Exception exception,
			IScheduler scheduler)
		{
			var sub = new ReplaySubject<TResult> (scheduler);
			sub.OnError (exception);
			return sub;
		}
		
		public static IObservable<TimeInterval<TSource>> TimeInterval<TSource> (this IObservable<TSource> source)
		{
			return TimeInterval (source, Scheduler.ThreadPool);
		}
		
		public static IObservable<TimeInterval<TSource>> TimeInterval<TSource> (
			this IObservable<TSource> source,
			IScheduler scheduler)
		{
			var sub = new Subject<TimeInterval<TSource>> ();
			DateTimeOffset last = scheduler.Now;
			var dis = source.Subscribe (Observer.Create<TSource> (
				v => { sub.OnNext (new TimeInterval<TSource> (v, Scheduler.Normalize (scheduler.Now - last))); last = scheduler.Now; },
				ex => sub.OnError (ex),
				() => sub.OnCompleted ()));
			return new WrappedSubject<TimeInterval<TSource>> (sub, dis);
		}
		
		public static IObservable<TSource> Timeout<TSource>(
			this IObservable<TSource> source,
			DateTimeOffset dueTime)
		{
			return Timeout<TSource> (source, dueTime, Scheduler.ThreadPool);
		}
		
		public static IObservable<TSource> Timeout<TSource>(
			this IObservable<TSource> source,
			TimeSpan dueTime)
		{
			return Timeout<TSource> (source, dueTime, Scheduler.ThreadPool);
		}
		
		public static IObservable<TSource> Timeout<TSource>(
			this IObservable<TSource> source,
			DateTimeOffset dueTime,
			IObservable<TSource> other)
		{
			return source.Timeout (dueTime, other, Scheduler.ThreadPool);
		}
		
		public static IObservable<TSource> Timeout<TSource>(
			this IObservable<TSource> source,
			TimeSpan dueTime,
			IObservable<TSource> other)
		{
			return Timeout<TSource> (source, dueTime, other, Scheduler.ThreadPool);
		}
		
		public static IObservable<TSource> Timeout<TSource>(
			this IObservable<TSource> source,
			DateTimeOffset dueTime,
			IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			return Timeout<TSource> (source, dueTime - scheduler.Now, scheduler);
		}
		
		public static IObservable<TSource> Timeout<TSource>(
			this IObservable<TSource> source,
			TimeSpan dueTime,
			IScheduler scheduler)
		{
			return TimeoutInternal<TSource> (source, dueTime, null, scheduler);
		}
		
		public static IObservable<TSource> Timeout<TSource>(
			this IObservable<TSource> source,
			DateTimeOffset dueTime,
			IObservable<TSource> other,
			IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			return Timeout<TSource> (source, dueTime - scheduler.Now, other, scheduler);
		}
		
		public static IObservable<TSource> Timeout<TSource>(
			this IObservable<TSource> source,
			TimeSpan dueTime,
			IObservable<TSource> other,
			IScheduler scheduler)
		{
			if (other == null)
				throw new ArgumentNullException ("other");
			return TimeoutInternal<TSource> (source, dueTime, other, scheduler);
		}
		
		static IObservable<TSource> TimeoutInternal<TSource>(
			this IObservable<TSource> source,
			TimeSpan dueTime,
			IObservable<TSource> other,
			IScheduler scheduler)
		{
			if (source == null)
				throw new ArgumentNullException ("other");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			var sub = new Subject<TSource> ();
			var wait = new ManualResetEvent (false);
			var dis = source.Subscribe (s => sub.OnNext (s), ex => sub.OnError (ex), () => wait.Set ());
			var sdis = scheduler.Schedule (() => {
				if (wait.WaitOne (Scheduler.Normalize (dueTime)))
					sub.OnCompleted ();
				else {
					if (other != null)
						other.Subscribe (s => sub.OnNext (s), ex => sub.OnError (ex), () => sub.OnCompleted ());
					else
						sub.OnError (new TimeoutException ());
				}
				wait.Dispose ();
			});
			return new WrappedSubject<TSource> (sub, new CompositeDisposable (dis, sdis));
		}
		
		public static IObservable<long> Timer (
			DateTimeOffset dueTime)
		{
			return Timer (dueTime, Scheduler.ThreadPool);
		}
		
		public static IObservable<long> Timer (
			TimeSpan dueTime)
		{
			return Timer (dueTime, Scheduler.ThreadPool);
		}
		
		public static IObservable<long> Timer (
			DateTimeOffset dueTime,
			IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			return Timer (dueTime - scheduler.Now, scheduler);
		}
		
		public static IObservable<long> Timer (
			TimeSpan dueTime,
			IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			return new ColdObservable<long> ((sub) => {
				try {
					Thread.Sleep (Scheduler.Normalize (dueTime));
					sub.OnNext (0);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, scheduler);
		}
		
		public static IObservable<long> Timer (
			DateTimeOffset dueTime,
			TimeSpan period)
		{
			return Timer (dueTime, period, Scheduler.ThreadPool);
		}
		
		public static IObservable<long> Timer (
			TimeSpan dueTime,
			TimeSpan period)
		{
			return Timer (dueTime, period, Scheduler.ThreadPool);
		}
		
		public static IObservable<long> Timer (
			DateTimeOffset dueTime,
			TimeSpan period,
			IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			return Timer (dueTime - Scheduler.Now, period, scheduler);
		}
		
		public static IObservable<long> Timer (
			TimeSpan dueTime,
			TimeSpan period,
			IScheduler scheduler)
		{
			var sub = new Subject<long> ();
			var t = Timer (dueTime, scheduler);
			IDisposable di = null;
			var dt = t.Subscribe (Observer.Create<long> ((v) => {}, () => {
				sub.OnNext (0);
				var i = Interval (period, scheduler);
				di = i.Subscribe ((v) => sub.OnNext (v + 1));
			}));
			return new WrappedSubject<long> (sub, Disposable.Create (() => { dt.Dispose (); if (di != null) di.Dispose (); }));
		}
		
		public static IObservable<Timestamped<TSource>> Timestamp<TSource> (this IObservable<TSource> source)
		{
			return Timestamp (source, Scheduler.ThreadPool);
		}
		
		public static IObservable<Timestamped<TSource>> Timestamp<TSource> (this IObservable<TSource> source, IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			var sub = new Subject<Timestamped<TSource>> ();
			var dis = source.Subscribe (v => sub.OnNext (new Timestamped<TSource> (v, scheduler.Now)), ex => sub.OnError (ex), () => sub.OnCompleted ());
			return new WrappedSubject<Timestamped<TSource>> (sub, dis);
		}
		
		public static IObservable<TSource[]> ToArray<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static Func<IObservable<Unit>> ToAsync (this Action action)
		{
			return ToAsync (action, Scheduler.ThreadPool);
		}
		
		public static Func<IObservable<Unit>> ToAsync (this Action action, IScheduler scheduler)
		{
			return () => Start (action, scheduler);
		}
		
		public static Func<TSource, IObservable<Unit>> ToAsync<TSource> (this Action<TSource> action)
		{
			return action.ToAsync (Scheduler.ThreadPool);
		}
		
		public static Func<IObservable<TResult>> ToAsync<TResult> (this Func<TResult> function)
		{
			return function.ToAsync (Scheduler.ThreadPool);
		}
		
		public static Func<TSource, IObservable<Unit>> ToAsync<TSource> (this Action<TSource> action, IScheduler scheduler)
		{
			return (s) => Start (() => action (s), scheduler);
		}
		
		public static Func<IObservable<TResult>> ToAsync<TResult> (this Func<TResult> function, IScheduler scheduler)
		{
			return () => Start (function, scheduler);
		}
		
		public static Func<T, IObservable<TResult>> ToAsync<T, TResult> (this Func<T, TResult> function)
		{
			return function.ToAsync (Scheduler.ThreadPool);
		}
		
		public static Func<T, IObservable<TResult>> ToAsync<T, TResult> (this Func<T, TResult> function, IScheduler scheduler)
		{
			return (t) => Start (() => function (t), scheduler);
		}
		
		public static IObservable<IDictionary<TKey, TSource>> ToDictionary<TSource, TKey> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector)
		{
			return source.ToDictionary (keySelector, EqualityComparer<TKey>.Default);
		}
		
		public static IObservable<IDictionary<TKey, TSource>> ToDictionary<TSource, TKey>(
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{
			return ToDictionary<TSource, TKey, TSource> (source, keySelector, s => s, comparer);
		}
		
		public static IObservable<IDictionary<TKey, TElement>> ToDictionary<TSource, TKey, TElement>(
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector)
		{
			return source.ToDictionary (keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}
		
		public static IObservable<IDictionary<TKey, TElement>> ToDictionary<TSource, TKey, TElement>(
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{
			var sub = new Subject<IDictionary<TKey, TElement>> ();
			var dic = new Dictionary<TKey, TElement> (comparer);
			var dis = source.Subscribe (Observer.Create<TSource> (
				v => dic.Add (keySelector (v), elementSelector (v)),
				ex => sub.OnError (ex),
				() => { sub.OnNext (dic); sub.OnCompleted (); }));
			return new WrappedSubject<IDictionary<TKey, TElement>> (sub, dis);
		}
		
		public static IEnumerable<TSource> ToEnumerable<TSource> (this IObservable<TSource> source)
		{
			var l = new BlockingCollection<TSource> ();
			source.Subscribe (Observer.Create<TSource> (
				v => { l.Add (v); },
				() => { l.CompleteAdding (); }
				));
			foreach (var s in l)
				yield return s;
		}
		
		public static IObservable<IList<TSource>> ToList<TSource> (this IObservable<TSource> source)
		{
			var sub = new Subject<IList<TSource>> ();
			var l = new List<TSource> ();
			var dis = source.Subscribe (Observer.Create<TSource> (
				v => l.Add (v),
				ex => sub.OnError (ex),
				() => { sub.OnNext (l); sub.OnCompleted (); }));
			return new WrappedSubject<IList<TSource>> (sub, dis);
		}
		
		public static IObservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IObservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IObservable<TSource> source,
			Func<TSource, TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IObservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IObservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector)
		{ throw new NotImplementedException (); }
		
		public static IObservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }

		public static IObservable<TSource> ToObservable<TSource> (this IEnumerable<TSource> source)
		{
			return ToObservable<TSource> (source, Scheduler.CurrentThread);
		}
		
		public static IObservable<TSource> ToObservable<TSource> (
			this IEnumerable<TSource> source,
			IScheduler scheduler)
		{
			return new ColdObservable<TSource> ((sub) => {
				try {
					foreach (var s in source)
						sub.OnNext (s);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, scheduler);
		}
		
		public static IObservable<TSource> Using<TSource, TResource> (
			Func<TResource> resourceFactory,
			Func<TResource, IObservable<TSource>> observableFactory)
			where TResource : IDisposable
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> When<TResult> (this IEnumerable<Plan<TResult>> plans)
		{
			return When (plans.ToArray ());
		}
		
		public static IObservable<TResult> When<TResult> (params Plan<TResult>[] plans)
		{
			return Merge (plans.Select (p => p.AsObservable ()));
		}
		
		public static IObservable<TSource> Where<TSource> (
			this IObservable<TSource> source,
			Func<TSource, bool> predicate)
		{
			return source.Where ((s, i) => predicate (s));
		}
		
		public static IObservable<TSource> Where<TSource>(
			this IObservable<TSource> source,
			Func<TSource, int, bool> predicate)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (predicate == null)
				throw new ArgumentNullException ("predicate");

			var sub = new Subject<TSource> ();
			IDisposable dis = null;
			int idx = 0;
			dis = source.Subscribe ((s) => {
				try {
					if (predicate (s, idx++))
						sub.OnNext (s);
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, () => {
				try {
					dis.Dispose ();
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				});
			return new WrappedSubject<TSource> (sub, dis);
		}
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			int count,
			int skip)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			int count,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IObservable<IObservable<TSource>> Window<TSource, TWindowClosing> (
			this IObservable<TSource> source,
			Func<IObservable<TWindowClosing>> windowClosingSelector)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IObservable<TSource>> Window<TSource, TWindowOpening, TWindowClosing> (
			this IObservable<TSource> source,
			IObservable<TWindowOpening> windowOpenings,
			Func<TWindowOpening, IObservable<TWindowClosing>> windowClosingSelector)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> Zip<TFirst, TSecond, TResult> (
			this IObservable<TFirst> first,
			IEnumerable<TSecond> second,
			Func<TFirst, TSecond, TResult> resultSelector)
		{
			return Zip (first, second.ToObservable (), resultSelector);
		}
		
		public static IObservable<TResult> Zip<TFirst, TSecond, TResult>(
			this IObservable<TFirst> first,
			IObservable<TSecond> second,
			Func<TFirst, TSecond, TResult> resultSelector)
		{
			return When (first.And (second).Then (resultSelector));
		}
	}
}
