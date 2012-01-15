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
		public static IObservable<IList<TSource>> Buffer<TSource, TBufferClosing> (
			this IObservable<TSource> source,
			Func<IObservable<TBufferClosing>> bufferClosingSelector)
		{
			return Buffer<TSource, int, TBufferClosing> (source, Range (0, int.MaxValue), l => bufferClosingSelector ());
		}
		
		public static IObservable<IList<TSource>> Buffer<TSource> (
			this IObservable<TSource> source,
			int count)
		{
			return source.Buffer (TimeSpan.MaxValue, count);
		}
		
		public static IObservable<IList<TSource>> Buffer<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan)
		{
			return Buffer<TSource> (source, timeSpan, Scheduler.ThreadPool);
		}
		
		public static IObservable<IList<TSource>> Buffer<TSource> (
			this IObservable<TSource> source,
			int count,
			int skip)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IList<TSource>> Buffer<TSource, TBufferOpening, TBufferClosing> (
			this IObservable<TSource> source,
			IObservable<TBufferOpening> bufferOpenings,
			Func<TBufferOpening, IObservable<TBufferClosing>> bufferClosingSelector)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (bufferOpenings == null)
				throw new ArgumentNullException ("bufferOpenings");
			if (bufferClosingSelector == null)
				throw new ArgumentNullException ("bufferClosingSelector");
			
			var sub = new Subject<IList<TSource>> ();
			var l = new List<TSource> ();
			var disc = new CompositeDisposable ();
			var diso = bufferOpenings.Subscribe (Observer.Create<TBufferOpening> (
				s => {
					var closing = bufferClosingSelector (s);
					disc.Add (closing.Subscribe (c => {
						sub.OnNext (l);
						l = new List<TSource> ();
						}));
				}, () => disc.Dispose ()));

			var dis = source.Subscribe (
				s => l.Add (s), ex => sub.OnError (ex), () => {
					if (l.Count > 0)
						sub.OnNext (l);
					sub.OnCompleted ();
				}
				);

			return new WrappedSubject<IList<TSource>> (sub, new CompositeDisposable (dis, diso));
		}

		public static IObservable<IList<TSource>> Buffer<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			int count)
		{
			return Buffer<TSource> (source, timeSpan, count, Scheduler.ThreadPool);
		}

		public static IObservable<IList<TSource>> Buffer<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			IScheduler scheduler)
		{
			return source.Buffer (timeSpan, int.MaxValue, scheduler);
		}

		public static IObservable<IList<TSource>> Buffer<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift)
		{
			return Buffer<TSource> (source, timeSpan, timeShift, Scheduler.ThreadPool);
		}

		public static IObservable<IList<TSource>> Buffer<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			int count,
			IScheduler scheduler)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			
			return new ColdObservableEach<IList<TSource>> (sub => {
			// ----
			var counter = new Subject<Unit> ();
			var l = new List<TSource> ();
			var dis = new CompositeDisposable ();
			dis.Add (source.Subscribe (Observer.Create<TSource> (
				v => { l.Add (v); counter.OnNext (Unit.Default); },
				ex => sub.OnError (ex),
				() => { if (l.Count > 0) sub.OnNext (l); sub.OnCompleted (); })));
			var buffer = new TimeOrCountObservable (timeSpan, counter, count, scheduler);
			dis.Add (buffer.Subscribe (Observer.Create<Unit> (
				u => {
					var n = l;
					l = new List<TSource> ();
					sub.OnNext (n);
				},
				ex => sub.OnError (ex),
				() => {})));
			return dis;
			// ----
			}, scheduler);
		}

		public static IObservable<IList<TSource>> Buffer<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }
	}
}
