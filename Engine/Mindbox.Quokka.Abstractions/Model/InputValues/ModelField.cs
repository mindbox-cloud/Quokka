namespace Mindbox.Quokka
{
	public class ModelField : IModelField
	{
		public string Name { get; }
		public IModelValue Value { get; }

		public ModelField(string name, IModelValue value)
		{
			Name = name;
			Value = value;
		}

		public ModelField(string name, object primitiveValue)
			: this(name, new PrimitiveModelValue(primitiveValue))
		{
		}
	}
}