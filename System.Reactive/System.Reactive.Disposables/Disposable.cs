using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public static class Disposable
	{
		static readonly IDisposable empty = new SimpleActionDisposable (() => {});
		
		public static IDisposable Empty {
			get { return empty; }
		}
		
		public static IDisposable Create (Action dispose)
		{
			if (dispose == null)
				throw new ArgumentNullException ("dispose");
			return new SimpleActionDisposable (dispose);
		}
		
		class SimpleActionDisposable : IDisposable
		{
			Action dispose;
			
			public SimpleActionDisposable (Action dispose)
			{
				this.dispose = dispose;
			}
			
			public void Dispose ()
			{
				if (dispose != null)
					dispose ();
				dispose = null;
			}
		}
	}
}
