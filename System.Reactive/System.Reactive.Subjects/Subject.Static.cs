using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;

namespace System.Reactive.Subjects
{
	public static class Subject
	{
		public static ISubject<TSource, TResult> Create<TSource, TResult> (IObserver<TSource> observer, IObservable<TResult> observable)
		{
			throw new NotImplementedException ();
		}
		
		public static ISubject<TSource, TResult> Synchronize<TSource, TResult> (ISubject<TSource, TResult> subject)
		{
			throw new NotImplementedException ();
		}
		
		public static ISubject<TSource, TResult> Synchronize<TSource, TResult> (ISubject<TSource, TResult> subject, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
	}
}
