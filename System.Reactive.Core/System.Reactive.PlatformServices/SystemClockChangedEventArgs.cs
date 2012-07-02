using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public class SystemClockChangedEventArgs : EventArgs
	{
		internal SystemClockChangedEventArgs (DateTimeOffset oldTime, DateTimeOffset newTime)
		{
			OldTime = oldTime;
			NewTime = newTime;
		}

		public DateTimeOffset OldTime { get; private set; }
		public DateTimeOffset NewTime { get; private set; }
	}
}

