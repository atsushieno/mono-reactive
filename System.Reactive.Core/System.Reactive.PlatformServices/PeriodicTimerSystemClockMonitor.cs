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

			thread = new Thread (Loop) { IsBackground = true };
			thread.Start ();
		}

		Thread thread;
		TimeSpan period;
		DateTimeOffset now;

		public event EventHandler<SystemClockChangedEventArgs> SystemClockChanged;

		void Loop ()
		{
			now = SystemClock.UtcNow;
			while (true) {
				Thread.Sleep (period);
				var delta = SystemClock.UtcNow - now - period;
				if (SystemClockChanged != null && Math.Abs (delta.Ticks) > TimeSpan.TicksPerSecond)
					SystemClockChanged (this, new SystemClockChangedEventArgs (now + period, SystemClock.UtcNow));
				now = SystemClock.UtcNow;
			}
		}
	}
}

