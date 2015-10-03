using System;

namespace Quokka
{
	internal abstract class TemplateNodeBase : ITemplateNode
	{
		public virtual void CompileVariableDefinitions(VariableCollection variableCollection, ISemanticErrorListener errorListener)
		{
		}
	}
}
