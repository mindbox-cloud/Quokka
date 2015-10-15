namespace Quokka
{
	internal class PrimitiveModelValue<TValue> : IPrimitiveModelValue
	{
		public TValue Value { get; }

		public PrimitiveModelValue(TValue value)
		{
			Value = value;
		}

		public bool TryGetValue<TExpectedValue>(out TExpectedValue value)
		{

			if (typeof(TExpectedValue) != typeof(TValue))
			{
				value = default(TExpectedValue);
				return false;
			}
			else
			{
				value = (TExpectedValue)(object)Value;
				return true;
			}
		}
	}
}