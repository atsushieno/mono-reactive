using System;
using System.Linq;
using System.Linq.Expressions;

namespace System.Reactive.Linq
{
	public interface IQbservable
	{
		Type ElementType { get; }
		Expression Expression { get; }
		IQbservableProvider Provider { get; }
	}
}
