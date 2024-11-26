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
using System.IO;
using System.Linq;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Html;

namespace Mindbox.Quokka.Tests;

[TestClass]
public class TemplateVisitorTests
{
	[TestMethod]
	[DynamicData(nameof(GetTemplateTestData), DynamicDataSourceType.Method)]
	public void TemplateVisitor_Template_ShouldVisitAllExpectedNodes(string templateText, string[] expectedNodes)
	{
		// Arrange
		var visitor = new TestTemplateVisitor();
		var template = new Template(templateText);
        
		// Act
		template.Accept(visitor);
        
		// Assert
		CollectionAssert.AreEqual(expectedNodes, visitor.VisitedNodes, "The visited nodes did not match the expected sequence.");
	}
	
	[TestMethod]
	[DynamicData(nameof(GetHtmlTemplateTestData), DynamicDataSourceType.Method)]
	public void TemplateVisitor_HtmlTemplate_ShouldVisitAllExpectedNodes(string templateText, string[] expectedNodes)
	{
		// Arrange
		var visitor = new TestTemplateVisitor();
		var template = new HtmlTemplate(templateText);
        
		// Act
		template.Accept(visitor);
        
		// Assert
		CollectionAssert.AreEqual(expectedNodes, visitor.VisitedNodes, "The visited nodes did not match the expected sequence.");
	}

	private static IEnumerable<object[]> GetHtmlTemplateTestData()
	{
		return new List<object[]>
		{
			new object[]
			{
				"<a href=\"http://example.com\">Test</a>",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitConstantBlock: \u003ca href\u003d\"",
					"EndVisit",
					"VisitLinkBlock",
					"VisitConstantBlock: http://example.com",
					"EndVisit",
					"EndVisit",
					"VisitConstantBlock: \"\u003eTest\u003c/a\u003e",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"<html><body> Hello </body></html>",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitConstantBlock: \u003chtml\u003e\u003cbody\u003e",
					"EndVisit",
					"VisitPreHeaderPlaceHolderBlock",
					"EndVisit",
					"VisitConstantBlock:  Hello ",
					"EndVisit",
					"VisitIdentificationCodePlaceHolderBlock",
					"EndVisit",
					"VisitConstantBlock: \u003c/body\u003e\u003c/html\u003e",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
		};
	}

	private static IEnumerable<object[]> GetTemplateTestData()
    {
		return new List<object[]>
		{
			new object[]
			{
				"${ Name }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitVariableValueExpression: Name",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ Math.Min(32, 16) }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitMemberValueExpression",
					"VisitVariableValueExpression: Math",
					"EndVisit",
					"VisitMethodMember: Min",
					"VisitArgumentValue",
					"VisitNumberExpression: 32",
					"EndVisit",
					"EndVisit",
					"VisitArgumentValue",
					"VisitNumberExpression: 16",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ 5 + 3 }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitAdditionExpression",
					"VisitPlusOperand",
					"VisitNumberExpression: 5",
					"EndVisit",
					"EndVisit",
					"VisitPlusOperand",
					"VisitNumberExpression: 3",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"Hello",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitConstantBlock: Hello",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"@{set myVar = 10}",
				new[]
				{
					"VisitTemplateBlock",
					"VisitAssignmentBlock: myVar",
					"VisitNumberExpression: 10",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"@{ if A } condition1 @{ else } condition2 @{ end if }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitIfBlock",
					"VisitConditionBlock",
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitConstantBlock:  condition1 ",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"VisitVariantValueBooleanExpression",
					"VisitVariableValueExpression: A",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"VisitConditionBlock",
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitConstantBlock:  condition2 ",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"VisitTrueExpression",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"@{ for a in Array } nothing @{ end for }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitForBlock: a",
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitConstantBlock:  nothing ",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"VisitVariableValueExpression: Array",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ 5 * 2 }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitMultiplicationExpression",
					"VisitMultOperand",
					"VisitNumberExpression: 5",
					"EndVisit",
					"EndVisit",
					"VisitMultOperand",
					"VisitNumberExpression: 2",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ 10 / 5 }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitMultiplicationExpression",
					"VisitMultOperand",
					"VisitNumberExpression: 10",
					"EndVisit",
					"EndVisit",
					"VisitDivOperand",
					"VisitNumberExpression: 5",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ isEmpty(value) }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitFunctionCallExpression: isEmpty",
					"VisitArgumentValue",
					"VisitVariableValueExpression: value",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ isEmpty('string') }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitFunctionCallExpression: isEmpty",
					"VisitArgumentValue",
					"VisitStringConstantExpression: string, QuoteType: Single",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ some.filed > 42 }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitArithmeticComparisonExpression: GreaterThan",
					"VisitVariantValueArithmeticExpression",
					"VisitMemberValueExpression",
					"VisitVariableValueExpression: some",
					"EndVisit",
					"VisitFieldMember: filed",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"VisitNumberExpression: 42",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ true and true }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitAndExpression",
					"VisitVariantValueBooleanExpression",
					"VisitVariableValueExpression: true",
					"EndVisit",
					"EndVisit",
					"VisitVariantValueBooleanExpression",
					"VisitVariableValueExpression: true",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ true or false }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitOrExpression",
					"VisitVariantValueBooleanExpression",
					"VisitVariableValueExpression: true",
					"EndVisit",
					"EndVisit",
					"VisitVariantValueBooleanExpression",
					"VisitVariableValueExpression: false",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ some.filed_4 != null }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitNullComparisonExpression: NotEquals",
					"VisitMemberValueExpression",
					"VisitVariableValueExpression: some",
					"EndVisit",
					"VisitFieldMember: filed_4",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ some.filed_5 = 'string' }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitStringComparisonExpression: Equals",
					"VisitMemberValueExpression",
					"VisitVariableValueExpression: some",
					"EndVisit",
					"VisitFieldMember: filed_5",
					"EndVisit",
					"EndVisit",
					"VisitStringConstantExpression: string, QuoteType: Single",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ 5 - 3 }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitAdditionExpression",
					"VisitPlusOperand",
					"VisitNumberExpression: 5",
					"EndVisit",
					"EndVisit",
					"VisitMinusOperand",
					"VisitNumberExpression: 3",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ not true }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitNotExpression",
					"VisitVariantValueBooleanExpression",
					"VisitVariableValueExpression: true",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ 'string' & 'string' }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitStringConcatenationExpression",
					"VisitStringConstantExpression: string, QuoteType: Single",
					"EndVisit",
					"VisitStringConstantExpression: string, QuoteType: Single",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			},
			new object[]
			{
				"${ -5 }",
				new[]
				{
					"VisitTemplateBlock",
					"VisitStaticBlock",
					"VisitOutputInstructionBlock",
					"VisitNegationExpression",
					"VisitNumberExpression: 5",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit",
					"EndVisit"
				}
			}
		};
    }
}