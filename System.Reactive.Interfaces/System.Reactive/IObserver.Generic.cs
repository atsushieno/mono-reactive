using System;
namespace System.Reactive
{
	public interface IObserver<in TValue, out TResult>
	{
		TResult OnCompleted ();
		TResult OnError (Exception exception);
		TResult OnNext (TValue value);
	}
}

