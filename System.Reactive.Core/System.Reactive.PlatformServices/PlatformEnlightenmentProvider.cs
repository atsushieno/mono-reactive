using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public static class PlatformEnlightenmentProvider
	{
		static Impl current = new Impl ();
		public static IPlatformEnlightenmentProvider Current {
			get { return current; }
		}

		class Impl : IPlatformEnlightenmentProvider
		{
			public Impl ()
			{
			}

			public T GetService<T> (params object[] args)
			{
				throw new System.NotImplementedException ();
			}
		}
	}
}

