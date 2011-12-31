using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace System.Reactive.Joins
{
	public abstract class Plan<TResult>
	{
		internal Plan ()
		{
		}
		
		internal abstract IObservable<TResult> AsObservable ();
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

		internal override IObservable<TResult> AsObservable ()
		{
			return source.AsObservable<TResult> (selector);
		}
	}
}
