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
		public class MyEventArgs : EventArgs
		{
			public MyEventArgs (string s)
			{
				Value = s;
			}
			
			public string Value { get; private set; }
		}
		
		public delegate void MyEventHandler (string arg);
		public delegate void MyEventHandler2 (MyEventArgs arg);
		
		public class EventHost
		{
			public event MyEventHandler MyEvent;
			public event MyEventHandler2 MyEvent2;
			public event EventHandler<EventArgs> StandardEvent;

			public bool HasMyEvent {
				get { return MyEvent != null; }
			}

			public bool HasMyEvent2 {
				get { return MyEvent2 != null; }
			}

			public bool HasStandardEvent {
				get { return StandardEvent != null; }
			}
			
			public void FireMyEvent (string s)
			{
				MyEvent (s);
			}
			
			public void FireMyEvent2 (string s)
			{
				MyEvent2 (new MyEventArgs (s));
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
		
		[Test]
		public void TestAttachAndDetach2 ()
		{
			var h = new EventHost ();
			Assert.IsFalse (h.HasMyEvent, "#1");
			string value = null;
			// FromEvent<TDelegate,TEventArgs> (Func<Action<TEventArgs>, TDelegate> conversion, Func<TDelegate> addHandler, Func<TDelegate> removeHandler)
			var source = Observable.FromEvent<MyEventHandler, MyEventArgs> (
				eh => new MyEventHandler (s => eh (new MyEventArgs (s))),
				ev => h.MyEvent += ev,
				ev => h.MyEvent -= ev);
			Assert.IsFalse (h.HasMyEvent, "#2"); // not subscribed yet
			var dis = source.Subscribe (v => value = v.Value);
			Assert.IsTrue (h.HasMyEvent, "#3");
			h.FireMyEvent ("foo");
			Assert.AreEqual ("foo", value, "#4");
			dis.Dispose (); // unsubscribe event handler
			Assert.IsFalse (h.HasMyEvent, "#6");
		}
		
		[Test]
		public void TestAttachAndDetach3 ()
		{
			var h = new EventHost ();
			Assert.IsFalse (h.HasMyEvent2, "#1");
			string value = null;
			// FromEvent<TDelegate,TEventArgs> (Func<TDelegate> addHandler, Func<TDelegate> removeHandler)
			var source = Observable.FromEvent<MyEventHandler2, MyEventArgs> (
				ev => h.MyEvent2 += ev,
				ev => h.MyEvent2 -= ev);
			Assert.IsFalse (h.HasMyEvent2, "#2"); // not subscribed yet
			var dis = source.Subscribe (v => value = v.Value);
			Assert.IsTrue (h.HasMyEvent2, "#3");
			h.FireMyEvent2 ("foo");
			Assert.AreEqual ("foo", value, "#4");
			dis.Dispose (); // unsubscribe event handler
			Assert.IsFalse (h.HasMyEvent2, "#6");
		}
	}
}

