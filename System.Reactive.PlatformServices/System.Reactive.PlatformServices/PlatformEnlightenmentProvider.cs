#if REACTIVE_2_0
using System;
using System.ComponentModel;
using System.Reactive;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Never)]
	public class PlatformEnlightenmentProvider : IPlatformEnlightenmentProvider
	{
		public PlatformEnlightenmentProvider ()
		{
		}

		public T GetService<T> (object[] args) where T : class
		{
			throw new NotImplementedException ();
		}
	}
}

#endif
