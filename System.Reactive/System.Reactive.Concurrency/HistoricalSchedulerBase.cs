using System;
using System.Collections.Generic;

namespace System.Reactive.Concurrency
{
	public abstract class HistoricalSchedulerBase : VirtualTimeSchedulerBase<DateTimeOffset, TimeSpan>
	{
#if REACTIVE_2_0
		protected HistoricalSchedulerBase ()
		{
		}

		protected HistoricalSchedulerBase (DateTimeOffset initialClock)
			: base (initialClock, Comparer<DateTimeOffset>.Default)
		{
		}

		protected HistoricalSchedulerBase (DateTimeOffset initialClock, IComparer<DateTimeOffset> comparer)
			: base (initialClock, comparer)
		{
		}
#endif
		
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

#if REACTIVE_2_0
		object IServiceProvider.GetService (Type serviceType)
		{
			throw new NotImplementedException ();
		}
#endif
	}
}
