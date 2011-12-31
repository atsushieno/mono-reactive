
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

	public class Pattern<T1, T2> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2)
		{
			this.t1 = t1;
			this.t2 = t2;
		}
		
		IObservable<T1> t1;
		IObservable<T2> t2;


		
		public Pattern<T1, T2, T3> And<T3> (IObservable<T3> other)
		{
			return new Pattern<T1, T2, T3> (t1, t2, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, TResult> selector)
		{
			return new Plan<T1, T2, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [2];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
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
	
	internal class Plan<T1, T2, TResult> : Plan<TResult>
	{
		Pattern<T1, T2> pattern;
		Func<T1, T2, TResult> selector;
		
		public Plan (Pattern<T1, T2> pattern, Func<T1, T2, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
		}
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;


		
		public Pattern<T1, T2, T3, T4> And<T4> (IObservable<T4> other)
		{
			return new Pattern<T1, T2, T3, T4> (t1, t2, t3, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, TResult> selector)
		{
			return new Plan<T1, T2, T3, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [3];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
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
	
	internal class Plan<T1, T2, T3, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3> pattern;
		Func<T1, T2, T3, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3> pattern, Func<T1, T2, T3, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
		}
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;


		
		public Pattern<T1, T2, T3, T4, T5> And<T5> (IObservable<T5> other)
		{
			return new Pattern<T1, T2, T3, T4, T5> (t1, t2, t3, t4, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [4];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4> pattern;
		Func<T1, T2, T3, T4, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4> pattern, Func<T1, T2, T3, T4, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
		}
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;


		
		public Pattern<T1, T2, T3, T4, T5, T6> And<T6> (IObservable<T6> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6> (t1, t2, t3, t4, t5, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [5];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5> pattern;
		Func<T1, T2, T3, T4, T5, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5> pattern, Func<T1, T2, T3, T4, T5, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5, T6> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
		}
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;
		IObservable<T6> t6;


		
		public Pattern<T1, T2, T3, T4, T5, T6, T7> And<T7> (IObservable<T7> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6, T7> (t1, t2, t3, t4, t5, t6, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, T6, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, T6, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, T6, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [6];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();
			var q6 = new Queue<T6> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<T6> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, T6, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5, T6> pattern;
		Func<T1, T2, T3, T4, T5, T6, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5, T6> pattern, Func<T1, T2, T3, T4, T5, T6, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5, T6, T7> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
			this.t4 = t4;
			this.t5 = t5;
			this.t6 = t6;
			this.t7 = t7;
		}
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;
		IObservable<T6> t6;
		IObservable<T7> t7;


		
		public Pattern<T1, T2, T3, T4, T5, T6, T7, T8> And<T8> (IObservable<T8> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6, T7, T8> (t1, t2, t3, t4, t5, t6, t7, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, T6, T7, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [7];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();
			var q6 = new Queue<T6> ();
			var q7 = new Queue<T7> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<T6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<T7> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, T6, T7, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5, T6, T7> pattern;
		Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5, T6, T7> pattern, Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8)
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
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;
		IObservable<T6> t6;
		IObservable<T7> t7;
		IObservable<T8> t8;


		
		public Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9> And<T9> (IObservable<T9> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9> (t1, t2, t3, t4, t5, t6, t7, t8, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, T6, T7, T8, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [8];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();
			var q6 = new Queue<T6> ();
			var q7 = new Queue<T7> ();
			var q8 = new Queue<T8> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<T6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<T7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<T8> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, T6, T7, T8, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5, T6, T7, T8> pattern;
		Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5, T6, T7, T8> pattern, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9)
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
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;
		IObservable<T6> t6;
		IObservable<T7> t7;
		IObservable<T8> t8;
		IObservable<T9> t9;


		
		public Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> And<T10> (IObservable<T10> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> (t1, t2, t3, t4, t5, t6, t7, t8, t9, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [9];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();
			var q6 = new Queue<T6> ();
			var q7 = new Queue<T7> ();
			var q8 = new Queue<T8> ();
			var q9 = new Queue<T9> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<T6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<T7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<T8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<T9> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9> pattern;
		Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9> pattern, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10)
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
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;
		IObservable<T6> t6;
		IObservable<T7> t7;
		IObservable<T8> t8;
		IObservable<T9> t9;
		IObservable<T10> t10;


		
		public Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> And<T11> (IObservable<T11> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [10];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();
			var q6 = new Queue<T6> ();
			var q7 = new Queue<T7> ();
			var q8 = new Queue<T8> ();
			var q9 = new Queue<T9> ();
			var q10 = new Queue<T10> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<T6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<T7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<T8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<T9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<T10> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> pattern;
		Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> pattern, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11)
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
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;
		IObservable<T6> t6;
		IObservable<T7> t7;
		IObservable<T8> t8;
		IObservable<T9> t9;
		IObservable<T10> t10;
		IObservable<T11> t11;


		
		public Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> And<T12> (IObservable<T12> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [11];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();
			var q6 = new Queue<T6> ();
			var q7 = new Queue<T7> ();
			var q8 = new Queue<T8> ();
			var q9 = new Queue<T9> ();
			var q10 = new Queue<T10> ();
			var q11 = new Queue<T11> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<T6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<T7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<T8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<T9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<T10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<T11> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pattern;
		Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pattern, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11, IObservable<T12> t12)
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
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;
		IObservable<T6> t6;
		IObservable<T7> t7;
		IObservable<T8> t8;
		IObservable<T9> t9;
		IObservable<T10> t10;
		IObservable<T11> t11;
		IObservable<T12> t12;


		
		public Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> And<T13> (IObservable<T13> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [12];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();
			var q6 = new Queue<T6> ();
			var q7 = new Queue<T7> ();
			var q8 = new Queue<T8> ();
			var q9 = new Queue<T9> ();
			var q10 = new Queue<T10> ();
			var q11 = new Queue<T11> ();
			var q12 = new Queue<T12> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<T6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<T7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<T8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<T9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<T10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<T11> (t => {
				q11.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [10] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t12.Subscribe (Observer.Create<T12> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> pattern;
		Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> pattern, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11, IObservable<T12> t12, IObservable<T13> t13)
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
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;
		IObservable<T6> t6;
		IObservable<T7> t7;
		IObservable<T8> t8;
		IObservable<T9> t9;
		IObservable<T10> t10;
		IObservable<T11> t11;
		IObservable<T12> t12;
		IObservable<T13> t13;


		
		public Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> And<T14> (IObservable<T14> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [13];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();
			var q6 = new Queue<T6> ();
			var q7 = new Queue<T7> ();
			var q8 = new Queue<T8> ();
			var q9 = new Queue<T9> ();
			var q10 = new Queue<T10> ();
			var q11 = new Queue<T11> ();
			var q12 = new Queue<T12> ();
			var q13 = new Queue<T13> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<T6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<T7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<T8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<T9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<T10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<T11> (t => {
				q11.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [10] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t12.Subscribe (Observer.Create<T12> (t => {
				q12.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [11] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t13.Subscribe (Observer.Create<T13> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> pattern;
		Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> pattern, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11, IObservable<T12> t12, IObservable<T13> t13, IObservable<T14> t14)
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
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;
		IObservable<T6> t6;
		IObservable<T7> t7;
		IObservable<T8> t8;
		IObservable<T9> t9;
		IObservable<T10> t10;
		IObservable<T11> t11;
		IObservable<T12> t12;
		IObservable<T13> t13;
		IObservable<T14> t14;


		
		public Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> And<T15> (IObservable<T15> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [14];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();
			var q6 = new Queue<T6> ();
			var q7 = new Queue<T7> ();
			var q8 = new Queue<T8> ();
			var q9 = new Queue<T9> ();
			var q10 = new Queue<T10> ();
			var q11 = new Queue<T11> ();
			var q12 = new Queue<T12> ();
			var q13 = new Queue<T13> ();
			var q14 = new Queue<T14> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<T6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<T7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<T8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<T9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<T10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<T11> (t => {
				q11.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [10] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t12.Subscribe (Observer.Create<T12> (t => {
				q12.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [11] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t13.Subscribe (Observer.Create<T13> (t => {
				q13.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [12] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t14.Subscribe (Observer.Create<T14> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> pattern;
		Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> pattern, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11, IObservable<T12> t12, IObservable<T13> t13, IObservable<T14> t14, IObservable<T15> t15)
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
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;
		IObservable<T6> t6;
		IObservable<T7> t7;
		IObservable<T8> t8;
		IObservable<T9> t9;
		IObservable<T10> t10;
		IObservable<T11> t11;
		IObservable<T12> t12;
		IObservable<T13> t13;
		IObservable<T14> t14;
		IObservable<T15> t15;


		
		public Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> And<T16> (IObservable<T16> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [15];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();
			var q6 = new Queue<T6> ();
			var q7 = new Queue<T7> ();
			var q8 = new Queue<T8> ();
			var q9 = new Queue<T9> ();
			var q10 = new Queue<T10> ();
			var q11 = new Queue<T11> ();
			var q12 = new Queue<T12> ();
			var q13 = new Queue<T13> ();
			var q14 = new Queue<T14> ();
			var q15 = new Queue<T15> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<T6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<T7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<T8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<T9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<T10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<T11> (t => {
				q11.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [10] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t12.Subscribe (Observer.Create<T12> (t => {
				q12.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [11] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t13.Subscribe (Observer.Create<T13> (t => {
				q13.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [12] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t14.Subscribe (Observer.Create<T14> (t => {
				q14.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [13] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t15.Subscribe (Observer.Create<T15> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> pattern;
		Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> pattern, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> selector)
		{
			this.pattern = pattern;
			this.selector = selector;
		}
		
		internal override IObservable<TResult> AsObservable ()
		{
			return pattern.AsObservable<TResult> (selector);
		}
	}
	
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : Pattern
	{
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11, IObservable<T12> t12, IObservable<T13> t13, IObservable<T14> t14, IObservable<T15> t15, IObservable<T16> t16)
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
		
		IObservable<T1> t1;
		IObservable<T2> t2;
		IObservable<T3> t3;
		IObservable<T4> t4;
		IObservable<T5> t5;
		IObservable<T6> t6;
		IObservable<T7> t7;
		IObservable<T8> t8;
		IObservable<T9> t9;
		IObservable<T10> t10;
		IObservable<T11> t11;
		IObservable<T12> t12;
		IObservable<T13> t13;
		IObservable<T14> t14;
		IObservable<T15> t15;
		IObservable<T16> t16;


		/*
		public Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> And<T17> (IObservable<T17> other)
		{
			return new Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, other);
		}
		*/

		public Plan<TResult> Then<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> selector)
		{
			return new Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> (this, selector);
		}
		
		internal IObservable<TResult> AsObservable<TResult> (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> selector)
		{
			var sub = new Subject<TResult> ();
			bool [] done = new bool [16];
			var q1 = new Queue<T1> ();
			var q2 = new Queue<T2> ();
			var q3 = new Queue<T3> ();
			var q4 = new Queue<T4> ();
			var q5 = new Queue<T5> ();
			var q6 = new Queue<T6> ();
			var q7 = new Queue<T7> ();
			var q8 = new Queue<T8> ();
			var q9 = new Queue<T9> ();
			var q10 = new Queue<T10> ();
			var q11 = new Queue<T11> ();
			var q12 = new Queue<T12> ();
			var q13 = new Queue<T13> ();
			var q14 = new Queue<T14> ();
			var q15 = new Queue<T15> ();
			var q16 = new Queue<T16> ();

			
			t1.Subscribe (Observer.Create<T1> (t => {
				q1.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [0] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t2.Subscribe (Observer.Create<T2> (t => {
				q2.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [1] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t3.Subscribe (Observer.Create<T3> (t => {
				q3.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [2] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t4.Subscribe (Observer.Create<T4> (t => {
				q4.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [3] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t5.Subscribe (Observer.Create<T5> (t => {
				q5.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [4] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t6.Subscribe (Observer.Create<T6> (t => {
				q6.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [5] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t7.Subscribe (Observer.Create<T7> (t => {
				q7.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [6] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t8.Subscribe (Observer.Create<T8> (t => {
				q8.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [7] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t9.Subscribe (Observer.Create<T9> (t => {
				q9.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [8] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t10.Subscribe (Observer.Create<T10> (t => {
				q10.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [9] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t11.Subscribe (Observer.Create<T11> (t => {
				q11.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [10] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t12.Subscribe (Observer.Create<T12> (t => {
				q12.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [11] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t13.Subscribe (Observer.Create<T13> (t => {
				q13.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [12] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t14.Subscribe (Observer.Create<T14> (t => {
				q14.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [13] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t15.Subscribe (Observer.Create<T15> (t => {
				q15.Enqueue (t);
				if (q1.Count > 0 && q2.Count > 0 && q3.Count > 0 && q4.Count > 0 && q5.Count > 0 && q6.Count > 0 && q7.Count > 0 && q8.Count > 0 && q9.Count > 0 && q10.Count > 0 && q11.Count > 0 && q12.Count > 0 && q13.Count > 0 && q14.Count > 0 && q15.Count > 0 && q16.Count > 0)
					sub.OnNext (selector (q1.Dequeue (), q2.Dequeue (), q3.Dequeue (), q4.Dequeue (), q5.Dequeue (), q6.Dequeue (), q7.Dequeue (), q8.Dequeue (), q9.Dequeue (), q10.Dequeue (), q11.Dequeue (), q12.Dequeue (), q13.Dequeue (), q14.Dequeue (), q15.Dequeue (), q16.Dequeue ()));
			}, (ex) => sub.OnError (ex), () => {
				done [14] = true;
				if (done.All (b => b))
					sub.OnCompleted ();
			}));
			
			t16.Subscribe (Observer.Create<T16> (t => {
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
	
	internal class Plan<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> : Plan<TResult>
	{
		Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> pattern;
		Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> selector;
		
		public Plan (Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> pattern, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> selector)
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
