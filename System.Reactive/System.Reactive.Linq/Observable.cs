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
		static IScheduler DefaultColdScheduler {
			get { return Scheduler.CurrentThread; }
		}
		
		public static IObservable<TSource> Aggregate<TSource> (
			this IObservable<TSource> source,
			Func<TSource, TSource, TSource> accumulator)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (accumulator == null)
				throw new ArgumentNullException ("accumulator");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			bool has_agg = false;
			TSource result = default (TSource);
			var dis = source.Subscribe (v => {
				if (!has_agg)
					result = v;
				else
					result = accumulator (result, v);
				has_agg = true;
			}, ex => sub.OnError (ex), () => {
				if (has_agg) {
					sub.OnNext (result);
					sub.OnCompleted ();
				}
				else
					sub.OnError (new InvalidOperationException ("There was no value to aggregate."));
			});
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TAccumulate> Aggregate<TSource, TAccumulate> (
			this IObservable<TSource> source,
			TAccumulate seed,
			Func<TAccumulate, TSource, TAccumulate> accumulator)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (accumulator == null)
				throw new ArgumentNullException ("accumulator");

			// note the results difference between those Aggregate() overloads...
			return new ColdObservableEach<TAccumulate> (sub => {
			// ----
			TAccumulate result = seed;
			var dis = source.Subscribe (v => result = accumulator (result, v), ex => sub.OnError (ex), () => { sub.OnNext (result); sub.OnCompleted (); });
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<bool> All<TSource> (
			this IObservable<TSource> source,
			Func<TSource, bool> predicate)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (predicate == null)
				throw new ArgumentNullException ("predicate");

			return new ColdObservableEach<bool> (sub => {
			// ----
			IDisposable dis = null;
			bool ret = true;
			bool hasValue = false;
			dis = source.Subscribe ((s) => {
				hasValue = true;
				ret &= predicate (s);
			}, () => {
				sub.OnNext (hasValue && ret);
				sub.OnCompleted ();
			});
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> Amb<TSource> (this IEnumerable<IObservable<TSource>> sources)
		{
			if (sources == null)
				throw new ArgumentNullException ("sources");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			IObservable<TSource> first = null;

			// avoided using "from source in sources select ..." for eager evaluation.
			var dis = new CompositeDisposable ();
			foreach (var source in sources) {
				dis.Add (source.Subscribe (
				s => {
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
				}));
			}
			return dis;
			// ----
			}, DefaultColdScheduler);
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
			if (left == null)
				throw new ArgumentNullException ("left");
			if (right == null)
				throw new ArgumentNullException ("right");

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

			return new ColdObservableEach<bool> (sub => {
			// ----
			var dis = new SingleAssignmentDisposable ();
			bool hit = false;
			dis.Disposable = source.Subscribe ((s) => {
				if (predicate (s)) {
					hit = true;
					sub.OnNext (true);
					sub.OnCompleted ();
					dis.Dispose ();
				}
			}, () => {
				if (!hit) {
					sub.OnNext (false);
					sub.OnCompleted ();
					dis.Dispose ();
				}
			});
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		class WrappedObservable<T> : IObservable<T>
		{
			IObservable<T> source;
			
			public WrappedObservable (IObservable<T> source)
			{
				this.source = source;
			}
			
			public IDisposable Subscribe (IObserver<T> observer)
			{
				return source.Subscribe (observer);
			}
		}
		
		public static IObservable<TSource> AsObservable<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new WrappedObservable<TSource> (source);
		}
		
		public static IObservable<TResult> Cast<TResult> (this IObservable<Object> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return source.Select (v => (TResult) v);
		}

		public static IObservable<TSource> Catch<TSource> (this IEnumerable<IObservable<TSource>> sources)
		{
			if (sources == null)
				throw new ArgumentNullException ("sources");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			var e = sources.GetEnumerator ();
			if (!e.MoveNext ()) {
				sub.OnCompleted ();
				return Disposable.Empty;
			}
			
			var dis = new CompositeDisposable ();
			Action subact = null;
			subact = () => dis.Add (e.Current.Subscribe (v => sub.OnNext (v), ex => { if (e.MoveNext ()) subact (); else sub.OnError (ex); }, () => sub.OnCompleted ()));
			subact ();
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> Catch<TSource> (params IObservable<TSource> [] sources)
		{
			return Catch<TSource> ((IEnumerable<IObservable<TSource>>) sources);
		}
		
		public static IObservable<TSource> Catch<TSource, TException> (
			this IObservable<TSource> source,
			Func<TException, IObservable<TSource>> handler)
			where TException : Exception
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (handler == null)
				throw new ArgumentNullException ("handler");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			var dis = source.Subscribe (v => sub.OnNext (v), ex => { var eex = ex as TException; if (eex != null) foreach (var vv in handler (eex).ToEnumerable ()) sub.OnNext (vv); else sub.OnError (ex); }, () => sub.OnCompleted ());
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> Catch<TSource> (
			this IObservable<TSource> first,
			IObservable<TSource> second)
		{
			if (first == null)
				throw new ArgumentNullException ("first");
			if (second == null)
				throw new ArgumentNullException ("second");

			return Catch (new IObservable<TSource> [] {first, second});
		}
		
		public static IObservable<TResult> CombineLatest<TFirst, TSecond, TResult> (
			this IObservable<TFirst> first,
			IObservable<TSecond> second,
			Func<TFirst, TSecond, TResult> resultSelector)
		{
			if (first == null)
				throw new ArgumentNullException ("first");
			if (second == null)
				throw new ArgumentNullException ("second");

			return new ColdObservableEach<TResult> (sub => {
			// ----
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
			return new CompositeDisposable (dis1, dis2);
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> Concat<TSource> (this IEnumerable<IObservable<TSource>> sources)
		{
			if (sources == null)
				throw new ArgumentNullException ("sources");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			// FIXME: SerialDisposable might be still applicable. I historically switched between those disposables. Composite is safer but inefficient.
			var dis = new CompositeDisposable ();
			StartConcat (sources.GetEnumerator (), sub, dis);
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		static bool StartConcat<TSource> (IEnumerator<IObservable<TSource>> sources, ISubject<TSource> sub, CompositeDisposable dis)
		{
			if (!sources.MoveNext ())
				return true;
			dis.Add (sources.Current.Subscribe (v => sub.OnNext (v), ex => sub.OnError (ex), () => { if (StartConcat (sources, sub, dis)) sub.OnCompleted (); }));
			return false;
		}
		
		public static IObservable<TSource> Concat<TSource> (this IObservable<IObservable<TSource>> sources)
		{
			if (sources == null)
				throw new ArgumentNullException ("sources");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			var dis = new CompositeDisposable ();
			// FIXME: I want to switch to SerialDisposable if it is safe to use.
			// Looks like OnCompleted() is not processed before the subscription is being disposed.
			//var sdis = new SerialDisposable ();
			//dis.Add (sdis);
			var l = new Queue<IObservable<TSource>> ();
			bool busy = false;
			bool quit = false;
			dis.Add (sources.Subscribe (source => {
				if (quit)
					return;
				if (busy)
					l.Enqueue (source);
				else {
					busy = true;
					IObserver<TSource> o = null;
					o = Observer.Create<TSource> (v => sub.OnNext (v), ex => { quit = true; throw ex; }, () => {
						if (l.Count > 0) {
							var next = l.Dequeue ();
							dis.Add (next.Subscribe (o));
						} else {
							busy = false;
							if (quit)
								sub.OnCompleted ();
						}
					});
					dis.Add (source.Subscribe (o));
				}
			}, () => { quit = true; if (!busy) sub.OnCompleted (); }));
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> Concat<TSource> (params IObservable<TSource> [] sources)
		{
			if (sources == null)
				throw new ArgumentNullException ("sources");

			return sources.AsEnumerable ().Concat ();
		}
		
		public static IObservable<TSource> Concat<TSource> (this IObservable<TSource> first, IObservable<TSource> second)
		{
			if (first == null)
				throw new ArgumentNullException ("first");
			if (second == null)
				throw new ArgumentNullException ("second");

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
			if (source == null)
				throw new ArgumentNullException ("source");

			return new ColdObservableEach<int> (sub => {
			// ----
			int count = 0;
			var dis = source.Subscribe ((s) => count++, () => { sub.OnNext (count); sub.OnCompleted (); });
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> Create<TSource> (Func<IObserver<TSource>, Action> subscribe)
		{
			return Create<TSource> (observer => Disposable.Create (subscribe (observer)));
		}
		
		public static IObservable<TSource> Create<TSource> (Func<IObserver<TSource>, IDisposable> subscribe)
		{
			if (subscribe == null)
				throw new ArgumentNullException ("subscribe");


			return new SimpleDisposableObservable<TSource> (subscribe);
		}
		
		public static IObservable<TSource> DefaultIfEmpty<TSource> (this IObservable<TSource> source)
		{
			return source.DefaultIfEmpty (default (TSource));
		}
		
		public static IObservable<TSource> DefaultIfEmpty<TSource> (this IObservable<TSource> source, TSource defaultValue)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			bool hadValue = false;
			var dis = source.Subscribe (v => { hadValue = true; sub.OnNext (v); }, ex => sub.OnError (ex), () => { if (!hadValue) sub.OnNext (defaultValue); sub.OnCompleted (); });
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		class DeferredObservable<TValue> : IObservable<TValue>
		{
			Func<IObservable<TValue>> factory;
			
			public DeferredObservable (Func<IObservable<TValue>> factory)
			{
				this.factory = factory;
			}

			public IDisposable Subscribe (IObserver<TValue> observer)
			{
				var o = factory ();
				return o.Subscribe (observer);
			}
		}
		
		public static IObservable<TValue> Defer<TValue> (Func<IObservable<TValue>> observableFactory)
		{
			if (observableFactory == null)
				throw new ArgumentNullException ("observableFactory");

			return new DeferredObservable<TValue> (observableFactory);
		}
		
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
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			return Delay<TSource> (source, dueTime - scheduler.Now, scheduler);
		}
		
		public static IObservable<TSource> Delay<TSource> (
			this IObservable<TSource> source,
			TimeSpan dueTime,
			IScheduler scheduler)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			int count = 0;
			bool done = false;
			return source.Subscribe (v => {
				count++;
				var d = new SingleAssignmentDisposable ();
				d.Disposable = scheduler.Schedule (Scheduler.Normalize (dueTime), () => {
					if (!d.IsDisposed)
						sub.OnNext (v);
					if (--count == 0 && done)
						sub.OnCompleted ();
					d.Dispose ();
				});
			}, () => {
				var d = new SingleAssignmentDisposable ();
				d.Disposable = scheduler.Schedule (Scheduler.Normalize (dueTime), () => {
					done = true;
					if (count == 0)
						sub.OnCompleted ();
					d.Dispose ();
				});
			});
			// ----
			}, scheduler);
		}
		
		public static IObservable<TSource> Dematerialize<TSource> (this IObservable<Notification<TSource>> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			return source.Subscribe (
				n => {
					switch (n.Kind) {
					case NotificationKind.OnNext:
						sub.OnNext (n.Value);
						break;
					case NotificationKind.OnError:
						sub.OnError (n.Exception);
						break;
					case NotificationKind.OnCompleted:
						sub.OnCompleted ();
						break;
					}
				});
			// ----
			}, DefaultColdScheduler);
		}
		
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

			return new ColdObservableEach<TSource> (sub => {
			// ----
			var keys = new HashSet<TKey> (comparer);
			return source.Subscribe (
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
				});
			// ----
			}, DefaultColdScheduler);
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

			return new ColdObservableEach<TSource> (sub => {
			// ----
			bool hit = false;
			TKey prev = default (TKey);
			return source.Subscribe (s => {
				var k = keySelector (s);
				if (!hit) {
					hit = true;
					prev = k;
					sub.OnNext (s);
				} else if (!comparer.Equals (k, prev)) {
					prev = k;
					sub.OnNext (s);
				}
			}, () => sub.OnCompleted ());
			// ----
			}, DefaultColdScheduler);
		}

		public static IObservable<TSource> Do<TSource> (
			this IObservable<TSource> source,
			Action<TSource> onNext)
		{
			return source.Do (Observer.Create<TSource> (onNext));
		}

		public static IObservable<TSource> Do<TSource> (
			this IObservable<TSource> source,
			IObserver<TSource> observer)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (observer == null)
				throw new ArgumentNullException ("observer");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			var dis = source.Subscribe (v => sub.OnNext (v), ex => sub.OnError (ex), () => sub.OnCompleted ());
			sub.Subscribe (observer);
			return dis;
			// ----
			}, DefaultColdScheduler);
		}

		public static IObservable<TSource> Do<TSource> (
			this IObservable<TSource> source,
			Action<TSource> onNext,
			Action<Exception> onError)
		{
			return source.Do (Observer.Create<TSource> (onNext, onError));
		}

		public static IObservable<TSource> Do<TSource> (
			this IObservable<TSource> source,
			Action<TSource> onNext,
			Action onCompleted)
		{
			return source.Do (Observer.Create<TSource> (onNext, onCompleted));
		}

		public static IObservable<TSource> Do<TSource> (
			this IObservable<TSource> source,
			Action<TSource> onNext,
			Action<Exception> onError,
			Action onCompleted)
		{
			return source.Do (Observer.Create<TSource> (onNext, onError, onCompleted));
		}
		
		public static IObservable<TSource> ElementAt<TSource> (this IObservable<TSource> source, int index)
		{
			return ElementAtOrDefault<TSource> (source, index, true);
		}
		
		public static IObservable<TSource> ElementAtOrDefault<TSource> (this IObservable<TSource> source, int index)
		{
			return ElementAtOrDefault<TSource> (source, index, false);
		}
		
		static IObservable<TSource> ElementAtOrDefault<TSource> (this IObservable<TSource> source, int index, bool throwError)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			long i = 0;
			return source.Subscribe (
				v => { if (i++ == index) sub.OnNext (v); sub.OnCompleted (); },
				ex => sub.OnError (ex),
				() => {
					if (i < index) {
						if (throwError)
							sub.OnError (new IndexOutOfRangeException ());
						else {
							sub.OnNext (default (TSource));
							sub.OnCompleted ();
						}
					}
				});
			// ----
			}, DefaultColdScheduler);
		}
		
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
		{
			return source.Do (v => {}, ex => finallyAction (), () => finallyAction ());
		}
		
		public static Func<IObservable<Unit>> FromAsyncPattern (
			Func<AsyncCallback, Object, IAsyncResult> begin,
			Action<IAsyncResult> end)
		{
			if (begin == null)
				throw new ArgumentNullException ("begin");
			if (end == null)
				throw new ArgumentNullException ("end");

			var sub = new Subject<Unit> ();
			return () => {
				begin ((res) => {
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
			if (begin == null)
				throw new ArgumentNullException ("begin");
			if (end == null)
				throw new ArgumentNullException ("end");

			var sub = new Subject<TResult> ();
			return () => {
				begin ((res) => {
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

			var dis = new CompositeDisposable ();
			return new ColdObservableEach<TResult> (sub => {
			// ----
			for (var i = initialState; condition (i); i = iterate (i)) {
				var sdis = new SingleAssignmentDisposable ();
				dis.Add (sdis);
				sdis.Disposable = scheduler.Schedule (timeSelector (i), () => { if (!sdis.IsDisposed) sub.OnNext (resultSelector (i)); });
			}
			sub.OnCompleted ();
			return dis;
			// ----
			}, scheduler);
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
			if (source == null)
				throw new ArgumentNullException ("source");
			if (keySelector == null)
				throw new ArgumentNullException ("keySelector");
			if (elementSelector == null)
				throw new ArgumentNullException ("elementSelector");
			if (comparer == null)
				throw new ArgumentNullException ("comparer");

			return new ColdObservableEach<IGroupedObservable<TKey, TElement>> (sub => {
			// ----
			var dic = new Dictionary<TKey, GroupedSubject<TKey, TElement>> (comparer);
			return source.Subscribe ((s) => {
				var k = keySelector (s);
				GroupedSubject<TKey, TElement> g;
				if (!dic.TryGetValue (k, out g)) {
					g = new GroupedSubject<TKey, TElement> (k);
					dic.Add (k, g);
					sub.OnNext (g);
				}
				g.OnNext (elementSelector (s));
			}, () => {
				foreach (var g in dic.Values)
					g.OnCompleted ();
				sub.OnCompleted ();
			});
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>> durationSelector)
		{
			return source.GroupByUntil (keySelector, durationSelector, EqualityComparer<TKey>.Default);
		}
		
		public static IObservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>> durationSelector,
			IEqualityComparer<TKey> comparer)
		{
			return GroupByUntil<TSource, TKey, TSource, TDuration> (source, keySelector, s => s, durationSelector, comparer);
		}
		
		public static IObservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			Func<IGroupedObservable<TKey, TElement>, IObservable<TDuration>> durationSelector)
		{
			return source.GroupByUntil (keySelector, elementSelector, durationSelector, EqualityComparer<TKey>.Default);
		}
		
		public static IObservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			Func<IGroupedObservable<TKey, TElement>, IObservable<TDuration>> durationSelector,
			IEqualityComparer<TKey> comparer)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (keySelector == null)
				throw new ArgumentNullException ("keySelector");
			if (elementSelector == null)
				throw new ArgumentNullException ("elementSelector");
			if (durationSelector == null)
				throw new ArgumentNullException ("durationSelector");
			if (comparer == null)
				throw new ArgumentNullException ("comparer");

			return new ColdObservableEach<IGroupedObservable<TKey, TElement>> (sub => {
			// ----
			var dic = new Dictionary<TKey, GroupedSubject<TKey, TElement>> (comparer);
			var dis = new CompositeDisposable ();
			dis.Add (source.Subscribe (Observer.Create<TSource> ((TSource s) => {
				var k = keySelector (s);
				GroupedSubject<TKey, TElement> g;
				if (!dic.TryGetValue (k, out g)) {
					g = new GroupedSubject<TKey, TElement> (k);
					var dur = durationSelector (g);
					var ddis = new SingleAssignmentDisposable ();
					// after the duration, it removes the GroupedSubject from the dictionary by key.
					Action cleanup = () => { ddis.Dispose (); dic.Remove (k); };
					ddis.Disposable = dur.Subscribe (Observer.Create<TDuration> ((TDuration dummy) => { g.OnCompleted (); cleanup (); }, ex => { g.OnError (ex); cleanup (); }, () => cleanup ()));
					dis.Add (ddis); // dispoe this by parent (in case dur submits events infinitely and ddis itself is never disposed by the cycle above...)
					dic.Add (k, g);
					sub.OnNext (g);
				}
				g.OnNext (elementSelector (s));
			}, () => {
				// note that those groups that received expiration from durationSelector are already invoked OnCompleted().
				foreach (var g in dic.Values)
					g.OnCompleted ();
				sub.OnCompleted ();
			})));
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TResult> GroupJoin<TLeft, TRight, TLeftDuration, TRightDuration, TResult> (
			this IObservable<TLeft> left,
			IObservable<TRight> right,
			Func<TLeft, IObservable<TLeftDuration>> leftDurationSelector,
			Func<TRight, IObservable<TRightDuration>> rightDurationSelector,
			Func<TLeft, IObservable<TRight>, TResult> resultSelector)
		{
			if (left == null)
				throw new ArgumentNullException ("left");
			if (right == null)
				throw new ArgumentNullException ("right");
			if (leftDurationSelector == null)
				throw new ArgumentNullException ("leftDurationSelector");
			if (rightDurationSelector == null)
				throw new ArgumentNullException ("rightDurationSelector");
			if (resultSelector == null)
				throw new ArgumentNullException ("resultSelector");

			/*
			
			- When subscribed, left and right immediately start.
			- When left observed a next value, it gets leftDuration observable for the value, and starts it.
			- Results with the left value are submitted until leftDuration observed a next value or completion.
			- Set an empty subject as the *active* right subject.
			- When right observed a next value,
			  - if there is no *active* rightDuration observable, then
			    - it gets rightDuration observable for the value.
			    - Then it creates an *active* right subject to store all right values while the rightDuration receives a next event or completes.
			    - The duration observable starts observing.
			  - At anytime, the right value is sent to the *active* right subject.
			- Right value observable is submitted with each valid left value.
			  - While the right subject is unique during one duration window (i.e. shared by many left values), each left value has to terminate the right subject in each window. To achieve that, we use Merge() with OnCompleted event for right observable.
			- Results with the right subject are submitted until rightDuration observed a next value or completion.
			
			Additional notes:
			- When left has completed, it sends OnCompleted event to the result (of the group join).
			- On the other hand, right completion does not result in OnCompleted on the result.
			  - The result keeps receiving OnNext with *empty* right observables whenever left value arrives.
			
			*/

			return new ColdObservableEach<TResult> (sub => {
			// ----
			var dis = new CompositeDisposable ();
			var lefts = new List<TLeft> ();
			var rightSubs = new List<ISubject<TRight>> ();
			var rightVals = new List<TRight> ();

			dis.Add (right.Subscribe (Observer.Create<TRight> (v => {
				var rightDuration = rightDurationSelector (v);
				var rddis = new SingleAssignmentDisposable ();
				dis.Add (rddis);
				Action disposeRDur = () => { rightVals.Remove (v); rddis.Dispose (); dis.Remove (rddis); }; // rightDuration is one-shot observable.
				rddis.Disposable = rightDuration.Subscribe (Observer.Create<TRightDuration> (dummy => disposeRDur (), disposeRDur));
				
				rightVals.Add (v);
				lock (rightSubs)
					foreach (var rsub in rightSubs)
						rsub.OnNext (v);
			})));

			var ldis = left.Subscribe (Observer.Create<TLeft> (v => {
				ISubject<TRight> rsub = new ReplaySubject<TRight> ();
				rightSubs.Add (rsub);
				lefts.Add (v);
				lock (rightVals)
					foreach (var r in rightVals)
						rsub.OnNext (r);
				sub.OnNext (resultSelector (v, rsub));
				var leftDuration = leftDurationSelector (v);
				var lddis = new SingleAssignmentDisposable ();
				dis.Add (lddis);
				Action disposeLDur = () => { rightSubs.Remove (rsub); rsub.OnCompleted (); lefts.Remove (v); lddis.Dispose (); dis.Remove (lddis); }; // leftDuration is one-shot observable.
				lddis.Disposable = leftDuration.Subscribe (dummy => disposeLDur (), disposeLDur);
			}, () => sub.OnCompleted ()));
			dis.Add (ldis);

			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> IgnoreElements<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			return source.Subscribe (v => {}, ex => sub.OnError (ex), () => sub.OnCompleted ());
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<long> Interval (TimeSpan period)
		{
			return Interval (period, Scheduler.ThreadPool);
		}

		/* It Notifies "current count" to *each* observer i.e. this
		   observable holds different count numbers to the observers.
		   See ObservableTest.Interval() as an example.
		*/
		public static IObservable<long> Interval (
			TimeSpan period,
			IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			return new ColdObservableEach<long> (sub => {
			// ----
			var dis = new SingleAssignmentDisposable ();
			long count = 0;
			dis.Disposable = scheduler.Schedule (period, a => { if (!dis.IsDisposed) { sub.OnNext (count++); a (period); } });
			return dis;
			// ----
			}, scheduler);
		}
		
		public static IObservable<TResult> Join<TLeft, TRight, TLeftDuration, TRightDuration, TResult>(
			this IObservable<TLeft> left,
			IObservable<TRight> right,
			Func<TLeft, IObservable<TLeftDuration>> leftDurationSelector,
			Func<TRight, IObservable<TRightDuration>> rightDurationSelector,
			Func<TLeft, TRight, TResult> resultSelector)
		{
			return GroupJoin (left, right, leftDurationSelector, rightDurationSelector, (l, rgrp) => rgrp.Select (r => resultSelector (l, r))).Merge ();
		}
		
		public static IObservable<long> LongCount<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new ColdObservableEach<long> (sub => {
			// ----
			long count = 0;
			return source.Subscribe ((s) => count++, () => { sub.OnNext (count); sub.OnCompleted (); });
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<Notification<TSource>> Materialize<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new ColdObservableEach<Notification<TSource>> (sub => {
			// ----
			return source.Subscribe (
				v => sub.OnNext (Notification.CreateOnNext<TSource> (v)),
				ex => { sub.OnNext (Notification.CreateOnError<TSource> (ex)); sub.OnCompleted (); },
				() => { sub.OnNext (Notification.CreateOnCompleted<TSource> ()); sub.OnCompleted (); });
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<IList<TSource>> MaxBy<TSource, TKey> (this IObservable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.MaxBy (keySelector, Comparer<TKey>.Default);
		}
		
		public static IObservable<IList<TSource>> MaxBy<TSource, TKey> (
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IComparer<TKey> comparer)
		
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (keySelector == null)
				throw new ArgumentNullException ("keySelector");
			if (comparer 
			== null)
				throw new ArgumentNullException ("comparer");

			return new ColdObservableEach<IList<TSource>> (sub => {
			// ----
			TKey maxk = default (TKey);
			List<TSource> max = new List<TSource> ();
			bool got = false;
			return source.Subscribe (
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
			// ----
			}, DefaultColdScheduler);
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
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			// avoided using "from source in sources select ..." for eager evaluation.
			var dis = new CompositeDisposable ();
			var l = new List<IObservable<TSource>> (sources);
			int index = 0;
			foreach (var source in l) {
				if (index >= maxConcurrent)
					continue;
				Func<IObservable<TSource>, IDisposable> subfunc = null;
				subfunc = ss => ss.Subscribe (s => {
					sub.OnNext (s);
				}, ex => {
					sub.OnError (ex);
				}, () => {
					sub.OnCompleted ();
					if (index < l.Count)
						dis.Add (subfunc (l [index++]));
				});
				dis.Add (subfunc (source));
			}
			return dis;
			// ----
			}, scheduler);
		}
		
		public static IObservable<TSource> Merge<TSource> (
			this IObservable<TSource> first,
			IObservable<TSource> second,
			IScheduler scheduler)
		{
			if (first == null)
				throw new ArgumentNullException ("first");
			if (second == null)
				throw new ArgumentNullException ("second");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

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
			if (source == null)
				throw new ArgumentNullException ("source");
			if (keySelector == null)
				throw new ArgumentNullException ("keySelector");
			if (comparer == null)
				throw new ArgumentNullException ("comparer");

			return new ColdObservableEach<IList<TSource>> (sub => {
			// ----
			TKey mink = default (TKey);
			List<TSource> min = new List<TSource> ();
			bool got = false;
			return source.Subscribe (
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
			// ----
			}, DefaultColdScheduler);
		}

		public static IEnumerable<TSource> MostRecent<TSource> (
			this IObservable<TSource> source,
			TSource initialValue)
		{
			return EnumerateLatest (source, initialValue, false);
		}
		
		public static IObservable<TResult> Never<TResult> ()
		{
			return new NeverObservable<TResult> ();
		}
		
		// blocking and without buffering.
		public static IEnumerable<TSource> Next<TSource> (this IObservable<TSource> source)
		{
			return EnumerateLatest (source, default (TSource), true);
		}
		
		static IEnumerable<TSource> EnumerateLatest<TSource> (this IObservable<TSource> source, TSource initialValue, bool block)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			var wait = block ? new AutoResetEvent (false) : null;
			bool ongoing = true;
			TSource current = initialValue;
			Exception error = null;
			var dis = source.Subscribe (
				v => { current = v; if (block) wait.Set (); },
				ex => { error = ex; ongoing = false; if (block) wait.Set (); },
				() => { ongoing = false; if (block) wait.Set (); }
				);
			while (true) {
				if (block)
					wait.WaitOne ();
				if (error != null)
					throw error;
				if (ongoing)
					yield return current;
				else
					break;
			}
			dis.Dispose ();
		}
		
		public static IObservable<TSource> ObserveOn<TSource> (
			this IObservable<TSource> source,
			IScheduler scheduler)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			return new ColdObservableEach<TSource> (sub => source.Subscribe (sub), DefaultColdScheduler, () => new SchedulerBoundSubject<TSource> (scheduler));

		}
		
		public static IObservable<TSource> ObserveOn<TSource> (
			this IObservable<TSource> source,
			SynchronizationContext context)
		{
			return ObserveOn (source, new SynchronizationContextScheduler (context));
		}
		
		public static IObservable<TResult> OfType<TResult> (this IObservable<Object> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return source.Where (v => v != null && typeof (TResult).IsAssignableFrom (v.GetType ())).Select (v => (TResult) v);
		}
		
		public static IObservable<TSource> OnErrorResumeNext<TSource> (this IEnumerable<IObservable<TSource>> sources)
		{
			if (sources == null)
				throw new ArgumentNullException ("sources");

			return new ColdObservableEach<TSource> (sub => {
			// ----
				var dis = new CompositeDisposable ();
				var e = sources.GetEnumerator ();
				OnErrorResumeNext<TSource> (null, sub, e, dis);
				return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		static void OnErrorResumeNext<TSource> (Exception error, ISubject<TSource> sub, IEnumerator<IObservable<TSource>> e, CompositeDisposable dis)
		{
			if (e.MoveNext ())
				e.Current.Subscribe (v => sub.OnNext (v), ex => OnErrorResumeNext (ex, sub, e, dis), () => sub.OnCompleted ());
			else if (error != null)
				sub.OnError (error);
			else // there was no Observable sequence
				sub.OnCompleted ();
		}
		
		public static IObservable<TSource> OnErrorResumeNext<TSource> (params IObservable<TSource>[] sources)
		{
			return OnErrorResumeNext ((IEnumerable<IObservable<TSource>>) sources);
		}
		
		public static IObservable<TSource> OnErrorResumeNext<TSource> (
			this IObservable<TSource> first,
			IObservable<TSource> second)
		{
			return OnErrorResumeNext (new IObservable<TSource> [] {first, second});
		}
		
		// see http://leecampbell.blogspot.com/2010/05/rx-part-2-static-and-extension-methods.html
		public static IObservable<int> Range (int start, int count)
		{
			return Range (start, count, Scheduler.CurrentThread);
		}
		
		public static IObservable<int> Range (int start, int count, IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			var sub = new ReplaySubject<int> (scheduler);
			foreach (var i in Enumerable.Range (start, count))
				sub.OnNext (i);
			sub.OnCompleted ();
			return sub;
		}
		
		public static IObservable<TSource> Repeat<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return Concat (RepeatInfinitely (source));
		}
		
		static IEnumerable<IObservable<TSource>> RepeatInfinitely<TSource> (IObservable<TSource> source)
		{
			while (true)
				yield return source;
		}
		
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
			return Repeat (value, int.MaxValue, scheduler);
		}
		
		public static IObservable<TResult> Repeat<TResult> (
			TResult value,
			int repeatCount,
			IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			var sub = new ReplaySubject<TResult> (scheduler);
			for (int i = 0; i < repeatCount; i++)
				sub.OnNext (value);
			sub.OnCompleted ();
			return sub;
		}

		public static IObservable<TSource> Retry<TSource> (this IObservable<TSource> source)
		{
			return source.Retry (int.MaxValue);
		}
		
		public static IObservable<TSource> Retry<TSource> (
			this IObservable<TSource> source,
			int retryCount)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (retryCount < 0)
				throw new ArgumentOutOfRangeException ("retryCount");
			if (retryCount == 0)
				return Observable.Empty<TSource> ();

			return new ColdObservableEach<TSource> (sub => {
			// ----

			/* To my understanding, this should be Replay. The example below won't print numbers at all if it is just a Subject<T>.
			
				var source = new ReplaySubject<int>();
				source.OnNext(5);
				source.OnNext(2);
				source.OnError(new NotSupportedException());
				int retryCount = 2;
				
				source.Retry (retryCount).Subscribe(Console.WriteLine, ex => Console.WriteLine("retry exceeded"), () => Console.WriteLine ("done"));
			
			*/
			Action<Exception> onError = null;
			var dis = new SerialDisposable ();
			onError = (error) => {
				--retryCount;
				if (retryCount <= 0)
					sub.OnError (error);
				else
					dis.Disposable = source.Subscribe (v => sub.OnNext (v), ex => onError (ex), () => sub.OnCompleted ());
				};
			dis.Disposable = source.Subscribe (
				v => sub.OnNext (v),
				ex => onError (ex),
				() => sub.OnCompleted ());
			return dis;
			// ----
			}, DefaultColdScheduler, () => new Subject<TSource> ());
		}
		
		// see http://leecampbell.blogspot.com/2010/05/rx-part-2-static-and-extension-methods.html
		public static IObservable<TResult> Return<TResult> (TResult value)
		{
			return Return (value, Scheduler.Immediate);
		}
		
		public static IObservable<TResult> Return<TResult> (TResult value, IScheduler scheduler)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

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
		{
			return Sample<TSource, long> (source, Interval (interval, scheduler));
		}
		
		public static IObservable<TSource> Sample<TSource, TSample> (
			this IObservable<TSource> source,
			IObservable<TSample> sampler)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			bool emit = false;
			TSource current = default (TSource);
			var sdis = sampler.Subscribe (v => { if (emit) sub.OnNext (current); }, ex => sub.OnError (ex), () => {}); // it does not send OnCompleted to the result.
			var dis = source.Subscribe (v => { current = v; emit = true; }, ex => sub.OnError (ex), () => sub.OnCompleted ());
			return new CompositeDisposable (sdis, dis);
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> Scan<TSource> (
			this IObservable<TSource> source,
			Func<TSource, TSource, TSource> accumulator)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (accumulator == null)
				throw new ArgumentNullException ("accumulator");

			// note the results difference between those Scan() overloads...
			bool has_value = false;
			TSource intermediate = default (TSource);
			return new ColdObservableEach<TSource> (sub => {
			// ----
			return source.Subscribe (v => { if (has_value) intermediate = accumulator (intermediate, v); else intermediate = v; sub.OnNext (intermediate); has_value = true; }, ex => sub.OnError (ex), () => sub.OnCompleted ());
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TAccumulate> Scan<TSource, TAccumulate> (
			this IObservable<TSource> source,
			TAccumulate seed,
			Func<TAccumulate, TSource, TAccumulate> accumulator)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (accumulator == null)
				throw new ArgumentNullException ("accumulator");

			return new ColdObservableEach<TAccumulate> (sub => {
			// ----
			// note the results difference between those Scan() overloads...
			TAccumulate intermediate = seed;
			return source.Subscribe (v => { intermediate = accumulator (intermediate, v); sub.OnNext (intermediate); }, ex => sub.OnError (ex), () => sub.OnCompleted ());
			// ----
			}, DefaultColdScheduler);
		}

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

			return new ColdObservableEach<TResult> (sub => {
			// ----
			int idx = 0;
			return source.Subscribe ((s) => sub.OnNext (selector (s, idx++)), () => sub.OnCompleted ());
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TResult> SelectMany<TSource, TResult> (
			this IObservable<TSource> source,
			Func<TSource, IEnumerable<TResult>> selector)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (selector == null)
				throw new ArgumentNullException ("selector");

			return new ColdObservableEach<TResult> (sub => {
			// ----
			return source.Subscribe (v => {
				foreach (var r in selector (v))
					sub.OnNext (r);
			}, ex => sub.OnError (ex), () => sub.OnCompleted ());
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TResult> SelectMany<TSource, TResult> (
			this IObservable<TSource> source,
			Func<TSource, IObservable<TResult>> selector)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (selector == null)
				throw new ArgumentNullException ("selector");

			return new ColdObservableEach<TResult> (sub => {
			// ----
			int count = 0;
			bool done = false;
			var dis = new CompositeDisposable ();
			dis.Add (source.Subscribe (v => {
				count++;
				var o = selector (v);
				dis.Add (o.Subscribe (vv => { if (!dis.IsDisposed) sub.OnNext (vv); }, () => {
					if (--count == 0 && done)
						sub.OnCompleted ();
				}));
			}, ex => sub.OnError (ex), () => {
				done = true;
				if (count == 0)
					sub.OnCompleted ();
			}));
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TOther> SelectMany<TSource, TOther> (
			this IObservable<TSource> source,
			IObservable<TOther> other)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (other == null)
				throw new ArgumentNullException ("other");

			return new ColdObservableEach<TOther> (sub => {
			// ----
			int waits = 0;
			return source.Subscribe (
				v => {
					waits++;
					var dis = new SingleAssignmentDisposable ();
					dis.Disposable = other.Subscribe (
						vv => sub.OnNext (vv),
						ex => sub.OnError (ex),
						() => { waits--; dis.Dispose (); });
				},
				ex => sub.OnError (ex),
				() => { if (waits == 0) sub.OnCompleted (); }
				);
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TResult> SelectMany<TSource, TResult> (
			this IObservable<TSource> source,
			Func<TSource, IObservable<TResult>> onNext,
			Func<Exception, IObservable<TResult>> onError,
			Func<IObservable<TResult>> onCompleted)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (onNext == null)
				throw new ArgumentNullException ("onNext");
			if (onError == null)
				throw new ArgumentNullException ("onError");
			if (onCompleted == null)
				throw new ArgumentNullException ("onCompleted");

			return new ColdObservableEach<TResult> (sub => {
			// ----
			return source.Subscribe (
				(v) => { var o = onNext (v); o.Subscribe (vv => sub.OnNext (vv)); },
				(ex) => { var o = onError (ex); o.Subscribe (vv => sub.OnNext (vv)); },
				() => { var o = onCompleted (); o.Subscribe (vv => sub.OnNext (vv)); });
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TResult> SelectMany<TSource, TCollection, TResult> (
			this IObservable<TSource> source,
			Func<TSource, IEnumerable<TCollection>> collectionSelector,
			Func<TSource, TCollection, TResult> resultSelector)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (collectionSelector == null)
				throw new ArgumentNullException ("collectionSelector");
			if (resultSelector == null)
				throw new ArgumentNullException ("resultSelector");

			return new ColdObservableEach<TResult> (sub => {
			// ----
			return source.Subscribe (
				v => {
					var c = collectionSelector (v);
					foreach (var v2 in c)
						sub.OnNext (resultSelector (v, v2));
				},
				ex => sub.OnError (ex),
				() => sub.OnCompleted ());
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TResult> SelectMany<TSource, TCollection, TResult> (
			this IObservable<TSource> source,
			Func<TSource, IObservable<TCollection>> collectionSelector,
			Func<TSource, TCollection, TResult> resultSelector)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (collectionSelector == null)
				throw new ArgumentNullException ("collectionSeelctor");
			if (resultSelector == null)
				throw new ArgumentNullException ("resultSelector");

			return new ColdObservableEach<TResult> (sub => {
			// ----
			int waits = 0;
			return source.Subscribe (
				v => {
					waits++;
					var cc = collectionSelector (v);
					var dis = new SingleAssignmentDisposable ();
					dis.Disposable = cc.Subscribe (
						c => sub.OnNext (resultSelector (v, c)),
						ex => sub.OnError (ex),
						() => { waits--; dis.Dispose (); });
				},
				ex => sub.OnError (ex),
				() => { if (waits == 0) sub.OnCompleted (); });
			// ----
			}, DefaultColdScheduler);
		}
		
		class CountingObservable<TSource> : IObservable<TSource>
		{
			IObservable<TSource> source;
			
			public CountingObservable (IObservable<TSource> source)
			{
				this.source = source;
				source.Subscribe (v => Count++);
			}
			
			public int Count { get; private set; }
			
			public IDisposable Subscribe (IObserver<TSource> observer)
			{
				return source.Subscribe (observer);
			}
		}
		
		public static IObservable<bool> SequenceEqual<TSource> (
			this IObservable<TSource> first,
			IObservable<TSource> second)
		{
			return first.SequenceEqual (second, EqualityComparer<TSource>.Default);
		}
		
		public static IObservable<bool> SequenceEqual<TSource> (
			this IObservable<TSource> first,
			IObservable<TSource> second,
			IEqualityComparer<TSource> comparer)
		{
			if (first == null)
				throw new ArgumentNullException ("first");
			if (second == null)
				throw new ArgumentNullException ("second");
			if (comparer == null)
				throw new ArgumentNullException ("comparer");

			return new ColdObservableEach<bool> (sub => {
			// ----
			var fo = new CountingObservable<TSource> (first);
			var so = new CountingObservable<TSource> (second);
			var cmp = When (fo.And (so).Then ((f, s) => comparer.Equals (f, s))).All (v => true);
			return cmp.Subscribe (v => sub.OnNext (v && fo.Count == so.Count), ex => sub.OnError (ex), () => sub.OnCompleted ());
			// ----
			}, DefaultColdScheduler);
		}
		
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

			return new ColdObservableEach<TSource> (sub => {
			// ----
			var q = new Queue<TSource> ();
			return source.Subscribe ((s) => {
				q.Enqueue (s);
				if (count > 0)
					count--;
				else
					sub.OnNext (q.Dequeue ());
				}, () => {
				q.Clear ();
				sub.OnCompleted ();
				});
			// ----
			}, DefaultColdScheduler);
		}
		
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
		
		public static IObservable<Unit> Start (Action action)
		{
			return Start (action, Scheduler.ThreadPool);
		}
		
		public static IObservable<Unit> Start (Action action, IScheduler scheduler)
		{
			if (action == null)
				throw new ArgumentNullException ("action");

			return Start<Unit> (() => { action (); return Unit.Default; }, scheduler);
		}
		
		public static IObservable<TSource> Start<TSource> (Func<TSource> function)
		{
			return Start (function, Scheduler.ThreadPool);
		}
		
		public static IObservable<TSource> Start<TSource> (Func<TSource> function, IScheduler scheduler)
		{
			if (function == null)
				throw new ArgumentNullException ("function");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			return new HotObservable<TSource> ((sub) => {
				var ret = function ();
				sub.OnNext (ret);
				sub.OnCompleted ();
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
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			if (values == null)
				throw new ArgumentNullException ("values");

			return new HotObservable<TSource> ((sub) => {
				foreach (var v in values)
					sub.OnNext (v);
				sub.OnCompleted ();
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
			if (source == null)
				throw new ArgumentNullException ("source");
			if (observer == null)
				throw new ArgumentNullException ("observer");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			var o = source.ToObservable ();
			var sub = new ReplaySubject<TSource> (scheduler);
			var sdis = sub.Subscribe (observer);
			var dis = o.Subscribe (s => sub.OnNext (s), ex => sub.OnError (ex), () => sub.OnCompleted ());
			return new CompositeDisposable (sdis, dis);
		}

		public static IObservable<TSource> SubscribeOn<TSource> (
			this IObservable<TSource> source,
			IScheduler scheduler)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

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
			if (source == null)
				throw new ArgumentNullException ("source");

			return source.NonNullableSum ((x, y) => x + y);
		}
		
		public static IObservable<TSource> Switch<TSource> (this IObservable<IObservable<TSource>> sources)
		{
			if (sources == null)
				throw new ArgumentNullException ("sources");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			var dis = new CompositeDisposable ();
			var wait = new ManualResetEvent (true);
			dis.Add (sources.Subscribe (s => {
				dis.Add (s.Subscribe (v => { wait.WaitOne (); sub.OnNext (v); }, ex => sub.OnError (ex), () => { wait.Set (); }));
				}, ex => sub.OnError (ex), () => { wait.Set (); sub.OnCompleted (); }));
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource> Synchronize<TSource> (this IObservable<TSource> source)
		{
			return source.Synchronize (new object ());
		}
		
		public static IObservable<TSource> Synchronize<TSource> (
			this IObservable<TSource> source,
			Object gate)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (gate == null)
				throw new ArgumentNullException ("gate");

			return new ColdObservableEach<TSource> (sub => source.Subscribe (sub), DefaultColdScheduler, () => new SynchronizedSubject<TSource> (gate));
		}
		
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
				}, () => {
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

			bool stopped = false;
            return source.Where((s, i) => 
            {
                if (stopped) return false;
                stopped = !predicate(s, i); 
                return !stopped; 
            });
		}
		
		public static Plan<TResult> Then<TSource, TResult> (
			this IObservable<TSource> source,
			Func<TSource, TResult> selector)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (selector == null)
				throw new ArgumentNullException ("selector");
				
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
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			DateTimeOffset last = scheduler.Now;
			bool fire = false;
			TSource value;
			return source.Subscribe (Observer.Create<TSource> (v => {
				if (scheduler.Now - last >= dueTime) {
					last = scheduler.Now;
					sub.OnNext (v);
				} else {
					value = v;
					if (!fire) {
						fire = true;
						var slotDueTime = dueTime - (scheduler.Now - last);
						var ddis = new SingleAssignmentDisposable ();
						ddis.Disposable = scheduler.Schedule (slotDueTime, () => { last = scheduler.Now; sub.OnNext (value); fire = false; value = default (TSource); ddis.Dispose ();});
					}
				}
			}, ex => sub.OnError (ex), () => sub.OnCompleted ()));
			// ----
			}, scheduler);
		}
		
		// see http://leecampbell.blogspot.com/2010/05/rx-part-2-static-and-extension-methods.html
		public static IObservable<TResult> Throw<TResult> (Exception exception)
		{
			return Throw<TResult> (exception, Scheduler.Immediate);
		}
		
		public static IObservable<TResult> Throw<TResult> (
			Exception exception,
			IScheduler scheduler)
		{
			if (exception == null)
				throw new ArgumentNullException ("exception");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
				
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
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			return new ColdObservableEach<TimeInterval<TSource>> (sub => {
			// ----
			DateTimeOffset last = scheduler.Now;
			return source.Subscribe (
				v => { sub.OnNext (new TimeInterval<TSource> (v, Scheduler.Normalize (scheduler.Now - last))); last = scheduler.Now; },
				ex => sub.OnError (ex),
				() => sub.OnCompleted ());
			// ----
			}, DefaultColdScheduler);
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

			return new ColdObservableEach<TSource> (sub => {
			// ----
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
			return new CompositeDisposable (dis, sdis);
			// ----
			}, scheduler);
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
			return new ColdObservableEach<long> ((sub) => {
			// ----
			var dis = new SingleAssignmentDisposable ();
			dis.Disposable = scheduler.Schedule (dueTime, () => { if (!dis.IsDisposed) sub.OnNext (0); sub.OnCompleted (); });
			return dis;
			// ----
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
			return Timer (dueTime - scheduler.Now, period, scheduler);
		}
		
		public static IObservable<long> Timer (
			TimeSpan dueTime,
			TimeSpan period,
			IScheduler scheduler)
		{
			return new ColdObservableEach<long> (sub => {
			// ----
			var t = Timer (dueTime, scheduler);
			var dis = new CompositeDisposable ();
			dis.Add (t.Subscribe (v => {}, ex => sub.OnError (ex), () => {
				sub.OnNext (0);
				var i = Interval (period, scheduler);
				dis.Add (i.Subscribe ((v) => sub.OnNext (v + 1)));
			}));
			return dis;
			// ----
			}, scheduler);
		}
		
		public static IObservable<Timestamped<TSource>> Timestamp<TSource> (this IObservable<TSource> source)
		{
			return Timestamp (source, Scheduler.ThreadPool);
		}
		
		public static IObservable<Timestamped<TSource>> Timestamp<TSource> (this IObservable<TSource> source, IScheduler scheduler)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

			return new ColdObservableEach<Timestamped<TSource>> (sub => {
			// ----
			return source.Subscribe (v => sub.OnNext (new Timestamped<TSource> (v, scheduler.Now)), ex => sub.OnError (ex), () => sub.OnCompleted ());
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TSource[]> ToArray<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new ColdObservableEach<TSource[]> ((sub) => {
			// ----
			var a = new List<TSource> ();
			return source.Subscribe (v => a.Add (v), ex => sub.OnError (ex), () => { sub.OnNext (a.ToArray ()); sub.OnCompleted (); });
			// ----
			}, DefaultColdScheduler);
		}
		
		public static Func<IObservable<Unit>> ToAsync (this Action action)
		{
			return ToAsync (action, Scheduler.ThreadPool);
		}
		
		public static Func<IObservable<Unit>> ToAsync (this Action action, IScheduler scheduler)
		{
			if (action == null)
				throw new ArgumentNullException ("action");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");

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
			if (source == null)
				throw new ArgumentNullException ("source");
			if (keySelector == null)
				throw new ArgumentNullException ("keySelector");
			if (elementSelector == null)
				throw new ArgumentNullException ("elementSelector");
			if (comparer == null)
				throw new ArgumentNullException ("comparer");
			
			return new ColdObservableEach<IDictionary<TKey, TElement>> ((sub) => {
			// ----
			var dic = new Dictionary<TKey, TElement> (comparer);
			return source.Subscribe (
				v => dic.Add (keySelector (v), elementSelector (v)),
				ex => sub.OnError (ex),
				() => { sub.OnNext (dic); sub.OnCompleted (); });
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IEnumerable<TSource> ToEnumerable<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			var wait = new AutoResetEvent (false);
			var q = new Queue<TSource> ();
			var dis = new SingleAssignmentDisposable ();
			bool active = true;
			Exception error = null;
			Action onDone = () => { active = false; dis.Dispose (); wait.Set (); };
			dis.Disposable = source.Subscribe (
				v => { q.Enqueue (v); wait.Set (); },
				ex => { error = ex; onDone (); },
				onDone
				);
			while (active) {
				wait.WaitOne (TimeSpan.FromSeconds (1));
				while (q.Count > 0)
					yield return q.Dequeue ();
			}
			while (q.Count > 0)
				yield return q.Dequeue ();
			if (error != null)
				throw error;
		}
		
		public static IObservable<IList<TSource>> ToList<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new ColdObservableEach<IList<TSource>> ((sub) => {
			// ----
			var l = new List<TSource> ();
			return source.Subscribe (
				v => l.Add (v),
				ex => sub.OnError (ex),
				() => { sub.OnNext (l); sub.OnCompleted (); });
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<ILookup<TKey, TSource>> ToLookup<TSource, TKey>(
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector)
		{
			return ToLookup<TSource, TKey> (source, keySelector, EqualityComparer<TKey>.Default);
		}
		
		public static IObservable<ILookup<TKey, TSource>> ToLookup<TSource, TKey>(
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{
			return ToLookup<TSource, TKey, TSource> (source, keySelector, s => s, comparer);
		}
		
		public static IObservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector)
		{
			return ToLookup (source, keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}
		
		public static IObservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IObservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (keySelector == null)
				throw new ArgumentNullException ("keySelector");
			if (elementSelector == null)
				throw new ArgumentNullException ("elementSelector");
			if (comparer == null)
				throw new ArgumentNullException ("comparer");
			
			return new ColdObservableEach<ILookup<TKey, TElement>> ((sub) => {
			// ----
			var l = new List<TSource> ();
			return source.Subscribe (v => l.Add (v), ex => sub.OnError (ex), () => { sub.OnNext (Enumerable.ToLookup<TSource, TKey, TElement> (l,keySelector, elementSelector, comparer)); sub.OnCompleted (); });
			// ----
			}, DefaultColdScheduler);
		}

		public static IObservable<TSource> ToObservable<TSource> (this IEnumerable<TSource> source)
		{
			return ToObservable<TSource> (source, Scheduler.CurrentThread);
		}
		
		public static IObservable<TSource> ToObservable<TSource> (
			this IEnumerable<TSource> source,
			IScheduler scheduler)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			
			return new ColdObservableEach<TSource> ((sub) => {
			// ----
			foreach (var s in source)
				sub.OnNext (s);
			sub.OnCompleted ();
			return Disposable.Empty;
			// ----
			}, scheduler);
		}
		
		public static IObservable<TSource> Using<TSource, TResource> (
			Func<TResource> resourceFactory,
			Func<TResource, IObservable<TSource>> observableFactory)
			where TResource : IDisposable
		{
			if (resourceFactory == null)
				throw new ArgumentNullException ("resourceFactory");
			if (observableFactory == null)
				throw new ArgumentNullException ("observableFactory");

			return new ColdObservableEach<TSource> (sub => {
			// ----
			var dis = new CompositeDisposable ();
			var rdis = resourceFactory ();
			dis.Add (rdis);
			var source = observableFactory (rdis);
			dis.Add (source.Subscribe (v => sub.OnNext (v), ex => sub.OnError (ex), () => sub.OnCompleted ()));
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
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
			if (source == null)
				throw new ArgumentNullException ("source");
			if (predicate == null)
				throw new ArgumentNullException ("predicate");
			
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

			return new ColdObservableEach<TSource> (sub => {
			// ----
			int idx = 0;
			return source.Subscribe ((s) => { if (predicate (s, idx++)) sub.OnNext (s); },ex => sub.OnError(ex),() => sub.OnCompleted());
			// ----
			}, DefaultColdScheduler);
		}
		
		public static IObservable<TResult> Zip<TFirst, TSecond, TResult> (
			this IObservable<TFirst> first,
			IEnumerable<TSecond> second,
			Func<TFirst, TSecond, TResult> resultSelector)
		{
			if (second == null)
				throw new ArgumentNullException ("second");
			
			return Zip (first, second.ToObservable (), resultSelector);
		}
		
		public static IObservable<TResult> Zip<TFirst, TSecond, TResult>(
			this IObservable<TFirst> first,
			IObservable<TSecond> second,
			Func<TFirst, TSecond, TResult> resultSelector)
		{
			if (first == null)
				throw new ArgumentNullException ("first");
			if (second == null)
				throw new ArgumentNullException ("second");
			if (resultSelector == null)
				throw new ArgumentNullException ("resultSelector");
			
			return When (first.And (second).Then (resultSelector));
		}
	}
}
