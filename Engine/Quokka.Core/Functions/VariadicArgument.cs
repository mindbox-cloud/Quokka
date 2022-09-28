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

namespace Mindbox.Quokka
{
	public class VariadicArgument<TType> : TemplateFunctionArgument
	{
		public int MinimumOccurrences { get; }

		private readonly ScalarArgument<TType> argument;

		public VariadicArgument(ScalarArgument<TType> argument, int minimumOccurrences = 0) : base(argument.Name)
		{
			MinimumOccurrences = minimumOccurrences;
			this.argument = argument;
		}

		internal override TypeDefinition Type => argument.Type;

		internal override ArgumentValueValidationResult ValidateConstantValue(VariableValueStorage value)
		{
			return argument.ValidateConstantValue(value);
		}

		internal TType ConvertValue(VariableValueStorage value)
		{
			return argument.ConvertValue(value);
		}
	}
}