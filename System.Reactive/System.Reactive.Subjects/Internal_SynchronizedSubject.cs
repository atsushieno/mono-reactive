using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace System.Reactive.Subjects
{
	class SynchronizedSubject<T> : ISubject<T>
	{
		object gate;
		ISubject<T> sub = new Subject<T> ();
		
		public SynchronizedSubject (object gate)
		{
			this.gate = gate;
		}
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			return sub.Subscribe (observer);
		}
		
		public void OnNext (T value)
		{
			lock (gate)
				sub.OnNext (value);
		}
		
		public void OnError (Exception error)
		{
			lock (gate)
				sub.OnError (error);
		}
		
		public void OnCompleted ()
		{
			lock (gate)
				sub.OnCompleted ();
		}
	}
	
	class SynchronizedSubject<TSource, TResult> : ISubject<TSource, TResult>
	{
		object gate = new object ();
		ISubject<TSource, TResult> sub;
		IScheduler scheduler;
		
		public SynchronizedSubject (ISubject<TSource, TResult> source, IScheduler scheduler)
		{
			this.sub = source;
			this.scheduler = scheduler;
		}
		
		public IDisposable Subscribe (IObserver<TResult> observer)
		{
			return sub.Subscribe (observer);
		}
		
		public void OnNext (TSource value)
		{
			lock (gate) {
				IDisposable dis = null;
				scheduler.Schedule (() => { sub.OnNext (value); if (dis != null) dis.Dispose (); });
			}
		}
		
		public void OnError (Exception error)
		{
			lock (gate) {
				IDisposable dis = null;
				scheduler.Schedule (() => { sub.OnError (error); if (dis != null) dis.Dispose (); });
			}
		}
		
		public void OnCompleted ()
		{
			lock (gate) {
				IDisposable dis = null;
				scheduler.Schedule (() => { sub.OnCompleted (); if (dis != null) dis.Dispose (); });
			}
		}
	}
}
