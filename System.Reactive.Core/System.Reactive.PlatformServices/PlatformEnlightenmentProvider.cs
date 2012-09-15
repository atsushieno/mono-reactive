using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public static class PlatformEnlightenmentProvider
	{
		static IPlatformEnlightenmentProvider current;
		
		static PlatformEnlightenmentProvider ()
		{
			var type = Type.GetType ("System.Reactive.PlatformServices.CurrentPlatformEnlightenmentProvider," + CommonAssemblyInfo.PlatformServicesAssemblyName);
			if (type == null)
				throw new InvalidOperationException ("Platform services assembly could not be loaded");

			current = (IPlatformEnlightenmentProvider) Activator.CreateInstance (type, true);
		}
		
		public static IPlatformEnlightenmentProvider Current {
			get { return current; }
		}
	}
}

