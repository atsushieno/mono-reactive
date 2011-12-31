using System;
using System.Linq;
using System.Linq.Expressions;

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
	}
}
