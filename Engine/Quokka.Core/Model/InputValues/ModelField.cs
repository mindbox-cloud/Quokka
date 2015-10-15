namespace Quokka
{
	internal class ModelField : IModelField
	{
		public string Name { get; }
		public IModelValue Value { get; }

		public ModelField(string name, IModelValue value)
		{
			Name = name;
			Value = value;
		}
		
		public ModelField Primitive<TValue>(string name, TValue value)
		{
			return new ModelField(name, new PrimitiveModelValue<TValue>(value));
		}
	}
}