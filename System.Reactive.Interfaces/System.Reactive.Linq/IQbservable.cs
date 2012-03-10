using System;
using System.Linq;
using System.Linq.Expressions;

namespace System.Reactive.Linq
{
	public interface IQbservable
	{
		Expression Expression { get; }
		IQbservableProvider Provider { get; }
		Type ElementType { get; }
	}
}

