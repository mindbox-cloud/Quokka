using System.Collections.Generic;

namespace Mindbox.Quokka
{
    public interface IMethodCallDefinition
    {
        string Name { get; }
        IReadOnlyList<IMethodArgumentDefinition> Arguments { get; }
    }
}