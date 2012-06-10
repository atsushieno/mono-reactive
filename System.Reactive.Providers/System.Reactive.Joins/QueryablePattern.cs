using System;
using System.Linq;
using System.Linq.Expressions;

namespace System.Reactive.Joins
{
	public abstract class QueryablePattern
	{
		protected QueryablePattern (Expression expression)
		{
		}
		
		public Expression Expression { get; private set; }
	}
}
