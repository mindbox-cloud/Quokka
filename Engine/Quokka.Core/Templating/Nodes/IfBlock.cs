using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class IfBlock : TemplateNodeBase
	{
		private readonly IReadOnlyCollection<ConditionBlock> conditions;

		public IfBlock(IEnumerable<ConditionBlock> conditions)
		{
			this.conditions = conditions.ToList().AsReadOnly();
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			foreach (var condition in conditions)
				condition.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
