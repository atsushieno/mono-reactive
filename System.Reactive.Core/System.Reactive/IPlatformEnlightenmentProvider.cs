#if REACTIVE_2_0
using System;
using System.ComponentModel;

namespace System.Reactive
{
	[EditorBrowsable (EditorBrowsableState.Never)]
	public interface IPlatformEnlightenmentProvider
	{
		T GetService<T> (params object [] args) where T : class;
	}
}

#endif
