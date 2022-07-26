using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
    public static class DictionaryExtensions
    {
        public static ICompositeModelValue ToCompositeModelValue(this Dictionary<string, Func<string>> dictionary)
        {
            return new CompositeModelValue(
                dictionary.Select(f => new ModelField(f.Key, new PrimitiveModelValue(f.Value())))
            );
        }
    }
}