using System;

namespace Mindbox.Quokka
{
	[Serializable]
	public class InvalidTemplateModelException : TemplateException
	{
		public InvalidTemplateModelException(string message, string details)
			: base($"{message} ({details})")
		{
		}
	}
}
