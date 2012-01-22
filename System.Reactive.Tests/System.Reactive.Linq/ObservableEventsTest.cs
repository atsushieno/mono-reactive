using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using NUnit.Framework;

namespace System.Reactive.Linq.Tests
{
	[TestFixture]
	public class ObservableEventsTest
	{
		public delegate void MyEventHandler (string arg);
		
		public class EventHost
		{
			public event MyEventHandler MyEvent;
			public event EventHandler<EventArgs> StandardEvent;

			public bool HasMyEvent {
				get { return MyEvent != null; }
			}

			public bool HasStandardEvent {
				get { return StandardEvent != null; }
			}
			
			public void FireMyEvent (string s)
			{
				MyEvent (s);
			}
			
			public void FireStandardEvent ()
			{
				StandardEvent (this, EventArgs.Empty);
			}
		}
		
		[Test]
		public void TestAttachAndDetach ()
		{
			var h = new EventHost ();
			Assert.IsFalse (h.HasStandardEvent, "#1");
			var source = Observable.FromEventPattern<EventArgs> (h, "StandardEvent");
			Assert.IsFalse (h.HasStandardEvent, "#2"); // not subscribed yet
			bool received = false;
			var dis = source.Subscribe (v => received = true);
			Assert.IsTrue (h.HasStandardEvent, "#3");
			Assert.IsFalse (received, "#4");
			
			h.FireStandardEvent ();
			Assert.IsTrue (received, "#5");
			dis.Dispose (); // unsubscribe event handler
			Assert.IsFalse (h.HasStandardEvent, "#6");
		}
		
		// FIXME: test MyEvent as well.
	}
}

