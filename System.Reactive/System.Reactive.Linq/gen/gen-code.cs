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
using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		");

		for (int i = 1; i <= 14; i++) {
			string s = String.Join (", ", (from t in Enumerable.Range (1, i) select "T" + t).ToArray ());
			string s2 = String.Join (", ", (from t in Enumerable.Range (1, i) select "t" + t).ToArray ());
			Console.WriteLine (@"
		public static Func<{0}, IObservable<Unit>> FromAsyncPattern<{0}> (Func<{0}, AsyncCallback, object, IAsyncResult> begin, Action<IAsyncResult> end)
		{{
			var sub = new Subject<Unit> ();
			return ({1}) => {{ begin ({1}, (res) => {{
				try {{
					end (res);
					sub.OnNext (Unit.Default);
					sub.OnCompleted ();
				}} catch (Exception ex) {{
					sub.OnError (ex);
				}}
				}}, sub); return sub; }};
		}}
		
		public static Func<{0}, IObservable<TResult>> FromAsyncPattern<{0}, TResult> (Func<{0}, AsyncCallback, Object, IAsyncResult> begin, Func<IAsyncResult, TResult> end)
		{{
			var sub = new Subject<TResult> ();
			return ({1}) => {{ begin ({1}, (res) => {{
				try {{
					var result = end (res);
					sub.OnNext (result);
					sub.OnCompleted ();
				}} catch (Exception ex) {{
					sub.OnError (ex);
				}}
				}}, sub); return sub; }};
		}}
		", s, s2);
		}

		for (int i = 2; i <= 16; i++) {
			string s = String.Join (", ", (from t in Enumerable.Range (1, i) select "T" + t).ToArray ());
			string s2 = String.Join (", ", (from t in Enumerable.Range (1, i) select "t" + t).ToArray ());
			
			Console.WriteLine (@"
		public static Func<{0}, IObservable<Unit>> ToAsync<{0}> (this Action<{0}> function)
		{{
			return ({1}) => Start (() => function ({1}));
		}}
		
		public static Func<{0}, IObservable<Unit>> ToAsync<{0}> (this Action<{0}> function, IScheduler scheduler)
		{{
			return ({1}) => Start (() => function ({1}), scheduler);
		}}
		
		public static Func<{0}, IObservable<TResult>> ToAsync<{0}, TResult> (this Func<{0}, TResult> function)
		{{
			return ({1}) => Start (() => function ({1}));
		}}
		
		public static Func<{0}, IObservable<TResult>> ToAsync<{0}, TResult> (this Func<{0}, TResult> function, IScheduler scheduler)
		{{
			return ({1}) => Start (() => function ({1}), scheduler);
		}}
		", s, s2);
		}

		Console.WriteLine (@"
	}
}
");
	}
}
