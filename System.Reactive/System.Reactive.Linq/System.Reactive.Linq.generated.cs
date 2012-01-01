
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		

		public static Func<T1, IObservable<TResult>> FromAsyncPattern<T1, TResult> (Func<T1, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1) => { begin (t1, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, IObservable<TResult>> FromAsyncPattern<T1, T2, TResult> (Func<T1, T2, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2) => { begin (t1, t2, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, TResult> (Func<T1, T2, T3, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3) => { begin (t1, t2, t3, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, T4, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, TResult> (Func<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3, t4) => { begin (t1, t2, t3, t4, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, T4, T5, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, TResult> (Func<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3, t4, t5) => { begin (t1, t2, t3, t4, t5, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, T4, T5, T6, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, TResult> (Func<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3, t4, t5, t6) => { begin (t1, t2, t3, t4, t5, t6, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3, t4, t5, t6, t7) => { begin (t1, t2, t3, t4, t5, t6, t7, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3, t4, t5, t6, t7, t8) => { begin (t1, t2, t3, t4, t5, t6, t7, t8, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => { begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => { begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => { begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => { begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => { begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{
			var sub = new Subject<TResult> ();
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => { begin (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, (res) => {
				try {
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				} catch (Exception ex) {
					sub.OnError (ex);
				}
				}, sub); return sub; };

		}
		

		public static Func<T1, T2, IObservable<TResult>> ToAsync<T1, T2, TResult> (Func<T1, T2, TResult> function)
		{
			return (t1, t2) => Start (() => function (t1, t2));
		}
		
		public static Func<T1, T2, IObservable<TResult>> ToAsync<T1, T2, TResult> (Func<T1, T2, TResult> function, IScheduler scheduler)
		{
			return (t1, t2) => Start (() => function (t1, t2), scheduler);
		}
		

		public static Func<T1, T2, T3, IObservable<TResult>> ToAsync<T1, T2, T3, TResult> (Func<T1, T2, T3, TResult> function)
		{
			return (t1, t2, t3) => Start (() => function (t1, t2, t3));
		}
		
		public static Func<T1, T2, T3, IObservable<TResult>> ToAsync<T1, T2, T3, TResult> (Func<T1, T2, T3, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3) => Start (() => function (t1, t2, t3), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, IObservable<TResult>> ToAsync<T1, T2, T3, T4, TResult> (Func<T1, T2, T3, T4, TResult> function)
		{
			return (t1, t2, t3, t4) => Start (() => function (t1, t2, t3, t4));
		}
		
		public static Func<T1, T2, T3, T4, IObservable<TResult>> ToAsync<T1, T2, T3, T4, TResult> (Func<T1, T2, T3, T4, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4) => Start (() => function (t1, t2, t3, t4), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, TResult> (Func<T1, T2, T3, T4, T5, TResult> function)
		{
			return (t1, t2, t3, t4, t5) => Start (() => function (t1, t2, t3, t4, t5));
		}
		
		public static Func<T1, T2, T3, T4, T5, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, TResult> (Func<T1, T2, T3, T4, T5, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5) => Start (() => function (t1, t2, t3, t4, t5), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, TResult> (Func<T1, T2, T3, T4, T5, T6, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6) => Start (() => function (t1, t2, t3, t4, t5, t6));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, TResult> (Func<T1, T2, T3, T4, T5, T6, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6) => Start (() => function (t1, t2, t3, t4, t5, t6), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => Start (() => function (t1, t2, t3, t4, t5, t6, t7));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7) => Start (() => function (t1, t2, t3, t4, t5, t6, t7), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15), scheduler);
		}
		

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> function)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16));
		}
		
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IObservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> function, IScheduler scheduler)
		{
			return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) => Start (() => function (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16), scheduler);
		}
		

	}
}

