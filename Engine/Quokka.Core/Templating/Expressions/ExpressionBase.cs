using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    internal abstract class ExpressionBase : IExpression
	{
		public abstract VariableValueStorage TryGetStaticEvaluationResult();

		public abstract VariableValueStorage Evaluate(RenderContext renderContext);

		public abstract TypeDefinition GetResultType(SemanticAnalysisContext context);

		public abstract void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition expectedExpressionType);
	}
}
