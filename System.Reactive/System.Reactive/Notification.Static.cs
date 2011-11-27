using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	public static class Notification
	{
		public static Notification<T> CreateOnCompleted<T> ()
		{
			return new Notification<T>.Completed ();
		}
		
		public static Notification<T> CreateOnError<T> (Exception error)
		{
			return new Notification<T>.Error (error);
		}
		
		public static Notification<T> CreateOnNext<T> (T value)
		{
			return new Notification<T>.Next (value);
		}
	}
}
