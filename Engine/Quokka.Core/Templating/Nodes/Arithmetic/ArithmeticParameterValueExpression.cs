namespace Quokka
{
	internal class ArithmeticParameterValueExpression : IArithmeticExpression
	{
		private readonly VariableOccurence variableOccurence;

		public ArithmeticParameterValueExpression(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public double GetValue()
		{
			// TODO: this is very temporary and should be removed. 
			// For now we consider the parameter to always be True if it's a complex parameter (member access), otherwise False.
			return variableOccurence.Name.Length;
		}
	}
}
