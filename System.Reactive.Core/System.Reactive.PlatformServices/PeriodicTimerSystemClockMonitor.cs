using System;
using System.ComponentModel;
using System.Threading;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public class PeriodicTimerSystemClockMonitor : INotifySystemClockChanged
	{
		public PeriodicTimerSystemClockMonitor (TimeSpan period)
		{
			this.period = period;
			timer = new Timer (Loop, null, TimeSpan.Zero, period);
			now = SystemClock.UtcNow;
		}

		TimeSpan period;
		Timer timer;
		DateTimeOffset now;

		public event EventHandler<SystemClockChangedEventArgs> SystemClockChanged;

		void Loop (object state)
		{
			if (SystemClockChanged == null)
				return;
			var delta = SystemClock.UtcNow - now - period;
			if (Math.Abs (delta.Ticks) > TimeSpan.TicksPerSecond)
				SystemClockChanged (this, new SystemClockChangedEventArgs (now + period, SystemClock.UtcNow));
			now = SystemClock.UtcNow;
		}
	}
}

