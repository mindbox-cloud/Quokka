namespace Quokka
{
	internal class PrimitiveModelDefinition : IPrimitiveModelDefinition
	{
		public TypeDefinition Type { get; }

		public PrimitiveModelDefinition(TypeDefinition type)
		{
			Type = type;
		}
	}
}
