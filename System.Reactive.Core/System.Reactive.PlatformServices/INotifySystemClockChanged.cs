using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	// Infrastructure
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public interface INotifySystemClockChanged
	{
		// FIXME: find out the actual event handler type
		event EventHandler<SystemClockChangedEventArgs> SystemClockChanged;
	}
}

