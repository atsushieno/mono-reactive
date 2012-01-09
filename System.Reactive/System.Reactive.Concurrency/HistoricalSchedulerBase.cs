using System;

namespace System.Reactive.Concurrency
{
	public abstract class HistoricalSchedulerBase : VirtualTimeSchedulerBase<DateTimeOffset, TimeSpan>
	{
		protected override DateTimeOffset Add (DateTimeOffset absolute, TimeSpan relative)
		{
			return absolute + relative;
		}
		
		protected override DateTimeOffset ToDateTimeOffset (DateTimeOffset absolute)
		{
			return absolute;
		}
		
		protected override TimeSpan ToRelative (TimeSpan timeSpan)
		{
			return timeSpan;
		}
	}
}
