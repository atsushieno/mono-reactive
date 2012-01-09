using System;
using NUnit.Framework;
using System.Reactive;

namespace System.Reactive.Tests
{
	[TestFixture]
	public class NotificationTest
	{
		[Test]
		public void Kind ()
		{
			var n = Notification.CreateOnNext<int> (5);
			Assert.AreEqual (NotificationKind.OnNext, n.Kind, "#1");
			n = Notification.CreateOnError<int> (new Exception ("foobar"));
			Assert.AreEqual (NotificationKind.OnError, n.Kind, "#2");
			n = Notification.CreateOnCompleted<int> ();
			Assert.AreEqual (NotificationKind.OnCompleted, n.Kind, "#3");
		}
		
		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CreateOnErrorNull ()
		{
			Notification.CreateOnError<int> (null);
		}
	}
}

