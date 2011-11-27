
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		

		public static Func<T1, IObservable<Unit>> FromAsyncPattern<T1> (Func<T1, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, IObservable<Unit>> FromAsyncPattern<T1, T2> (Func<T1, T2, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, IObservable<Unit>> FromAsyncPattern<T1, T2, T3> (Func<T1, T2, T3, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4> (Func<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5> (Func<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6> (Func<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7> (Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8> (Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, IObservable<TResult>> ToAsync<T1, TResult> (Func<T1, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, IObservable<TResult>> ToAsync<T1, TResult> (Func<T1, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, IObservable<TResult>> ToAsync<T1, T2, TResult> (Func<T1, T2, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, IObservable<TResult>> ToAsync<T1, T2, TResult> (Func<T1, T2, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, IObservable<TResult>> ToAsync<T1, T2, T3, TResult> (Func<T1, T2, T3, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, IObservable<TResult>> ToAsync<T1, T2, T3, TResult> (Func<T1, T2, T3, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, IObservable<TResult>> ToAsync<T1, T2, T3, T4, TResult> (Func<T1, T2, T3, T4, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, IObservable<TResult>> ToAsync<T1, T2, T3, T4, TResult> (Func<T1, T2, T3, T4, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, TResult> (Func<T1, T2, T3, T4, T5, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, TResult> (Func<T1, T2, T3, T4, T5, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, TResult> (Func<T1, T2, T3, T4, T5, T6, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, TResult> (Func<T1, T2, T3, T4, T5, T6, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

	}
}

