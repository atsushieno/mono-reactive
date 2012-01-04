using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	[SerializableAttribute]
	public struct Timestamped<T>
	{
		T value;
		DateTimeOffset timestamp;
		
		public Timestamped (T value, DateTimeOffset timestamp)
		{
			this.value = value;
			this.timestamp = timestamp;
		}
		
		public DateTimeOffset Timestamp { get { return timestamp; } }
		
		public T Value { get { return value; } }

		public override bool Equals (object obj)
		{
			return obj is Timestamped<T> ? Equals ((Timestamped<T>) obj) : false;
		}
		
		bool Equals (Timestamped<T> other)
		{
			return timestamp == other.timestamp && value.Equals (other.value);
		}
		
		public static bool operator == (Timestamped<T> first, Timestamped<T> second)
		{
			return (object) first == null ? (object) second == null : first.Equals (second);
		}
		
		public static bool operator != (Timestamped<T> first, Timestamped<T> second)
		{
			return (object) first == null ? (object) second != null : !first.Equals (second);
		}
		
		public override int GetHashCode ()
		{
			return value.GetHashCode () ^ 7 + (int) timestamp.Ticks;
		}
		
		public override string ToString ()
		{
			return String.Format ("{0}@{1}", value, timestamp);
		}
	}
}
