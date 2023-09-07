using System.Globalization;

namespace Mindbox.Quokka.Abstractions;

public class RenderSettings
{
    public static RenderSettings Default = new()
    {
        CultureInfo = CultureInfo.CurrentCulture
    };
    
    public CultureInfo CultureInfo { get; set; }
}