namespace Quokka
{
	public interface IParameterField
	{
		string Name { get; }
		IParameterValue Value { get; }
	}
}