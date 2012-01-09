using System;
using System.Reactive;
using System.Reactive.Linq;
using NUnit.Framework;

namespace System.Reactive.Linq.Tests
{
	public class ExceptionalObservableException : Exception
	{
	}
	
	public class ExceptionalObservable<T> : IObservable<T>
	{
		public IDisposable Subscribe (IObserver<T> observer)
		{
			throw new ExceptionalObservableException ();
		}
	}
}
