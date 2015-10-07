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
				new CompositeParameterValue(
					new ParameterField("Collection",
						new ArrayParameterValue(
							new PrimitiveParameterValue(1),
							new PrimitiveParameterValue(2),
							new PrimitiveParameterValue(3)))));

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
				new CompositeParameterValue(
					new ParameterField("Collection",
						new ArrayParameterValue(
							new PrimitiveParameterValue(1),
							new PrimitiveParameterValue(2),
							new PrimitiveParameterValue(3)))));

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
				new CompositeParameterValue(
					new ParameterField("Collection",
						new ArrayParameterValue(
							new CompositeParameterValue(
								new ParameterField("Name", "Carl")),
							new CompositeParameterValue(
								new ParameterField("Name", "Ashley")),
							new CompositeParameterValue(
								new ParameterField("Name", "Malcolm"))))));

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
				new CompositeParameterValue(
					new ParameterField("Collection",
						new ArrayParameterValue(
							new CompositeParameterValue(
								new ParameterField("Name", "Carl")),
							new CompositeParameterValue(
								new ParameterField("Name", "Ashley")),
							new CompositeParameterValue(
								new ParameterField("Name", "Malcolm"))))));

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
				new CompositeParameterValue(
					new ParameterField("Collection",
						new ArrayParameterValue())));

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
				new CompositeParameterValue(
					new ParameterField("Context",
						new CompositeParameterValue(
							new ParameterField("Data",
								new CompositeParameterValue(
									new ParameterField("Elements",
										new ArrayParameterValue(
											new PrimitiveParameterValue(1),
											new PrimitiveParameterValue(2),
											new PrimitiveParameterValue(3)))))))));

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
				new CompositeParameterValue(
					new ParameterField("Elements",
						new ArrayParameterValue(
							new PrimitiveParameterValue(1),
							new PrimitiveParameterValue(2),
							new PrimitiveParameterValue(3)))));

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
				new CompositeParameterValue(
					new ParameterField("Coefficients",
						new ArrayParameterValue(
							new PrimitiveParameterValue(2),
							new PrimitiveParameterValue(3))),
					new ParameterField("Values",
						new ArrayParameterValue(
							new PrimitiveParameterValue(5),
							new PrimitiveParameterValue(6)))));

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
				new CompositeParameterValue(
					new ParameterField("Values",
						new ArrayParameterValue(
							new CompositeParameterValue(
								new ParameterField("Number", 2),
								new ParameterField("Coefficients",
									new ArrayParameterValue(
										new PrimitiveParameterValue(5),
										new PrimitiveParameterValue(6)))),
							new CompositeParameterValue(
								new ParameterField("Number", 3),
								new ParameterField("Coefficients",
									new ArrayParameterValue(
										new PrimitiveParameterValue(5),
										new PrimitiveParameterValue(6))))))));

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
				new CompositeParameterValue(
					new ParameterField("Arrays",
						new ArrayParameterValue(
							new ArrayParameterValue(
								new PrimitiveParameterValue(22),
								new PrimitiveParameterValue(24)),
							new ArrayParameterValue(
								new PrimitiveParameterValue(52),
								new PrimitiveParameterValue(54))))));

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
				new CompositeParameterValue(
					new ParameterField("Collection",
						new ArrayParameterValue(
							new PrimitiveParameterValue(1),
							new PrimitiveParameterValue(2),
							new PrimitiveParameterValue(3)))));

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
	}
}
