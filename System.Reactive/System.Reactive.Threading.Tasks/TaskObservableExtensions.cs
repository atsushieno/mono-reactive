using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace System.Reactive.Threading.Tasks
{
	public static class TaskObservableExtensions
	{
		#region ToObservable
		
		// It is a hot observable.
		class TaskObservable<T> : IObservable<T>
		{
			Task task;
			List<IObserver<T>> observers = new List<IObserver<T>> ();
			
			public TaskObservable (Task task)
			{
				if (task == null)
					throw new ArgumentNullException ("task");
				this.task = task;
			}

			T Result {
				get {return task is Task<T> ? ((Task<T>) task).Result : (T) (object) Unit.Default; }
			}
			
			public IDisposable Subscribe (IObserver<T> observer)
			{
				switch (task.Status) {
				case TaskStatus.Faulted:
					observer.OnError (task.Exception);
					break;
				case TaskStatus.Canceled: // actually, not sure...
					observer.OnCompleted ();
					break;
				case TaskStatus.RanToCompletion:
					observer.OnNext (Result);
					observer.OnCompleted ();
					break;
				default:
					observers.Add (observer);
					Action<Task> onEnd = t => {
						if (t.IsCanceled)
							return;
						foreach (var o in observers) {
							o.OnNext (Result);
							o.OnCompleted ();
						}
					};
					task.ContinueWith (onEnd);
					if (task.Status == TaskStatus.Created)
						task.Start ();
					break;
				}
				return Disposable.Create (() => observers.Remove (observer));
			}
		}
		
		public static IObservable<Unit> ToObservable (this Task task)
		{
			return new TaskObservable<Unit> (task);
		}
		
		public static IObservable<TResult> ToObservable<TResult> (this Task<TResult> task)
		{
			return new TaskObservable<TResult> (task);
		}
		
		#endregion
		
		#region ToTask
		
		public static Task<TResult> ToTask<TResult> (this IObservable<TResult> observable)
		{
			return ToTask (observable, new object ());
		}
		
		public static Task<TResult> ToTask<TResult> (this IObservable<TResult> observable, object state)
		{
			return ToTask (observable, new CancellationToken (false), state);
		}
		
		public static Task<TResult> ToTask<TResult> (this IObservable<TResult> observable, CancellationToken cancellationToken)
		{
			return ToTask (observable, cancellationToken, new object ());
		}
		
		public static Task<TResult> ToTask<TResult> (this IObservable<TResult> observable, CancellationToken cancellationToken, object state)
		{
			if (observable == null)
				throw new ArgumentNullException ("observable");
			
			var ret = new Task<TResult> (stat => {
				TResult result = default (TResult);
				Exception error = null;
				var wait = new ManualResetEvent (false);
				var dis = new SingleAssignmentDisposable ();
				dis.Disposable = observable.Subscribe (
					v => result = v,
					ex => { dis.Dispose (); error = ex; wait.Set (); },
					() => { dis.Dispose (); wait.Set (); }
				);
				wait.WaitOne ();
				if (error != null)
					throw error;
				return result;
			}, state, cancellationToken);
			ret.Start ();
			return ret;
		}
		
		#endregion
	}
}
