using System;

namespace Quokka
{
	public sealed class TemplateFunctionArgument<TType> : TemplateFunctionArgument
	{
		internal override Type RuntimeType => typeof(TType);

		public TemplateFunctionArgument(string name) 
			:base(name)
		{
		}
	}

	public abstract class TemplateFunctionArgument
	{
		public string Name { get; }
		internal abstract Type RuntimeType { get; }

		protected TemplateFunctionArgument(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Argument name should not be null or blank", nameof(name));

			Name = name;
		}
	}
}
