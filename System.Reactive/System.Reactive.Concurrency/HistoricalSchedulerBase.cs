using System;

namespace System.Reactive.Concurrency
{
	public abstract class HistoricalSchedulerBase : VirtualTimeSchedulerBase<DateTimeOffset, TimeSpan>
	{
		protected override DateTimeOffset Add (DateTimeOffset absolute, TimeSpan relative)
		{
			throw new NotImplementedException ();
		}
		
		protected override DateTimeOffset ToDateTimeOffset (DateTimeOffset absolute)
		{
			throw new NotImplementedException ();
		}
		
		protected override TimeSpan ToRelative (TimeSpan timeSpan)
		{
			throw new NotImplementedException ();
		}
	}
}
