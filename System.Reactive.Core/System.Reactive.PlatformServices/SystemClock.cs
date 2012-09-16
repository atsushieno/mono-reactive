using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public static class SystemClock
	{
		static DefaultSystemClock clock = new DefaultSystemClock ();
		static PeriodicTimerSystemClockMonitor monitor;
		static int refcount;

		public static void AddRef ()
		{
			if (monitor == null) {
				monitor = new PeriodicTimerSystemClockMonitor (TimeSpan.FromSeconds (10));
				monitor.SystemClockChanged += SystemClockChanged;
			}
			refcount++;
		}

		public static void Release ()
		{
			if (--refcount == 0) {
				monitor.SystemClockChanged -= SystemClockChanged;
				monitor = null;
			}
		}

		public static DateTimeOffset UtcNow {
			get { return clock.UtcNow; }
		}

		public static event EventHandler<SystemClockChangedEventArgs> SystemClockChanged;
	}
}

