using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public class SingleAssignmentDisposable : IDisposable
	{
		IDisposable d;
		
		public void Dispose ()
		{
			if (IsDisposed)
				return;
			IsDisposed = true;
			if (d != null)
				d.Dispose ();
		}
		
		public bool IsDisposed { get; private set; }
		
		public IDisposable Disposable {
			get { return d; }
			set {
				if (d != null)
					throw new InvalidOperationException ("Target disposable is already set to this instance");
				if (IsDisposed) {
					if (value != null)
						value.Dispose ();
				}
				d = value;
			}
		}
	}
}
