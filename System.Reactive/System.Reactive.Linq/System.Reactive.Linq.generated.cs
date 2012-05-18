
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		

		public static Func<T1, IObservable<Unit>> FromAsyncPattern<T1> (Func<T1, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1) => {
				var sub = new Subject<Unit> ();
				begin (t1, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, IObservable<TResult>> FromAsyncPattern<T1, TResult> (Func<T1, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1) => {
				var sub = new Subject<TResult> ();
				begin (t1, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, IObservable<Unit>> FromAsyncPattern<T1, T2> (Func<T1, T2, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, IObservable<TResult>> FromAsyncPattern<T1, T2, TResult> (Func<T1, T2, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, IObservable<Unit>> FromAsyncPattern<T1, T2, T3> (Func<T1, T2, T3, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, TResult> (Func<T1, T2, T3, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, T4, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4> (Func<T1, T2, T3, T4, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3, t4) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, t4, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, T4, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, TResult> (Func<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3, t4) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, t4, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, T4, T5, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5> (Func<T1, T2, T3, T4, T5, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3, t4, t5) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, t4, t5, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, T4, T5, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, TResult> (Func<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3, t4, t5) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, t4, t5, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6> (Func<T1, T2, T3, T4, T5, T6, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3, t4, t5, t6) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, t4, t5, t6, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, TResult> (Func<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3, t4, t5, t6) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, t4, t5, t6, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7> (Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, t4, t5, t6, t7, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, t4, t5, t6, t7, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8> (Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => {
				var sub = new Subject<Unit> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, (res) => {
				try {
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => {
				var sub = new Subject<TResult> ();
				begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };
		}
		

		public static Func<T1, T2, IObservable<Unit>> ToAsync<T1, T2> (this Action<T1, T2> function)
		{
			return (t1, t2) => Start (() => function (t1, t2));
		}
		
		public static Func<T1, T2, IObservable<Unit>> ToAsync<T1, T2> (this Action<T1, T2> function, IScheduler scheduler)
		{
			return (t1, t2) => Start (() => function (t1, t2), scheduler);
		}
		
		public static Func<T1, T2, IObservable<TResult>> ToAsync<T1, T2, TResult> (this Func<T1, T2, TResult> function)
		{
			return (t1, t2) => Start (() => function (t1, t2));
		}
		
		public static Func<T1, T2, IObservable<TResult>> ToAsync<T1, T2, TResult> (this Func<T1, T2, TResult> function, IScheduler scheduler)
		{
			return (t1, t2) => Start (() => function (t1, t2), scheduler);
		}
		

		public static Func<T1, T2, T3, IObservable<Unit>> ToAsync<T1, T2, T3> (this Action<T1, T2, T3> function)
		{
			return (t1, t2, t3) => Start (() => function (t1, t2, t3));
		}
		
		public static Func<T1, T2, T3, IObservable<Unit>> ToAsync<T1, T2, T3> (this Action<T1, T2, T3> function, IScheduler scheduler)
		{
			return (t1, t2, t3) => Start (() => function (t1, t2, t3), scheduler);
		}
		
		public static Func<T1, T2, T3, IObservable<TResult>> ToAsync<T1, T2, T3, TResult> (this Func<T1, T2, T3, TResult> function)
		{
			return (t1, t2, t3) => Start (() => function (t1, t2, t3));
		}
		
		public static Func<T1, T2, T3, IObservable<TResult>> ToAsync<T1, T2, T3, TResult> (this Func<T1, T2, T3, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3) => Start (() => function (t1, t2, t3), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, IObservable<Unit>> ToAsync<T1, T2, T3, T4> (this Action<T1, T2, T3, T4> function)
		{
			return (t1, t2, t3, t4) => Start (() => function (t1, t2, t3, t4));
		}
		
		public static Func<T1, T2, T3, T4, IObservable<Unit>> ToAsync<T1, T2, T3, T4> (this Action<T1, T2, T3, T4> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4) => Start (() => function (t1, t2, t3, t4), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, IObservable<TResult>> ToAsync<T1, T2, T3, T4, TResult> (this Func<T1, T2, T3, T4, TResult> function)
		{
			return (t1, t2, t3, t4) => Start (() => function (t1, t2, t3, t4));
		}
		
		public static Func<T1, T2, T3, T4, IObservable<TResult>> ToAsync<T1, T2, T3, T4, TResult> (this Func<T1, T2, T3, T4, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4) => Start (() => function (t1, t2, t3, t4), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5> (this Action<T1, T2, T3, T4, T5> function)
		{
			return (t1, t2, t3, t4, t5) => Start (() => function (t1, t2, t3, t4, t5));
		}
		
		public static Func<T1, T2, T3, T4, T5, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5> (this Action<T1, T2, T3, T4, T5> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5) => Start (() => function (t1, t2, t3, t4, t5), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, TResult> (this Func<T1, T2, T3, T4, T5, TResult> function)
		{
			return (t1, t2, t3, t4, t5) => Start (() => function (t1, t2, t3, t4, t5));
		}
		
		public static Func<T1, T2, T3, T4, T5, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, TResult> (this Func<T1, T2, T3, T4, T5, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5) => Start (() => function (t1, t2, t3, t4, t5), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6> (this Action<T1, T2, T3, T4, T5, T6> function)
		{
			return (t1, t2, t3, t4, t5, t6) => Start (() => function (t1, t2, t3, t4, t5, t6));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6> (this Action<T1, T2, T3, T4, T5, T6> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6) => Start (() => function (t1, t2, t3, t4, t5, t6), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, TResult> (this Func<T1, T2, T3, T4, T5, T6, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6) => Start (() => function (t1, t2, t3, t4, t5, t6));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, TResult> (this Func<T1, T2, T3, T4, T5, T6, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6) => Start (() => function (t1, t2, t3, t4, t5, t6), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7> (this Action<T1, T2, T3, T4, T5, T6, T7> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => Start (() => function (t1, t2, t3, t4, t5, t6, t7));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7> (this Action<T1, T2, T3, T4, T5, T6, T7> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => Start (() => function (t1, t2, t3, t4, t5, t6, t7), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => Start (() => function (t1, t2, t3, t4, t5, t6, t7));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => Start (() => function (t1, t2, t3, t4, t5, t6, t7), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8> (this Action<T1, T2, T3, T4, T5, T6, T7, T8> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8> (this Action<T1, T2, T3, T4, T5, T6, T7, T8> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IObservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> (this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16), scheduler);
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16), scheduler);
		}
		

	}
}

