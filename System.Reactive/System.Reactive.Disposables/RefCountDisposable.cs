using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public sealed class RefCountDisposable : IDisposable, ICancelable
	{
		IDisposable disposable;
		
		public RefCountDisposable (IDisposable disposable)
		{
			if (disposable == null)
				throw new ArgumentNullException ("disposable");
			this.disposable = disposable;
		}
		
		public void Dispose ()
		{
			dispose_invoked = true;
			if (IsDisposed || count > 0)
				return;
			IsDisposed = true;
			disposable.Dispose ();
		}
		
		public bool IsDisposed { get; private set; }
		
		bool dispose_invoked;
		int count;
		
		public IDisposable GetDisposable ()
		{
			count++;
			return Disposable.Create (() => {
				count--;
				if (count == 0 && dispose_invoked)
					Dispose ();
			});
		}
	}
}
