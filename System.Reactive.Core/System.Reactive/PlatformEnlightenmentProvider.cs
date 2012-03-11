#if REACTIVE_2_0
using System;
using System.ComponentModel;
using System.Reactive;

namespace System.Reactive
{
	[EditorBrowsable (EditorBrowsableState.Never)]
	public static class PlatformEnlightenmentProvider
	{
		public static IPlatformEnlightenmentProvider Current {
			get { throw new NotImplementedException (); }
		}
	}
}

#endif
