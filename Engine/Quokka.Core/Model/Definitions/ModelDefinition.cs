namespace Quokka
{
	internal class ModelDefinition : IModelDefinition
	{
		public string Name { get; }
		public VariableType Type { get; }

		public ModelDefinition(string name, VariableType type)
		{
			Name = name;
			Type = type;
		}
	}
}
