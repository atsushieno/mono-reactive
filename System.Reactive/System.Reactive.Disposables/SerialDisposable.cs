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
			if (IsDisposed)
				return;
			IsDisposed = true;
			if (Disposable != null)
				Disposable.Dispose ();
		}
		
		public bool IsDisposed { get; private set; }
		
		IDisposable d;
		public IDisposable Disposable {
			get { return d; }
			set {
				// null is allowed.
				if (d != null)
					d.Dispose (); // regardless of whether this instance is disposed or not.
				d = value;
				if (IsDisposed && d != null)
					d.Dispose ();
			}
		}
	}
}
