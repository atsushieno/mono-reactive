using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public static class PlatformEnlightenmentProvider
	{
		const string version = "2.0.20814.0";
		const string publickeytoken = "System.Reactive.Core, Version=2.0.20823.0, Culture=neutral, PublicKeyToken=f300afd708cefcd3";
		static IPlatformEnlightenmentProvider current;
		
		static PlatformEnlightenmentProvider ()
		{
			var type = Type.GetType (String.Format ("System.Reactive.PlatformServices.CurrentPlatformEnlightenmentProvider, System.Reactive.PlatformServices, Version={0}, Culture=neutral, PublicKeyToken={1}",
			                         version, publickeytoken));
			if (type == null)
				throw new InvalidOperationException ("Platform services assembly could not be loaded");

			current = (IPlatformEnlightenmentProvider) Activator.CreateInstance (type, true);
		}
		
		public static IPlatformEnlightenmentProvider Current {
			get { return current; }
		}
	}
}

