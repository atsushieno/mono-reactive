using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public static class SystemClock
	{
		public static void AddRef ()
		{
			throw new NotImplementedException ();
		}

		public static void Release ()
		{
			throw new NotImplementedException ();
		}

		public static DateTimeOffset UtcNow {
			get { return DateTimeOffset.UtcNow; }
		}

		public static event EventHandler<SystemClockChangedEventArgs> SystemClockChanged;
	}
}

