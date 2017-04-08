namespace Mindbox.Quokka
{
	public class PrimitiveModelValue : IPrimitiveModelValue
	{
		public object Value { get; }

		public PrimitiveModelValue(object value)
		{
			Value = value;
		}
	}
}