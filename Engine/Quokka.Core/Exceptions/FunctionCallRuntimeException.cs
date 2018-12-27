using System;

namespace Mindbox.Quokka
{
	public class FunctionCallRuntimeException : Exception
	{
		public FunctionCallRuntimeException(string message, Exception innerException) : base(message, innerException) {}
	}
}