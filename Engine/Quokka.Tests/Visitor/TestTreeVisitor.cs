using System.Collections.Generic;

namespace Mindbox.Quokka.Tests;

using System.Collections.Generic;

public class TestTreeVisitor : ITreeVisitor
{
    public List<string> VisitedNodes { get; } = [];

    public void EndVisit()
    {
        VisitedNodes.Add("EndVisit");
    }

    public void VisitTemplateBlock()
    {
        VisitedNodes.Add("VisitTemplateBlock");
    }

    public void VisitConstantBlock(string text)
    {
        VisitedNodes.Add($"VisitConstantBlock: {text}");
    }

    public void VisitAssignmentBlock(string variableName)
    {
        VisitedNodes.Add($"VisitAssignmentBlock: {variableName}");
    }

    public void VisitConditionBlock()
    {
        VisitedNodes.Add("VisitConditionBlock");
    }

    public void VisitForBlock(string iterationVariableName)
    {
        VisitedNodes.Add($"VisitForBlock: {iterationVariableName}");
    }

    public void VisitIfBlock()
    {
        VisitedNodes.Add("VisitIfBlock");
    }

    public void VisitLinkBlock()
    {
        VisitedNodes.Add("VisitLinkBlock");
    }

    public void VisitMultiplicationExpression()
    {
        VisitedNodes.Add("VisitMultiplicationExpression");
    }

    public void VisitDivOperand()
    {
        VisitedNodes.Add("VisitDivOperand");
    }

    public void VisitMultOperand()
    {
        VisitedNodes.Add("VisitMultOperand");
    }

    public void VisitNegationExpression()
    {
        VisitedNodes.Add("VisitNegationExpression");
    }

    public void VisitNumberExpression(double number)
    {
        VisitedNodes.Add($"VisitNumberExpression: {number}");
    }

    public void VisitOutputInstructionBlock()
    {
        VisitedNodes.Add("VisitOutputInstructionBlock");
    }

    public void VisitStaticBlock()
    {
        VisitedNodes.Add("VisitStaticBlock");
    }

    public void VisitFunctionCallExpression(string functionName)
    {
        VisitedNodes.Add($"VisitFunctionCallExpression: {functionName}");
    }

    public void VisitArgumentValue()
    {
        VisitedNodes.Add("VisitArgumentValue");
    }

    public void VisitStringConcatenationExpression()
    {
        VisitedNodes.Add("VisitStringConcatenationExpression");
    }

    public void VisitStringConstantExpression(string stringValue, string quoteType)
    {
        VisitedNodes.Add($"VisitStringConstantExpression: {stringValue}, QuoteType: {quoteType}");
    }

    public void VisitVariantValueArithmeticExpression()
    {
        VisitedNodes.Add("VisitVariantValueArithmeticExpression");
    }

    public void VisitAndExpression()
    {
        VisitedNodes.Add("VisitAndExpression");
    }

    public void VisitMemberValueExpression()
    {
        VisitedNodes.Add("VisitMemberValueExpression");
    }

    public void VisitFieldMember(string stringRepresentation)
    {
        VisitedNodes.Add($"VisitFieldMember: {stringRepresentation}");
    }

    public void VisitMethodMember(string name)
    {
        VisitedNodes.Add($"VisitMethodMember: {name}");
    }

    public void VisitArithmeticComparisonExpression(string comparisonOperation)
    {
        VisitedNodes.Add($"VisitArithmeticComparisonExpression: {comparisonOperation}");
    }

    public void VisitNotExpression()
    {
        VisitedNodes.Add("VisitNotExpression");
    }

    public void VisitNullComparisonExpression(string comparisonOperation)
    {
        VisitedNodes.Add($"VisitNullComparisonExpression: {comparisonOperation}");
    }

    public void VisitTrueExpression()
    {
        VisitedNodes.Add("VisitTrueExpression");
    }

    public void VisitOrExpression()
    {
        VisitedNodes.Add("VisitOrExpression");
    }

    public void VisitVariantValueBooleanExpression()
    {
        VisitedNodes.Add("VisitVariantValueBooleanExpression");
    }

    public void VisitStringComparisonExpression(string comparisonOperation)
    {
        VisitedNodes.Add($"VisitStringComparisonExpression: {comparisonOperation}");
    }

    public void VisitAdditionExpression()
    {
        VisitedNodes.Add("VisitAdditionExpression");
    }

    public void VisitPlusOperand()
    {
        VisitedNodes.Add("VisitPlusOperand");
    }

    public void VisitMinusOperand()
    {
        VisitedNodes.Add("VisitMinusOperand");
    }

    public void VisitVariableValueExpression(string variableName)
    {
        VisitedNodes.Add($"VisitVariableValueExpression: {variableName}");
    }

    public void VisitIdentificationCodePlaceHolderBlock()
    {
        VisitedNodes.Add("VisitIdentificationCodePlaceHolderBlock");
    }
}