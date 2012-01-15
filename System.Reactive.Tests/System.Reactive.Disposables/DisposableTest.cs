using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NUnit.Framework;

namespace System.Reactive.Disposables.Tests
{
	[TestFixture]
	public class DisposableTest
	{
		[Test]
		public void Empty ()
		{
			Assert.IsNotNull (Disposable.Empty);
		}
		
		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void TestCreateNull ()
		{
			Disposable.Create ((Action) null);
		}
		
		[Test]
		public void SimpleCreate ()
		{
			string s = null;
			var dis = Disposable.Create (() => s += "foo");
			Assert.IsNull (s, "#1");
			dis.Dispose ();
			Assert.AreEqual ("foo", s, "#2");
			dis.Dispose (); // call multiple time -> no error
			Assert.AreEqual ("foo", s, "#3"); // and no multiple invocation of action
		}
	}
}
