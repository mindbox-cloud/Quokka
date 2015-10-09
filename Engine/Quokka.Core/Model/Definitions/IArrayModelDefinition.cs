namespace Quokka
{
	public interface IArrayModelDefinition : IModelDefinition
	{
		IModelDefinition ElementModelDefinition { get; }
	}
}