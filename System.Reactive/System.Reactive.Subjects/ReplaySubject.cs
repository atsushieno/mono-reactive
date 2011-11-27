using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;

namespace System.Reactive.Subjects
{
	public sealed class ReplaySubject<T>
		: ISubject<T>, ISubject<T, T>, IObserver<T>, IObservable<T>, IDisposable
	{
		public ReplaySubject ()
		{
		}

		public ReplaySubject (int bufferSize)
		{
		}

		public ReplaySubject (TimeSpan window)
		{
		}

		public ReplaySubject (IScheduler scheduler)
		{
		}

		public ReplaySubject (int bufferSize, IScheduler scheduler)
		{
		}

		public ReplaySubject (int bufferSize, TimeSpan window)
		{
		}

		public ReplaySubject (TimeSpan window, IScheduler scheduler)
		{
		}

		public ReplaySubject (int bufferSize, TimeSpan window, IScheduler scheduler)
		{
		}

		public void Dispose ()
		{
			throw new NotImplementedException ();
		}
		
		public void OnCompleted ()
		{
			throw new NotImplementedException ();
		}
		
		public void OnError (Exception error)
		{
			throw new NotImplementedException ();
		}
		
		public void OnNext (T value)
		{
			throw new NotImplementedException ();
		}
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			throw new NotImplementedException ();
		}
	}
}
