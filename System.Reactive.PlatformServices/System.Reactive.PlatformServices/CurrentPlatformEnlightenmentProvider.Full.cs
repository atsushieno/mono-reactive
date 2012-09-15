using System;
using System.ComponentModel;
using System.Reactive.Concurrency;

namespace System.Reactive.PlatformServices
{
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public class CurrentPlatformEnlightenmentProvider : IPlatformEnlightenmentProvider
	{
		internal CurrentPlatformEnlightenmentProvider ()
		{
		}
		
		public T GetService<T> (object[] args)
		{
			if (typeof (T) == typeof (IConcurrencyAbstractionLayer))
				return (T) (object) new ConcurrencyAbstractionLayer ();
#if NET_4_5
			if (typeof (T) == typeof (IExceptionServices))
				return (T) new ExceptionServices ();
#endif
			// not sure what else is expected.
			// none in 4.0/4.5 for:
			// - IHostLifecycleNotifications
			// - INotifySystemClockChanged
			
			return default (T);
		}
	}
	
	class ConcurrencyAbstractionLayer : IConcurrencyAbstractionLayer
	{
		public IDisposable QueueUserWorkItem (Action<object> action, object state)
		{
			throw new NotImplementedException ();
		}
		
		public void Sleep (TimeSpan timeout)
		{
			throw new NotImplementedException ();
		}
		
		public IDisposable StartPeriodicTimer (Action action, TimeSpan period)
		{
			throw new NotImplementedException ();
		}
		
		public IStopwatch StartStopwatch ()
		{
			throw new NotImplementedException ();
		}
		
		public void StartThread (Action<object> action, object state)
		{
			throw new NotImplementedException ();
		}
		
		public IDisposable StartTimer (Action<object> action, object state, TimeSpan dueTime)
		{
			throw new NotImplementedException ();
		}
		
		public bool SupportsLongRunning {
			get { return true; }
		}
	}

#if NET_4_5
	class ExceptionServices : IExceptionServices
	{
		public void Rethrow (Exception source)
		{
			ExceptionDispatchInfo.Capture (source).Throw ();
		}
	}
#endif
}

