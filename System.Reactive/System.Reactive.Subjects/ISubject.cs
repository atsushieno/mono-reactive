using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;

namespace System.Reactive.Subjects
{
	public interface ISubject<T>
		: ISubject<T, T>, IObserver<T>, IObservable<T>
	{
	}
}
