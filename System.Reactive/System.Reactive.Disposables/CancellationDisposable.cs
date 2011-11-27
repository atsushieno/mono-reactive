using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public sealed class CancellationDisposable : IDisposable
	{
		public CancellationDisposable ()
		{
			throw new NotImplementedException ();
		}
		
		public CancellationDisposable (CancellationTokenSource cts)
		{
			throw new NotImplementedException ();
		}
		public void Dispose ()
		{
			throw new NotImplementedException ();
		}
		
		public bool IsDisposed { get; private set; }
		
		public CancellationToken Token {
			get { throw new NotImplementedException (); }
		}
	}
}
