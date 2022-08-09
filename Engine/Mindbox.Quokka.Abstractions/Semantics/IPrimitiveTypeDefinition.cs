using System;

namespace Mindbox.Quokka;

interface IPrimitiveTypeDefinition
{
    Type RuntimeType { get; }
}