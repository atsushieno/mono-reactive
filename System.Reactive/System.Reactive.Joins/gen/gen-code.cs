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

namespace System.Reactive.Joins
{");

		for (int i = 2; i <= 16; i++) {
			string s = String.Join (", ", (from t in Enumerable.Range (1, i) select "T" + t).ToArray ());
			Console.Write (@"
	public class Pattern<{0}> : Pattern
	{{
		internal Pattern ()
		{{
		}}

		public Plan<TResult> Then<TResult> (Func<{0}, TResult> selector)
		{{
			throw new NotImplementedException ();
		}}
	}}", s, i + 1, i == 16 ? "/*" : null, i == 16 ? "*/" : null);
		}

		Console.WriteLine (@"
}");
	}
}
