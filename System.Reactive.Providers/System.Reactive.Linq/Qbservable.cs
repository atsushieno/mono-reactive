// FIXME: I almost just copied Observable.cs and replaced IObservable with IQbservable. Turned out that many things are different from Observable.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Joins;
using System.Reactive.Subjects;
using System.Threading;

namespace System.Reactive.Linq
{
	public static class Qbservable
	{
		public static IQbservableProvider Provider {
			get { throw new NotImplementedException (); }
		}
		
		public static IQbservable<TSource> Aggregate<TSource> (
			this IQbservable<TSource> source,
			Func<TSource, TSource, TSource> accumulator)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TAccumulate> Aggregate<TSource, TAccumulate> (
			this IQbservable<TSource> source,
			TAccumulate seed,
			Func<TAccumulate, TSource, TAccumulate> accumulator)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> All<TSource> (
			this IQbservable<TSource> source,
			Func<TSource, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Amb<TSource> (this IEnumerable<IQbservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Amb<TSource> (params IQbservable<TSource>[] sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Amb<TSource> (
			this IQbservableProvider provider,
			IEnumerable<IObservable<TSource>> sources)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Amb<TSource> (
			this IQbservableProvider provider,
			params IObservable<TSource> [] sources)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Amb<TSource> (this IQbservable<TSource> first, IObservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static Pattern<TLeft, TRight> And<TLeft, TRight> (this IQbservable<TLeft> left, IObservable<TRight> right)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> Any<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> Any<TSource> (this IObservable<TSource> source, Func<TSource, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> AsQbservable<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<decimal> Average (this IQbservable<decimal> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double> Average (this IQbservable<double> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int> Average (this IQbservable<int> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Average (this IQbservable<long> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<float> Average (this IQbservable<float> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<decimal?> Average (this IQbservable<decimal?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double?> Average (this IQbservable<double?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int?> Average (this IQbservable<int?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long?> Average (this IQbservable<long?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<float?> Average (this IQbservable<float?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> Buffer<TSource, TBufferClosing> (
			this IQbservable<TSource> source,
			Func<IQbservable<TBufferClosing>> bufferClosingSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> Buffer<TSource> (
			this IQbservable<TSource> source,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> Buffer<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> Buffer<TSource> (
			this IQbservable<TSource> source,
			int count,
			int skip)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> Buffer<TSource, TBufferOpening, TBufferClosing> (
			this IQbservable<TSource> source,
			IObservable<TBufferOpening> bufferOpenings,
			Func<TBufferOpening, IObservable<TBufferClosing>> bufferClosingSelector)
		{ throw new NotImplementedException (); }

		public static IQbservable<IList<TSource>> Buffer<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			int count)
		{ throw new NotImplementedException (); }

		public static IQbservable<IList<TSource>> Buffer<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<IList<TSource>> Buffer<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift)
		{ throw new NotImplementedException (); }

		public static IQbservable<IList<TSource>> Buffer<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			int count,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<IList<TSource>> Buffer<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Cast<TResult> (this IQbservable<Object> source)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Catch<TSource> (this IEnumerable<IQbservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Catch<TSource> (params IQbservable<TSource> [] sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Catch<TSource, TException> (
			this IQbservable<TSource> source,
			Func<TException, IObservable<TSource>> handler)
			where TException : Exception
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Catch<TSource> (
			this IQbservable<TSource> first,
			IObservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> CombineLatest<TFirst, TSecond, TResult> (
			this IQbservable<TFirst> first,
			IQbservable<TSecond> second,
			Func<TFirst, TSecond, TResult> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Concat<TSource> (this IEnumerable<IQbservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Concat<TSource> (this IQbservable<IQbservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Concat<TSource> (params IQbservable<TSource> [] sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Concat<TSource> (this IQbservable<TSource> first, IQbservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> Contains<TSource> (
			this IQbservable<TSource> source,
			TSource value)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> Contains<TSource> (
			this IQbservable<TSource> source,
			TSource value,
			IEqualityComparer<TSource> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int> Count<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Create<TSource> (Func<IObserver<TSource>, Action> subscribe)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Create<TSource> (Func<IObserver<TSource>, IDisposable> subscribe)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> DefaultIfEmpty<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> DefaultIfEmpty<TSource> (this IQbservable<TSource> source, TSource defaultValue)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TValue> Defer<TValue> (Func<IQbservable<TValue>> QbservableFactory)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Delay<TSource> (this IQbservable<TSource> source, DateTimeOffset dueTime)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Delay<TSource> (this IQbservable<TSource> source, TimeSpan dueTime)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Delay<TSource> (
			this IQbservable<TSource> source,
			DateTimeOffset dueTime,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Delay<TSource> (
			this IQbservable<TSource> source,
			TimeSpan dueTime,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Dematerialize<TSource> (this IQbservable<Notification<TSource>> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Distinct<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Distinct<TSource> (
			this IQbservable<TSource> source,
			IEqualityComparer<TSource> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Distinct<TSource, TKey> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Distinct<TSource, TKey> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> DistinctUntilChanged<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> DistinctUntilChanged<TSource> (
			this IQbservable<TSource> source,
			IEqualityComparer<TSource> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> DistinctUntilChanged<TSource, TKey> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> DistinctUntilChanged<TSource, TKey> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Do<TSource> (
			this IQbservable<TSource> source,
			Action<TSource> onNext)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Do<TSource> (
			this IQbservable<TSource> source,
			IObserver<TSource> observer)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Do<TSource> (
			this IQbservable<TSource> source,
			Action<TSource> onNext,
			Action<Exception> onError)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Do<TSource> (
			this IQbservable<TSource> source,
			Action<TSource> onNext,
			Action onCompleted)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Do<TSource> (
			this IQbservable<TSource> source,
			Action<TSource> onNext,
			Action<Exception> onError,
			Action onCompleted)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> ElementAt<TSource> (this IQbservable<TSource> source, int index)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> ElementAtOrDefault<TSource> (this IQbservable<TSource> source, int index)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Empty<TResult> ()
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Empty<TResult> (IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Finally<TSource> (this IQbservable<TSource> source, Action finallyAction)
		{ throw new NotImplementedException (); }
		
		public static TSource First<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static TSource First<TSource> (this IQbservable<TSource> source, Func<TSource, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static TSource FirstOrDefault<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static TSource FirstOrDefault<TSource> (this IQbservable<TSource> source, Func<TSource, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static void ForEach<TSource> (this IQbservable<TSource> source, Action<TSource> onNext)
		{ throw new NotImplementedException (); }
		
		public static Func<IQbservable<Unit>> FromAsyncPattern(
			Func<AsyncCallback, Object, IAsyncResult> begin,
			Action<IAsyncResult> end)
		{ throw new NotImplementedException (); }
		
		public static Func<IQbservable<TResult>> FromAsyncPattern<TResult> (
			Func<AsyncCallback, Object, IAsyncResult> begin,
			Func<IAsyncResult, TResult> end)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TEventArgs> FromEvent<TEventArgs> (
			Action<Action<TEventArgs>> addHandler,
			Action<Action<TEventArgs>> removeHandler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<Unit> FromEvent (
			Action<Action> addHandler,
			Action<Action> removeHandler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TEventArgs> FromEvent<TDelegate, TEventArgs> (
			Action<TDelegate> addHandler,
			Action<TDelegate> removeHandler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TEventArgs> FromEvent<TDelegate, TEventArgs> (
			Func<Action<TEventArgs>, TDelegate> conversion,
			Action<TDelegate> addHandler,
			Action<TDelegate> removeHandler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs> (
			Action<EventHandler<TEventArgs>> addHandler,
			Action<EventHandler<TEventArgs>> removeHandler)
			where TEventArgs : EventArgs
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<EventArgs>> FromEventPattern (
			Action<EventHandler> addHandler,
			Action<EventHandler> removeHandler)
		{ throw new NotImplementedException (); }

		public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TDelegate, TEventArgs> (
			Action<TDelegate> addHandler,
			Action<TDelegate> removeHandler)
			where TEventArgs : EventArgs
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<EventArgs>> FromEventPattern (
			object target,
			string eventName)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs> (
			object target,
			string eventName)
			where TEventArgs : EventArgs
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<EventArgs>> FromEventPattern (Type type, string eventName)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs> (Type type, string eventName)
			where TEventArgs : EventArgs
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TDelegate, TEventArgs> (
			Func<EventHandler<TEventArgs>, TDelegate> conversion,
			Action<TDelegate> addHandler,
			Action<TDelegate> removeHandler)
			where TEventArgs : EventArgs
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector,
			Func<TState, DateTimeOffset> timeSelector)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector,
			Func<TState, TimeSpan> timeSelector)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector,
			Func<TState, DateTimeOffset> timeSelector,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			TState initialState,
			Func<TState, bool> condition,
			Func<TState, TState> iterate,
			Func<TState, TResult> resultSelector,
			Func<TState, TimeSpan> timeSelector,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IEnumerator<TSource> GetEnumerator<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TSource>> GroupBy<TSource, TKey> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TSource>> GroupBy<TSource, TKey> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TElement>> GroupBy<TSource, TKey, TElement> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TElement>> GroupBy<TSource, TKey, TElement> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<IGroupedObservable<TKey, TSource>, IQbservable<TDuration>> durationSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<IGroupedObservable<TKey, TSource>, IQbservable<TDuration>> durationSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			Func<IGroupedObservable<TKey, TElement>, IQbservable<TDuration>> durationSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			Func<IGroupedObservable<TKey, TElement>, IQbservable<TDuration>> durationSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> GroupJoin<TLeft, TRight, TLeftDuration, TRightDuration, TResult> (
			this IQbservable<TLeft> left,
			IQbservable<TRight> right,
			Func<TLeft, IQbservable<TLeftDuration>> leftDurationSelector,
			Func<TRight, IQbservable<TRightDuration>> rightDurationSelector,
			Func<TLeft, IQbservable<TRight>, TResult> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> IgnoreElements<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Interval (TimeSpan period)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Interval (
			TimeSpan period,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Join<TLeft, TRight, TLeftDuration, TRightDuration, TResult>(
			this IQbservable<TLeft> left,
			IQbservable<TRight> right,
			Func<TLeft, IQbservable<TLeftDuration>> leftDurationSelector,
			Func<TRight, IQbservable<TRightDuration>> rightDurationSelector,
			Func<TLeft, TRight, TResult> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static TSource Last<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static TSource Last<TSource> (this IQbservable<TSource> source, Func<TSource, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static TSource LastOrDefault<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static TSource LastOrDefault<TSource> (this IQbservable<TSource> source, Func<TSource, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static IEnumerable<TSource> Latest<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> LongCount<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<Notification<TSource>> Materialize<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<decimal> Max (this IQbservable<decimal> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double> Max (this IQbservable<double> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int> Max (this IQbservable<int> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Max (this IQbservable<long> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<float> Max (this IQbservable<float> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<decimal?> Max (this IQbservable<decimal?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double?> Max (this IQbservable<double?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int?> Max (this IQbservable<int?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long?> Max (this IQbservable<long?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<float?> Max (this IQbservable<float?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Max<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Max<TSource> (this IQbservable<TSource> source, IComparer<TSource> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> MaxBy<TSource, TKey> (this IQbservable<TSource> source, Func<TSource, TKey> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> MaxBy<TSource, TKey> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (this IEnumerable<IQbservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (this IQbservable<IQbservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (params IQbservable<TSource>[] sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IEnumerable<IQbservable<TSource>> sources,
			int maxConcurrent)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IEnumerable<IQbservable<TSource>> sources,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IQbservable<IQbservable<TSource>> sources,
			int maxConcurrent)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IQbservable<TSource> first,
			IQbservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			IScheduler scheduler,
			params IQbservable<TSource>[] sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IEnumerable<IQbservable<TSource>> sources,
			int maxConcurrent,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IQbservable<TSource> first,
			IQbservable<TSource> second,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<decimal> Min (this IQbservable<decimal> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double> Min (this IQbservable<double> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int> Min (this IQbservable<int> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Min (this IQbservable<long> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<float> Min (this IQbservable<float> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<decimal?> Min (this IQbservable<decimal?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double?> Min (this IQbservable<double?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int?> Min (this IQbservable<int?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long?> Min (this IQbservable<long?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<float?> Min (this IQbservable<float?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Min<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Min<TSource> (this IQbservable<TSource> source, IComparer<TSource> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> MinBy<TSource, TKey> (this IQbservable<TSource> source, Func<TSource, TKey> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> MinBy<TSource, TKey> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IComparer<TKey> comparer)
		{ throw new NotImplementedException (); }

		public static IEnumerable<TSource> MostRecent<TSource> (
			this IQbservable<TSource> source,
			TSource initialValue)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TResult> Multicast<TSource, TResult> (
	this IQbservable<TSource> source,
	ISubject<TSource, TResult> subject)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Multicast<TSource, TIntermediate, TResult> (
			this IQbservable<TSource> source,
			Func<ISubject<TSource, TIntermediate>> subjectSelector,
			Func<IQbservable<TIntermediate>, IQbservable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Never<TResult> ()
		{ throw new NotImplementedException (); }
		
		public static IEnumerable<TSource> Next<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> ObserveOn<TSource> (
			this IQbservable<TSource> source,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> ObserveOn<TSource> (
			this IQbservable<TSource> source,
			SynchronizationContext context)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> OfType<TResult> (this IQbservable<Object> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> OnErrorResumeNext<TSource> (this IEnumerable<IQbservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> OnErrorResumeNext<TSource> (params IQbservable<TSource>[] sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> OnErrorResumeNext<TSource> (
			this IQbservable<TSource> first,
			IQbservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TSource> Publish<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TSource> Publish<TSource> (
			this IQbservable<TSource> source,
			TSource initialValue)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Publish<TSource, TResult>(
			this IQbservable<TSource> source,
			Func<IQbservable<TSource>, IQbservable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Publish<TSource, TResult>(
			this IQbservable<TSource> source,
			Func<IQbservable<TSource>, IQbservable<TResult>> selector,
			TSource initialValue)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TSource> PublishLast<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> PublishLast<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<IQbservable<TSource>, IQbservable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int> Range (int start, int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int> Range (int start, int count, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> RefCount<TSource> (this IConnectableObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Repeat<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Repeat<TResult> (TResult value)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Repeat<TResult> (TResult value, int repeatCount)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Repeat<TResult> (TResult value, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Repeat<TResult> (
			TResult value,
			int repeatCount,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			int bufferSize)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			TimeSpan window)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			int bufferSize,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			int bufferSize,
			TimeSpan window)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			TimeSpan window,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			int bufferSize,
			TimeSpan window,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<IQbservable<TSource>, IQbservable<TResult>> selector)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<IQbservable<TSource>, IQbservable<TResult>> selector,
			int bufferSize)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<IQbservable<TSource>, IQbservable<TResult>> selector,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<IQbservable<TSource>, IQbservable<TResult>> selector,
			TimeSpan window)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<IQbservable<TSource>, IQbservable<TResult>> selector,
			int bufferSize,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<IQbservable<TSource>, IQbservable<TResult>> selector,
			int bufferSize,
			TimeSpan window)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<IQbservable<TSource>, IQbservable<TResult>> selector,
			TimeSpan window,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<IQbservable<TSource>, IQbservable<TResult>> selector,
			int bufferSize,
			TimeSpan window,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Retry<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Retry<TSource> (
			this IQbservable<TSource> source,
			int retryCount)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Return<TResult> (TResult value)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Return<TResult> (TResult value, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Sample<TSource> (
			this IQbservable<TSource> source,
			TimeSpan interval)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Sample<TSource> (
			this IQbservable<TSource> source,
			TimeSpan interval,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Sample<TSource, TSample> (
			this IQbservable<TSource> source,
			IQbservable<TSample> sampler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Scan<TSource> (
			this IQbservable<TSource> source,
			Func<TSource, TSource, TSource> accumulator)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TAccumulate> Scan<TSource, TAccumulate> (
			this IQbservable<TSource> source,
			TAccumulate seed,
			Func<TAccumulate, TSource, TAccumulate> accumulator)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Select<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<TSource, TResult> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Select<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<TSource, int, TResult> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> SelectMany<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<TSource, IEnumerable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> SelectMany<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<TSource, IQbservable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TOther> SelectMany<TSource, TOther> (
			this IQbservable<TSource> source,
			IQbservable<TOther> other)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> SelectMany<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<TSource, IQbservable<TResult>> onNext,
			Func<Exception, IQbservable<TResult>> onError,
			Func<IQbservable<TResult>> onCompleted)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> SelectMany<TSource, TCollection, TResult> (
			this IQbservable<TSource> source,
			Func<TSource, IEnumerable<TCollection>> collectionSelector,
			Func<TSource, TCollection, TResult> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> SelectMany<TSource, TCollection, TResult> (
			this IQbservable<TSource> source,
			Func<TSource, IQbservable<TCollection>> collectionSelector,
			Func<TSource, TCollection, TResult> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> SequenceEqual<TSource> (
			this IQbservable<TSource> first,
			IQbservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> SequenceEqual<TSource> (
			this IQbservable<TSource> first,
			IQbservable<TSource> second,
			IEqualityComparer<TSource> comparer)
		{ throw new NotImplementedException (); }
		
		public static TSource Single<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static TSource Single<TSource> (
			this IQbservable<TSource> source,
			Func<TSource, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static TSource SingleOrDefault<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static TSource SingleOrDefault<TSource> (
			this IQbservable<TSource> source,
			Func<TSource, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Skip<TSource> (
			this IQbservable<TSource> source,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> SkipLast<TSource> (
			this IQbservable<TSource> source,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> SkipUntil<TSource, TOther> (
			this IQbservable<TSource> source,
			IQbservable<TOther> other)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> SkipWhile<TSource> (
			this IQbservable<TSource> source,
			Func<TSource, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> SkipWhile<TSource> (
			this IQbservable<TSource> source,
			Func<TSource, int, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<Unit> Start (Action action)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<Unit> Start (Action action, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Start<TSource> (Func<TSource> function)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Start<TSource> (Func<TSource> function, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> StartWith<TSource>( 
			this IQbservable<TSource> source,
			params TSource [] values)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> StartWith<TSource> (
			this IQbservable<TSource> source,
			IScheduler scheduler,
			params TSource [] values)
		{ throw new NotImplementedException (); }
		
		public static IDisposable Subscribe<TSource> (
			this IEnumerable<TSource> source,
			IObserver<TSource> observer)
		{ throw new NotImplementedException (); }
		
		public static IDisposable Subscribe<TSource> (
			this IEnumerable<TSource> source,
			IObserver<TSource> observer,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> SubscribeOn<TSource> (
			this IQbservable<TSource> source,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> SubscribeOn<TSource> (
			this IQbservable<TSource> source,
			SynchronizationContext context)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<decimal> Sum (this IQbservable<decimal> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double> Sum (this IQbservable<double> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int> Sum (this IQbservable<int> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Sum (this IQbservable<long> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<float> Sum (this IQbservable<float> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<decimal?> Sum (this IQbservable<decimal?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double?> Sum (this IQbservable<double?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int?> Sum (this IQbservable<int?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long?> Sum (this IQbservable<long?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<float?> Sum (this IQbservable<float?> source)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Switch<TSource> (this IQbservable<IQbservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Synchronize<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Synchronize<TSource> (
			this IQbservable<TSource> source,
			Object gate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Take<TSource> (
			this IQbservable<TSource> source,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> TakeLast<TSource> (
			this IQbservable<TSource> source,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> TakeUntil<TSource, TOther> (
			this IQbservable<TSource> source,
			IQbservable<TOther> other)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> TakeWhile<TSource> (
			this IQbservable<TSource> source,
			Func<TSource, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> TakeWhile<TSource> (
			this IQbservable<TSource> source,
			Func<TSource, int, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static Plan<TResult> Then<TSource, TResult> (
			this IQbservable<TSource> source,
			Func<TSource, TResult> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Throttle<TSource> (
			this IQbservable<TSource> source,
			TimeSpan dueTime)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Throttle<TSource> (
			this IQbservable<TSource> source,
			TimeSpan dueTime,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Throw<TResult> (Exception exception)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Throw<TResult> (
			Exception exception,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TimeInterval<TSource>> TimeInterval<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TimeInterval<TSource>> TimeInterval<TSource> (
			this IQbservable<TSource> source,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Timeout<TSource>(
			this IQbservable<TSource> source,
			DateTimeOffset dueTime)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Timeout<TSource>(
			this IQbservable<TSource> source,
			TimeSpan dueTime)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Timeout<TSource>(
			this IQbservable<TSource> source,
			DateTimeOffset dueTime,
			IQbservable<TSource> other)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Timeout<TSource>(
			this IQbservable<TSource> source,
			TimeSpan dueTime,
			IQbservable<TSource> other)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Timeout<TSource>(
			this IQbservable<TSource> source,
			DateTimeOffset dueTime,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Timeout<TSource>(
			this IQbservable<TSource> source,
			TimeSpan dueTime,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Timeout<TSource>(
			this IQbservable<TSource> source,
			DateTimeOffset dueTime,
			IQbservable<TSource> other,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Timeout<TSource>(
			this IQbservable<TSource> source,
			TimeSpan dueTime,
			IQbservable<TSource> other,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			DateTimeOffset dueTime)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			TimeSpan dueTime)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			DateTimeOffset dueTime,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			TimeSpan dueTime,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			DateTimeOffset dueTime,
			TimeSpan period)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			TimeSpan dueTime,
			TimeSpan period)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			DateTimeOffset dueTime,
			TimeSpan period,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			TimeSpan dueTime,
			TimeSpan period,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<Timestamped<TSource>> Timestamp<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<Timestamped<TSource>> Timestamp<TSource> (this IQbservable<TSource> source, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource[]> ToArray<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static Func<IQbservable<Unit>> ToAsync (this Action action)
		{ throw new NotImplementedException (); }
		
		public static Func<IQbservable<Unit>> ToAsync (this Action action, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static Func<TSource, IQbservable<Unit>> ToAsync<TSource> (this Action<TSource> action)
		{ throw new NotImplementedException (); }
		
		public static Func<IQbservable<TResult>> ToAsync<TResult> (this Func<TResult> function)
		{ throw new NotImplementedException (); }
		
		public static Func<TSource, IQbservable<Unit>> ToAsync<TSource> (this Action<TSource> action, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static Func<IQbservable<TResult>> ToAsync<TResult> (this Func<TResult> function, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static Func<T, IQbservable<TResult>> ToAsync<T, TResult> (this Func<T, TResult> function)
		{ throw new NotImplementedException (); }
		
		public static Func<T, IQbservable<TResult>> ToAsync<T, TResult> (this Func<T, TResult> function, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IDictionary<TKey, TSource>> ToDictionary<TSource, TKey> (
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IDictionary<TKey, TSource>> ToDictionary<TSource, TKey>(
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IDictionary<TKey, TElement>> ToDictionary<TSource, TKey, TElement>(
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IDictionary<TKey, TElement>> ToDictionary<TSource, TKey, TElement>(
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IEnumerable<TSource> ToEnumerable<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IEventSource<Unit> ToEvent (this IQbservable<Unit> source)
		{ throw new NotImplementedException (); }
		
		public static IEventSource<TSource> ToEvent<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IEventPatternSource<TEventArgs> ToEventPattern<TEventArgs> (
			this IQbservable<EventPattern<TEventArgs>> source)
			where TEventArgs : EventArgs
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> ToList<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IQbservable<TSource> source,
			Func<TSource, TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IQbservable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> ToQbservable<TSource> (this IEnumerable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> ToQbservable<TSource> (
			this IEnumerable<TSource> source,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Using<TSource, TResource> (
			Func<TResource> resourceFactory,
			Func<TResource, IQbservable<TSource>> QbservableFactory)
			where TResource : IDisposable
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> When<TResult> (this IEnumerable<Plan<TResult>> plans)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> When<TResult> (params Plan<TResult>[] plans)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Where<TSource> (
			this IQbservable<TSource> source,
			Func<TSource, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Where<TSource>(
			this IQbservable<TSource> source,
			Func<TSource, int, bool> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IQbservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IQbservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IQbservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			int count,
			int skip)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IQbservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IQbservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IQbservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IQbservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			int count,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IQbservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<IQbservable<TSource>> Window<TSource, TWindowClosing> (
			this IQbservable<TSource> source,
			Func<IQbservable<TWindowClosing>> windowClosingSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IQbservable<TSource>> Window<TSource, TWindowOpening, TWindowClosing> (
			this IQbservable<TSource> source,
			IQbservable<TWindowOpening> windowOpenings,
			Func<TWindowOpening, IQbservable<TWindowClosing>> windowClosingSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Zip<TFirst, TSecond, TResult> (
			this IQbservable<TFirst> first,
			IEnumerable<TSecond> second,
			Func<TFirst, TSecond, TResult> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Zip<TFirst, TSecond, TResult>(
			this IQbservable<TFirst> first,
			IQbservable<TSecond> second,
			Func<TFirst, TSecond, TResult> resultSelector)
		{ throw new NotImplementedException (); }
	}
}
