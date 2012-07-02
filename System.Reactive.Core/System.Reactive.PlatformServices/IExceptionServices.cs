using System;
using System.ComponentModel;

namespace System.Reactive.PlatformServices
{
	// Infrastructure
	[EditorBrowsable (EditorBrowsableState.Advanced)]
	public interface IExceptionServices
	{
		void Rethrow (Exception exception);
	}
}

