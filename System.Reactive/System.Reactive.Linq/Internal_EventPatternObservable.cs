using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
	class EventObservable2<THandlerType, T> : IObservable<T>
	{
		Func<ISubject<T>, THandlerType> handler_creator;
		Action<THandlerType> add_handler;
		Action<THandlerType> remove_handler;
		
		public EventObservable2 (Func<ISubject<T>, THandlerType> handlerCreator, Action<THandlerType> addHandler, Action<THandlerType> removeHandler)
		{
			handler_creator = handlerCreator;
			add_handler = addHandler;
			remove_handler = removeHandler;
		}
		
		public IDisposable Subscribe (IObserver<T> observer)
		{
			var sub = new Subject<T> ();
			var handler = handler_creator (sub);
			add_handler (handler);
			var dis = sub.Subscribe (observer);
			return Disposable.Create (() => { dis.Dispose (); remove_handler (handler); });
		}
	}

	class EventPatternObservableGeneric<TEventArgs> : IObservable<EventPattern<TEventArgs>> where TEventArgs : EventArgs
	{
		Action<EventHandler<TEventArgs>> add_handler;
		Action<EventHandler<TEventArgs>> remove_handler;
		
		public EventPatternObservableGeneric (Action<EventHandler<TEventArgs>> addHandler, Action<EventHandler<TEventArgs>> removeHandler)
		{
			add_handler = addHandler;
			remove_handler = removeHandler;
		}
		
		public IDisposable Subscribe (IObserver<EventPattern<TEventArgs>> observer)
		{
			var sub = new Subject<EventPattern<TEventArgs>> ();
			var handler = new EventHandler<TEventArgs> ((o, e) => sub.OnNext (new EventPattern<TEventArgs> (o, e)));
			add_handler (handler);
			var dis = sub.Subscribe (observer);
			return Disposable.Create (() => { dis.Dispose (); remove_handler (handler); });
		}
	}

	class EventPatternObservableNonGeneric : IObservable<EventPattern<EventArgs>>
	{
		Action<EventHandler> add_handler;
		Action<EventHandler> remove_handler;
		
		public EventPatternObservableNonGeneric (Action<EventHandler> addHandler, Action<EventHandler> removeHandler)
		{
			add_handler = addHandler;
			remove_handler = removeHandler;
		}
		
		public IDisposable Subscribe (IObserver<EventPattern<EventArgs>> observer)
		{
			var sub = new Subject<EventPattern<EventArgs>> ();
			var handler = new EventHandler ((o, e) => sub.OnNext (new EventPattern<EventArgs> (o, e)));
			add_handler (handler);
			var dis = sub.Subscribe (observer);
			return Disposable.Create (() => { dis.Dispose (); remove_handler (handler); });
		}
	}

	class EventPatternObservable<TDelegate, TEventArgs> : IObservable<EventPattern<TEventArgs>> where TEventArgs : EventArgs
	{
		Func<EventHandler<TEventArgs>, TDelegate> handler_creator;
		Action<TDelegate> add_handler;
		Action<TDelegate> remove_handler;
		
		public EventPatternObservable (Func<EventHandler<TEventArgs>, TDelegate> handlerCreator, Action<TDelegate> addHandler, Action<TDelegate> removeHandler)
		{
			handler_creator = handlerCreator;
			add_handler = addHandler;
			remove_handler = removeHandler;
		}
		
		public IDisposable Subscribe (IObserver<EventPattern<TEventArgs>> observer)
		{
			var sub = new Subject<EventPattern<TEventArgs>> ();
			var handler = handler_creator ((o, ea) => sub.OnNext (new EventPattern<TEventArgs> (o, ea)));
			add_handler (handler);
			var dis = sub.Subscribe (observer);
			return Disposable.Create (() => { dis.Dispose (); remove_handler (handler); });
		}
	}
}
