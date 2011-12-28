using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
	class GroupedSubject<TKey, TElement> : IGroupedObservable<TKey, TElement>, ISubject<TElement>
	{
		TKey key;
		Subject<TElement> sub = new Subject<TElement> ();
		
		public GroupedSubject (TKey key)
		{
			this.key = key;
		}
		
		public TKey Key {
			get { return key; }
		}
		
		public IDisposable Subscribe (IObserver<TElement> observer)
		{
			return sub.Subscribe (observer);
		}
		
		public void OnNext (TElement value)
		{
			sub.OnNext (value);
		}
		
		public void OnError (Exception error)
		{
			sub.OnError (error);
		}
		
		public void OnCompleted ()
		{
			sub.OnCompleted ();
		}
	}
}
