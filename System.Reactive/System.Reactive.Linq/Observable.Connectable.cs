using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		class ConnectableObservable<TSource, TIntermediate, TResult> : IConnectableObservable<TResult>
		{
			IObservable<TSource> source;
			ISubject<TSource, TResult> sub;
			Func<ISubject<TSource, TIntermediate>> subject_creator;
			Func<IObservable<TIntermediate>, IObservable<TResult>> result_selector;
			
			// FIXME: is it safe to leave created subject not disposed? (though also note that Multicast() does not *create* returned subject; it's just passing the argument)
			public ConnectableObservable (IObservable<TSource> source, Func<ISubject<TSource, TIntermediate>> subjectCreator, Func<IObservable<TIntermediate>, IObservable<TResult>> resultSelector)
			{
				this.source = source;
				this.subject_creator = subjectCreator;
				this.result_selector = resultSelector;
			}

			bool connected;
			List<IObserver<TResult>> observers = new List<IObserver<TResult>> ();
			CompositeDisposable disposables;

			public IDisposable Subscribe (IObserver<TResult> observer)
			{
				observers.Add (observer);
				var dis = new SingleAssignmentDisposable ();
				if (connected) {
					dis.Disposable = sub.Subscribe (observer);
					disposables.Add (dis);
				}
				return Disposable.Create (() => { observers.Remove (observer); disposables.Remove (dis); dis.Dispose (); });
			}

			public IDisposable Connect ()
			{
				if (connected)
					return disposables;

				connected = true;
				var isub = subject_creator ();
				sub = Subject.Create (isub, result_selector (isub));
				disposables = new CompositeDisposable ();
				foreach (var o in observers)
					disposables.Add (sub.Subscribe (o));
				disposables.Add (source.Subscribe (sub));
				disposables.Add (Disposable.Create (() =>  {
					connected = false;
					// commented out. This is not necessary, and if any subscription is disposed *after* this disposable is disposed, this causes NRE.
					// this.disposables = null; // clean up itself in the final stage
				}));
				return disposables;
			}
		}
		
		public static IConnectableObservable<TSource> Publish<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			
			return new ConnectableObservable<TSource, TSource, TSource> (source, () => new Subject<TSource> (), o => o);
		}
		
		public static IConnectableObservable<TSource> Publish<TSource> (
			this IObservable<TSource> source,
			TSource initialValue)
		{
			return new ConnectableObservable<TSource, TSource, TSource> (source, () => new BehaviorSubject<TSource> (initialValue), o => o);
		}
		
		public static IObservable<TResult> Publish<TSource, TResult>(
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (selector == null)
				throw new ArgumentNullException ("selector");
			
			return Multicast<TSource, TSource, TResult> (source, () => new Subject<TSource> (), selector);
		}
		
		public static IObservable<TResult> Publish<TSource, TResult>(
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			TSource initialValue)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (selector == null)
				throw new ArgumentNullException ("selector");
			
			return Multicast<TSource, TSource, TResult> (source, () => new BehaviorSubject<TSource> (initialValue), selector);
		}
		
		public static IConnectableObservable<TSource> PublishLast<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			
			return new ConnectableObservable <TSource, TSource, TSource> (source, () => new AsyncSubject<TSource> (), o => o);
		}
		
		public static IObservable<TResult> PublishLast<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			
			return Multicast<TSource, TSource, TResult> (source, () => new AsyncSubject<TSource> (), selector);
		}
		
		public static IConnectableObservable<TResult> Multicast<TSource, TResult> (
			this IObservable<TSource> source,
			ISubject<TSource, TResult> subject)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			
			return new ConnectableObservable<TSource, TResult, TResult> (source, () => subject, o => o);
		}
		
		public static IObservable<TResult> Multicast<TSource, TIntermediate, TResult> (
			this IObservable<TSource> source,
			Func<ISubject<TSource, TIntermediate>> subjectSelector,
			Func<IObservable<TIntermediate>, IObservable<TResult>> selector)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (subjectSelector == null)
				throw new ArgumentNullException ("subjectSelector");
			if (selector == null)
				throw new ArgumentNullException ("selector");
			
			return new ConnectableObservable<TSource, TIntermediate, TResult> (source, subjectSelector, selector);
		}
		
		class RefCountObservable<T> : IObservable<T>
		{
			IConnectableObservable<T> source;
			
			public RefCountObservable (IConnectableObservable<T> source)
			{
				this.source = source;
			}
			
			int count;
			IDisposable connection_disposable;
			
			public IDisposable Subscribe (IObserver<T> observer)
			{
				source.Subscribe (observer);
				if (count++ == 0)
					connection_disposable = source.Connect ();
				return Disposable.Create (() => { if (--count == 0) connection_disposable.Dispose (); });
			}
		}
		
		public static IObservable<TSource> RefCount<TSource> (this IConnectableObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			
			return new RefCountObservable<TSource> (source);
		}

		static IConnectableObservable<TSource> Replay<TSource> (
			this IObservable<TSource> source, Func<ReplaySubject<TSource>> createSubject)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			
			return new ConnectableObservable<TSource, TSource, TSource> (source, createSubject, o => o);
		}

		public static IConnectableObservable<TSource> Replay<TSource> (
			this IObservable<TSource> source)
		{
			return Replay<TSource> (source, () => new ReplaySubject<TSource> ());
		}

		public static IConnectableObservable<TSource> Replay<TSource> (
			this IObservable<TSource> source,
			int bufferSize)
		{
			return Replay<TSource> (source, () => new ReplaySubject<TSource> (bufferSize));
		}

		public static IConnectableObservable<TSource> Replay<TSource> (
			this IObservable<TSource> source,
			IScheduler scheduler)
		{
			return Replay<TSource> (source, () => new ReplaySubject<TSource> (scheduler));
		}

		public static IConnectableObservable<TSource> Replay<TSource> (
			this IObservable<TSource> source,
			TimeSpan window)
		{
			return Replay<TSource> (source, () => new ReplaySubject<TSource> (window));
		}

		public static IConnectableObservable<TSource> Replay<TSource> (
			this IObservable<TSource> source,
			int bufferSize,
			IScheduler scheduler)
		{
			return Replay<TSource> (source, () => new ReplaySubject<TSource> (bufferSize, scheduler));
		}

		public static IConnectableObservable<TSource> Replay<TSource> (
			this IObservable<TSource> source,
			int bufferSize,
			TimeSpan window)
		{
			return Replay<TSource> (source, () => new ReplaySubject<TSource> (bufferSize, window));
		}

		public static IConnectableObservable<TSource> Replay<TSource> (
			this IObservable<TSource> source,
			TimeSpan window,
			IScheduler scheduler)
		{
			return Replay<TSource> (source, () => new ReplaySubject<TSource> (window, scheduler));
		}

		public static IConnectableObservable<TSource> Replay<TSource> (
			this IObservable<TSource> source,
			int bufferSize,
			TimeSpan window,
			IScheduler scheduler)
		{
			return Replay<TSource> (source, () => new ReplaySubject<TSource> (bufferSize, window, scheduler));
		}

		static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			Func<ReplaySubject<TSource>> createSubject)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			
			return Multicast<TSource, TSource, TResult> (source, createSubject, selector);
		}
		
		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TSource> ());
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TSource> (bufferSize));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			IScheduler scheduler)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TSource> (scheduler));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			TimeSpan window)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TSource> (window));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize,
			IScheduler scheduler)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TSource> (bufferSize, scheduler));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize,
			TimeSpan window)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TSource> (bufferSize, window));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			TimeSpan window,
			IScheduler scheduler)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TSource> (window, scheduler));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize,
			TimeSpan window,
			IScheduler scheduler)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TSource> (bufferSize, window, scheduler));
		}
	}
}
