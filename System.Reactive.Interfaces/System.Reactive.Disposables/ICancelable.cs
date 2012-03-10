using System;

namespace System.Reactive.Disposables
{
	public interface ICancelable : IDisposable
	{
		bool IsDisposed { get; }
	}
}
