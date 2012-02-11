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
		
		struct SubjectCountContext<TSource>
		{
			public SubjectCountContext (int start, ISubject<TSource> subject)
			{
				this.start = start;
				this.subject = subject;
			}
			
			readonly int start;
			readonly ISubject<TSource> subject;
			
			public int Start {
				get { return start; }
			}
			public ISubject<TSource> Subject {
				get { return subject; }
			}
		}
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			int count,
			int skip)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (count < 0)
				throw new ArgumentOutOfRangeException ("timeSpan");
			if (skip < 0)
				throw new ArgumentOutOfRangeException ("timeShift");

			return new ColdObservableEach<IObservable<TSource>> (sub => {
			// ----
			var subjects = new List<SubjectCountContext<TSource>> ();
			int nextStart = 0;
			int current = 0;
			var dis = source.Subscribe (Observer.Create<TSource> (v => {
				if (nextStart == current) {
					var sc = new SubjectCountContext<TSource> (nextStart, new ReplaySubject<TSource> ());
					subjects.Add (sc);
					sub.OnNext (sc.Subject);
					nextStart += skip;
				}
				for (int x = 0; x < subjects.Count; ) {
					if (current - subjects [x].Start == count) {
						subjects [x].Subject.OnCompleted ();
						subjects.RemoveAt (x);
					}
					else
						subjects [x++].Subject.OnNext (v);
				}
				current++;
			}, () => { foreach (var sc in subjects) sc.Subject.OnCompleted (); sub.OnCompleted (); }));
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
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
		
		struct SubjectTimeShiftContext<TSource>
		{
			public SubjectTimeShiftContext (DateTimeOffset start, ISubject<TSource> sub)
			{
				this.start = start;
				this.sub = sub;
			}
			
			readonly DateTimeOffset start;
			readonly ISubject<TSource> sub;
			
			public DateTimeOffset Start {
				get { return start; }
			}
			public ISubject<TSource> Subject {
				get { return sub; }
			}
		}
		
		public static IObservable<IObservable<TSource>> Window<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift,
			IScheduler scheduler)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			if (timeSpan < TimeSpan.Zero)
				throw new ArgumentOutOfRangeException ("timeSpan");
			if (timeShift < TimeSpan.Zero)
				throw new ArgumentOutOfRangeException ("timeShift");

			return new ColdObservableEach<IObservable<TSource>> (sub => {
			// ----
			var subjects = new List<SubjectTimeShiftContext<TSource>> ();
			DateTimeOffset nextStart = scheduler.Now;
			var dis = new CompositeDisposable ();
			dis.Add (source.Subscribe (Observer.Create<TSource> (v => {
				if (nextStart <= scheduler.Now) {
					var lc = new SubjectTimeShiftContext<TSource> (nextStart, new ReplaySubject<TSource> ());
					subjects.Add (lc);
					sub.OnNext (lc.Subject);
					nextStart += timeShift;
					var ddis = new SingleAssignmentDisposable ();
					ddis.Disposable = scheduler.Schedule (timeSpan, () => {
						lc.Subject.OnCompleted ();
						subjects.Remove (lc);
						dis.Remove (ddis);
					});
				}
				for (int x = 0; x < subjects.Count; x++)
					// This check makes sense when the event was published *at the same time* the subject ends its life time by timeSpan.
					if (scheduler.Now - subjects [x].Start < timeSpan)
						subjects [x].Subject.OnNext (v);
			}, () => { foreach (var sc in subjects) sc.Subject.OnCompleted (); sub.OnCompleted (); })));
			return dis;
			// ----
			}, DefaultColdScheduler);
		}

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
