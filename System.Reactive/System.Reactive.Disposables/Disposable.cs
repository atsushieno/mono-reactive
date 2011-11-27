using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reactive.Concurrency;

namespace System.Reactive.Disposables
{
	public static class Disposable
	{
		public static IDisposable Empty {
			get { throw new NotImplementedException (); }
		}
		
		public static IDisposable Create (Action dispose)
		{
			throw new NotImplementedException ();
		}
	}
}
