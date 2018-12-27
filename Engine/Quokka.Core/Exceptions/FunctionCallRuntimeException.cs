using System;

namespace Mindbox.Quokka
{
	[Serializable]
	public class FunctionCallRuntimeException : Exception
	{
		public FunctionCallRuntimeException(string message, Exception innerException) : base(message, innerException) {}
	}
}