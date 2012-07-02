using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public class PeriodicTimerSystemClockMonitor : INotifySystemClockChanged
	{
		public PeriodicTimerSystemClockMonitor (TimeSpan period)
		{
			this.period = period;
		}

		TimeSpan period;

		public event EventHandler<SystemClockChangedEventArgs> SystemClockChanged;
	}
}

