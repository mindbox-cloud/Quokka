namespace Quokka
{
	internal class ParameterOutputBlock : TemplateNodeBase
	{
		public ParameterMetadata Parameter { get; }

		public ParameterOutputBlock(ParameterMetadata parameter)
		{
			Parameter = parameter;
		}
	}
}
