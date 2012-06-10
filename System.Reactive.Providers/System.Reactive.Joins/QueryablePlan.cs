using System;
using System.Linq;
using System.Linq.Expressions;

namespace System.Reactive.Joins
{
	public class QueryablePlan<TResult>
	{
		internal QueryablePlan ()
		{
		}
		
		public Expression Expression { get; private set; }
	}
}
