using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public sealed class MultipleAssignmentDisposable : IDisposable, ICancelable
	{
		public void Dispose ()
		{
			if (IsDisposed)
				return;
			IsDisposed = true; // set to true before call to d.Dispose().
			if (d != null)
				d.Dispose ();
		}
		
		public bool IsDisposed { get; private set; }

		IDisposable d;
		public IDisposable Disposable {
			get { return d; }
			set {
				// null is allowed.
				d = value;
				// Immediately dispose if this instance is already disposed.
				if (IsDisposed && d != null)
					d.Dispose ();
			}
		}
	}
}
