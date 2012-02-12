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
	internal class HotObservable<T> : IObservable<T>
	{
		IDisposable scheduler_disposable;
		Subject<T> subject;

		public HotObservable (Action<IObserver<T>> work, IScheduler scheduler)
		{
			subject = new Subject<T> ();
			scheduler_disposable = scheduler.Schedule (() => work (subject));
		}
		
		bool disposed;
		
		public void Dispose ()
		{
			if (disposed)
				return;
			disposed = true;
			scheduler_disposable.Dispose ();
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			return subject.Subscribe (observer);
		}
	}
}
