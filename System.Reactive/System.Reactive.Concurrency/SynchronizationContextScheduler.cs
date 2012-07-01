using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace System.Reactive.Concurrency
{
#if REACTIVE_2_0
	public class SynchronizationContextScheduler : LocalScheduler
#else
	public class SynchronizationContextScheduler : IScheduler
#endif
	{
		public SynchronizationContextScheduler (SynchronizationContext context)
			: this (context, true)
		{
		}
		
#if REACTIVE_2_0
		public
#endif
		SynchronizationContextScheduler (SynchronizationContext context, bool alwaysPost)
		{
			if (context == null)
				throw new ArgumentNullException ("context");
			this.context = context;
			always_post = alwaysPost;
		}
		
		SynchronizationContext context;
		bool always_post;
		
#if !REACTIVE_2_0
		public DateTimeOffset Now {
			get { return Scheduler.Now; }
		}
		
		public IDisposable Schedule<TState> (TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
		{
			return Schedule (state, dueTime - Now, action);
		}
#endif
		
#if REACTIVE_2_0
		public override IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#else
		public IDisposable Schedule<TState> (TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
#endif
		{
			var dis = new SingleAssignmentDisposable ();
			int dueTimeMillis = (int) Scheduler.Normalize (dueTime).TotalMilliseconds;
			if (!always_post && dueTimeMillis == 0 && Object.ReferenceEquals (SynchronizationContext.Current, context))
				dis.Disposable = action (this, state);
			else {
				context.Post (stat => {
					Thread.Sleep (dueTimeMillis);
					dis.Disposable = new ContextDisposable (context, action (this, state));
					}, state);
			}
			return dis;
		}

#if REACTIVE_2_0
		public override IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
#else
		public IDisposable Schedule<TState> (TState state, Func<IScheduler, TState, IDisposable> action)
#endif
		{
			return Schedule (state, TimeSpan.Zero, action);
		}
	}
}
