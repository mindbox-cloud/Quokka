using System;
using System.Linq;
using System.Text;

namespace Mindbox.Quokka
{
    public class TruncateTemplateFunction : ScalarTemplateFunction<string, decimal, string>
    {
        public TruncateTemplateFunction() 
            : base(
                "truncate", 
                new StringFunctionArgument("inputString"), 
                new DecimalFunctionArgument("allowedLength", valueValidator: ValidateAllowedLength))
        {
        }

        private static ArgumentValueValidationResult ValidateAllowedLength(decimal arg)
        {
            return arg >= 5 
                ? new ArgumentValueValidationResult(true) 
                : new ArgumentValueValidationResult(false, "Нельзя ограничить строку менее чем пятью символами");
        }

        public override string Invoke(string inputString, decimal allowedLength)
        {
            var targetLength = Convert.ToInt32(allowedLength);
            if (inputString.Length <= targetLength)
                return inputString;

            var trimmedInput = inputString.Substring(0, targetLength - 3);
		
            var parts = trimmedInput.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var resultBuilder = new StringBuilder(trimmedInput);
            var lastPartSize = parts.Last().Length;
            if (lastPartSize == 1)
                RemoveLast(resultBuilder);
            while (!char.IsLetterOrDigit(resultBuilder[resultBuilder.Length - 1]))
                RemoveLast(resultBuilder);
            return resultBuilder.Append("...").ToString();
        }
        
        private void RemoveLast(StringBuilder stringBuilder)
            => stringBuilder.Remove(stringBuilder.Length - 1, 1);
    }
}