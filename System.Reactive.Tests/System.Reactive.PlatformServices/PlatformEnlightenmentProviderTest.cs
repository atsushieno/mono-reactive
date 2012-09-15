#if REACTIVE_2_0
using System;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using NUnit.Framework;

namespace System.Reactive.PlatformServices.Tests
{
	[TestFixture]
	public class PlatformEnlightenmentProviderTest
	{
		[Test]
		public void Current ()
		{
			var pep = PlatformEnlightenmentProvider.Current;
			Assert.IsNotNull (pep, "#1");
			Assert.IsNotNull (pep.GetService<IConcurrencyAbstractionLayer> (), "#2");
#if NET_4_5
			Assert.IsNotNull (pep.GetService<IExceptionServices> (), "#3");
#endif
		}
	}
}
#endif
