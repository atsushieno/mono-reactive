using System;
using System.IO;
using System.Linq;

public class CodeGen
{
	public static void Main ()
	{
		Console.WriteLine (@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace System.Reactive.Joins
{");

		for (int i = 2; i <= 16; i++) {
			string s = String.Join (", ", (from t in Enumerable.Range (1, i) select "TSource" + t).ToArray ());
			string s2 = String.Join (", ", (from t in Enumerable.Range (1, i) select "IObservable<TSource" + t + "> t" + t).ToArray ());
			string s3 = String.Join (", ", (from t in Enumerable.Range (1, i) select "t" + t).ToArray ());
			string s4 = String.Join ("\t\t", (from t in Enumerable.Range (1, i) select "IObservable<TSource" + t + "> t" + t + ";\n").ToArray ());
			string s5 = String.Join ("\n\t\t\t", (from t in Enumerable.Range (1, i) select "this.t" + t + " = t" + t + ";").ToArray ());
			string s6 = String.Join ("\n\t\t\t", (from t in Enumerable.Range (1, i) select "var q" + t + " = new Queue<TSource" + t + "> ();").ToArray ());

			string s7 = null;
			foreach (var t in Enumerable.Range (1, i)) {
				string s8 = String.Join (" && ", (from t2 in Enumerable.Range (1, i) select "q" + t2 + ".Count > 0").ToArray ());
				string s9 = String.Join (", ", (from t2 in Enumerable.Range (1, i) select "q" + t2 + ".Dequeue ()").ToArray ());

				s7 += String.Format (@"
			t{0}.Subscribe (Observer.Create<TSource{0}> (t => {{
				q{0}.Enqueue (t);
				if ({1})
					sub.OnNext (selector ({2}));
			}}, (ex) => sub.OnError (ex), () => {{
				done [{3}] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}}));
			", t, s8, s9, t - 1);
			}

			Console.Write (@"
	public class Pattern<{0}> : Pattern
	{{
		internal Pattern ({2})
		{{
			{5}
		}}
		
		{4}

		{6}
		public Pattern<{0}, TSource{1}> And<TSource{1}> (IObservable<TSource{1}> other)
		{{
			return new Pattern<{0}, TSource{1}> ({3}, other);
		}}
		{7}

		public Plan<TResult> Then<TResult> (Func<{0}, TResult> selector)
		{{
			return new Plan<{0}, TResult> (this, selector);
		}}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<{0}, TResult> selector)
		{{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [{8}];
			{9}

			{10}

			return sub;
		}}
	}}
	
	internal class Plan<{0}, TResult> : Plan<TResult>
	{{
		Pattern<{0}> pattern;
		Func<{0}, TResult> selector;
		
		public Plan (Pattern<{0}> pattern, Func<{0}, TResult> selector)
		{{
			this.pattern = pattern;
			this.selector = selector;
		}}
		
		internal override IObservable<TResult> AsObservable ()
		{{
			return pattern.AsObservable<TResult> (selector);
		}}
	}}
	", s, i + 1, s2, s3, s4, s5, i == 16 ? "/*" : null, i == 16 ? "*/" : null, i, s6, s7);
		}

		Console.WriteLine (@"
}");
	}
}
