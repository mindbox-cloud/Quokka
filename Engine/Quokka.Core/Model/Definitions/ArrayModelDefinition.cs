namespace Quokka
{
	internal class ArrayModelDefinition : IArrayModelDefinition
	{
		public IModelDefinition ElementModelDefinition { get; }

		public ArrayModelDefinition(IModelDefinition elementModelDefinition)
		{
			ElementModelDefinition = elementModelDefinition;
		}
	}
}
