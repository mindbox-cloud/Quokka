using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ApplyForBlockTests
	{
		[TestMethod]
		public void Apply_ForBlock_SimpleFor_CollectionOfPrimitives_NoItemOutput()
		{
			var template = new Template(@"
				@{ for item in Collection }
					List element
				@{ end for }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"
				
					List element
				
					List element
				
					List element
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_SimpleFor_CollectionOfPrimitives_ItemOutput()
		{
			var template = new Template(@"
				@{ for item in Collection }
					${ item }
				@{ end for }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"
				
					1
				
					2
				
					3
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_SimpleFor_CollectionOfComposites_NoItemOutput()
		{
			var template = new Template(@"
				@{ for item in Collection }
					List element
				@{ end for }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new CompositeModelValue(
								new ModelField("Name", "Carl")),
							new CompositeModelValue(
								new ModelField("Name", "Ashley")),
							new CompositeModelValue(
								new ModelField("Name", "Malcolm"))))));

			var expected = @"
				
					List element
				
					List element
				
					List element
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_SimpleFor_CollectionOfComposites_FirstLevelMemberOutput()
		{
			var template = new Template(@"
				@{ for item in Collection }
					${ item.Name }
				@{ end for }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new CompositeModelValue(
								new ModelField("Name", "Carl")),
							new CompositeModelValue(
								new ModelField("Name", "Ashley")),
							new CompositeModelValue(
								new ModelField("Name", "Malcolm"))))));

			var expected = @"
				
					Carl
				
					Ashley
				
					Malcolm
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_SimpleFor_EmptyCollection()
		{
			var template = new Template(@"
				(start)@{ for item in Collection }
					List element
				@{ end for }(end)
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue())));

			var expected = @"
				(start)(end)
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_SimpleFor_CollectionAsANthLevelMember()
		{
			var template = new Template(@"
				@{ for item in Context.Data.Elements }
					${ item }
				@{ end for }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Context",
						new CompositeModelValue(
							new ModelField("Data",
								new CompositeModelValue(
									new ModelField("Elements",
										new ArrayModelValue(
											new PrimitiveModelValue(1),
											new PrimitiveModelValue(2),
											new PrimitiveModelValue(3)))))))));

			var expected = @"
				
					1
				
					2
				
					3
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_SimpleFor_IfOnElement()
		{
			var template = new Template(@"
				@{ for item in Elements }
					@{ if item != 2 }
						${ item }
					@{ end if }
				@{ end for }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Elements",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"
				
					
						1
					
				
					
				
					
						3
					
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_NestedForOnDifferentCollection()
		{
			var template = new Template(@"
				@{ for coef in Coefficients }
					@{ for value in Values }
						${ coef * value }
					@{ end for }
				@{ end for }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Coefficients",
						new ArrayModelValue(
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3))),
					new ModelField("Values",
						new ArrayModelValue(
							new PrimitiveModelValue(5),
							new PrimitiveModelValue(6)))));

			var expected = @"
				
					
						10
					
						12
					
				
					
						15
					
						18
					
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_NestedForOnElementField()
		{
			var template = new Template(@"
				@{ for value in Values }
					@{ for coef in value.Coefficients }
						${ coef * value.Number }
					@{ end for }
				@{ end for }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Values",
						new ArrayModelValue(
							new CompositeModelValue(
								new ModelField("Number", 2),
								new ModelField("Coefficients",
									new ArrayModelValue(
										new PrimitiveModelValue(5),
										new PrimitiveModelValue(6)))),
							new CompositeModelValue(
								new ModelField("Number", 3),
								new ModelField("Coefficients",
									new ArrayModelValue(
										new PrimitiveModelValue(5),
										new PrimitiveModelValue(6))))))));

			var expected = @"
				
					
						10
					
						12
					
				
					
						15
					
						18
					
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_NestedForOnElementItself()
		{
			var template = new Template(@"
				@{ for array in Arrays }
					@{ for number in array }
						${ number }
					@{ end for }
				@{ end for }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Arrays",
						new ArrayModelValue(
							new ArrayModelValue(
								new PrimitiveModelValue(22),
								new PrimitiveModelValue(24)),
							new ArrayModelValue(
								new PrimitiveModelValue(52),
								new PrimitiveModelValue(54))))));

			var expected = @"
				
					
						22
					
						24
					
				
					
						52
					
						54
					
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_MultipleForsOnSameCollection()
		{
			var template = new Template(@"
				@{ for item in Collection }
					${ item }
				@{ end for }
				@{ for item in Collection }
					${ item }
				@{ end for }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"
				
					1
				
					2
				
					3
				
				
					1
				
					2
				
					3
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_CaseInsensitivity()
		{
			var template = new Template(@"
				@{ FoR item In Collection }
					List element
				@{ eNd fOr }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("coLLectiON",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"
				
					List element
				
					List element
				
					List element
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_ForBlock_EmptyBlock()
		{
			var template = new Template(@"@{ for item in Collection }@{ end for }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"";

			Assert.AreEqual(expected, result);
		}
	}
}
