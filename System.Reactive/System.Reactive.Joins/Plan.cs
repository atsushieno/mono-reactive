using System;
using System.Linq;
using System.Linq.Expressions;

namespace System.Reactive.Joins
{
	public abstract class Plan<TResult>
	{
		internal Plan ()
		{
		}
	}
	
	internal class Plan<T, TResult> : Plan<TResult>
	{
		Pattern<T> source;
		Func<T, TResult> selector;
		
		public Plan (Pattern<T> source, Func<T, TResult> selector)
		{
			this.source = source;
			this.selector = selector;
		}
	}
}
