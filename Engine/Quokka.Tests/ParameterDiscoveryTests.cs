using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ParameterDiscoveryTests
	{
		[TestMethod]
		public void ParameterDiscovery_SingleOutputParameter()
		{
			var parameterDefinitions = new Template("${ Name }")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ParameterDefinition(
						"Name",
						VariableType.Primitive)
				},
				parameterDefinitions);
		}
	}
}
