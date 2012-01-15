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
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			int count)
		{
			return source.Window (TimeSpan.MaxValue, count);
		}
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan)
		{
			return source.Window (timeSpan, int.MaxValue);
		}
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			int count,
			int skip)
		{ throw new NotImplementedException (); }
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			int count)
		{
			return source.Window (timeSpan, count, Scheduler.ThreadPool);
		}
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			IScheduler scheduler)
		{
			return source.Window (timeSpan, int.MaxValue, scheduler);
		}
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift)
		{
			return source.Window (timeSpan, timeShift, Scheduler.ThreadPool);
		}
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			int count,
			IScheduler scheduler)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			
			return new ColdObservableEach<IObservable<TSource>> (sub => {
			// ----
			var counter = new Subject<Unit> ();
			var l = new Subject<TSource> ();
			var dis = new CompositeDisposable ();
			dis.Add (source.Subscribe (
				v => { l.OnNext (v); counter.OnNext (Unit.Default); },
				ex => sub.OnError (ex),
				() => { sub.OnNext (l); sub.OnCompleted (); }));
			var buffer = new TimeOrCountObservable (timeSpan, counter, count, scheduler);
			dis.Add (buffer.Subscribe (u => {
					var n = l;
					l = new Subject<TSource> ();
					sub.OnNext (n);
				}, ex => sub.OnError (ex), () => {}));
			return dis;
			// ----
			}, scheduler);
		}
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		public static IObservable<IObservable<TSource>> Window<TSource, TWindowClosing> (
			this IObservable<TSource> source,
			Func<IObservable<TWindowClosing>> windowClosingSelector)
		{
			return Window<TSource, int, TWindowClosing> (source, Range (0, int.MaxValue), l => windowClosingSelector ());
		}
		
		public static IObservable<IObservable<TSource>> Window<TSource, TWindowOpening, TWindowClosing> (
			this IObservable<TSource> source,
			IObservable<TWindowOpening> windowOpenings,
			Func<TWindowOpening, IObservable<TWindowClosing>> windowClosingSelector)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (windowOpenings == null)
				throw new ArgumentNullException ("windowOpenings");
			if (windowClosingSelector == null)
				throw new ArgumentNullException ("windowClosingSelector");
			
			return new ColdObservableEach<IObservable<TSource>> (sub => {
			// ----
			var l = new Subject<TSource> ();
			var dis = new CompositeDisposable ();
			var disClosings = new CompositeDisposable ();
			dis.Add (windowOpenings.Subscribe (Observer.Create<TWindowOpening> (
				s => {
					var closing = windowClosingSelector (s);
					disClosings.Add (closing.Subscribe (c => {
						sub.OnNext (l);
						l = new Subject<TSource> ();
						}));
				}, () => disClosings.Dispose ())));

			dis.Add (source.Subscribe (
				s => l.OnNext (s), ex => sub.OnError (ex), () => {
					sub.OnNext (l);
					sub.OnCompleted ();
				}
				));

			return dis;
			// ----
			}, DefaultColdScheduler);
		}
	}
}
