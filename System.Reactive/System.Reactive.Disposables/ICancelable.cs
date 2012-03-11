using System;

namespace System.Reactive.Disposables
{
#if REACTIVE_2_0
	public
#endif
	interface ICancelable : IDisposable
	{
		bool IsDisposed { get; }
	}
}
