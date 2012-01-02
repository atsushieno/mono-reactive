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
			return Buffer<TSource, long, TBufferClosing> (source, Timer (TimeSpan.Zero), l => bufferClosingSelector ());
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
			var sub = new Subject<IList<TSource>> ();
			var l = new List<TSource> ();
			var disc = new List<IDisposable> ();
			var diso = bufferOpenings.Subscribe (Observer.Create<TBufferOpening> (
				s => {
					var closing = bufferClosingSelector (s);
					disc.Add (closing.Subscribe (c => {
						sub.OnNext (l);
						l = new List<TSource> ();
						}));
				}, () => new CompositeDisposable (disc).Dispose ()));

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
			var counter = new Subject<Unit> ();
			var sub = new Subject<IList<TSource>> ();
			var l = new List<TSource> ();
			var dis = source.Subscribe (Observer.Create<TSource> (
				v => { l.Add (v); counter.OnNext (Unit.Default); },
				ex => sub.OnError (ex),
				() => { if (l.Count > 0) sub.OnNext (l); sub.OnCompleted (); }));
			var buffer = new BufferObservable (timeSpan, counter, count, scheduler);
			var bdis = buffer.Subscribe (Observer.Create<Unit> (
				u => {
					var n = l;
					l = new List<TSource> ();
					sub.OnNext (n);
				},
				ex => sub.OnError (ex),
				() => {}));
			return new WrappedSubject<IList<TSource>> (sub, Disposable.Create (() => { dis.Dispose (); bdis.Dispose (); }));
		}

		public static IObservable<IList<TSource>> Buffer<TSource> (
			this IObservable<TSource> source,
			TimeSpan timeSpan,
			TimeSpan timeShift,
			IScheduler scheduler)
		{ throw new NotImplementedException (); }

		class BufferObservable : IObservable<Unit>
		{
			ISubject<Unit> subject = new Subject<Unit> ();
			TimeSpan interval;
			IScheduler scheduler;
			bool started, stop;
			AutoResetEvent wait;
			IDisposable schedule_disposable;
			IObservable<Unit> counter;
			int threshold_count;
			int current_count;
			
			public BufferObservable (TimeSpan interval, IObservable<Unit> counter, int count, IScheduler scheduler)
			{
				this.interval = interval;
				this.counter = counter;
				this.threshold_count = count;
				this.scheduler = scheduler;
			}
			
			public IDisposable Subscribe (IObserver<Unit> observer)
			{
				var dis = subject.Subscribe (observer);

				if (started)
					return dis;
				started = true;
				schedule_disposable = scheduler.Schedule (() => {
					wait = new AutoResetEvent (false);
					counter.Subscribe (Observer.Create<Unit> (u => { if (++current_count == threshold_count) wait.Set (); }, ex => subject.OnError (ex)));
					Tick ();
				});
				return Disposable.Create (() => {
					stop = true;
					if (wait != null)
						wait.Set ();
					dis.Dispose ();
					schedule_disposable.Dispose ();
				});
			}
			
			void SubmitNext ()
			{
				subject.OnNext (Unit.Default);
				current_count = 0;
			}
			
			void Tick ()
			{
				wait.WaitOne (interval);
				if (stop)
					return;
				SubmitNext ();
				Tick (); // repeat
			}
		}
	}
}
