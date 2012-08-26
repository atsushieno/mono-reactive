
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		

		public static Func<TArg1, IObservable<Unit>> FromAsyncPattern<TArg1> (Func<TArg1, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, IObservable<TResult>> FromAsyncPattern<TArg1, TResult> (Func<TArg1, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2> (Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TResult> (Func<TArg1, TArg2, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3> (Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TResult> (Func<TArg1, TArg2, TArg3, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, TArg4, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4> (Func<TArg1, TArg2, TArg3, TArg4, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, TArg4, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TResult> (Func<TArg1, TArg2, TArg3, TArg4, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, IObservable<Unit>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
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
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, IObservable<TResult>> FromAsyncPattern<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult> (Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
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
		

		public static Func<TArg1, TArg2, IObservable<Unit>> ToAsync<TArg1, TArg2> (this Action<TArg1, TArg2> action)
		{
			return (t1, t2) => Start (() => action (t1, t2));
		}
		
		public static Func<TArg1, TArg2, IObservable<Unit>> ToAsync<TArg1, TArg2> (this Action<TArg1, TArg2> action, IScheduler scheduler)
		{
			return (t1, t2) => Start (() => action (t1, t2), scheduler);
		}
		
		public static Func<TArg1, TArg2, IObservable<TResult>> ToAsync<TArg1, TArg2, TResult> (this Func<TArg1, TArg2, TResult> function)
		{
			return (t1, t2) => Start (() => function (t1, t2));
		}
		
		public static Func<TArg1, TArg2, IObservable<TResult>> ToAsync<TArg1, TArg2, TResult> (this Func<TArg1, TArg2, TResult> function, IScheduler scheduler)
		{
			return (t1, t2) => Start (() => function (t1, t2), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3> (this Action<TArg1, TArg2, TArg3> action)
		{
			return (t1, t2, t3) => Start (() => action (t1, t2, t3));
		}
		
		public static Func<TArg1, TArg2, TArg3, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3> (this Action<TArg1, TArg2, TArg3> action, IScheduler scheduler)
		{
			return (t1, t2, t3) => Start (() => action (t1, t2, t3), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TResult> (this Func<TArg1, TArg2, TArg3, TResult> function)
		{
			return (t1, t2, t3) => Start (() => function (t1, t2, t3));
		}
		
		public static Func<TArg1, TArg2, TArg3, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TResult> (this Func<TArg1, TArg2, TArg3, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3) => Start (() => function (t1, t2, t3), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4> (this Action<TArg1, TArg2, TArg3, TArg4> action)
		{
			return (t1, t2, t3, t4) => Start (() => action (t1, t2, t3, t4));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4> (this Action<TArg1, TArg2, TArg3, TArg4> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4) => Start (() => action (t1, t2, t3, t4), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TResult> function)
		{
			return (t1, t2, t3, t4) => Start (() => function (t1, t2, t3, t4));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4) => Start (() => function (t1, t2, t3, t4), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5> action)
		{
			return (t1, t2, t3, t4, t5) => Start (() => action (t1, t2, t3, t4, t5));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5) => Start (() => action (t1, t2, t3, t4, t5), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> function)
		{
			return (t1, t2, t3, t4, t5) => Start (() => function (t1, t2, t3, t4, t5));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5) => Start (() => function (t1, t2, t3, t4, t5), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> action)
		{
			return (t1, t2, t3, t4, t5, t6) => Start (() => action (t1, t2, t3, t4, t5, t6));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6) => Start (() => action (t1, t2, t3, t4, t5, t6), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6) => Start (() => function (t1, t2, t3, t4, t5, t6));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6) => Start (() => function (t1, t2, t3, t4, t5, t6), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> action)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => Start (() => action (t1, t2, t3, t4, t5, t6, t7));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => Start (() => action (t1, t2, t3, t4, t5, t6, t7), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => Start (() => function (t1, t2, t3, t4, t5, t6, t7));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => Start (() => function (t1, t2, t3, t4, t5, t6, t7), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> action)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> action)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> action)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> action)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> action)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> action)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> action)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> action)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15), scheduler);
		}
		

		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> action)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, IObservable<Unit>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> (this Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> action, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) => Start (() => action (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16), scheduler);
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16));
		}
		
		public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, IObservable<TResult>> ToAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TResult> (this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16), scheduler);
		}
		
