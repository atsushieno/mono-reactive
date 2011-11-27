using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public sealed class ContextDisposable : IDisposable
	{
		public ContextDisposable (SynchronizationContext context, IDisposable disposable)
		{
			throw new NotImplementedException ();
		}
		
		public void Dispose ()
		{
			throw new NotImplementedException ();
		}
		
		public bool IsDisposed { get; private set; }
		
		public SynchronizationContext Context { get; private set; }
	}
}
