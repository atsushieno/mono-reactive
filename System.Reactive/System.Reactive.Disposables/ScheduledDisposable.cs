using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public sealed class ScheduledDisposable : IDisposable
	{
		public ScheduledDisposable (IScheduler scheduler, IDisposable disposable)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			if (disposable == null)
				throw new ArgumentNullException ("disposable");
			this.Scheduler = scheduler;
			this.Disposable = disposable;
		}
		
		public void Dispose ()
		{
			if (IsDisposed)
				return;
			IsDisposed = true;
			Scheduler.Schedule (() => Disposable.Dispose ());
		}
		
		public bool IsDisposed { get; private set; }
		
		public IDisposable Disposable { get; private set; }
		
		public IScheduler Scheduler { get; private set; }
	}
}
