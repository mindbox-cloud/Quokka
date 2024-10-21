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

namespace Mindbox.Quokka;

public interface ITemplateVisitor
{
    void EndVisit();
    void VisitTemplateBlock();
    void VisitConstantBlock(string text);
    void VisitAssignmentBlock(string variableName);
    void VisitConditionBlock();
    void VisitForBlock(string iterationVariableName);
    void VisitIfBlock();
    void VisitLinkBlock();
    void VisitMultiplicationExpression();
    void VisitDivOperand();
    void VisitMultOperand();
    void VisitNegationExpression();
    void VisitNumberExpression(double number);
    void VisitOutputInstructionBlock();
    void VisitStaticBlock();
    void VisitFunctionCallExpression(string functionName);
    void VisitArgumentValue();
    void VisitStringConcatenationExpression();
    void VisitStringConstantExpression(string stringValue, string quoteType);
    void VisitVariantValueArithmeticExpression();
    void VisitAndExpression();
    void VisitMemberValueExpression();
    void VisitFieldMember(string stringRepresentation);
    void VisitMethodMember(string name);
    void VisitArithmeticComparisonExpression(string comparisonOperation);
    void VisitNotExpression();
    void VisitNullComparisonExpression(string comparisonOperation);
    void VisitTrueExpression();
    void VisitOrExpression();
    void VisitVariantValueBooleanExpression();
    void VisitStringComparisonExpression(string comparisonOperation);
    void VisitAdditionExpression();
    void VisitPlusOperand();
    void VisitMinusOperand();
    void VisitVariableValueExpression(string variableName);
    void VisitIdentificationCodePlaceHolderBlock();
}