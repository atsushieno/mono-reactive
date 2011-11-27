using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;

namespace System.Reactive.Subjects
{
	public interface ISubject<in TSource, out TResult>
		: IObserver<TSource>, IObservable<TResult>
	{
	}
}
