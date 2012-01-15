using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using NUnit.Framework;

namespace System.Reactive.Linq.Tests
{
	[TestFixture]
	public class ObservableTest
	{
		[Test]
		public void Concat ()
		{
			int i = 0, j = 0;
			var source = Observable.Range (0, 5).Concat (Observable.Range (11, 3));
			source.Subscribe (v => i += v, () => j++);
			Assert.IsTrue (SpinWait.SpinUntil (() => j != 0, 1000), "#1");
			Assert.AreEqual (46, i, "#2");
		}
		
		[Test]
		public void Do ()
		{
			int i = 0, j = 0, k = 0;
			var source = Observable.Range (0, 5).Do (v => k += v);
			source.Subscribe (v => i += v, () => j++);
			Assert.IsTrue (SpinWait.SpinUntil (() => j != 0, 1000), "#1");
			Assert.AreEqual (10, i, "#2");
			Assert.AreEqual (10, k, "#3");
		}
	}
}
