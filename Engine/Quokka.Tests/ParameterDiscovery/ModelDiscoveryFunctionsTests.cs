using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ModelDiscoveryFunctionsTests
	{
		[TestMethod]
		public void ModelDiscovery_FunctionCallStringArgument()
		{
			var model = new Template("${ toUpper(Name) }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Name", new PrimitiveModelDefinition(VariableType.String)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FunctionCallBooleanArgument()
		{
			var model = new Template("${ if (IsTest, \"test\", \"no test\") }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"IsTest", new PrimitiveModelDefinition(VariableType.Boolean)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FunctionCallDateTimeArgument()
		{
			var model = new Template("${ formatDateTime(Date, \"hh\") }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Date", new PrimitiveModelDefinition(VariableType.DateTime)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FunctionCallTimeSpanArgument()
		{
			var model = new Template("${ formatTime(Time, \"hh\") }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Time", new PrimitiveModelDefinition(VariableType.TimeSpan)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FunctionCallDecimalArgument()
		{
			var model = new Template("${ formatDecimal(Price, \"N2\") }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Price", new PrimitiveModelDefinition(VariableType.Decimal)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FunctionCallBooleanArgument_ComplexExpression()
		{
			var model = new Template("${ if ((IsTest) or (IsStaging) or (Version > 1), \"test\", \"no test\") }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"IsTest", new PrimitiveModelDefinition(VariableType.Boolean)
					},
					{
						"IsStaging", new PrimitiveModelDefinition(VariableType.Boolean)
					},
					{
						"Version", new PrimitiveModelDefinition(VariableType.Integer)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_NestedFunctionCalls()
		{
			var model = new Template("${ if (A, ToUpper(str1), ToLower(str2)) }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"A", new PrimitiveModelDefinition(VariableType.Boolean)
					},
					{
						"str1", new PrimitiveModelDefinition(VariableType.String)
					},
					{
						"str2", new PrimitiveModelDefinition(VariableType.String)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FilterChain_SingleFilter_String()
		{
			var model = new Template("${ Name | toUpper() }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Name", new PrimitiveModelDefinition(VariableType.String)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FilterChain_SingleFilter_Boolean()
		{
			var model = new Template("${ IsTest | if (\"A\", \"B\") }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"IsTest", new PrimitiveModelDefinition(VariableType.Boolean)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FilterChain_MultipleFilters()
		{
			var model = new Template("${ IsTest | if (\"test-site\", \"\") | replaceIfEmpty(BackupValue) }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"BackupValue", new PrimitiveModelDefinition(VariableType.String)
					},
                    {
						"IsTest", new PrimitiveModelDefinition(VariableType.Boolean)
					}
				}),
				model);
		}
	}
}
