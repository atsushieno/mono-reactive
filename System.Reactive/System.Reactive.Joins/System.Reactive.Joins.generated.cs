
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace System.Reactive.Joins
{

	public class Pattern<TSource1, TSource2> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2)
		{
			this.t1 = t1;
			this.t2 = t2;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;


		
		public Pattern<TSource1, TSource2, TSource3> And<TSource3> (IObservable<TSource3> other)
		{
			return new Pattern<TSource1, TSource2, TSource3> (t1, t2, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [2];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2> pattern;
		Func<TSource1, TSource2, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2> pattern, Func<TSource1, TSource2, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4> And<TSource4> (IObservable<TSource4> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4> (t1, t2, t3, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [3];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3> pattern;
		Func<TSource1, TSource2, TSource3, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3> pattern, Func<TSource1, TSource2, TSource3, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5> And<TSource5> (IObservable<TSource5> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5> (t1, t2, t3, t4, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [4];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4> pattern, Func<TSource1, TSource2, TSource3, TSource4, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6> And<TSource6> (IObservable<TSource6> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6> (t1, t2, t3, t4, t5, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [5];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5, IObservable<TSource6> t6)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;
		IObservable<TSource6> t6;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7> And<TSource7> (IObservable<TSource7> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7> (t1, t2, t3, t4, t5, t6, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [6];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();
			var q6 = new Queue<TSource6> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<TSource6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5, IObservable<TSource6> t6, IObservable<TSource7> t7)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
			this.t7 = t7;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;
		IObservable<TSource6> t6;
		IObservable<TSource7> t7;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8> And<TSource8> (IObservable<TSource8> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8> (t1, t2, t3, t4, t5, t6, t7, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [7];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();
			var q6 = new Queue<TSource6> ();
			var q7 = new Queue<TSource7> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<TSource6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<TSource7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5, IObservable<TSource6> t6, IObservable<TSource7> t7, IObservable<TSource8> t8)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
			this.t7 = t7;
			this.t8 = t8;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;
		IObservable<TSource6> t6;
		IObservable<TSource7> t7;
		IObservable<TSource8> t8;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9> And<TSource9> (IObservable<TSource9> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9> (t1, t2, t3, t4, t5, t6, t7, t8, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [8];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();
			var q6 = new Queue<TSource6> ();
			var q7 = new Queue<TSource7> ();
			var q8 = new Queue<TSource8> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<TSource6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<TSource7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<TSource8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5, IObservable<TSource6> t6, IObservable<TSource7> t7, IObservable<TSource8> t8, IObservable<TSource9> t9)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
			this.t7 = t7;
			this.t8 = t8;
			this.t9 = t9;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;
		IObservable<TSource6> t6;
		IObservable<TSource7> t7;
		IObservable<TSource8> t8;
		IObservable<TSource9> t9;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10> And<TSource10> (IObservable<TSource10> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10> (t1, t2, t3, t4, t5, t6, t7, t8, t9, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [9];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();
			var q6 = new Queue<TSource6> ();
			var q7 = new Queue<TSource7> ();
			var q8 = new Queue<TSource8> ();
			var q9 = new Queue<TSource9> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<TSource6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<TSource7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<TSource8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<TSource9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5, IObservable<TSource6> t6, IObservable<TSource7> t7, IObservable<TSource8> t8, IObservable<TSource9> t9, IObservable<TSource10> t10)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
			this.t7 = t7;
			this.t8 = t8;
			this.t9 = t9;
			this.t10 = t10;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;
		IObservable<TSource6> t6;
		IObservable<TSource7> t7;
		IObservable<TSource8> t8;
		IObservable<TSource9> t9;
		IObservable<TSource10> t10;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11> And<TSource11> (IObservable<TSource11> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [10];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();
			var q6 = new Queue<TSource6> ();
			var q7 = new Queue<TSource7> ();
			var q8 = new Queue<TSource8> ();
			var q9 = new Queue<TSource9> ();
			var q10 = new Queue<TSource10> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<TSource6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<TSource7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<TSource8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<TSource9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<TSource10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5, IObservable<TSource6> t6, IObservable<TSource7> t7, IObservable<TSource8> t8, IObservable<TSource9> t9, IObservable<TSource10> t10, IObservable<TSource11> t11)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
			this.t7 = t7;
			this.t8 = t8;
			this.t9 = t9;
			this.t10 = t10;
			this.t11 = t11;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;
		IObservable<TSource6> t6;
		IObservable<TSource7> t7;
		IObservable<TSource8> t8;
		IObservable<TSource9> t9;
		IObservable<TSource10> t10;
		IObservable<TSource11> t11;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12> And<TSource12> (IObservable<TSource12> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [11];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();
			var q6 = new Queue<TSource6> ();
			var q7 = new Queue<TSource7> ();
			var q8 = new Queue<TSource8> ();
			var q9 = new Queue<TSource9> ();
			var q10 = new Queue<TSource10> ();
			var q11 = new Queue<TSource11> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<TSource6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<TSource7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<TSource8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<TSource9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<TSource10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<TSource11> (t => {
				q11.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [10] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5, IObservable<TSource6> t6, IObservable<TSource7> t7, IObservable<TSource8> t8, IObservable<TSource9> t9, IObservable<TSource10> t10, IObservable<TSource11> t11, IObservable<TSource12> t12)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
			this.t7 = t7;
			this.t8 = t8;
			this.t9 = t9;
			this.t10 = t10;
			this.t11 = t11;
			this.t12 = t12;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;
		IObservable<TSource6> t6;
		IObservable<TSource7> t7;
		IObservable<TSource8> t8;
		IObservable<TSource9> t9;
		IObservable<TSource10> t10;
		IObservable<TSource11> t11;
		IObservable<TSource12> t12;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13> And<TSource13> (IObservable<TSource13> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [12];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();
			var q6 = new Queue<TSource6> ();
			var q7 = new Queue<TSource7> ();
			var q8 = new Queue<TSource8> ();
			var q9 = new Queue<TSource9> ();
			var q10 = new Queue<TSource10> ();
			var q11 = new Queue<TSource11> ();
			var q12 = new Queue<TSource12> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<TSource6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<TSource7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<TSource8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<TSource9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<TSource10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<TSource11> (t => {
				q11.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [10] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t12.Subscribe (Observer.Create<TSource12> (t => {
				q12.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [11] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5, IObservable<TSource6> t6, IObservable<TSource7> t7, IObservable<TSource8> t8, IObservable<TSource9> t9, IObservable<TSource10> t10, IObservable<TSource11> t11, IObservable<TSource12> t12, IObservable<TSource13> t13)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
			this.t7 = t7;
			this.t8 = t8;
			this.t9 = t9;
			this.t10 = t10;
			this.t11 = t11;
			this.t12 = t12;
			this.t13 = t13;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;
		IObservable<TSource6> t6;
		IObservable<TSource7> t7;
		IObservable<TSource8> t8;
		IObservable<TSource9> t9;
		IObservable<TSource10> t10;
		IObservable<TSource11> t11;
		IObservable<TSource12> t12;
		IObservable<TSource13> t13;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14> And<TSource14> (IObservable<TSource14> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [13];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();
			var q6 = new Queue<TSource6> ();
			var q7 = new Queue<TSource7> ();
			var q8 = new Queue<TSource8> ();
			var q9 = new Queue<TSource9> ();
			var q10 = new Queue<TSource10> ();
			var q11 = new Queue<TSource11> ();
			var q12 = new Queue<TSource12> ();
			var q13 = new Queue<TSource13> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<TSource6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<TSource7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<TSource8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<TSource9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<TSource10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<TSource11> (t => {
				q11.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [10] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t12.Subscribe (Observer.Create<TSource12> (t => {
				q12.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [11] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t13.Subscribe (Observer.Create<TSource13> (t => {
				q13.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [12] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5, IObservable<TSource6> t6, IObservable<TSource7> t7, IObservable<TSource8> t8, IObservable<TSource9> t9, IObservable<TSource10> t10, IObservable<TSource11> t11, IObservable<TSource12> t12, IObservable<TSource13> t13, IObservable<TSource14> t14)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
			this.t7 = t7;
			this.t8 = t8;
			this.t9 = t9;
			this.t10 = t10;
			this.t11 = t11;
			this.t12 = t12;
			this.t13 = t13;
			this.t14 = t14;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;
		IObservable<TSource6> t6;
		IObservable<TSource7> t7;
		IObservable<TSource8> t8;
		IObservable<TSource9> t9;
		IObservable<TSource10> t10;
		IObservable<TSource11> t11;
		IObservable<TSource12> t12;
		IObservable<TSource13> t13;
		IObservable<TSource14> t14;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15> And<TSource15> (IObservable<TSource15> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [14];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();
			var q6 = new Queue<TSource6> ();
			var q7 = new Queue<TSource7> ();
			var q8 = new Queue<TSource8> ();
			var q9 = new Queue<TSource9> ();
			var q10 = new Queue<TSource10> ();
			var q11 = new Queue<TSource11> ();
			var q12 = new Queue<TSource12> ();
			var q13 = new Queue<TSource13> ();
			var q14 = new Queue<TSource14> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<TSource6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<TSource7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<TSource8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<TSource9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<TSource10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<TSource11> (t => {
				q11.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [10] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t12.Subscribe (Observer.Create<TSource12> (t => {
				q12.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [11] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t13.Subscribe (Observer.Create<TSource13> (t => {
				q13.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [12] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t14.Subscribe (Observer.Create<TSource14> (t => {
				q14.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [13] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5, IObservable<TSource6> t6, IObservable<TSource7> t7, IObservable<TSource8> t8, IObservable<TSource9> t9, IObservable<TSource10> t10, IObservable<TSource11> t11, IObservable<TSource12> t12, IObservable<TSource13> t13, IObservable<TSource14> t14, IObservable<TSource15> t15)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
			this.t7 = t7;
			this.t8 = t8;
			this.t9 = t9;
			this.t10 = t10;
			this.t11 = t11;
			this.t12 = t12;
			this.t13 = t13;
			this.t14 = t14;
			this.t15 = t15;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;
		IObservable<TSource6> t6;
		IObservable<TSource7> t7;
		IObservable<TSource8> t8;
		IObservable<TSource9> t9;
		IObservable<TSource10> t10;
		IObservable<TSource11> t11;
		IObservable<TSource12> t12;
		IObservable<TSource13> t13;
		IObservable<TSource14> t14;
		IObservable<TSource15> t15;


		
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16> And<TSource16> (IObservable<TSource16> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [15];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();
			var q6 = new Queue<TSource6> ();
			var q7 = new Queue<TSource7> ();
			var q8 = new Queue<TSource8> ();
			var q9 = new Queue<TSource9> ();
			var q10 = new Queue<TSource10> ();
			var q11 = new Queue<TSource11> ();
			var q12 = new Queue<TSource12> ();
			var q13 = new Queue<TSource13> ();
			var q14 = new Queue<TSource14> ();
			var q15 = new Queue<TSource15> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<TSource6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<TSource7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<TSource8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<TSource9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<TSource10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<TSource11> (t => {
				q11.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [10] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t12.Subscribe (Observer.Create<TSource12> (t => {
				q12.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [11] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t13.Subscribe (Observer.Create<TSource13> (t => {
				q13.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [12] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t14.Subscribe (Observer.Create<TSource14> (t => {
				q14.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [13] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t15.Subscribe (Observer.Create<TSource15> (t => {
				q15.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [14] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16> : Pattern
	{
		internal Pattern (IObservable<TSource1> t1, IObservable<TSource2> t2, IObservable<TSource3> t3, IObservable<TSource4> t4, IObservable<TSource5> t5, IObservable<TSource6> t6, IObservable<TSource7> t7, IObservable<TSource8> t8, IObservable<TSource9> t9, IObservable<TSource10> t10, IObservable<TSource11> t11, IObservable<TSource12> t12, IObservable<TSource13> t13, IObservable<TSource14> t14, IObservable<TSource15> t15, IObservable<TSource16> t16)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
			this.t7 = t7;
			this.t8 = t8;
			this.t9 = t9;
			this.t10 = t10;
			this.t11 = t11;
			this.t12 = t12;
			this.t13 = t13;
			this.t14 = t14;
			this.t15 = t15;
			this.t16 = t16;
		}
		
		IObservable<TSource1> t1;
		IObservable<TSource2> t2;
		IObservable<TSource3> t3;
		IObservable<TSource4> t4;
		IObservable<TSource5> t5;
		IObservable<TSource6> t6;
		IObservable<TSource7> t7;
		IObservable<TSource8> t8;
		IObservable<TSource9> t9;
		IObservable<TSource10> t10;
		IObservable<TSource11> t11;
		IObservable<TSource12> t12;
		IObservable<TSource13> t13;
		IObservable<TSource14> t14;
		IObservable<TSource15> t15;
		IObservable<TSource16> t16;


		/*
		public Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16, TSource17> And<TSource17> (IObservable<TSource17> other)
		{
			return new Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16, TSource17> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, other);
		}
		*/

		public Plan<TResult> Then<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16, TResult> selector)
		{
			return new Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [16];
			var q1 = new Queue<TSource1> ();
			var q2 = new Queue<TSource2> ();
			var q3 = new Queue<TSource3> ();
			var q4 = new Queue<TSource4> ();
			var q5 = new Queue<TSource5> ();
			var q6 = new Queue<TSource6> ();
			var q7 = new Queue<TSource7> ();
			var q8 = new Queue<TSource8> ();
			var q9 = new Queue<TSource9> ();
			var q10 = new Queue<TSource10> ();
			var q11 = new Queue<TSource11> ();
			var q12 = new Queue<TSource12> ();
			var q13 = new Queue<TSource13> ();
			var q14 = new Queue<TSource14> ();
			var q15 = new Queue<TSource15> ();
			var q16 = new Queue<TSource16> ();

			
			t1.Subscribe (Observer.Create<TSource1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<TSource2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<TSource3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<TSource4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<TSource5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<TSource6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<TSource7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<TSource8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<TSource9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<TSource10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<TSource11> (t => {
				q11.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [10] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t12.Subscribe (Observer.Create<TSource12> (t => {
				q12.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [11] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t13.Subscribe (Observer.Create<TSource13> (t => {
				q13.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [12] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t14.Subscribe (Observer.Create<TSource14> (t => {
				q14.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [13] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t15.Subscribe (Observer.Create<TSource15> (t => {
				q15.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [14] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t16.Subscribe (Observer.Create<TSource16> (t => {
				q16.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [15] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			

			return sub;
		}
	}
	
	internal class Plan<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16, TResult> : Plan<TResult>
	{
		Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16> pattern;
		Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16, TResult> selector;
		
		public Plan (Pattern<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16> pattern, Func<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TSource16, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
}
