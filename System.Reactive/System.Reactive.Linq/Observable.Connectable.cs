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
		class BehaviorConnectableObservable<TSource> : IConnectableObservable<TSource>
		{
			IObservable<TSource> source;
			TSource initial_value;
			BehaviorSubject<TSource> sub;
			
			public BehaviorConnectableObservable (IObservable<TSource> source, TSource initialValue)
			{
				this.source = source;
				this.initial_value = initialValue;
			}

			bool connected;
			List<IObserver<TSource>> observers = new List<IObserver<TSource>> ();
			new List<IDisposable> disposables;

			public IDisposable Subscribe (IObserver<TSource> observer)
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
				sub = new BehaviorSubject<TSource> (initial_value);
				disposables = new List<IDisposable> ();
				disposables.Add (source.Subscribe (sub));
				foreach (var o in observers)
					disposables.Add (sub.Subscribe (o));
				disposables.Add (Disposable.Create (() => connected = false));
				disposables.Add (Disposable.Create (() => this.disposables = null)); // clean up itself in the final stage
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
			return new BehaviorConnectableObservable<TSource> (source, initialValue);
		}
		
		public static IObservable<TResult> Publish<TSource, TResult>(
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> Publish<TSource, TResult>(
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector,
			TSource initialValue)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TSource> PublishLast<TSource> (this IObservable<TSource> source)
		{ throw new NotImplementedException (); }
		
		public static IObservable<TResult> PublishLast<TSource, TResult> (
			this IObservable<TSource> source,
			Func<IObservable<TSource>, IObservable<TResult>> selector)
		{ throw new NotImplementedException (); }
		
		public static IConnectableObservable<TResult> Multicast<TSource, TResult> (
			this IObservable<TSource> source,
			ISubject<TSource, TResult> subject)
		{ throw new NotImplementedException (); }
		
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
	}
}
