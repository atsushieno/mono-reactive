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
		}
		
		public void Dispose ()
		{
			throw new NotImplementedException ();
		}
		
		public bool IsDisposed { get; private set; }
		
		public IDisposable Disposable { get; private set; }
		
		public IScheduler Scheduler { get; private set; }
	}
}
