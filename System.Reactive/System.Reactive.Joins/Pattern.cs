using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;

namespace System.Reactive.Joins
{
	public abstract class Pattern
	{
		internal Pattern ()
		{
		}
	}
	
	public class Pattern<TSource1> : Pattern
	{
		internal Pattern (IObservable<TSource1> t)
		{
			this.t = t;
		}
		
		IObservable<TSource1> t;

		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TResult> selector)
		{
			return t.Select (selector);
		}

		public Plan<TResult> Then<TResult> (Func<TSource1,TResult> selector)
		{
			throw new NotImplementedException ();
		}
	}
}
