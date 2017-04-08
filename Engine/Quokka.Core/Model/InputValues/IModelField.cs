namespace Mindbox.Quokka
{
	public interface IModelField
	{
		string Name { get; }
		IModelValue Value { get; }
	}
}