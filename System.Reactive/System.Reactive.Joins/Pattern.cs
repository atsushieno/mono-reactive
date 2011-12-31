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
	
	internal class Pattern<T> : Pattern
	{
		internal Pattern (IObservable<T> t)
		{
			this.t = t;
		}
		
		IObservable<T> t;

		public IObservable<TResult> AsObservable<TResult> (Func<T, TResult> selector)
		{
			return t.Select (selector);
		}
	}
}
