using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	// Infrastructure
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public interface ISystemClock
	{
		DateTimeOffset UtcNow { get; }
	}
}

