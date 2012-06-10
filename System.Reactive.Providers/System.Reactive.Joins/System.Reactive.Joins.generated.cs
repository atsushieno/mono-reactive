
using System;
using System.Linq;
using System.Linq.Expressions;

namespace System.Reactive.Joins
{

	public class QueryablePattern<T1, T2> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3> And<T3> (IObservable<T3> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4> And<T4> (IObservable<T4> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5> And<T5> (IObservable<T5> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5, T6> And<T6> (IObservable<T6> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5, T6> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5, T6, T7> And<T7> (IObservable<T7> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5, T6, T7> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8> And<T8> (IObservable<T8> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9> And<T9> (IObservable<T9> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> And<T10> (IObservable<T10> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> And<T11> (IObservable<T11> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> And<T12> (IObservable<T12> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> And<T13> (IObservable<T13> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> And<T14> (IObservable<T14> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> And<T15> (IObservable<T15> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		
		public QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> And<T16> (IObservable<T16> other)
		{
			throw new NotImplementedException ();
		}
		

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}

	public class QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : QueryablePattern
	{
		internal QueryablePattern ()
			: base (null)
		{
		}

		/*
		public QueryablePattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> And<T17> (IObservable<T17> other)
		{
			throw new NotImplementedException ();
		}
		*/

		public QueryablePlan<TResult> Then<TResult> (Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> selector)
		{
			throw new NotImplementedException ();
		}
	}
}
