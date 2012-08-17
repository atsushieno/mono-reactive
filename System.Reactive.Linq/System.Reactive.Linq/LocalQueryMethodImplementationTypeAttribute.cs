using System;
using System.ComponentModel;

namespace System.Reactive.Linq
{
	[AttributeUsage (AttributeTargets.Method)]
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public sealed class LocalQueryMethodImplementationTypeAttribute : Attribute
	{
		public LocalQueryMethodImplementationTypeAttribute (Type targetType)
		{
			TargetType = targetType;
		}

		public Type TargetType { get; private set; }
	}
}

