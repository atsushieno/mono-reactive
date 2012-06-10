
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
	public static partial class Qbservable
	{
		

		public static Func<T1, IQbservable<Unit>> FromAsyncPattern<T1> (this IQbservableProvider provider, Expression<Func<T1, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, IQbservable<TResult>> FromAsyncPattern<T1, TResult> (this IQbservableProvider provider, Expression<Func<T1, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, IQbservable<Unit>> FromAsyncPattern<T1, T2> (this IQbservableProvider provider, Expression<Func<T1, T2, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, IQbservable<TResult>> FromAsyncPattern<T1, T2, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, IQbservable<Unit>> ToAsync<T1, T2> (this IQbservableProvider provider, Expression<Action<T1, T2>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, IQbservable<Unit>> ToAsync<T1, T2> (this IQbservableProvider provider, Expression<Action<T1, T2>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, IQbservable<TResult>> ToAsync<T1, T2, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, IQbservable<TResult>> ToAsync<T1, T2, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, IQbservable<Unit>> ToAsync<T1, T2, T3> (this IQbservableProvider provider, Expression<Action<T1, T2, T3>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, IQbservable<Unit>> ToAsync<T1, T2, T3> (this IQbservableProvider provider, Expression<Action<T1, T2, T3>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, IQbservable<TResult>> ToAsync<T1, T2, T3, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, IQbservable<TResult>> ToAsync<T1, T2, T3, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, IQbservable<Unit>> ToAsync<T1, T2, T3, T4> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, IQbservable<Unit>> ToAsync<T1, T2, T3, T4> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>> action)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> (this IQbservableProvider provider, Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>> action, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> function)
		{
			throw new NotImplementedException ();
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> (this IQbservableProvider provider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> function, IScheduler scheduler)
		{
			throw new NotImplementedException ();
		}
		

	}
}

