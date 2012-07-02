using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public class DefaultSystemClock : ISystemClock
	{
		public DateTimeOffset UtcNow {
			get { return DateTimeOffset.UtcNow; }
		}
	}
}

