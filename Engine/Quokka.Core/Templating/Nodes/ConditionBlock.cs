using System;
using System.Text;

namespace Quokka
{
	internal class ConditionBlock : TemplateNodeBase
	{
		private readonly IBooleanExpression condition;
		private readonly ITemplateNode block;
		
		public ConditionBlock(IBooleanExpression condition, ITemplateNode block)
		{
			this.block = block;
			this.condition = condition;
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			condition.CompileVariableDefinitions(scope, errorListener);
			block.CompileVariableDefinitions(scope, errorListener);
		}

		public override void Render(StringBuilder resultBuilder, VariableValueStorage valueStorage)
		{
			block.Render(resultBuilder, valueStorage);
		}

		public bool ShouldRender(VariableValueStorage valueStorage)
		{
			return condition.Evaluate(valueStorage);
		}
	}
}
