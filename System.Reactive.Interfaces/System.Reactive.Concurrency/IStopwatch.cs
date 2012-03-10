using System;

namespace System.Reactive.Concurrency
{
	public interface IStopwatch : IDisposable
	{
		TimeSpan Elapsed { get; }
	}
}

