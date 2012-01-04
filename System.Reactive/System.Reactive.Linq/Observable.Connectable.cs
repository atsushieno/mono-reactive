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
		class ConnectableObservable<TSource> : IConnectableObservable<TSource>
		{
			IObservable<TSource> source;
			ISubject<TSource> sub;
			Func<ISubject<TSource>> subject_creator;
			
			public ConnectableObservable (IObservable<TSource> source, Func<ISubject<TSource>> subjectCreator)
			{
				this.source = source;
				this.subject_creator = subjectCreator;
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
			return new ConnectableObservable<TSource> (source, () => new BehaviorSubject<TSource> (initialValue));
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
		{
			return new ConnectableObservable <TSource> (source, () => new AsyncSubject<TSource> ());
		}
		
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
	// see http://leecampbell.blogspot.com/2010/05/intro-to-rx.html
	public sealed class PublishLastSubject<T>
		: ISubject<T>, ISubject<T, T>, IObserver<T>, IObservable<T>, IDisposable
	{
		bool disposed;
		bool done;

		public void Dispose ()
		{
			if (n != null)
				observers.ForEach ((o) => n.Accept (o));
			disposed = true;
		}
		
		void CheckDisposed ()
		{
			if (disposed)
				throw new ObjectDisposedException ("subject");
		}
		
		Notification<T> n;
		
		public void OnCompleted ()
		{
			CheckDisposed ();
			if (!done)
				n = Notification.CreateOnCompleted<T> ();
			done = true;
		}
		
		public void OnError (Exception error)
		{
			CheckDisposed ();
			if (!done)
				n = Notification.CreateOnError<T> (error);
			done = true;
		}
		
		public void OnNext (T value)
		{
			CheckDisposed ();
			if (!done)
				n = Notification.CreateOnNext<T> (value);
		}
		
		List<IObserver<T>> observers = new List<IObserver<T>> ();
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			if (observer == null)
				throw new ArgumentNullException ("observer");
			CheckDisposed ();
			observers.Add (observer);

			if (n != null && done)
				n.Accept (observer);

			return Disposable.Create (() => observers.Remove (observer));
		}
	}
}
