namespace Quokka
{
	internal class PrimitiveParameterValue : IPrimitiveParameterValue
	{
		public object Value { get; }

		public PrimitiveParameterValue(object value)
		{
			Value = value;
		}
	}
}