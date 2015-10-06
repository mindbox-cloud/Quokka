using System.Text;

namespace Quokka
{
	internal interface ITemplateNode
	{
		void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener);

		void Render(StringBuilder resultBuilder, VariableValueStorage valueStorage);
	}
}
