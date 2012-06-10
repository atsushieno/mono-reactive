using System;
using System.IO;
using System.Linq;

public class CodeGen
{
	public static void Main ()
	{
		Console.WriteLine (@"
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
		");

		for (int i = 1; i <= 14; i++) {
			string s = String.Join (", ", (from t in Enumerable.Range (1, i) select "T" + t).ToArray ());
			string s2 = String.Join (", ", (from t in Enumerable.Range (1, i) select "t" + t).ToArray ());
			Console.WriteLine (@"
		public static Func<{0}, IQbservable<Unit>> FromAsyncPattern<{0}> (this IQbservableProvider provider, Expression<Func<{0}, AsyncCallback, object, IAsyncResult>> begin, Expression<Action<IAsyncResult>> end)
		{{
			throw new NotImplementedException ();
		}}
		
		public static Func<{0}, IQbservable<TResult>> FromAsyncPattern<{0}, TResult> (this IQbservableProvider provider, Expression<Func<{0}, AsyncCallback, Object, IAsyncResult>> begin, Expression<Func<IAsyncResult, TResult>> end)
		{{
			throw new NotImplementedException ();
		}}
		", s, s2);
		}

		for (int i = 2; i <= 16; i++) {
			string s = String.Join (", ", (from t in Enumerable.Range (1, i) select "T" + t).ToArray ());
			string s2 = String.Join (", ", (from t in Enumerable.Range (1, i) select "t" + t).ToArray ());
			
			Console.WriteLine (@"
		public static Func<{0}, IQbservable<Unit>> ToAsync<{0}> (this IQbservableProvider provider, Expression<Action<{0}>> action)
		{{
			throw new NotImplementedException ();
		}}
		
		public static Func<{0}, IQbservable<Unit>> ToAsync<{0}> (this IQbservableProvider provider, Expression<Action<{0}>> action, IScheduler scheduler)
		{{
			throw new NotImplementedException ();
		}}
		
		public static Func<{0}, IQbservable<TResult>> ToAsync<{0}, TResult> (this IQbservableProvider provider, Expression<Func<{0}, TResult>> function)
		{{
			throw new NotImplementedException ();
		}}
		
		public static Func<{0}, IQbservable<TResult>> ToAsync<{0}, TResult> (this IQbservableProvider provider, Expression<Func<{0}, TResult>> function, IScheduler scheduler)
		{{
			throw new NotImplementedException ();
		}}
		", s, s2);
		}

		Console.WriteLine (@"
	}
}
");
	}
}
