using System;
using System.Linq.Expressions;
using System.Reactive;

namespace System.Reactive.Linq
{
	public interface IQbservableProvider
	{
		IQbservable<TResult> CreateQuery<TResult> (Expression expression);
	}
}

