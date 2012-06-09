using System;
using System.ComponentModel;
using System.Threading;

namespace System.Reactive.Concurrency
{
	[EditorBrowsable (EditorBrowsableState.Never)]
#if REACTIVE_2_0
	public
#endif
	static class Synchronization
	{
		public static IObservable<TSource> ObserveOn<TSource> (IObservable<TSource> source, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static IObservable<TSource> ObserveOn<TSource> (IObservable<TSource> source, SynchronizationContext context)
		{
			return ObserveOn (source, new SynchronizationContextScheduler (context));
		}
		
		public static IObservable<TSource> SubscribeOn<TSource> (IObservable<TSource> source, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static IObservable<TSource> SubscribeOn<TSource> (IObservable<TSource> source, SynchronizationContext context)
		{
			return SubscribeOn (source, new SynchronizationContextScheduler (context));
		}
		
		public static IObservable<TSource> Synchronize<TSource> (IObservable<TSource> source)
		{
			return Synchronize (source, new object ());
		}
		
		public static IObservable<TSource> Synchronize<TSource> (IObservable<TSource> source, object gate)
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (gate == null)
				throw new ArgumentNullException ("gate");
			return new SynchronizedObservable<TSource> (source, gate);
		}
	}
}

