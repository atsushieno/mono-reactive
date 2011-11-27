using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;

namespace System.Reactive.Subjects
{
	public sealed class AsyncSubject<T>
		: ISubject<T>, ISubject<T, T>, IObserver<T>, IObservable<T>, IDisposable
	{
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
