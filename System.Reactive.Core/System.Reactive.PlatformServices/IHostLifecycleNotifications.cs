using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	// Infrastructure
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public interface IHostLifecycleNotifications
	{
		event EventHandler<HostResumingEventArgs> Resuming;
		
		event EventHandler<HostSuspendingEventArgs> Suspending;
	}
}

