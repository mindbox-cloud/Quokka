using System;
using System.Collections.Generic;

using Antlr4.Runtime;

using Quokka.Generated;

namespace Quokka
{
	public class Template
	{
		private readonly TemplateBlock rootBlock;
		private readonly VariableCollection externalVariables;

		public Template(string templateText)
		{
			if (templateText == null)
				throw new ArgumentNullException(nameof(templateText));

			var inputStream = new AntlrInputStream(templateText);
			var lexer = new QuokkaLex(inputStream);
			var commonTokenStream = new CommonTokenStream(lexer);
			var parser = new QuokkaParser(commonTokenStream);

			var compilationVisitor = new RootTemplateVisitor();
			rootBlock = compilationVisitor.Visit(parser.template());

			var scope = new Scope();
			var errorListener = new SemanticErrorListener();
			
			rootBlock.CompileVariableDefinitions(scope, errorListener);
			externalVariables = scope.Variables;
			errorListener = null;
		}

		public IList<IParameterDefinition> GetParameterDefinitions()
		{
			return externalVariables.GetParameterDefinitions();
		}
	}
}

