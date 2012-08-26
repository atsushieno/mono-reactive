using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using NUnit.Framework;

namespace System.Reactive.Subjects.Tests
{
	public class SubjectTest
	{
		[Test]
		public void SubscriptionAfterNotification ()
		{
			var s = new Subject<int> ();
			s.OnNext (123);
			s.Subscribe (i => { Assert.Fail ("should not raise"); });
		}
	}
}

