namespace Quokka
{
	internal class ParameterField : IParameterField
	{
		public string Name { get; }
		public IParameterValue Value { get; }

		public ParameterField(string name, IParameterValue value)
		{
			Name = name;
			Value = value;
		}

		public ParameterField(string name, object primitiveValue)
			: this(name, new PrimitiveParameterValue(primitiveValue))
		{
		}
	}
}