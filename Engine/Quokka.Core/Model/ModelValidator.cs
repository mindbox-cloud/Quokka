using System;
using System.Linq;
using System.Text;

namespace Quokka
{
	internal class ModelValidator
	{
		public void ValidateModel(ICompositeModelValue model, ICompositeModelDefinition requiredModelDefinition)
		{
			var errorMessageBuilder = new StringBuilder();
			if (!ValidateCompositeModel(model, requiredModelDefinition, null, errorMessageBuilder))
				throw new InvalidTemplateModelException(
					"The model doesn't meet template requirements",
					errorMessageBuilder.ToString());
		}

		private bool ValidateCompositeModel(
			ICompositeModelValue model,
			ICompositeModelDefinition requiredModelDefinition,
			string modelPrefix,
			StringBuilder errorMessageBuilder)
		{
			var requiredFields = requiredModelDefinition.Fields.OrderBy(kvp => kvp.Key).ToList();
			var actualFields = model
				.Fields
				.ToDictionary(field => field.Name, StringComparer.InvariantCultureIgnoreCase);

			bool hasErrors = false;
			foreach (var requiredField in requiredFields)
			{
				var fieldFullName = modelPrefix == null 
					? requiredField.Key 
					: $"{modelPrefix}.{requiredField.Key}";

				IModelField actualField;
				if (!actualFields.TryGetValue(requiredField.Key, out actualField))
				{
					hasErrors = true;
					errorMessageBuilder.AppendLine($"Field {fieldFullName} not found");
				}
				else
				{
					var requiredValue = requiredField.Value;
					var actualValue = actualField.Value;

					if (actualValue == null)
					{
						hasErrors = true;
						errorMessageBuilder.AppendLine($"Field {fieldFullName} value is null");
					}
					else
					{
						if (!ValidateValue(requiredValue, actualValue, fieldFullName, errorMessageBuilder))
							hasErrors = true;
					}
				}
			}

			return !hasErrors;
		}

		private bool ValidateValue(
			IModelDefinition requiredValue,
			IModelValue actualValue,
			string fieldFullName,
			StringBuilder errorMessageBuilder)
		{
			if (requiredValue is IPrimitiveModelDefinition)
			{
				var primitiveModelDefinition = (IPrimitiveModelDefinition)requiredValue;
				if (primitiveModelDefinition.Type != VariableType.Unknown)
				{
					if (actualValue is IPrimitiveModelValue)
					{
						return ValidatePrimitiveModel(
							(IPrimitiveModelValue)actualValue,
							primitiveModelDefinition,
							fieldFullName,
							errorMessageBuilder);
					}
					else
					{
						errorMessageBuilder.AppendLine($"Field {fieldFullName} expects a primitive value");
						return false;
					}
				}
				else
				{
					return true;
				}
			}
			else if (requiredValue is ICompositeModelDefinition)
			{
				if (actualValue is ICompositeModelValue)
				{
					return ValidateCompositeModel(
						(ICompositeModelValue)actualValue,
						(ICompositeModelDefinition)requiredValue,
						fieldFullName,
						errorMessageBuilder);
				}
				else
				{
					errorMessageBuilder.AppendLine($"Field {fieldFullName} expects a composite value");
					return false;
				}
			}
			else if (requiredValue is IArrayModelDefinition)
			{
				if (actualValue is IArrayModelValue)
				{
					return ValidateArrayModel(
						(IArrayModelValue)actualValue,
						(IArrayModelDefinition)requiredValue,
						fieldFullName,
						errorMessageBuilder);
				}
				else
				{
					errorMessageBuilder.AppendLine($"Field {fieldFullName} expects an array");
					return false;
				}
			}
			else
			{
				throw new InvalidOperationException("Unexpected model definition type");
			}
		}

		private bool ValidatePrimitiveModel(
			IPrimitiveModelValue model,
			IPrimitiveModelDefinition requiredModelDefinition,
			string modelPrefix,
			StringBuilder errorMessageBuilder)
		{
			bool hasErrors = false;
			if (model.Value == null)
			{
				hasErrors = true;
				errorMessageBuilder.AppendLine($"{modelPrefix} value is null");
			}
			else
			{
				var actualType = VariableTypeTools.GetVariableTypeByRuntimeType(model.Value.GetType());
				if (!VariableTypeTools.IsTypeCompatibleWithRequired(actualType, requiredModelDefinition.Type))
				{
					hasErrors = true;
					errorMessageBuilder.AppendLine(
						$"{modelPrefix} value \"{model.Value}\" is not compatible " +
						$"with a required type {requiredModelDefinition.Type}");
				}
			}

			return !hasErrors;
		}

		private bool ValidateArrayModel(
			IArrayModelValue model,
			IArrayModelDefinition requiredModelDefinition,
			string modelPrefix,
			StringBuilder errorMessageBuilder)
		{
			bool hasErrors = false;
			if (model.Values == null)
			{
				hasErrors = true;
				errorMessageBuilder.AppendLine($"{modelPrefix} values collection is null");
			}
			else
			{
				for (int index = 0; index < model.Values.Count; index++)
				{
					var arrayElementValue = model.Values[index];
					string fullElementName = $"{modelPrefix}[{index}]";

					if (!ValidateValue(requiredModelDefinition.ElementModelDefinition,
						arrayElementValue,
						fullElementName,
						errorMessageBuilder))
					{
						hasErrors = true;
					}
				}
			}

			return !hasErrors;
		}
	}
}
