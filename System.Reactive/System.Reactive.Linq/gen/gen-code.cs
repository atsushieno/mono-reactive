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
using System.Reactive;
using System.Reactive.Concurrency;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		");

		for (int i = 1; i <= 14; i++) {
			string s = String.Join (", ", (from t in Enumerable.Range (1, i) select "T" + t).ToArray ());
			Console.WriteLine (@"
		public static Func<{0}, IObservable<Unit>> FromAsyncPattern<{0}> (Func<{0}, AsyncCallback, Object, IAsyncResult> begin, Action<IAsyncResult> end)
		{{
			throw new NotImplementedException ();
		}}
		", s);
		}

		for (int i = 1; i <= 16; i++) {
			string s = String.Join (", ", (from t in Enumerable.Range (1, i) select "T" + t).ToArray ());
			Console.WriteLine (@"
		public static Func<{0}, IObservable<TResult>> ToAsync<{0}, TResult> (Func<{0}, TResult> function)
		{{
			throw new NotImplementedException ();
		}}
		
		public static Func<{0}, IObservable<TResult>> ToAsync<{0}, TResult> (Func<{0}, TResult> function, IScheduler scheduler)
		{{
			throw new NotImplementedException ();
		}}
		", s);
		}

		Console.WriteLine (@"
	}
}
");
	}
}
