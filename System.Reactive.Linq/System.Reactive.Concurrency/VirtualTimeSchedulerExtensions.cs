using System;

namespace System.Reactive.Concurrency
{
	public static class VirtualTimeSchedulerExtensions
	{
		public static IDisposable ScheduleAbsolute<TAbsolute, TRelative> (this VirtualTimeSchedulerBase<TAbsolute,TRelative> scheduler, TAbsolute dueTime, Action action)
			where TAbsolute : IComparable<TAbsolute>
		{
			throw new NotImplementedException ();
		}
		
		public static IDisposable ScheduleRelative<TAbsolute, TRelative> (this VirtualTimeSchedulerBase<TAbsolute,TRelative> scheduler, TRelative dueTime, Action action)
			where TAbsolute : IComparable<TAbsolute>
		{
			throw new NotImplementedException ();
		}
	}
}

