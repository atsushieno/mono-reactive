using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	// Infrastructure
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public interface IPlatformEnlightenmentProvider
	{
		T GetService<T> (params object [] args);
	}
}

