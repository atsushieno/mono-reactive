using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;

namespace System.Reactive.Subjects
{
	public interface IConnectableObservable<out T> : IObservable<T>
	{
		IDisposable Connect ();
	}
}
