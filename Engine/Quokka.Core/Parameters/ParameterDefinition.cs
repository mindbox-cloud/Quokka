namespace Quokka
{
	internal class ParameterDefinition : IParameterDefinition
	{
		public string Name { get; }
		public VariableType Type { get; }

		public ParameterDefinition(string name, VariableType type)
		{
			Name = name;
			Type = type;
		}
	}
}
