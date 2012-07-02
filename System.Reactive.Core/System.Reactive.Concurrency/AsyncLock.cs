using System;

namespace System.Reactive.Concurrency
{
	public sealed class AsyncLock : IDisposable
	{
		public AsyncLock ()
		{
			throw new NotImplementedException ();
		}
		
		public void Dispose ()
		{
			// but it doesn't implement IDisposable?
			throw new NotImplementedException ();
		}
		
		public void Wait (Action action)
		{
			throw new NotImplementedException ();
		}
	}
}

