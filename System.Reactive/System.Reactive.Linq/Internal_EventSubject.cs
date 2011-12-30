using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
	class EventObservable<THandlerType, TEventArgs> : IObservable<TEventArgs>
	{
		Func<Action<TEventArgs>, THandlerType> handler_creator;
		Action<THandlerType> add_handler;
		Action<THandlerType> remove_handler;
		
		public EventObservable (Func<Action<TEventArgs>, THandlerType> handlerCreator, Action<THandlerType> addHandler, Action<THandlerType> removeHandler)
		{
			this.handler_creator = handlerCreator;
			add_handler = addHandler;
			remove_handler = removeHandler;
		}
		
		public IDisposable Subscribe (IObserver<TEventArgs> observer)
		{
			Subject<TEventArgs> sub = new Subject<TEventArgs> ();
			var handler = handler_creator (arg => sub.OnNext (arg));
			add_handler (handler);
			var dis = sub.Subscribe (observer);
			return Disposable.Create (() => { dis.Dispose (); remove_handler (handler); });
		}
	}
}
