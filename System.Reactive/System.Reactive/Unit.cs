using System;
using System.Reactive.Concurrency;

namespace System.Reactive
{
	[SerializableAttribute]
	public struct Unit : IEquatable<Unit>
	{
		static readonly Unit default_instance = new Unit ();

		public static Unit Default { get { return default_instance; } }

		public override bool Equals (object obj)
		{
			return obj is Unit;
		}
		
		public bool Equals (Unit other)
		{
			return true;
		}
		
		public static bool operator == (Unit first, Unit second)
		{
			return (object) first == null ? (object) second == null : first.Equals (second);
		}
		
		public static bool operator != (Unit first, Unit second)
		{
			return (object) first == null ? (object) second != null : !first.Equals (second);
		}
		
		public override int GetHashCode ()
		{
			return 0;
		}

		public override string ToString ()
		{
			// FIXME: return the actual value
			return string.Format ("[Unit]");
		}
	}
}
