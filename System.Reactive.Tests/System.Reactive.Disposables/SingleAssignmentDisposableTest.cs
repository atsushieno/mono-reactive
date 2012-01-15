using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using NUnit.Framework;

namespace System.Reactive.Disposables.Tests
{
	[TestFixture]
	public class SingleAssignmentDisposableTest
	{
		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void AssignMultipleTimes ()
		{
			int i = 0, j = 0;
			var d = new SingleAssignmentDisposable ();
			d.Disposable = Disposable.Create (() => i++);
			d.Disposable = Disposable.Create (() => j++);
		}
		
		[Test]
		[ExpectedException (typeof (InvalidOperationException))] // MS does not throw this, but I believe this should.
		public void AssignMultipleOnceAfterDispose ()
		{
			var d = new SingleAssignmentDisposable ();
			d.Disposable = Disposable.Create (() => {});
			d.Dispose ();
			d.Disposable = Disposable.Create (() => {});
		}
		
		[Test]
		[ExpectedException (typeof (InvalidOperationException))] // MS does not throw this, but I believe this should.
		public void AssignMultipleBothAfterDispose ()
		{
			var d = new SingleAssignmentDisposable ();
			d.Dispose ();
			d.Disposable = Disposable.Create (() => {});
			d.Disposable = Disposable.Create (() => {});
		}
		
		[Test]
		public void AssignNullAfterDispose ()
		{
			var d = new SingleAssignmentDisposable ();
			d.Dispose ();
			d.Disposable = null; // should not result in NRE, nor treated as if it assigned an instance.
			d.Disposable = Disposable.Create (() => {});
		}
		
		[Test]
		public void AssignNull ()
		{
			var d = new SingleAssignmentDisposable ();
			d.Disposable = null;
			d.Disposable = null; // allowed (no effect)
			d.Disposable = Disposable.Create (() => {}); // should be allowed
		}
		
		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void AssignOneThenNull ()
		{
			var d = new SingleAssignmentDisposable ();
			d.Disposable = Disposable.Create (() => {});
			d.Disposable = null; // this is not allowed
		}
	}
}
