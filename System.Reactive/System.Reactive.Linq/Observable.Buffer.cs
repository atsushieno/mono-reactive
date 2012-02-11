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
		
		struct ListCountContext<TSource>
		{
			public ListCountContext (int start, List<TSource> list)
			{
				this.start = start;
				this.list = list;
			}
			
			readonly int start;
			readonly List<TSource> list;
			
			public int Start {
				get { return start; }
			}
			public List<TSource> List {
				get { return list; }
			}
		}
		
		public static IObservable<IList<TSource>> Buffer<TSource> (
			this IObservable<TSource> source,
			int count,
			int skip)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			return new ColdObservableEach<IList<TSource>> (sub => {
			// ----
			var lists = new List<ListCountContext<TSource>> ();
			int current = 0;
			int nextStart = 0;
			Action cleanup = () => { foreach (var lc in lists) sub.OnNext (lc.List); };
			var dis = source.Subscribe (Observer.Create<TSource> (v => {
				if (current == nextStart) {
					var lc = new ListCountContext<TSource> (current, new List<TSource> ());
					lists.Add (lc);
					nextStart += skip;
				}
				for (int x = 0; x < lists.Count; ) {
					if (current - lists [x].Start == count) {
						sub.OnNext (lists [x].List);
						lists.RemoveAt (x);
					}
					else
						lists [x++].List.Add (v);
				}
				current++;
}, ex => { cleanup (); throw ex; }, () => { cleanup (); sub.OnCompleted (); }));
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
		
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
			
			return new ColdObservableEach<IList<TSource>> (sub => {
			// ----
			var l = new List<TSource> ();
			var dis = new CompositeDisposable ();
			var disClosing = new CompositeDisposable ();
			dis.Add (bufferOpenings.Subscribe (Observer.Create<TBufferOpening> (
				s => {
					var closing = bufferClosingSelector (s);
					disClosing.Add (closing.Subscribe (c => {
						sub.OnNext (l);
						l = new List<TSource> ();
						}));
				}, () => disClosing.Dispose ())));

			dis.Add (source.Subscribe (
				s => l.Add (s), ex => sub.OnError (ex), () => {
					if (l.Count > 0)
						sub.OnNext (l);
					sub.OnCompleted ();
				}));
			return dis;
			// ----
			}, DefaultColdScheduler);
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
			var buffer = new TimeOrCountObservable (timeSpan, counter, count, scheduler);
			dis.Add (buffer.Subscribe (
				u => {
					var n = l;
					l = new List<TSource> ();
					sub.OnNext (n);
				},
				ex => sub.OnError (ex),
				() => {}));
			dis.Add (source.Subscribe (
				v => { l.Add (v); counter.OnNext (Unit.Default); },
				ex => sub.OnError (ex),
				() => { if (l.Count > 0) sub.OnNext (l); sub.OnCompleted (); }));
			return dis;
			// ----
			}, scheduler);
		}
		
		struct ListTimeShiftContext<TSource>
		{
			public ListTimeShiftContext (DateTimeOffset start, List<TSource> list)
			{
				this.start = start;
				this.list = list;
			}
			
			readonly DateTimeOffset start;
			readonly List<TSource> list;
			
			public DateTimeOffset Start {
				get { return start; }
			}
			public List<TSource> List {
				get { return list; }
			}
		}

		public static IObservable<IList<TSource>> Buffer<TSource> (
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

			return new ColdObservableEach<IList<TSource>> (sub => {
			// ----
			var lists = new List<ListTimeShiftContext<TSource>> ();
			Action cleanup = () => { foreach (var lc in lists) sub.OnNext (lc.List); };
			DateTimeOffset nextStart = scheduler.Now;
			var dis = source.Subscribe (Observer.Create<TSource> (v => {
				if (nextStart <= scheduler.Now) {
					var lc = new ListTimeShiftContext<TSource> (nextStart, new List<TSource> ());
					lists.Add (lc);
					nextStart += timeShift;
				}
				for (int x = 0; x < lists.Count; ) {
					if (scheduler.Now - lists [x].Start >= timeSpan) {
						sub.OnNext (lists [x].List);
						lists.RemoveAt (x);
					}
					else
						lists [x++].List.Add (v);
				}
}, ex => { cleanup (); throw ex; }, () => { cleanup (); sub.OnCompleted (); }));
			return dis;
			// ----
			}, DefaultColdScheduler);
		}
	}
}
