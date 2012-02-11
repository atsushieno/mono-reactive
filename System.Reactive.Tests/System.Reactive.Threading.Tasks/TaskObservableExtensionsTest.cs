using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace System.Reactive.Threading.Tasks.Tests
{
	[TestFixture]
	public class TaskObservableExtensionsTest
	{
		[Test]
		public void ToObservable ()
		{
			ToObservable (0);
		}
		
		[Test]
		public void ToObservable2 ()
		{
			ToObservable (1);
		}
		
		[Test]
		public void ToObservable3 ()
		{
			ToObservable (2);
		}
		
		void ToObservable (int startStage)
		{
			// stupid one.
			var task = new Task<int> (() => {
				Thread.Sleep (100);
				return 5;
				});
			if (startStage == 1)
				task.Start ();
			var source = task.ToObservable ();
			if (startStage == 2)
				task.Start ();
			int result = 0;
			bool done = false;
			source.Subscribe (v => result = v, () => done = true);
			Assert.IsTrue (SpinWait.SpinUntil (() => done, 200), "#1");
			Assert.AreEqual (5, result, "#2");
		}
		
		[Test]
		public void ToTask ()
		{
			// stupid one.
			var task = Observable.Range (0, 3).ToTask ();
			SpinWait.SpinUntil (() => task.Status == TaskStatus.RanToCompletion, 100); // should be enough to wait.
			Assert.AreEqual (TaskStatus.RanToCompletion, task.Status, "#1");
			Assert.AreEqual (2, task.Result, "#2");
		}
	}
}
