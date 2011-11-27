using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public sealed class BooleanDisposable : IDisposable
	{
		public void Dispose ()
		{
			if (!IsDisposed)
				IsDisposed = true;
		}
		
		public bool IsDisposed { get; private set; }
	}
}
