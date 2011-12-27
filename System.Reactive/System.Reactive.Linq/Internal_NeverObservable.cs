using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace System.Reactive.Linq
{
	internal class NeverObservable<T> : IObservable<T>
	{
		public NeverObservable ()
		{
		}
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			return Disposable.Empty;
		}
	}
}

