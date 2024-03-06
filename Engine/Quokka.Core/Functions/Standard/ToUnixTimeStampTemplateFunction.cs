using System;

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka;

public class ToUnixTimeStampTemplateFunction : ScalarTemplateFunction<DateTime, int>
{
    public ToUnixTimeStampTemplateFunction()
        : base("toUnixTimeStamp", new DateTimeFunctionArgument("dateTime"))
    {
    }

    public override int Invoke(RenderSettings settings, DateTime value)
    {
        return (int)new DateTimeOffset(value.ToUniversalTime()).ToUnixTimeSeconds();
    }
}