// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

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