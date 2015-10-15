namespace Quokka
{
	internal class ModelDefinition : IModelDefinition
	{
		public string Name { get; }
		public TypeDefinition Type { get; }

		public ModelDefinition(string name, TypeDefinition type)
		{
			Name = name;
			Type = type;
		}
	}
}
