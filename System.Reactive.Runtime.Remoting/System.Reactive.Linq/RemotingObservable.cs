using System;
using System.Reactive;
using System.Reactive.Subjects;
using System.Runtime.Remoting.Lifetime;

namespace System.Reactive.Linq
{
	public static class RemotingObservable
	{
		public static IObservable<TSource> Remotable<TSource>(this IObservable<TSource> source)
		{
			throw new NotImplementedException ();
		}
		
		public static IObservable<TSource> Remotable<TSource>(this IObservable<TSource> source, ILease lease)
		{
			throw new NotImplementedException ();
		}
		
		public static IQbservable<TSource> Remotable<TSource>(this IQbservable<TSource> source)
		{
			throw new NotImplementedException ();
		}
		
		public static IQbservable<TSource> Remotable<TSource>(this IQbservable<TSource> source, ILease lease)
		{
			throw new NotImplementedException ();
		}
	}
}

