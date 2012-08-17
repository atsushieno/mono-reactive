using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public class CurrentPlatformEnlightenmentProvider : IPlatformEnlightenmentProvider
	{
		public T GetService<T> (object[] args)
		{
			throw new NotImplementedException ();
		}
	}
}

