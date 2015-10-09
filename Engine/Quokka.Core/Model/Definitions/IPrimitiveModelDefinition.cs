namespace Quokka
{
	public interface IPrimitiveModelDefinition : IModelDefinition
	{
		 VariableType Type { get; }
	}
}