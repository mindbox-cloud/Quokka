namespace Mindbox.Quokka
{
	public interface IArrayModelDefinition : ICompositeModelDefinition
	{
		IModelDefinition ElementModelDefinition { get; }
	}
}