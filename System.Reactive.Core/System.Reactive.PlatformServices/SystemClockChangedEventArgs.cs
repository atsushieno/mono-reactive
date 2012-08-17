using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public class SystemClockChangedEventArgs : EventArgs
	{
		public SystemClockChangedEventArgs ()
		{
			throw new NotImplementedException ();
		}

		public SystemClockChangedEventArgs (DateTimeOffset oldTime, DateTimeOffset newTime)
		{
			OldTime = oldTime;
			NewTime = newTime;
		}

		public DateTimeOffset OldTime { get; private set; }
		public DateTimeOffset NewTime { get; private set; }
	}
}

