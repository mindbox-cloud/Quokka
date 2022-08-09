using System;

namespace Mindbox.Quokka;

internal class PrimitiveTypeDefinition<TRuntimeType> : TypeDefinition, IPrimitiveTypeDefinition
{
    public Type RuntimeType => typeof(TRuntimeType);

    public PrimitiveTypeDefinition(string name, TypeDefinition baseType, int priority)
        : base(name, baseType, priority)
    {

    }
}