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

#pragma warning disable 0169, 0649, 0414

namespace System.Reactive.Joins
{");

		for (int i = 2; i <= 16; i++) {
			string s = String.Join (", ", (from t in Enumerable.Range (1, i) select "T" + t).ToArray ());
			string s2 = String.Join (", ", (from t in Enumerable.Range (1, i) select "IObservable<T" + t + "> t" + t).ToArray ());
			string s3 = String.Join (", ", (from t in Enumerable.Range (1, i) select "t" + t).ToArray ());
			string s4 = String.Join ("\t\t", (from t in Enumerable.Range (1, i) select "IObservable<T" + t + "> t" + t + ";\n").ToArray ());
			Console.Write (@"
	public class Pattern<{0}> : Pattern
	{{
		internal Pattern ({1})
		{{
		}}
		
		{3}

		{5}
		public Pattern<{0}, T{4}> And<T{4}> (IObservable<T{4}> other)
		{{
			return new Pattern<{0}, T{4}> ({2}, other);
		}}
		{6}

		public Plan<TResult> Then<TResult> (Func<{0}, TResult> selector)
		{{
			return new Plan<{0}, TResult> (this, selector);
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
	}}
	", s, s2, s3, s4, i + 1, i == 16 ? "/*" : null, i == 16 ? "*/" : null);
		}

		Console.WriteLine (@"
}");
	}
}
