#if REACTIVE_2_0
using System;
using System.Reactive;

namespace System.Reactive.PlatformServices
{
	public class PlatformEnlightenmentProvider : IPlatformEnlightenmentProvider
	{
		public PlatformEnlightenmentProvider ()
		{
		}

		public T GetService<T> (params object[] args) where T : class
		{
			throw new NotImplementedException ();
		}
	}
}

#endif
