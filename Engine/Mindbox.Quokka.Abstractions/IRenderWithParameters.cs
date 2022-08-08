using System;
using System.Collections.Generic;

namespace Mindbox.Quokka.Abstractions
{
    public interface IRenderWithParameters
    {
        string Render(Dictionary<string, Func<string>> parameters);
    }
}