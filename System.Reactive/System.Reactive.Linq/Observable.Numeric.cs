using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Joins;
using System.Reactive.Subjects;
using System.Threading;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		static void VerifyCompleted<T> (bool hasValue, ISubject<T> sub, T value, IDisposable dis)
		{
			if (!hasValue)
				sub.OnError (new InvalidOperationException ());
			else {
				sub.OnNext (value);
				sub.OnCompleted ();
			}
			dis.Dispose (); 
		}
		
		static IObservable<T> NonNullableMin<T> (this IObservable<T> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			T min = default (T);
			var sub = new Subject<T> ();
			IDisposable dis = null;
			bool got = false;
			dis = source.Subscribe (
				(s) => {
					if (!got) {
						got = true;
						min = s;
					} else if (Comparer<T>.Default.Compare (min, s) > 0)
						min = s;
				},
				() => VerifyCompleted (got, sub, min, dis)
				);
			return sub;
		}
		
		static IObservable<T> NullableMin<T> (this IObservable<T> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			T min = default (T);
			var sub = new Subject<T> ();
			IDisposable dis = null;
			dis = source.Subscribe ((s) => { if (Comparer<T>.Default.Compare (min, s) > 0) min = s; }, () => VerifyCompleted (true, sub, min, dis));
			return sub;
		}
		
		static IObservable<T> NonNullableMax<T> (this IObservable<T> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			T max = default (T);
			var sub = new Subject<T> ();
			IDisposable dis = null;
			bool got = false;
			dis = source.Subscribe (
				(s) => {
					if (!got) {
						got = true;
						max = s;
					} else if (Comparer<T>.Default.Compare (max, s) < 0)
						max = s;
				},
				() => VerifyCompleted (got, sub, max, dis)
				);
			return sub;
		}
		
		static IObservable<T> NullableMax<T> (this IObservable<T> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			T max = default (T);
			var sub = new Subject<T> ();
			IDisposable dis = null;
			dis = source.Subscribe ((s) => { if (Comparer<T>.Default.Compare (max, s) < 0) max = s; }, () => VerifyCompleted (true, sub, max, dis));
			return sub;
		}
		
		static IObservable<T> NonNullableSum<T> (this IObservable<T> source, Func<T,T,T> add)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			T sum = default (T);
			var sub = new Subject<T> ();
			IDisposable dis = null;
			dis = source.Subscribe (s => sum = add (sum, s), () => VerifyCompleted (true, sub, sum, dis));
			return sub;
		}
		
		static IObservable<T> NullableSum<T> (this IObservable<T> source, Func<T,T,T> add)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			T sum = default (T);
			var sub = new Subject<T> ();
			IDisposable dis = null;
			dis = source.Subscribe (s => sum = sum != null ? s : add (sum, s), () => VerifyCompleted (true, sub, sum, dis));
			return sub;
		}
		
		static IObservable<T> NonNullableAverage<T> (this IObservable<T> source, Func<T,T,T> add, Func<T,int,T> avg)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			T sum = default (T);
			var sub = new Subject<T> ();
			IDisposable dis = null;
			int count = 0;
			dis = source.Subscribe (s => { count++; sum = add (sum, s); }, () => VerifyCompleted (true, sub, avg (sum, count), dis));
			return sub;
		}
		
		static IObservable<T> NullableAverage<T> (this IObservable<T> source, Func<T,T,T> add, Func<T,int,T> avg)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			T sum = default (T);
			var sub = new Subject<T> ();
			IDisposable dis = null;
			int count = 0;
			dis = source.Subscribe (s => { count++; sum = sum != null ? s : add (sum, s); }, () => VerifyCompleted (true, sub, avg (sum, count), dis));
			return sub;
		}

		#region Average

		public static IObservable<decimal> Average (this IObservable<decimal> source)
		{
			return source.NonNullableAverage ((x, y) => x + y, (x, y) => x / y);
		}
		
		public static IObservable<double> Average (this IObservable<double> source)
		{
			return source.NonNullableAverage ((x, y) => x + y, (x, y) => x / y);
		}
		
		public static IObservable<int> Average (this IObservable<int> source)
		{
			return source.NonNullableAverage ((x, y) => x + y, (x, y) => x / y);
		}
		
		public static IObservable<long> Average (this IObservable<long> source)
		{
			return source.NonNullableAverage ((x, y) => x + y, (x, y) => x / y);
		}
		
		public static IObservable<float> Average (this IObservable<float> source)
		{
			return source.NonNullableAverage ((x, y) => x + y, (x, y) => x / y);
		}
		
		public static IObservable<decimal?> Average (this IObservable<decimal?> source)
		{
			return source.NullableAverage ((x, y) => x + y, (x, y) => x / y);
		}
		
		public static IObservable<double?> Average (this IObservable<double?> source)
		{
			return source.NullableAverage ((x, y) => x + y, (x, y) => x / y);
		}
		
		public static IObservable<int?> Average (this IObservable<int?> source)
		{
			return source.NullableAverage ((x, y) => x + y, (x, y) => x / y);
		}
		
		public static IObservable<long?> Average (this IObservable<long?> source)
		{
			return source.NullableAverage ((x, y) => x + y, (x, y) => x / y);
		}
		
		public static IObservable<float?> Average (this IObservable<float?> source)
		{
			return source.NullableAverage ((x, y) => x + y, (x, y) => x / y);
		}
		
		#endregion

		#region Max

		public static IObservable<decimal> Max (this IObservable<decimal> source)
		{
			return source.NonNullableMax ();
		}
		
		public static IObservable<double> Max (this IObservable<double> source)
		{
			return source.NonNullableMax ();
		}
		
		public static IObservable<int> Max (this IObservable<int> source)
		{
			return source.NonNullableMax ();
		}
		
		public static IObservable<long> Max (this IObservable<long> source)
		{
			return source.NonNullableMax ();
		}
		
		public static IObservable<float> Max (this IObservable<float> source)
		{
			return source.NonNullableMax ();
		}
		
		public static IObservable<decimal?> Max (this IObservable<decimal?> source)
		{
			return source.NullableMax ();
		}
		
		public static IObservable<double?> Max (this IObservable<double?> source)
		{
			return source.NullableMax ();
		}
		
		public static IObservable<int?> Max (this IObservable<int?> source)
		{
			return source.NullableMax ();
		}
		
		public static IObservable<long?> Max (this IObservable<long?> source)
		{
			return source.NullableMax ();
		}
		
		public static IObservable<float?> Max (this IObservable<float?> source)
		{
			return source.NullableMax ();
		}
		
		public static IObservable<TSource> Max<TSource> (this IObservable<TSource> source)
		{
			return source.Max (Comparer<TSource>.Default);
		}
		
		public static IObservable<TSource> Max<TSource> (this IObservable<TSource> source, IComparer<TSource> comparer)
		{
			TSource max = default (TSource);
			var sub = new Subject<TSource> ();
			bool got = false;
			IDisposable dis = null;
			dis = source.Subscribe (
				(s) => {
					if (!got) {
						got = true;
						max = s;
					} else if (comparer.Compare (max, s) < 0)
						max = s;
				},
				() => VerifyCompleted (got, sub, max, dis));
			return sub;
		}

		#endregion
		
		#region Min
		
		public static IObservable<decimal> Min (this IObservable<decimal> source)
		{
			return source.NonNullableMin ();
		}
		
		public static IObservable<double> Min (this IObservable<double> source)
		{
			return source.NonNullableMin ();
		}
		
		public static IObservable<int> Min (this IObservable<int> source)
		{
			return source.NonNullableMin ();
		}
		
		public static IObservable<long> Min (this IObservable<long> source)
		{
			return source.NonNullableMin ();
		}
		
		public static IObservable<float> Min (this IObservable<float> source)
		{
			return source.NonNullableMin ();
		}
		
		public static IObservable<decimal?> Min (this IObservable<decimal?> source)
		{
			return source.NullableMin ();
		}
		
		public static IObservable<double?> Min (this IObservable<double?> source)
		{
			return source.NullableMin ();
		}
		
		public static IObservable<int?> Min (this IObservable<int?> source)
		{
			return source.NullableMin ();
		}
		
		public static IObservable<long?> Min (this IObservable<long?> source)
		{
			return source.NullableMin ();
		}
		
		public static IObservable<float?> Min (this IObservable<float?> source)
		{
			return source.NullableMin ();
		}
		
		public static IObservable<TSource> Min<TSource> (this IObservable<TSource> source)
		{
			return source.Min (Comparer<TSource>.Default);
		}
		
		public static IObservable<TSource> Min<TSource> (this IObservable<TSource> source, IComparer<TSource> comparer)
		{
			TSource min = default (TSource);
			var sub = new Subject<TSource> ();
			bool got = false;
			IDisposable dis = null;
			dis = source.Subscribe (
				(s) => {
					if (!got) {
						got = true;
						min = s;
					} else if (comparer.Compare (min, s) > 0)
						min = s;
				},
				() => VerifyCompleted (got, sub, min, dis));
			return sub;
		}
		
		#endregion

		#region Sum

		public static IObservable<double> Sum (this IObservable<double> source)
		{
			return source.NonNullableSum ((x, y) => x + y);
		}
		
		public static IObservable<int> Sum (this IObservable<int> source)
		{
			return source.NonNullableSum ((x, y) => x + y);
		}
		
		public static IObservable<long> Sum (this IObservable<long> source)
		{
			return source.NonNullableSum ((x, y) => x + y);
		}
		
		public static IObservable<float> Sum (this IObservable<float> source)
		{
			return source.NonNullableSum ((x, y) => x + y);
		}
		
		public static IObservable<decimal?> Sum (this IObservable<decimal?> source)
		{
			return source.NullableSum ((x, y) => x + y);
		}
		
		public static IObservable<double?> Sum (this IObservable<double?> source)
		{
			return source.NullableSum ((x, y) => x + y);
		}
		
		public static IObservable<int?> Sum (this IObservable<int?> source)
		{
			return source.NullableSum ((x, y) => x + y);
		}
		
		public static IObservable<long?> Sum (this IObservable<long?> source)
		{
			return source.NullableSum ((x, y) => x + y);
		}
		
		public static IObservable<float?> Sum (this IObservable<float?> source)
		{
			return source.NullableSum ((x, y) => x + y);
		}

		#endregion
	}
}
