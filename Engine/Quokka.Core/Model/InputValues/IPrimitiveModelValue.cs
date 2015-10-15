namespace Quokka
{
	public interface IPrimitiveModelValue : IModelValue
	{
		bool TryGetValue<TValue>(out TValue value);
	}
}