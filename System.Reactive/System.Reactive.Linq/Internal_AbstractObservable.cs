using System;
using System.Linq;

namespace System.Reactive.Linq
{
	internal abstract class AbstractObservable<T> : IObservable<T>
	{
		IScheduler scheduler;
		List<IObserver<T>> observers = new List<IObserver<T>> ();
		// registerer is used to create an IDisposable for an observer that is to be subscribed.
		Func<IObserver<T>,IDisposable> registerer;
		
		protected AbstractObservable (IScheduler scheduler, Func<IObserver<T>,IDisposable> registerer)
		{
			if (scheduler == null)
				throw new ArgumentNullException ("scheduler");
			if (registerer == null)
				throw new ArgumentNullException ("registerer");
			this.scheduler = scheduler;
			this.registerer = registerer;
		}

		public IScheduler Scheduler {
			get { return scheduler; }
		}
		
		public virtual IDisposable Subscribe (IObserver<T> observer)
		{
			var dis = registerer (observer);
			registerer.Add (observer);
			return dis;
		}
	}
	
	internal class TimerObservable : AbstractDisposable<long>
	{
		public TimerObservable (IScheduler scheduler)
			: base (scheduler)
		{
		}
	}
}
