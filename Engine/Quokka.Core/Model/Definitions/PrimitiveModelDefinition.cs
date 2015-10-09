namespace Quokka
{
	internal class PrimitiveModelDefinition : IPrimitiveModelDefinition
	{
		public VariableType Type { get; }

		public PrimitiveModelDefinition(VariableType type)
		{
			Type = type;
		}
	}
}
