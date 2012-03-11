using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public sealed class CancellationDisposable : IDisposable, ICancelable
	{
		CancellationTokenSource cts;
		
		public CancellationDisposable ()
			: this (new CancellationTokenSource ())
		{
		}
		
		public CancellationDisposable (CancellationTokenSource cts)
		{
			if (cts == null)
				throw new ArgumentNullException ("cts");
			this.cts = cts;
		}
		public void Dispose ()
		{
			if (!IsDisposed) {
				IsDisposed = true;
				cts.Cancel ();
			}
		}
		
		public bool IsDisposed { get; private set; }
		
		public CancellationToken Token {
			get { return cts.Token; }
		}
	}
}
