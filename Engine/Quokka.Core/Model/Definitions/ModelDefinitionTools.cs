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
			IList<ITemplateError> errors;
			var result = TryCombineModelDefinitions(definitions, out errors);

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

			return new CompositeModelDefinition(fieldsFromAllDefinitions);
		}

		private static IModelDefinition CombineModelDefinition(
			string fieldName,
			List<IModelDefinition> allValues,
			ModelDefinitionErrorListener errorListener)
		{
			var firstValue = allValues[0];

			if (allValues.Count == 1)
				return firstValue;
			
			if (firstValue is ICompositeModelDefinition)
			{
				if (firstValue is IArrayModelDefinition)
				{
					var arrayValues = allValues.OfType<IArrayModelDefinition>().ToList();
					if (arrayValues.Count != allValues.Count)
					{
						errorListener.AddInconsistenDefinitionTypesError(fieldName);
						return null;
					}

					return
						new ArrayModelDefinition(
							CombineModelDefinition(fieldName + "[].",
								arrayValues.Select(av => av.ElementModelDefinition).ToList(),
								errorListener));
				}

				var compositeValues = allValues.OfType<ICompositeModelDefinition>().ToList();
				if (compositeValues.Count != allValues.Count)
				{
					errorListener.AddInconsistenDefinitionTypesError(fieldName);
					return null;
				}

				return CombineCompositeModelDefinitions(compositeValues, fieldName + ".", errorListener);
			}

			if (firstValue is IPrimitiveModelDefinition)
			{
				var primitiveValues = allValues.OfType<IPrimitiveModelDefinition>().ToList();
				if (primitiveValues.Count != allValues.Count)
				{
					errorListener.AddInconsistenDefinitionTypesError(fieldName);
					return null;
				}

				var resultingType = TypeDefinition.GetResultingTypeForMultipleOccurences(
					primitiveValues,
					primitiveValue => primitiveValue.Type,
					(primitiveValue, correctType) => errorListener.AddInconsistenDefinitionTypesError(fieldName));
				return new PrimitiveModelDefinition(resultingType);
			}

			throw new InvalidOperationException("Unknown model definition type");
		}
	}
}
