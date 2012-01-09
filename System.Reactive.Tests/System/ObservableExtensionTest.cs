using System;
using NUnit.Framework;
using System.Reactive;
using System.Reactive.Linq;

namespace System.Reactive.Tests
{
	[TestFixture]
	public class ObservableExtensionTest
	{
		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void SubscribeNullSource ()
		{
			ObservableExtensions.Subscribe<int> (null);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void SubscribeSourceOnNextNullSource ()
		{
			ObservableExtensions.Subscribe<int> (null, (int v) => {});
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void SubscribeSourceOnErrorNullSource ()
		{
			ObservableExtensions.Subscribe<int> (null, v => {}, ex => {});
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void SubscribeSourceOnCompletedNullSource ()
		{
			ObservableExtensions.Subscribe<int> (null, (int v) => {}, () => {});
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void SubscribeAllNullSource ()
		{
			ObservableExtensions.Subscribe<int> (null, (int v) => {}, ex => {}, () => {});
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void SubscribeSourceOnNextNullOnNext ()
		{
			ObservableExtensions.Subscribe<int> (Observable.Never<int> (), null);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void SubscribeSourceOnErrorNullOnError ()
		{
			ObservableExtensions.Subscribe<int> (Observable.Never<int> (), v => {}, (Action<Exception>) null);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void SubscribeSourceOnCompletedNullOnCompleted ()
		{
			ObservableExtensions.Subscribe<int> (Observable.Never<int> (), v => {}, (Action) null);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void SubscribeAllNullAllActions ()
		{
			ObservableExtensions.Subscribe<int> (Observable.Never<int> (), null, null, null);
		}
	}
}
