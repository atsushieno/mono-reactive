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
		class ConnectableObservable<TSource, TResult> : IConnectableObservable<TResult>
		{
			IObservable<TSource> source;
			ISubject<TSource, TResult> sub;
			Func<ISubject<TSource, TResult>> subject_creator;
			
			// FIXME: is it safe to leave created subject not disposed? (though also note that Multicast() does not *create* returned subject; it's just passing the argument)
			public ConnectableObservable (IObservable<TSource> source, Func<ISubject<TSource, TResult>> subjectCreator)
			{
				this.source = source;
				this.subject_creator = subjectCreator;
			}

			bool connected;
			List<IObserver<TResult>> observers = new List<IObserver<TResult>> ();
			new List<IDisposable> disposables;

			public IDisposable Subscribe (IObserver<TResult> observer)
			{
				observers.Add (observer);
				IDisposable dis = null;
				if (connected) {
					dis = sub.Subscribe (observer);
					disposables.Add (dis);
				}
				return Disposable.Create (() => { observers.Remove (observer); if (dis != null) disposables.Remove (dis); dis.Dispose (); });
			}

			public IDisposable Connect ()
			{
				if (connected)
					throw new InvalidOperationException ("This connectable observable is already connected");
				connected = true;
				sub = subject_creator ();
				disposables = new List<IDisposable> ();
				disposables.Add (source.Subscribe (sub));
				foreach (var o in observers)
					disposables.Add (sub.Subscribe (o));
				disposables.Add (Disposable.Create (() =>  {
					connected = false;
					this.disposables = null; // clean up itself in the final stage
				}));
				return new CompositeDisposable (disposables);
			}
		}
		
		public static IConnectableObservable<TSource> Publish<TSource> (this IObservable<TSource> source)
		{
			return source.Publish (default (TSource));
		}
		
		public static IConnectableObservable<TSource> Publish<TSource> (
			this IObservable<TSource> source,
			TSource initialValue)
		{
			return new ConnectableObservable<TSource, TSource> (source, () => new BehaviorSubject<TSource> (initialValue));
		}
		
		public static IObservable<TResult> Publish<TSource, TResult>(
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector)
		{
			return source.Publish (selector, default (TSource));
		}
		
		public static IObservable<TResult> Publish<TSource, TResult>(
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			TSource initialValue)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TSource> PublishLast<TSource> (this IObservable<TSource> source)
		{
			return new ConnectableObservable <TSource, TSource> (source, () => new AsyncSubject<TSource> ());
		}
		
		public static IObservable<TResult> PublishLast<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TResult> Multicast<TSource, TResult> (
			this IObservable<TSource> source,
			ISubject<TSource, TResult> subject)
		{
			return new ConnectableObservable<TSource, TResult> (source, () => subject);
		}
		
		public static IObservable<TResult> Multicast<TSource, TIntermediate, TResult> (
			this IObservable<TSource> source,
			Func<ISubject<TSource, TIntermediate>> subjectSelector,
			Func<IObservable<TIntermediate>, IObservable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
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
			return new RefCountObservable<TSource> (source);
		}

		static IConnectableObservable<TSource> Replay<TSource> (
			this IObservable<TSource> source, Func<ReplaySubject<TSource>> createSubject)
		{
			return new ConnectableObservable<TSource, TSource> (source, createSubject);
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
			Func<ReplaySubject<TResult>> createSubject)
		{
			throw new NotImplementedException ();
		}
		
		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TResult> ());
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TResult> (bufferSize));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			IScheduler scheduler)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TResult> (scheduler));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			TimeSpan window)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TResult> (window));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize,
			IScheduler scheduler)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TResult> (bufferSize, scheduler));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize,
			TimeSpan window)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TResult> (bufferSize, window));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			TimeSpan window,
			IScheduler scheduler)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TResult> (window, scheduler));
		}

		public static IObservable<TResult> Replay<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			int bufferSize,
			TimeSpan window,
			IScheduler scheduler)
		{
			return Replay<TSource, TResult> (source, selector, () => new ReplaySubject<TResult> (bufferSize, window, scheduler));
		}
	}
}
