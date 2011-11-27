using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Reactive.Concurrency
{
	public interface IScheduledItem<TAbsolute>
	{
		TAbsolute DueTime { get; }
		void Invoke ();
	}
}
