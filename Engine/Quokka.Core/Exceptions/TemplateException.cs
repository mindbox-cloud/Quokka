using System;
using System.Runtime.Serialization;

namespace Quokka
{
	[Serializable]
	public class TemplateException : Exception
	{
		public TemplateException()
		{
		}

		public TemplateException(string message)
			: base(message)
		{
		}

		public TemplateException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected TemplateException(
			SerializationInfo info,
			StreamingContext context)
			: base(info, context)
		{
		}
	}
}
