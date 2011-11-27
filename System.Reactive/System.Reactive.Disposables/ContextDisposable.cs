using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public sealed class ContextDisposable : IDisposable
	{
		IDisposable disposable;
		
		public ContextDisposable (SynchronizationContext context, IDisposable disposable)
		{
			if (context == null)
				throw new ArgumentNullException ("context");
			if (disposable == null)
				throw new ArgumentNullException ("disposable");
			this.Context = context;
			this.disposable = disposable;
		}
		
		public void Dispose ()
		{
			if (IsDisposed)
				return;
			IsDisposed = true;
			/* async. To verify that it is not sync (Send()), try following lines:
				var d = new ContextDisposable(new SynchronizationContext (), Disposable.Create(() => { Thread.Sleep(10000); Console.WriteLine("OK"); }));
				d.Dispose();
				Console.WriteLine(d.IsDisposed);
			*/
			Context.Post ((o) => disposable.Dispose (), null);
		}
		
		public bool IsDisposed { get; private set; }
		
		public SynchronizationContext Context { get; private set; }
	}
}
