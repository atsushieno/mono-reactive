using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	public static class Notification
	{
		public static Notification<T> CreateOnCompleted<T> ()
		{
			return new Notification<T>.OnCompleted ();
		}
		
		public static Notification<T> CreateOnError<T> (Exception error)
		{
			return new Notification<T>.OnError (error);
		}
		
		public static Notification<T> CreateOnNext<T> (T value)
		{
			return new Notification<T>.OnNext (value);
		}
	}
}
