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

namespace Mindbox.Quokka;

public class CheckForPreHeaderVisitor : ITemplateVisitor
{
    public bool PreHeaderNodeWasVisited { get; private set; }

    public void EndVisit()
    {
    }

    public void VisitTemplateBlock()
    {
    }

    public void VisitConstantBlock(string text)
    {
    }

    public void VisitAssignmentBlock(string variableName)
    {
    }

    public void VisitConditionBlock()
    {
    }

    public void VisitForBlock(string iterationVariableName)
    {
    }

    public void VisitIfBlock()
    {
    }

    public void VisitLinkBlock()
    {
    }

    public void VisitMultiplicationExpression()
    {
    }

    public void VisitDivOperand()
    {
    }

    public void VisitMultOperand()
    {
    }

    public void VisitNegationExpression()
    {
    }

    public void VisitNumberExpression(double number)
    {
    }

    public void VisitOutputInstructionBlock()
    {
    }

    public void VisitStaticBlock()
    {
    }

    public void VisitFunctionCallExpression(string functionName)
    {
    }

    public void VisitArgumentValue()
    {
    }

    public void VisitStringConcatenationExpression()
    {
    }

    public void VisitStringConstantExpression(string stringValue, string quoteType)
    {
    }

    public void VisitVariantValueArithmeticExpression()
    {
    }

    public void VisitAndExpression()
    {
    }

    public void VisitMemberValueExpression()
    {
    }

    public void VisitFieldMember(string stringRepresentation)
    {
    }

    public void VisitMethodMember(string name)
    {
    }

    public void VisitArithmeticComparisonExpression(string comparisonOperation)
    {
    }

    public void VisitNotExpression()
    {
    }

    public void VisitNullComparisonExpression(string comparisonOperation)
    {
    }

    public void VisitTrueExpression()
    {
    }

    public void VisitOrExpression()
    {
    }

    public void VisitVariantValueBooleanExpression()
    {
    }

    public void VisitStringComparisonExpression(string comparisonOperation)
    {
    }

    public void VisitAdditionExpression()
    {
    }

    public void VisitPlusOperand()
    {
    }

    public void VisitMinusOperand()
    {
    }

    public void VisitVariableValueExpression(string variableName)
    {
    }

    public void VisitIdentificationCodePlaceHolderBlock()
    {
    }

    public void VisitPreHeaderPlaceHolderBlock()
    {
        PreHeaderNodeWasVisited = true;
    }
}