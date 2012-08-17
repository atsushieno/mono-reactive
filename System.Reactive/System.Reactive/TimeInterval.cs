using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	[SerializableAttribute]
	public struct TimeInterval<T> : IEquatable<TimeInterval<T>>
	{
		T value;
		TimeSpan interval;
		
		public TimeInterval (T value, TimeSpan interval)
		{
			this.value = value;
			this.interval = interval;
		}
		
		public TimeSpan Interval { get { return interval; } }
		
		public T Value { get { return value; } }

		public override bool Equals (object obj)
		{
			return obj is TimeInterval<T> ? Equals ((TimeInterval<T>) obj) : false;
		}
		
		public bool Equals (TimeInterval<T> other)
		{
			return interval == other.interval && value.Equals (other.value);
		}
		
		public static bool operator == (TimeInterval<T> first, TimeInterval<T> second)
		{
			return (object) first == null ? (object) second == null : first.Equals (second);
		}
		
		public static bool operator != (TimeInterval<T> first, TimeInterval<T> second)
		{
			return (object) first == null ? (object) second != null : !first.Equals (second);
		}
		
		public override int GetHashCode ()
		{
			return value.GetHashCode () ^ 7 + (int) interval.Ticks;
		}
		
		public override string ToString ()
		{
			return String.Format ("{0}@{1}", value, interval);
		}
	}
}
