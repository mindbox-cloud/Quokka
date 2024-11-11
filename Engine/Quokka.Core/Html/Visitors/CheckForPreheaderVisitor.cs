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