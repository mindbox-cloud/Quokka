using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ModelvalidationTests
	{
		[TestMethod]
		[ExpectedException(typeof(InvalidTemplateModelException))]
		public void ModelValidation_RootFieldMissing_Error()
		{
			var template = new Template(@"
				${ A }
			");

			template.Render(new CompositeModelValue());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidTemplateModelException))]
		public void ModelValidation_NLevelFieldMissing_Error()
		{
			var template = new Template(@"
				${ Context.Varialbes.Var }
			");

			template.Render(new CompositeModelValue(
				new ModelField("Context",
					new CompositeModelValue(
						new ModelField("Variables",
							new CompositeModelValue(
								new ModelField("AnotherVar", 5)))))));
		}

		[TestMethod]
		public void ModelValidation_RootFieldPrimitiveType_String_Success()
		{
			var template = new Template(@"
				${ A }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("A", "some string")));
		}

		[TestMethod]
		public void ModelValidation_RootFieldPrimitiveType_Integer_Success()
		{
			var template = new Template(@"
				${ A }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("A", 43)));
		}

		[TestMethod]
		public void ModelValidation_RootFieldPrimitiveType_Datetime_Success()
		{
			var template = new Template(@"
				${ A }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("A", new DateTime(2015, 10, 13))));
		}

		[TestMethod]
		public void ModelValidation_RootFieldPrimitiveType_Decimal_Success()
		{
			var template = new Template(@"
				${ A }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("A", 2.5m)));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidTemplateModelException))]
		public void ModelValidation_RootFieldPrimitiveType_Composite_Error()
		{
			var template = new Template(@"
				${ A }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("A",
						new CompositeModelValue(
							new ModelField("SomeVAlue", 5)))));
		}

		[TestMethod]
		public void ModelValidation_RootFieldIntegerType_Integer_Success()
		{
			var template = new Template(@"
				${ A + 5 }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("A", 43)));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidTemplateModelException))]
		public void ModelValidation_RootFieldIntegerType_String_Error()
		{
			var template = new Template(@"
				${ A + 5 }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("A", "some string")));
		}

		[TestMethod]
		public void ModelValidation_ArrayElements_AllValidSuccess()
		{
			var template = new Template(@"
				@{ for item in Elements }
					${ item.Name }
				@{ end for }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("Elements",
						new ArrayModelValue(
							new CompositeModelValue(new ModelField("Name", "Andrew")),
							new CompositeModelValue(new ModelField("Name", "Justin")),
                            new CompositeModelValue(new ModelField("Name", "Carl"))))));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidTemplateModelException))]
		public void ModelValidation_ArrayElements_OneElementInvalid_Error()
		{
			var template = new Template(@"
				@{ for item in Elements }
					${ item.Name }
				@{ end for }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("Elements",
						new ArrayModelValue(
							new CompositeModelValue(new ModelField("Name", "Andrew")),
							new CompositeModelValue(new ModelField("LastName", "Sokoloff")),
							new CompositeModelValue(new ModelField("Name", "Carl"))))));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidTemplateModelException))]
		public void ModelValidation_ArrayElements_OneElementInvalidType_Error()
		{
			var template = new Template(@"
				@{ for item in Elements }
					${ item.Name }
				@{ end for }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("Elements",
						new ArrayModelValue(
							new CompositeModelValue(new ModelField("Name", "Andrew")),
							new PrimitiveModelValue(5),
							new CompositeModelValue(new ModelField("Name", "Carl"))))));
		}
	}
}
