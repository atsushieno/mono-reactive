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
	public static partial class Qbservable
	{
		public static IQbservableProvider Provider {
			get { throw new NotImplementedException (); }
		}
		
		public static IQbservable<TSource> Aggregate<TSource> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TSource, TSource>> accumulator)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TAccumulate> Aggregate<TSource, TAccumulate> (
			this IQbservable<TSource> source,
			TAccumulate seed,
			Expression<Func<TAccumulate, TSource, TAccumulate>> accumulator)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> All<TSource> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, bool>> predicate)
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
		
		public static QueryablePattern<TLeft, TRight> And<TLeft, TRight> (this IQbservable<TLeft> left, IObservable<TRight> right)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> Any<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> Any<TSource> (this IQbservable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TSource> AsObservable<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> AsQbservable<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<decimal> Average (this IQbservable<decimal> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double> Average (this IQbservable<double> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double> Average (this IQbservable<int> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double> Average (this IQbservable<long> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<float> Average (this IQbservable<float> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<decimal?> Average (this IQbservable<decimal?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double?> Average (this IQbservable<double?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double?> Average (this IQbservable<int?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<double?> Average (this IQbservable<long?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<float?> Average (this IQbservable<float?> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> Buffer<TSource, TBufferClosing> (
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TBufferClosing>>> bufferClosingSelector)
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
			Expression<Func<TBufferOpening, IObservable<TBufferClosing>>> bufferClosingSelector)
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

		public static IQbservable<TSource> Catch<TSource> (
			this IQbservableProvider provider,
			IEnumerable<IObservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Catch<TSource> (
			this IQbservableProvider provider,
			params IObservable<TSource> [] sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Catch<TSource, TException> (
			this IQbservable<TSource> source,
			Expression<Func<TException, IObservable<TSource>>> handler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Catch<TSource> (
			this IQbservable<TSource> first,
			IObservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> CombineLatest<TFirst, TSecond, TResult> (
			this IQbservable<TFirst> first,
			IObservable<TSecond> second,
			Expression<Func<TFirst, TSecond, TResult>> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Concat<TSource> (this IQbservableProvider provider, IEnumerable<IObservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Concat<TSource> (this IQbservable<IObservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Concat<TSource> (this IQbservableProvider provider, params IObservable<TSource> [] sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Concat<TSource> (this IQbservable<TSource> first, IObservable<TSource> second)
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
		
		public static IQbservable<TSource> Create<TSource> (this IQbservableProvider provider, Expression<Func<IObserver<TSource>, Action>> subscribe)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Create<TSource> (this IQbservableProvider provider, Expression<Func<IObserver<TSource>, IDisposable>> subscribe)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> DefaultIfEmpty<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> DefaultIfEmpty<TSource> (this IQbservable<TSource> source, TSource defaultValue)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TValue> Defer<TValue> (this IQbservableProvider provider, Expression<Func<IObservable<TValue>>> observableFactory)
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
			Expression<Func<TSource, TKey>> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Distinct<TSource, TKey> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
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
			Expression<Func<TSource, TKey>> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> DistinctUntilChanged<TSource, TKey> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Do<TSource> (
			this IQbservable<TSource> source,
			Expression<Action<TSource>> onNext)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Do<TSource> (
			this IQbservable<TSource> source,
			IObserver<TSource> observer)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Do<TSource> (
			this IQbservable<TSource> source,
			Expression<Action<TSource>> onNext,
			Expression<Action<Exception>> onError)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Do<TSource> (
			this IQbservable<TSource> source,
			Expression<Action<TSource>> onNext,
			Expression<Action> onCompleted)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> Do<TSource> (
			this IQbservable<TSource> source,
			Expression<Action<TSource>> onNext,
			Expression<Action<Exception>> onError,
			Expression<Action> onCompleted)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> ElementAt<TSource> (this IQbservable<TSource> source, int index)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> ElementAtOrDefault<TSource> (this IQbservable<TSource> source, int index)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Empty<TResult> (this IQbservableProvider provider)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Empty<TResult> (this IQbservableProvider provider, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Finally<TSource> (this IQbservable<TSource> source, Expression<Action> finallyAction)
		{ throw new NotImplementedException (); }

		public static Func<IQbservable<Unit>> FromAsyncPattern(
			this IQbservableProvider provider,
			Expression<Func<AsyncCallback, Object, IAsyncResult>> begin,
			Expression<Action<IAsyncResult>> end)
		{ throw new NotImplementedException (); }
		
		public static Func<IQbservable<TResult>> FromAsyncPattern<TResult> (
			this IQbservableProvider provider,
			Expression<Func<AsyncCallback, Object, IAsyncResult>> begin,
			Expression<Func<IAsyncResult, TResult>> end)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TEventArgs> FromEvent<TEventArgs> (
			this IQbservableProvider provider,
			Expression<Action<Action<TEventArgs>>> addHandler,
			Expression<Action<Action<TEventArgs>>> removeHandler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<Unit> FromEvent (
			this IQbservableProvider provider,
			Expression<Action<Action>> addHandler,
			Expression<Action<Action>> removeHandler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TEventArgs> FromEvent<TDelegate, TEventArgs> (
			this IQbservableProvider provider,
			Expression<Action<TDelegate>> addHandler,
			Expression<Action<TDelegate>> removeHandler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TEventArgs> FromEvent<TDelegate, TEventArgs> (
			this IQbservableProvider provider,
			Expression<Func<Action<TEventArgs>, TDelegate>> conversion,
			Expression<Action<TDelegate>> addHandler,
			Expression<Action<TDelegate>> removeHandler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs> (
			this IQbservableProvider provider,
			Expression<Action<EventHandler<TEventArgs>>> addHandler,
			Expression<Action<EventHandler<TEventArgs>>> removeHandler)
			where TEventArgs : EventArgs
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<EventArgs>> FromEventPattern (
			this IQbservableProvider provider,
			Expression<Action<EventHandler>> addHandler,
			Expression<Action<EventHandler>> removeHandler)
		{ throw new NotImplementedException (); }

		public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TDelegate, TEventArgs> (
			this IQbservableProvider provider,
			Expression<Action<TDelegate>> addHandler,
			Expression<Action<TDelegate>> removeHandler)
			where TEventArgs : EventArgs
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<EventArgs>> FromEventPattern (
			this IQbservableProvider provider,
			object target,
			string eventName)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs> (
			this IQbservableProvider provider,
			object target,
			string eventName)
			where TEventArgs : EventArgs
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<EventArgs>> FromEventPattern (
			this IQbservableProvider provider,
			Type type,
			string eventName)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs> (
			this IQbservableProvider provider,
			Type type,
			string eventName)
			where TEventArgs : EventArgs
		{ throw new NotImplementedException (); }
		
		public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TDelegate, TEventArgs> (
			this IQbservableProvider provider,
			Expression<Func<EventHandler<TEventArgs>, TDelegate>> conversion,
			Expression<Action<TDelegate>> addHandler,
			Expression<Action<TDelegate>> removeHandler)
			where TEventArgs : EventArgs
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			this IQbservableProvider provider,
			TState initialState,
			Expression<Func<TState, bool>> condition,
			Expression<Func<TState, TState>> iterate,
			Expression<Func<TState, TResult>> resultSelector)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			this IQbservableProvider provider,
			TState initialState,
			Expression<Func<TState, bool>> condition,
			Expression<Func<TState, TState>> iterate,
			Expression<Func<TState, TResult>> resultSelector,
			Expression<Func<TState, DateTimeOffset>> timeSelector)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			this IQbservableProvider provider,
			TState initialState,
			Expression<Func<TState, bool>> condition,
			Expression<Func<TState, TState>> iterate,
			Expression<Func<TState, TResult>> resultSelector,
			Expression<Func<TState, TimeSpan>> timeSelector)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			this IQbservableProvider provider,
			TState initialState,
			Expression<Func<TState, bool>> condition,
			Expression<Func<TState, TState>> iterate,
			Expression<Func<TState, TResult>> resultSelector,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			this IQbservableProvider provider,
			TState initialState,
			Expression<Func<TState, bool>> condition,
			Expression<Func<TState, TState>> iterate,
			Expression<Func<TState, TResult>> resultSelector,
			Expression<Func<TState, DateTimeOffset>> timeSelector,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Generate<TState, TResult> (
			this IQbservableProvider provider,
			TState initialState,
			Expression<Func<TState, bool>> condition,
			Expression<Func<TState, TState>> iterate,
			Expression<Func<TState, TResult>> resultSelector,
			Expression<Func<TState, TimeSpan>> timeSelector,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<IGroupedObservable<TKey, TSource>> GroupBy<TSource, TKey> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TSource>> GroupBy<TSource, TKey> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TElement>> GroupBy<TSource, TKey, TElement> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			Expression<Func<TSource, TElement>> elementSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TElement>> GroupBy<TSource, TKey, TElement> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			Expression<Func<TSource, TElement>> elementSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			Expression<Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>>> durationSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			Expression<Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>>> durationSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			Expression<Func<TSource, TElement>> elementSelector,
			Expression<Func<IGroupedObservable<TKey, TElement>, IObservable<TDuration>>> durationSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			Expression<Func<TSource, TElement>> elementSelector,
			Expression<Func<IGroupedObservable<TKey, TElement>, IObservable<TDuration>>> durationSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> GroupJoin<TLeft, TRight, TLeftDuration, TRightDuration, TResult> (
			this IQbservable<TLeft> left,
			IObservable<TRight> right,
			Expression<Func<TLeft, IObservable<TLeftDuration>>> leftDurationSelector,
			Expression<Func<TRight, IObservable<TRightDuration>>> rightDurationSelector,
			Expression<Func<TLeft, IObservable<TRight>, TResult>> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> IgnoreElements<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Interval (this IQbservableProvider provider, TimeSpan period)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Interval (
			this IQbservableProvider provider,
			TimeSpan period,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Join<TLeft, TRight, TLeftDuration, TRightDuration, TResult>(
			this IQbservable<TLeft> left,
			IObservable<TRight> right,
			Expression<Func<TLeft, IObservable<TLeftDuration>>> leftDurationSelector,
			Expression<Func<TRight, IObservable<TRightDuration>>> rightDurationSelector,
			Expression<Func<TLeft, TRight, TResult>> resultSelector)
		{ throw new NotImplementedException (); }

		public static IQueryable<TSource> Latest<TSource> (this IQbservable<TSource> source)
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
		
		public static IQbservable<IList<TSource>> MaxBy<TSource, TKey> (this IQbservable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> MaxBy<TSource, TKey> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			IComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (this IQbservableProvider provider, IEnumerable<IObservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (this IQbservable<IObservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (this IQbservableProvider provider, params IObservable<TSource>[] sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IQbservableProvider provider,
			IEnumerable<IObservable<TSource>> sources,
			int maxConcurrent)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IQbservableProvider provider,
			IEnumerable<IObservable<TSource>> sources,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IQbservable<IObservable<TSource>> sources,
			int maxConcurrent)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IQbservable<TSource> first,
			IObservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IQbservableProvider provider,
			IScheduler scheduler,
			params IObservable<TSource>[] sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IQbservableProvider provider,
			IEnumerable<IObservable<TSource>> sources,
			int maxConcurrent,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Merge<TSource> (
			this IQbservable<TSource> first,
			IObservable<TSource> second,
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
		
		public static IQbservable<IList<TSource>> MinBy<TSource, TKey> (this IQbservable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IList<TSource>> MinBy<TSource, TKey> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			IComparer<TKey> comparer)
		{ throw new NotImplementedException (); }

		public static IQueryable<TSource> MostRecent<TSource> (
			this IQbservable<TSource> source,
			TSource initialValue)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Multicast<TSource, TIntermediate, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<ISubject<TSource, TIntermediate>>> subjectSelector,
			Expression<Func<IObservable<TIntermediate>, IObservable<TResult>>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Never<TResult> (this IQbservableProvider provider)
		{ throw new NotImplementedException (); }
		
		public static IQueryable<TSource> Next<TSource> (this IQbservable<TSource> source)
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
		
		public static IQbservable<TSource> OnErrorResumeNext<TSource> (this IQbservableProvider provider, IEnumerable<IObservable<TSource>> sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> OnErrorResumeNext<TSource> (this IQbservableProvider provider, params IObservable<TSource>[] sources)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> OnErrorResumeNext<TSource> (
			this IQbservable<TSource> first,
			IObservable<TSource> second)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Publish<TSource, TResult>(
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Publish<TSource, TResult>(
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
			TSource initialValue)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> PublishLast<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int> Range (this IQbservableProvider provider, int start, int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<int> Range (this IQbservableProvider provider, int start, int count, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> RefCount<TSource> (
			this IQbservableProvider provider,
			IConnectableObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Repeat<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Repeat<TSource> (this IQbservable<TSource> source, int repeatCount)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Repeat<TResult> (this IQbservableProvider provider, TResult value)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Repeat<TResult> (this IQbservableProvider provider, TResult value, int repeatCount)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Repeat<TResult> (this IQbservableProvider provider, TResult value, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Repeat<TResult> (
			this IQbservableProvider provider,
			TResult value,
			int repeatCount,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
			int bufferSize)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
			TimeSpan window)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
			int bufferSize,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
			int bufferSize,
			TimeSpan window)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
			TimeSpan window,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Replay<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
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
		
		public static IQbservable<TResult> Return<TResult> (
			this IQbservableProvider provider,
			TResult value)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Return<TResult> (
			this IQbservableProvider provider,
			TResult value, IScheduler scheduler)
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
			IObservable<TSample> sampler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Scan<TSource> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TSource, TSource>> accumulator)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TAccumulate> Scan<TSource, TAccumulate> (
			this IQbservable<TSource> source,
			TAccumulate seed,
			Expression<Func<TAccumulate, TSource, TAccumulate>> accumulator)
		{ throw new NotImplementedException (); }

		public static IQbservable<TResult> Select<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Select<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, int, TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> SelectMany<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, IEnumerable<TResult>>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> SelectMany<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, IObservable<TResult>>> selector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TOther> SelectMany<TSource, TOther> (
			this IQbservable<TSource> source,
			IObservable<TOther> other)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> SelectMany<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, IObservable<TResult>>> onNext,
			Expression<Func<Exception, IObservable<TResult>>> onError,
			Expression<Func<IObservable<TResult>>> onCompleted)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> SelectMany<TSource, TCollection, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, IEnumerable<TCollection>>> collectionSelector,
			Expression<Func<TSource, TCollection, TResult>> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> SelectMany<TSource, TCollection, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, IObservable<TCollection>>> collectionSelector,
			Expression<Func<TSource, TCollection, TResult>> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> SequenceEqual<TSource> (
			this IQbservable<TSource> first,
			IObservable<TSource> second)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<bool> SequenceEqual<TSource> (
			this IQbservable<TSource> first,
			IObservable<TSource> second,
			IEqualityComparer<TSource> comparer)
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
			IObservable<TOther> other)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> SkipWhile<TSource> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, bool>> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> SkipWhile<TSource> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, int, bool>> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<Unit> Start (
			this IQbservableProvider provider,
			Expression<Action> action)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<Unit> Start (
			this IQbservableProvider provider,
			Expression<Action> action,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Start<TSource> (
			this IQbservableProvider provider,
			Expression<Func<TSource>> function)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Start<TSource> (
			this IQbservableProvider provider,
			Expression<Func<TSource>> function,
			IScheduler scheduler)
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

		public static IQbservable<TSource> Switch<TSource> (this IQbservable<IObservable<TSource>> sources)
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
		
		public static IQbservable<TSource> Take<TSource> (
			this IQbservable<TSource> source,
			int count,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> TakeLast<TSource> (
			this IQbservable<TSource> source,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> TakeUntil<TSource, TOther> (
			this IQbservable<TSource> source,
			IObservable<TOther> other)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> TakeWhile<TSource> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, bool>> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> TakeWhile<TSource> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, int, bool>> predicate)
		{ throw new NotImplementedException (); }
		
		public static QueryablePlan<TResult> Then<TSource, TResult> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TResult>> selector)
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
		
		public static IQbservable<TResult> Throw<TResult> (
			this IQbservableProvider provider,
			Exception exception)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Throw<TResult> (
			this IQbservableProvider provider,
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
			IObservable<TSource> other)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Timeout<TSource>(
			this IQbservable<TSource> source,
			TimeSpan dueTime,
			IObservable<TSource> other)
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
			IObservable<TSource> other,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Timeout<TSource>(
			this IQbservable<TSource> source,
			TimeSpan dueTime,
			IObservable<TSource> other,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			this IQbservableProvider provider,
			DateTimeOffset dueTime)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			this IQbservableProvider provider,
			TimeSpan dueTime)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			this IQbservableProvider provider,
			DateTimeOffset dueTime,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			this IQbservableProvider provider,
			TimeSpan dueTime,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			this IQbservableProvider provider,
			DateTimeOffset dueTime,
			TimeSpan period)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			this IQbservableProvider provider,
			TimeSpan dueTime,
			TimeSpan period)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			this IQbservableProvider provider,
			DateTimeOffset dueTime,
			TimeSpan period,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<long> Timer (
			this IQbservableProvider provider,
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
		
		public static Func<IQbservable<Unit>> ToAsync (
			this IQbservableProvider provider,
			Expression<Action> action)
		{ throw new NotImplementedException (); }
		
		public static Func<IQbservable<Unit>> ToAsync (
			this IQbservableProvider provider,
			Expression<Action> action,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static Func<TSource, IQbservable<Unit>> ToAsync<TSource> (
			this IQbservableProvider provider,
			Expression<Action<TSource>> action)
		{ throw new NotImplementedException (); }
		
		public static Func<IQbservable<TResult>> ToAsync<TResult> (
			this IQbservableProvider provider,
			Expression<Func<TResult>> function)
		{ throw new NotImplementedException (); }
		
		public static Func<TSource, IQbservable<Unit>> ToAsync<TSource> (
			this IQbservableProvider provider,
			Expression<Action<TSource>> action,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static Func<IQbservable<TResult>> ToAsync<TResult> (
			this IQbservableProvider provider,
			Expression<Func<TResult>> function,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static Func<TSource, IQbservable<TResult>> ToAsync<TSource, TResult> (
			this IQbservableProvider provider,
			Expression<Func<TSource, TResult>> function)
		{ throw new NotImplementedException (); }
		
		public static Func<TSource, IQbservable<TResult>> ToAsync<TSource, TResult> (
			this IQbservableProvider provider,
			Expression<Func<TSource, TResult>> function,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IDictionary<TKey, TSource>> ToDictionary<TSource, TKey> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IDictionary<TKey, TSource>> ToDictionary<TSource, TKey>(
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IDictionary<TKey, TElement>> ToDictionary<TSource, TKey, TElement>(
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			Expression<Func<TSource, TElement>> elementSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IDictionary<TKey, TElement>> ToDictionary<TSource, TKey, TElement>(
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			Expression<Func<TSource, TElement>> elementSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> ToQbservable<TSource> (this IQueryable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> ToQbservable<TSource> (this IQueryable<TSource> source, IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQueryable<TSource> ToQueryable<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }

		public static IQbservable<IList<TSource>> ToList<TSource> (this IQbservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<ILookup<TKey, TSource>> ToLookup<TSource, TKey>(
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<ILookup<TKey, TSource>> ToLookup<TSource, TKey>(
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }

		public static IQbservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			Expression<Func<TSource, TElement>> elementSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
			this IQbservable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector,
			Expression<Func<TSource, TElement>> elementSelector,
			IEqualityComparer<TKey> comparer)
		{ throw new NotImplementedException (); }

		public static IQbservable<TSource> ToObservable<TSource> (
			this IQbservableProvider provider,
			IEnumerable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> ToObservable<TSource> (
			this IQbservableProvider provider,
			IEnumerable<TSource> source,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Using<TSource, TResource> (
			this IQbservableProvider provider,
			Expression<Func<TResource>> resourceFactory,
			Expression<Func<TResource, IObservable<TSource>>> observableFactory)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> When<TResult> (
			this IQbservableProvider provider,
			IEnumerable<QueryablePlan<TResult>> plans)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> When<TResult> (
			this IQbservableProvider provider,
			params QueryablePlan<TResult>[] plans)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Where<TSource> (
			this IQbservable<TSource> source,
			Expression<Func<TSource, bool>> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TSource> Where<TSource>(
			this IQbservable<TSource> source,
			Expression<Func<TSource, int, bool>> predicate)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IObservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IObservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IObservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			int count,
			int skip)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IObservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			int count)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IObservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IObservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IObservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			int count,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IObservable<TSource>> Window<TSource> (
			this IQbservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IQbservable<IObservable<TSource>> Window<TSource, TWindowClosing> (
			this IQbservable<TSource> source,
			Expression<Func<IObservable<TWindowClosing>>> windowClosingSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<IObservable<TSource>> Window<TSource, TWindowOpening, TWindowClosing> (
			this IQbservable<TSource> source,
			IObservable<TWindowOpening> windowOpenings,
			Expression<Func<TWindowOpening, IObservable<TWindowClosing>>> windowClosingSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Zip<TFirst, TSecond, TResult> (
			this IQbservable<TFirst> first,
			IEnumerable<TSecond> second,
			Expression<Func<TFirst, TSecond, TResult>> resultSelector)
		{ throw new NotImplementedException (); }
		
		public static IQbservable<TResult> Zip<TFirst, TSecond, TResult>(
			this IQbservable<TFirst> first,
			IObservable<TSecond> second,
			Expression<Func<TFirst, TSecond, TResult>> resultSelector)
		{ throw new NotImplementedException (); }
	}
}
