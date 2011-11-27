using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public class SerialDisposable : IDisposable
	{
		public void Dispose ()
		{
			throw new NotImplementedException ();
		}
		
		public bool IsDisposed { get; private set; }
		
		public IDisposable Disposable { get; private set; }
	}
}
