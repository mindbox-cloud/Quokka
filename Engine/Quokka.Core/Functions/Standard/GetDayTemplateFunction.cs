using System;

namespace Mindbox.Quokka
{
    internal class GetDayTemplateFunction : ScalarTemplateFunction<DateTime, int>
    {
        public GetDayTemplateFunction()
            : base(
                "getDay",
                new DateTimeFunctionArgument("dateTime"))
        {
        }

        public override int Invoke(DateTime dateTime)
        {
            return dateTime.Day;
        }
    }
}