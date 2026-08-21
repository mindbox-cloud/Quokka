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

using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Tests;

namespace Mindbox.Quokka
{
	[TestClass]
	public class ModelDiscoveryNonIdempotentMethodsTests
	{
		private static readonly string[] NonIdempotentMethodNames = { "SkipMemorized" };

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_IdenticalCalls_AreNumberedSeparately()
		{
			var modelDefinition = CreateTemplate("${ Object.SkipMemorized() }${ Object.SkipMemorized() }")
				.GetModelDefinition();

			AssertObjectMethodsDiscovered(
				modelDefinition,
				ExpectedCall("SkipMemorized", 1),
				ExpectedCall("SkipMemorized", 2));
		}

		[TestMethod]
		public void ModelDiscovery_IdempotentMethod_IdenticalCalls_ShareASingleDefinition()
		{
			var modelDefinition = CreateTemplate("${ Object.GetValue() }${ Object.GetValue() }")
				.GetModelDefinition();

			AssertObjectMethodsDiscovered(modelDefinition, ExpectedCall("GetValue", null));
		}

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_CallsAreNumberedPerArgumentList()
		{
			var modelDefinition = CreateTemplate(
					@"
						${ Object.SkipMemorized('a') }
						${ Object.SkipMemorized('b') }
						${ Object.SkipMemorized('a') }
					")
				.GetModelDefinition();

			AssertObjectMethodsDiscovered(
				modelDefinition,
				ExpectedCall("SkipMemorized", 1, "a"),
				ExpectedCall("SkipMemorized", 2, "a"),
				ExpectedCall("SkipMemorized", 1, "b"));
		}

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_NameMatchingIsCaseInsensitive()
		{
			var modelDefinition = CreateTemplate(
					"${ Object.SkipMemorized() }${ Object.SkipMemorized() }",
					new[] { "skipmemorized" })
				.GetModelDefinition();

			AssertObjectMethodsDiscovered(
				modelDefinition,
				ExpectedCall("SkipMemorized", 1),
				ExpectedCall("SkipMemorized", 2));
		}

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_HtmlTemplate_IdenticalCalls_AreNumberedSeparately()
		{
			var modelDefinition = new DefaultTemplateFactory(nonIdempotentMethodNames: NonIdempotentMethodNames)
				.CreateHtmlTemplate("<p>${ Object.SkipMemorized() }${ Object.SkipMemorized() }</p>")
				.GetModelDefinition();

			AssertObjectMethodsDiscovered(
				modelDefinition,
				ExpectedCall("SkipMemorized", 1),
				ExpectedCall("SkipMemorized", 2));
		}

		private static ITemplate CreateTemplate(string templateText) =>
			CreateTemplate(templateText, NonIdempotentMethodNames);

		private static ITemplate CreateTemplate(string templateText, IEnumerable<string> nonIdempotentMethodNames) =>
			new DefaultTemplateFactory(nonIdempotentMethodNames: nonIdempotentMethodNames)
				.CreateTemplate(templateText);

		private static IMethodCallDefinition ExpectedCall(string name, int? callOrdinal, params string[] arguments) =>
			new MethodCallDefinition(
				name,
				arguments
					.Select(argument =>
						(IMethodArgumentDefinition)new MethodArgumentDefinition(TypeDefinition.String, argument))
					.ToArray(),
				callOrdinal);

		private static void AssertObjectMethodsDiscovered(
			ICompositeModelDefinition modelDefinition,
			params IMethodCallDefinition[] expectedCalls)
		{
			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Object", new CompositeModelDefinition(
								methods: expectedCalls.ToDictionary(
									expectedCall => expectedCall,
									_ => (IModelDefinition)new PrimitiveModelDefinition(TypeDefinition.Primitive)))
						}
					}),
				modelDefinition);
		}
	}
}
