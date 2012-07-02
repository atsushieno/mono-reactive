using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public static class HostLifecycleService
	{
		public static event EventHandler<HostResumingEventArgs> Resuming;

		public static event EventHandler<HostSuspendingEventArgs> Suspending;

		public static void AddRef ()
		{
			throw new NotImplementedException ();
		}

		public static void Release ()
		{
			throw new NotImplementedException ();
		}
	}
}

