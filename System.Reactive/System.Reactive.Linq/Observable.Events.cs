using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
	public static partial class Observable
	{
		public static IObservable<TEventArgs> FromEvent<TEventArgs> (
			Action<Action<TEventArgs>> addHandler,
			Action<Action<TEventArgs>> removeHandler)
		{
			return new EventObservable<Action<TEventArgs>, TEventArgs> (action => action, addHandler, removeHandler);
		}
		
		public static IObservable<Unit> FromEvent (
			Action<Action> addHandler,
			Action<Action> removeHandler)
		{
			return FromEvent<Action, Unit> (au => () => au (Unit.Default), addHandler, removeHandler);
		}
		
		public static IObservable<TEventArgs> FromEvent<TDelegate, TEventArgs> (
			Action<TDelegate> addHandler,
			Action<TDelegate> removeHandler)
		// TDelegate must be a delegate that only takes a TEventArgs (no "object sender")
		{
			if (addHandler == null)
				throw new ArgumentNullException ("addHandler");
			if (removeHandler == null)
				throw new ArgumentNullException ("removeHandler");

			return FromEvent<TDelegate, TEventArgs> (a => CastDelegate<TDelegate> (a), addHandler, removeHandler);
		}
		
		public static IObservable<TEventArgs> FromEvent<TDelegate, TEventArgs> (
			Func<Action<TEventArgs>, TDelegate> conversion,
			Action<TDelegate> addHandler,
			Action<TDelegate> removeHandler)
		{
			if (conversion == null)
				throw new ArgumentNullException ("conversion");
			if (addHandler == null)
				throw new ArgumentNullException ("addHandler");
			if (removeHandler == null)
				throw new ArgumentNullException ("removeHandler");

			return new EventObservable<TDelegate, TEventArgs> (conversion, addHandler, removeHandler);
		}
		
		public static IObservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs> (
			Action<EventHandler<TEventArgs>> addHandler,
			Action<EventHandler<TEventArgs>> removeHandler)
			where TEventArgs : EventArgs
		{
			if (addHandler == null)
				throw new ArgumentNullException ("addHandler");
			if (removeHandler == null)
				throw new ArgumentNullException ("removeHandler");
			
			return new EventPatternObservableGeneric<TEventArgs> (addHandler, removeHandler);
		}
		
		public static IObservable<EventPattern<EventArgs>> FromEventPattern (
			Action<EventHandler> addHandler,
			Action<EventHandler> removeHandler)
		{
			if (addHandler == null)
				throw new ArgumentNullException ("addHandler");
			if (removeHandler == null)
				throw new ArgumentNullException ("removeHandler");

			return new EventPatternObservableNonGeneric (addHandler, removeHandler);
		}

		public static IObservable<EventPattern<TEventArgs>> FromEventPattern<TDelegate, TEventArgs> (
			Action<TDelegate> addHandler,
			Action<TDelegate> removeHandler)
			where TEventArgs : EventArgs
		{
			if (addHandler == null)
				throw new ArgumentNullException ("addHandler");
			if (removeHandler == null)
				throw new ArgumentNullException ("removeHandler");

			return FromEventPattern<TDelegate, TEventArgs> (a => CastDelegate<TDelegate> (a), addHandler, removeHandler);
		}
		
		public static IObservable<EventPattern<EventArgs>> FromEventPattern (
			object target,
			string eventName)
		{
			if (target == null)
				throw new ArgumentNullException ("target");
			if (eventName == null)
				throw new ArgumentNullException ("eventName");

			var type = target.GetType ();
			var evt = type.GetEvent (eventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			return FromEventInfoNonGeneric (evt, target);
		}
		
		public static IObservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs> (
			object target,
			string eventName)
			where TEventArgs : EventArgs
		{
			if (target == null)
				throw new ArgumentNullException ("target");
			if (eventName == null)
				throw new ArgumentNullException ("eventName");

			var type = target.GetType ();
			var evt = type.GetEvent (eventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			return FromEventInfoGeneric<TEventArgs> (evt, target);
		}
		
		public static IObservable<EventPattern<EventArgs>> FromEventPattern (Type type, string eventName)
		{
			if (type == null)
				throw new ArgumentNullException ("type");
			if (eventName == null)
				throw new ArgumentNullException ("eventName");

			var evt = type.GetEvent (eventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
			return FromEventInfoNonGeneric (evt, null);
		}
		
		static IObservable<EventPattern<EventArgs>> FromEventInfoNonGeneric (EventInfo info, object target)
		{
			return new EventPatternObservableNonGeneric (handler => info.AddEventHandler (target, handler), handler => info.RemoveEventHandler (target, handler));
		}
		
		static IObservable<EventPattern<TEventArgs>> FromEventInfoGeneric<TEventArgs> (EventInfo info, object target) where TEventArgs : EventArgs
		{
			return new EventPatternObservableGeneric<TEventArgs> (handler => info.AddEventHandler (target, handler), handler => info.RemoveEventHandler (target, handler));
		}
		
		public static IObservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs> (Type type, string eventName)
			where TEventArgs : EventArgs
		{
			if (type == null)
				throw new ArgumentNullException ("type");
			if (eventName == null)
				throw new ArgumentNullException ("eventName");

			var evt = type.GetEvent (eventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
			return FromEventInfoGeneric<TEventArgs> (evt, null);
		}
		
		public static IObservable<EventPattern<TEventArgs>> FromEventPattern<TDelegate, TEventArgs> (
			Func<EventHandler<TEventArgs>, TDelegate> conversion,
			Action<TDelegate> addHandler,
			Action<TDelegate> removeHandler)
			where TEventArgs : EventArgs
		{
			if (addHandler == null)
				throw new ArgumentNullException ("addHandler");
			if (removeHandler == null)
				throw new ArgumentNullException ("removeHandler");

			return new EventPatternObservable<TDelegate, TEventArgs> (conversion, addHandler, removeHandler);
		}

		
		class EventSource<T> : IEventSource<T>
		{
			public event Action<T> OnNext;
			
			public EventSource (IObservable<T> source)
			{
				source.Subscribe (t => { if (OnNext != null) OnNext (t); });
			}
		}
		
		public static IEventSource<Unit> ToEvent (this IObservable<Unit> source)
		{
			return ToEvent<Unit> (source);
		}
		
		public static IEventSource<TSource> ToEvent<TSource> (this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new EventSource<TSource> (source);
		}
		
		class EventPatternSource<TEventArgs> : IEventPatternSource<TEventArgs>
			where TEventArgs : EventArgs
		{
			public event EventHandler<TEventArgs> OnNext;
			
			public EventPatternSource (IObservable<EventPattern<TEventArgs>> source)
			{
				source.Subscribe ((ep) => { if (OnNext != null) OnNext (ep.Sender, ep.EventArgs); });
			}
		}

		public static IEventPatternSource<TEventArgs> ToEventPattern<TEventArgs> (
			this IObservable<EventPattern<TEventArgs>> source)
			where TEventArgs : EventArgs
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			return new EventPatternSource<TEventArgs> (source);
		}
		
		static T CastDelegate<T> (Delegate source)
		{
			return (T) (object) Delegate.Combine ((from i in source.GetInvocationList () select Delegate.CreateDelegate (typeof (T), i.Target, i.Method)).ToArray ());
		}
	}
}
