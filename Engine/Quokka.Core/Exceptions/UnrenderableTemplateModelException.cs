using System;

namespace Mindbox.Quokka
{
	/// <summary>
	/// This exceptions occurs when the model technically fits the requirements, but some runtime operations can't be performed.
	/// Examples would be trying to output or use in a condition a null value without checking variable for null first,
	/// trying to divide by zero etc.
	/// </summary>
	[Serializable]
	public class UnrenderableTemplateModelException : TemplateException
	{
		public UnrenderableTemplateModelException(string message)
			: base(message)
		{
		}
	}
}
