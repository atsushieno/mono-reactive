using System;
using System.ComponentModel;

namespace System.Reactive.Concurrency
{
	// Infrastructure
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public interface IConcurrencyAbstractionLayer
	{
		IDisposable QueueUserWorkItem (Action<object> action, object state);
		IDisposable StartPeriodicTimer (Action action, TimeSpan period);
		IDisposable StartTimer (Action<object> action, object state, TimeSpan dueTime);
		IStopwatch StartStopwatch ();
		void Sleep (TimeSpan timeout);
	}
}

