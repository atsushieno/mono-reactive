
using System;
using System.Linq;
using System.Linq.Expressions;

namespace System.Reactive.Joins
{

	public class Pattern<T1, T2> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2)
		{
		}
		
		IObservable<T1> t1;
		IObservable<T2> t2;


		
		public Pattern<T1, T2, T3> And<T3> (IObservable<T3> other)
		{
			return new Pattern<T1, T2, T3> (t1, t2, other);
		}
		

		public Plan<TResult> Then<TResult> (Func<T1, T2, TResult> selector)
		{
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5, T6> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5, T6, T7> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11, IObservable<T12> t12)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11, IObservable<T12> t12, IObservable<T13> t13)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11, IObservable<T12> t12, IObservable<T13> t13, IObservable<T14> t14)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11, IObservable<T12> t12, IObservable<T13> t13, IObservable<T14> t14, IObservable<T15> t15)
		{
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
			throw new NotImplementedException ();
		}
	}
	public class Pattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : Pattern
	{
#pragma warning disable 0169, 0649
		internal Pattern (IObservable<T1> t1, IObservable<T2> t2, IObservable<T3> t3, IObservable<T4> t4, IObservable<T5> t5, IObservable<T6> t6, IObservable<T7> t7, IObservable<T8> t8, IObservable<T9> t9, IObservable<T10> t10, IObservable<T11> t11, IObservable<T12> t12, IObservable<T13> t13, IObservable<T14> t14, IObservable<T15> t15, IObservable<T16> t16)
		{
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
			throw new NotImplementedException ();
		}
	}
}
