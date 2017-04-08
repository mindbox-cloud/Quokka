using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
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
						"Name", new PrimitiveModelDefinition(TypeDefinition.String)
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
						"IsTest", new PrimitiveModelDefinition(TypeDefinition.Boolean)
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
						"Date", new PrimitiveModelDefinition(TypeDefinition.DateTime)
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
						"Time", new PrimitiveModelDefinition(TypeDefinition.TimeSpan)
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
						"Price", new PrimitiveModelDefinition(TypeDefinition.Decimal)
					}
				}),
				model);
		}
		
		[TestMethod]
		public void ModelDiscovery_FunctionCallArithmeticExpressionArgument()
		{
			var model = new Template("${ formatDecimal(A + 5, \"N2\") }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"A", new PrimitiveModelDefinition(TypeDefinition.Decimal)
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
						"IsTest", new PrimitiveModelDefinition(TypeDefinition.Boolean)
					},
					{
						"IsStaging", new PrimitiveModelDefinition(TypeDefinition.Boolean)
					},
					{
						"Version", new PrimitiveModelDefinition(TypeDefinition.Decimal)
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
						"A", new PrimitiveModelDefinition(TypeDefinition.Boolean)
					},
					{
						"str1", new PrimitiveModelDefinition(TypeDefinition.String)
					},
					{
						"str2", new PrimitiveModelDefinition(TypeDefinition.String)
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
						"Name", new PrimitiveModelDefinition(TypeDefinition.String)
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
						"IsTest", new PrimitiveModelDefinition(TypeDefinition.Boolean)
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
						"BackupValue", new PrimitiveModelDefinition(TypeDefinition.String)
					},
					{
						"IsTest", new PrimitiveModelDefinition(TypeDefinition.Boolean)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FunctionDecimal_ArithmeticExpression_Compatible()
		{
			var model = new Template(@"
				${ formatDecimal(A, ""N2"") }
				${ A + 5 }
			")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"A", new PrimitiveModelDefinition(TypeDefinition.Decimal)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FunctionCount_Collection()
		{
			var model = new Template(@"
				${ count(Collection) }
			")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Collection", new ArrayModelDefinition(new PrimitiveModelDefinition(TypeDefinition.Unknown))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FunctionCount_CountOfArrayInArray()
		{
			var model = new Template(@"
				@{ for a in ArrayCollection }
					${ count(a) }
				@{ end for }
			")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"ArrayCollection", new ArrayModelDefinition(
							new ArrayModelDefinition(
								new PrimitiveModelDefinition(TypeDefinition.Unknown)))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FunctionTableRows_AccessToCellValueFields()
		{
			var model = new Template(@"
				@{ for row in tableRows(Collection, 1) }
					@{ for cell in row.Cells }
						${ cell.Value.FirstName }
						${ cell.Value.LastName }
						@{ for x in cell.Value.Array }
							${ x }
						@{ end for }
					@{ end for }
				@{ end for }
			")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Collection", new ArrayModelDefinition(
							new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
							{
								{ "FirstName", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
								{ "LastName", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
								{ "Array", new ArrayModelDefinition(new PrimitiveModelDefinition(TypeDefinition.Primitive)) }
							}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_FunctionTableRows_AccessToCellValueFieldsThroughMultipleIterations()
		{
			var model = new Template(@"
				@{ for row in tableRows(Collection, 1) }
					@{ for cell in row.Cells }
						${ cell.Value.FirstName }
						${ cell.Value.LastName }
						@{ for x in cell.Value.Array }
							${ x }
						@{ end for }
						@{ for x in cell.Value.Array }
							${ x + 5 }
						@{ end for }
					@{ end for }
				@{ end for }

				@{ for row2 in tableRows(Collection, 1) }
					@{ for cell2 in row2.Cells }
						${ cell2.Value.Age + 10 }
					@{ end for }
				@{ end for }
			")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Collection", new ArrayModelDefinition(
							new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
							{
								{ "FirstName", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
								{ "LastName", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
								{ "Age", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
								{ "Array", new ArrayModelDefinition(new PrimitiveModelDefinition(TypeDefinition.Decimal)) }
							}))
					}
				}),
				model);
		}
	}
}
