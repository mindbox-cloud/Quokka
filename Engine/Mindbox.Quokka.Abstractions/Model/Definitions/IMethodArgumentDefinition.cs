namespace Mindbox.Quokka;

public interface IMethodArgumentDefinition
{
    TypeDefinition Type { get; }
    object Value { get; }
}