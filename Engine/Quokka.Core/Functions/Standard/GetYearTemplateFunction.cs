using System;

namespace Mindbox.Quokka
{
    internal class GetYearTemplateFunction : ScalarTemplateFunction<DateTime, int>
    {
        public GetYearTemplateFunction()
            : base(
                "getYear",
                new DateTimeFunctionArgument("dateTime"))
        {
        }

        public override int Invoke(DateTime dateTime)
        {
            return dateTime.Year;
        }
    }
}