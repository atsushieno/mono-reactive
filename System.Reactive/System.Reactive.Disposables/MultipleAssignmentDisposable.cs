using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public sealed class MultipleAssignmentDisposable : IDisposable
	{
		public void Dispose ()
		{
			throw new NotImplementedException ();
		}
		
		public bool IsDisposed { get; private set; }

		public IDisposable Disposable { get; set; }
	}
}
