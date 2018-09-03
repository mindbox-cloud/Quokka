using System;

namespace Mindbox.Quokka
{
    internal class GetMonthTemplateFunction : ScalarTemplateFunction<DateTime, int>
    {
        public GetMonthTemplateFunction()
            : base(
                "getMonth",
                new DateTimeFunctionArgument("dateTime"))
        {
        }

        public override int Invoke(DateTime dateTime)
        {
            return dateTime.Month;
        }
    }
}