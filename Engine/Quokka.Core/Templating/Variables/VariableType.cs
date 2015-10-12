namespace Quokka
{
	// Todo: this is too heavy for enum now and should be refactored into some class structure.
	// The order of fields matters as it defined the priorities.
	public enum VariableType
	{
		Unknown,
		
		Primitive,

		Boolean,
		Decimal,
		Integer,
		String,

		Composite,

		Array
	}
}
