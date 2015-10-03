using System.Collections.Generic;

namespace Quokka
{
	internal interface ITemplateNode
	{
		void CompileVariableDefinitions(VariableCollection variableCollection, ISemanticErrorListener errorListener);
	}
}
