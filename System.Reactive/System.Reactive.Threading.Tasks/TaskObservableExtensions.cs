using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Reactive.Threading.Tasks
{
	public static class TaskObservableExtensions
	{
		public static IObservable<Unit> ToObservable (this Task task)
		{
			throw new NotImplementedException ();
		}
		
		public static IObservable<TResult> ToObservable<TResult> (this Task<TResult> task)
		{
			throw new NotImplementedException ();
		}
		
		public static Task<TResult> ToTask<TResult> (this IObservable<TResult> observable)
		{
			throw new NotImplementedException ();
		}
		
		public static Task<TResult> ToTask<TResult> (this IObservable<TResult> observable, object state)
		{
			throw new NotImplementedException ();
		}
		
		public static Task<TResult> ToTask<TResult> (this IObservable<TResult> observable, CancellationToken cancellationToken)
		{
			throw new NotImplementedException ();
		}
		
		public static Task<TResult> ToTask<TResult> (this IObservable<TResult> observable, CancellationToken cancellationToken, object state)
		{
			throw new NotImplementedException ();
		}
	}
}
