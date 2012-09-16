using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	// Infrastructure used to adjust the sleep time gap after suspend/resume.
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public interface IHostLifecycleNotifications
	{
		event EventHandler<HostResumingEventArgs> Resuming;
		
		event EventHandler<HostSuspendingEventArgs> Suspending;
	}
}

