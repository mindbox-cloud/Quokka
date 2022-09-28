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
using System.Collections.Generic;
using System.Linq;

using Mindbox.Quokka.Errors;

namespace Mindbox.Quokka
{
	internal static class ModelDefinitionTools
	{
		public static ICompositeModelDefinition TryCombineModelDefinitions(
			IEnumerable<ICompositeModelDefinition> definitions,
			out IList<ITemplateError> errors)
		{
			if (definitions == null)
				throw new ArgumentNullException(nameof(definitions));

			var errorListener = new ModelDefinitionErrorListener();

			var definitionList = definitions.ToList();
			if (!definitionList.Any())
			{
				errors = new ITemplateError[0];
				return new CompositeModelDefinition(new Dictionary<string, IModelDefinition>());
			}

			if (definitionList.Count == 1)
			{
				errors = new ITemplateError[0];
				return definitionList[0];
			}

			var result = CombineCompositeModelDefinitions(definitionList, "", errorListener);
			errors = errorListener.GetErrors().ToList();

			return errors.Any() ? null : result;
		}

		public static ICompositeModelDefinition CombineModelDefinitions(IEnumerable<ICompositeModelDefinition> definitions)
		{
			var result = TryCombineModelDefinitions(definitions, out var errors);

			if (errors.Any())
				throw new TemplateException("Inconsistent model definitions");

			return result;
		}

		private static ICompositeModelDefinition CombineCompositeModelDefinitions(
			IList<ICompositeModelDefinition> definitions,
			string namePrefix,
			ModelDefinitionErrorListener errorListener)
		{
			var fieldsFromAllDefinitions = definitions
				.SelectMany(d => d.Fields)
				.GroupBy(
					fieldKvp => fieldKvp.Key,
					fieldKvp => fieldKvp.Value,
					StringComparer.InvariantCultureIgnoreCase)
				.Select(grouping => new
				{
					Name = grouping.Key,
					SubDefinition = CombineModelDefinition(
						namePrefix + grouping.Key,
						grouping.ToList(),
						errorListener)
				})
				.ToDictionary(item => item.Name, item => item.SubDefinition, StringComparer.InvariantCultureIgnoreCase);

			var methodsFromAllDefinitions = definitions
				.SelectMany(d => d.Methods)
				.GroupBy(d => d.Key, d => d.Value)
				.ToDictionary(
					grouping => grouping.Key,
					grouping => CombineModelDefinition(grouping.Key.ToString(), grouping.ToList(), errorListener));

			return new CompositeModelDefinition(fieldsFromAllDefinitions, methodsFromAllDefinitions);
		}

		private static IModelDefinition CombineModelDefinition(
			string fieldName,
			List<IModelDefinition> allValues,
			ModelDefinitionErrorListener errorListener)
		{
			if (!allValues.Any())
				throw new ArgumentException("allValues list must not be empty", nameof(allValues));

			// Unknown types are unfortunately just primitive types with TypeDefinition = Unknown.
			// They are compatible with composite types unlike any other primitive type.

			// It's easier to just filter out the Unknown as they don't affect the resulting definition in any way
			// (unless all values are of Unknown type, in which case the result is also Unknown).

			// This needs to be rewrited in a more obvious manner, probably when the type system is reimplemented.

			var meaningfulValues = allValues.Where(
					value =>
					{
						if (!(value is IPrimitiveModelDefinition primitiveValue))
							return true;

						return primitiveValue.Type != TypeDefinition.Unknown;
					})
				.ToList();

			if (!meaningfulValues.Any())
				return new PrimitiveModelDefinition(TypeDefinition.Unknown);

			var firstValue = meaningfulValues[0];

			if (meaningfulValues.Count == 1)
				return firstValue;
			
			switch (firstValue)
			{
				case ICompositeModelDefinition _:
				{
					var compositeValues = meaningfulValues.OfType<ICompositeModelDefinition>().ToList();
					var arrayValues= meaningfulValues.OfType<IArrayModelDefinition>().ToList();
				
					if (compositeValues.Count != meaningfulValues.Count)
					{
						errorListener.AddInconsistenDefinitionTypesError(fieldName);
						return null;
					}

					var combinedCompositeDefinition =
						CombineCompositeModelDefinitions(compositeValues, fieldName + ".", errorListener);

					return arrayValues.Any()
								? new ArrayModelDefinition(
									CombineModelDefinition(
										fieldName + "[].",
										arrayValues.Select(av => av.ElementModelDefinition).ToList(),
										errorListener),
									combinedCompositeDefinition.Fields,
									combinedCompositeDefinition.Methods)
								: combinedCompositeDefinition;
				}
				case IPrimitiveModelDefinition _:
				{
					var primitiveValues = meaningfulValues.OfType<IPrimitiveModelDefinition>().ToList();
					if (primitiveValues.Count != meaningfulValues.Count)
					{
						errorListener.AddInconsistenDefinitionTypesError(fieldName);
						return null;
					}

					var resultingType = TypeDefinition.GetResultingTypeForMultipleOccurrences(
						primitiveValues,
						primitiveValue => primitiveValue.Type,
						(primitiveValue, correctType) => errorListener.AddInconsistenDefinitionTypesError(fieldName));
					return new PrimitiveModelDefinition(resultingType);
				}
				default:
					throw new InvalidOperationException("Unknown model definition type");
			}
		}
	}
}
