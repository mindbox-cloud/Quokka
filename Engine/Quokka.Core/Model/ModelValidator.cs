using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindbox.Quokka
{
	internal class ModelValidator
	{
		public void ValidateModel(ICompositeModelValue model, ICompositeModelDefinition requiredModelDefinition)
		{
			var validationContext = new ValidationContext();
			ValidateCompositeModel(validationContext, model, requiredModelDefinition, null);

			if (!validationContext.IsValid)
				throw new InvalidTemplateModelException(
					"The model doesn't meet template requirements",
					string.Join(Environment.NewLine, validationContext.ErrorMessages));
		}

		private void ValidateCompositeModel(
			ValidationContext validationContext,
			ICompositeModelValue model,
			ICompositeModelDefinition requiredModelDefinition,
			string modelPrefix)
		{
			ValidationCompositeModelFields(validationContext, model, requiredModelDefinition, modelPrefix);
			ValidationCompositeModelMethods(validationContext, model, requiredModelDefinition, modelPrefix);
		}

		private void ValidationCompositeModelFields(
			ValidationContext validationContext,
			ICompositeModelValue model,
			ICompositeModelDefinition requiredModelDefinition,
			string modelPrefix)
		{
			var requiredFields = requiredModelDefinition.Fields.OrderBy(kvp => kvp.Key).ToList();
			var actualFields = model
				.Fields
				.ToDictionary(field => field.Name, StringComparer.InvariantCultureIgnoreCase);

			foreach (var requiredField in requiredFields)
			{
				var fieldFullName = modelPrefix == null
										? requiredField.Key
										: $"{modelPrefix}.{requiredField.Key}";

				if (!actualFields.TryGetValue(requiredField.Key, out IModelField actualField))
				{
					validationContext.AddError($"Field {fieldFullName} not found");
				}
				else
				{
					var requiredValue = requiredField.Value;
					var actualValue = actualField.Value;

					if (actualValue == null)
						validationContext.AddError($"Field {fieldFullName} value is null");
					else
						ValidateValue(validationContext, requiredValue, actualValue, fieldFullName);
				}
			}
		}

		private void ValidationCompositeModelMethods(
			ValidationContext validationContext,
			ICompositeModelValue model,
			ICompositeModelDefinition requiredModelDefinition,
			string modelPrefix)
		{
			var requiredMethods = requiredModelDefinition.Methods.ToList();
			var actualMethods = model
				.Methods
				.ToDictionary(method => new MethodCall(method.Name, method.Arguments));

			foreach (var requiredMethod in requiredMethods)
			{
				string argumentList = string.Join(", ", requiredMethod.Key.Arguments.Select(arg => $"{arg.Type.Name}: {arg.Value}"));
				var requiredMethodSignature = $"{requiredMethod.Key.Name}({argumentList})";
				var methodFullName = modelPrefix == null
										? requiredMethodSignature
										: $"{modelPrefix}.{requiredMethodSignature}";

				var requiredMethodCall = new MethodCall(
					requiredMethod.Key.Name,
					requiredMethod.Key.Arguments.Select(arg => arg.Value).ToArray());

				if (!actualMethods.TryGetValue(requiredMethodCall, out IModelMethod actualMethod))
				{
					validationContext.AddError($"Method call result for {methodFullName} not found");
				}
				else
				{
					var requiredValue = requiredMethod.Value;
					var actualValue = actualMethod.Value;

					if (actualValue == null)
						validationContext.AddError($"Field {methodFullName} value is null");
					else
						ValidateValue(validationContext, requiredValue, actualValue, methodFullName);
				}
			}
		}

		private void ValidateValue(
			ValidationContext validationContext,
			IModelDefinition requiredValue,
			IModelValue actualValue,
			string fieldFullName)
		{
			if (requiredValue is IPrimitiveModelDefinition primitiveModelDefinition)
			{
				if (primitiveModelDefinition.Type != TypeDefinition.Unknown)
				{
					if (actualValue is IPrimitiveModelValue primitivaActualValue)
					{
						ValidatePrimitiveModel(
							validationContext,
							primitivaActualValue,
							primitiveModelDefinition,
							fieldFullName);
					}
					else
					{
						validationContext.AddError($"Field {fieldFullName} expects a primitive value");
					}
				}
			}
			else if (requiredValue is ICompositeModelDefinition compositeRequiredValue)
			{
				if (actualValue is ICompositeModelValue compositeActualValue)
				{
					ValidateCompositeModel(
						validationContext,
						compositeActualValue,
						compositeRequiredValue,
						fieldFullName);
				}
				else
				{
					validationContext.AddError($"Field {fieldFullName} expects a composite value");
				}
			}
			else if (requiredValue is IArrayModelDefinition arrayRequiredValue)
			{
				if (actualValue is IArrayModelValue arrayActualValue)
				{
					ValidateArrayModel(
						validationContext,
						arrayActualValue,
						arrayRequiredValue,
						fieldFullName);
				}
				else
				{
					validationContext.AddError($"Field {fieldFullName} expects an array");
				}
			}
			else
			{
				throw new InvalidOperationException("Unexpected model definition type");
			}
		}

		private void ValidatePrimitiveModel(
			ValidationContext validationContext,
			IPrimitiveModelValue model,
			IPrimitiveModelDefinition requiredModelDefinition,
			string modelPrefix)
		{
			if (model.Value != null)
			{
				var actualType = TypeDefinition.GetTypeDefinitionByRuntimeType(model.Value.GetType());
				if (!actualType.IsAssignableTo(requiredModelDefinition.Type))
				{
					validationContext.AddError(
						$"{modelPrefix} value \"{model.Value}\" is not compatible " +
						$"with a required type {requiredModelDefinition.Type}");
				}
			}
		}


		private void ValidateArrayModel(
			ValidationContext validationContext,
			IArrayModelValue model,
			IArrayModelDefinition requiredModelDefinition,
			string modelPrefix)
		{
			if (model.Elements == null)
			{
				validationContext.AddError($"{modelPrefix} values collection is null");
			}
			else
			{
				for (int index = 0; index < model.Elements.Count; index++)
				{
					var arrayElementValue = model.Elements[index];
					string fullElementName = $"{modelPrefix}[{index}]";

					ValidateValue(
						validationContext,
						requiredModelDefinition.ElementModelDefinition,
						arrayElementValue,
						fullElementName);
				}
			}
		}

		private class ValidationContext
		{
			public IList<string> ErrorMessages { get; } = new List<string>();

			public bool IsValid => !ErrorMessages.Any();

			public void AddError(string errorMessage)
			{
				ErrorMessages.Add(errorMessage);
			}
		}
	}
}