#if REACTIVE_2_0

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, Func<TSource1, TSource2, TSource3,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			return When ((source1).And (source2).And (source3).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, Func<TSource1, TSource2, TSource3, TSource4,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			return When ((source1).And (source2).And (source3).And (source4).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, Func<TSource1, TSource2, TSource3, TSource4, TSource5,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, IObservable<TSource6> source6, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			if (source6 == null) throw new ArgumentNullException ("source6");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).And (source6).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, IObservable<TSource6> source6, IObservable<TSource7> source7, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			if (source6 == null) throw new ArgumentNullException ("source6");
			if (source7 == null) throw new ArgumentNullException ("source7");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).And (source6).And (source7).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, IObservable<TSource6> source6, IObservable<TSource7> source7, IObservable<TSource8> source8, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			if (source6 == null) throw new ArgumentNullException ("source6");
			if (source7 == null) throw new ArgumentNullException ("source7");
			if (source8 == null) throw new ArgumentNullException ("source8");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).And (source6).And (source7).And (source8).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, IObservable<TSource6> source6, IObservable<TSource7> source7, IObservable<TSource8> source8, IObservable<TSource9> source9, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			if (source6 == null) throw new ArgumentNullException ("source6");
			if (source7 == null) throw new ArgumentNullException ("source7");
			if (source8 == null) throw new ArgumentNullException ("source8");
			if (source9 == null) throw new ArgumentNullException ("source9");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).And (source6).And (source7).And (source8).And (source9).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, IObservable<TSource6> source6, IObservable<TSource7> source7, IObservable<TSource8> source8, IObservable<TSource9> source9, IObservable<TSource10> source10, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			if (source6 == null) throw new ArgumentNullException ("source6");
			if (source7 == null) throw new ArgumentNullException ("source7");
			if (source8 == null) throw new ArgumentNullException ("source8");
			if (source9 == null) throw new ArgumentNullException ("source9");
			if (source10 == null) throw new ArgumentNullException ("source10");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).And (source6).And (source7).And (source8).And (source9).And (source10).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, IObservable<TSource6> source6, IObservable<TSource7> source7, IObservable<TSource8> source8, IObservable<TSource9> source9, IObservable<TSource10> source10, IObservable<TSource11> source11, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			if (source6 == null) throw new ArgumentNullException ("source6");
			if (source7 == null) throw new ArgumentNullException ("source7");
			if (source8 == null) throw new ArgumentNullException ("source8");
			if (source9 == null) throw new ArgumentNullException ("source9");
			if (source10 == null) throw new ArgumentNullException ("source10");
			if (source11 == null) throw new ArgumentNullException ("source11");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).And (source6).And (source7).And (source8).And (source9).And (source10).And (source11).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, IObservable<TSource6> source6, IObservable<TSource7> source7, IObservable<TSource8> source8, IObservable<TSource9> source9, IObservable<TSource10> source10, IObservable<TSource11> source11, IObservable<TSource12> source12, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			if (source6 == null) throw new ArgumentNullException ("source6");
			if (source7 == null) throw new ArgumentNullException ("source7");
			if (source8 == null) throw new ArgumentNullException ("source8");
			if (source9 == null) throw new ArgumentNullException ("source9");
			if (source10 == null) throw new ArgumentNullException ("source10");
			if (source11 == null) throw new ArgumentNullException ("source11");
			if (source12 == null) throw new ArgumentNullException ("source12");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).And (source6).And (source7).And (source8).And (source9).And (source10).And (source11).And (source12).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, IObservable<TSource6> source6, IObservable<TSource7> source7, IObservable<TSource8> source8, IObservable<TSource9> source9, IObservable<TSource10> source10, IObservable<TSource11> source11, IObservable<TSource12> source12, IObservable<TSource13> source13, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			if (source6 == null) throw new ArgumentNullException ("source6");
			if (source7 == null) throw new ArgumentNullException ("source7");
			if (source8 == null) throw new ArgumentNullException ("source8");
			if (source9 == null) throw new ArgumentNullException ("source9");
			if (source10 == null) throw new ArgumentNullException ("source10");
			if (source11 == null) throw new ArgumentNullException ("source11");
			if (source12 == null) throw new ArgumentNullException ("source12");
			if (source13 == null) throw new ArgumentNullException ("source13");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).And (source6).And (source7).And (source8).And (source9).And (source10).And (source11).And (source12).And (source13).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, IObservable<TSource6> source6, IObservable<TSource7> source7, IObservable<TSource8> source8, IObservable<TSource9> source9, IObservable<TSource10> source10, IObservable<TSource11> source11, IObservable<TSource12> source12, IObservable<TSource13> source13, IObservable<TSource14> source14, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			if (source6 == null) throw new ArgumentNullException ("source6");
			if (source7 == null) throw new ArgumentNullException ("source7");
			if (source8 == null) throw new ArgumentNullException ("source8");
			if (source9 == null) throw new ArgumentNullException ("source9");
			if (source10 == null) throw new ArgumentNullException ("source10");
			if (source11 == null) throw new ArgumentNullException ("source11");
			if (source12 == null) throw new ArgumentNullException ("source12");
			if (source13 == null) throw new ArgumentNullException ("source13");
			if (source14 == null) throw new ArgumentNullException ("source14");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).And (source6).And (source7).And (source8).And (source9).And (source10).And (source11).And (source12).And (source13).And (source14).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, IObservable<TSource6> source6, IObservable<TSource7> source7, IObservable<TSource8> source8, IObservable<TSource9> source9, IObservable<TSource10> source10, IObservable<TSource11> source11, IObservable<TSource12> source12, IObservable<TSource13> source13, IObservable<TSource14> source14, IObservable<TSource15> source15, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			if (source6 == null) throw new ArgumentNullException ("source6");
			if (source7 == null) throw new ArgumentNullException ("source7");
			if (source8 == null) throw new ArgumentNullException ("source8");
			if (source9 == null) throw new ArgumentNullException ("source9");
			if (source10 == null) throw new ArgumentNullException ("source10");
			if (source11 == null) throw new ArgumentNullException ("source11");
			if (source12 == null) throw new ArgumentNullException ("source12");
			if (source13 == null) throw new ArgumentNullException ("source13");
			if (source14 == null) throw new ArgumentNullException ("source14");
			if (source15 == null) throw new ArgumentNullException ("source15");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).And (source6).And (source7).And (source8).And (source9).And (source10).And (source11).And (source12).And (source13).And (source14).And (source15).Then (resultSelector));
		}
		

		public static IObservable<TResult> Zip<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16,TResult> (this IObservable<TSource1> source1, IObservable<TSource2> source2, IObservable<TSource3> source3, IObservable<TSource4> source4, IObservable<TSource5> source5, IObservable<TSource6> source6, IObservable<TSource7> source7, IObservable<TSource8> source8, IObservable<TSource9> source9, IObservable<TSource10> source10, IObservable<TSource11> source11, IObservable<TSource12> source12, IObservable<TSource13> source13, IObservable<TSource14> source14, IObservable<TSource15> source15, IObservable<TSource16> source16, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16,TResult> resultSelector)
		{
			if (source1 == null) throw new ArgumentNullException ("source1");
			if (source2 == null) throw new ArgumentNullException ("source2");
			if (source3 == null) throw new ArgumentNullException ("source3");
			if (source4 == null) throw new ArgumentNullException ("source4");
			if (source5 == null) throw new ArgumentNullException ("source5");
			if (source6 == null) throw new ArgumentNullException ("source6");
			if (source7 == null) throw new ArgumentNullException ("source7");
			if (source8 == null) throw new ArgumentNullException ("source8");
			if (source9 == null) throw new ArgumentNullException ("source9");
			if (source10 == null) throw new ArgumentNullException ("source10");
			if (source11 == null) throw new ArgumentNullException ("source11");
			if (source12 == null) throw new ArgumentNullException ("source12");
			if (source13 == null) throw new ArgumentNullException ("source13");
			if (source14 == null) throw new ArgumentNullException ("source14");
			if (source15 == null) throw new ArgumentNullException ("source15");
			if (source16 == null) throw new ArgumentNullException ("source16");
			return When ((source1).And (source2).And (source3).And (source4).And (source5).And (source6).And (source7).And (source8).And (source9).And (source10).And (source11).And (source12).And (source13).And (source14).And (source15).And (source16).Then (resultSelector));
		}
		
#endif

	}
}

