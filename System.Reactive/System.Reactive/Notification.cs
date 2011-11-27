using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	public static class Notification
	{
		public static Notification<T> CreateOnCompleted<T> ()
		{
			throw new NotImplementedException ();
		}
		
		public static Notification<T> CreateOnError<T> (Exception error)
		{
			throw new NotImplementedException ();
		}
		
		public static Notification<T> CreateOnNext<T> (T value)
		{
			throw new NotImplementedException ();
		}
	}
}
