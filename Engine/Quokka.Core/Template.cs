using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Quokka.Generated;

namespace Quokka
{
	public sealed class Template
	{
		private readonly TemplateBlock compiledTemplateTree;
		private readonly ICompositeModelDefinition externalModelDefinition;
		private readonly FunctionRegistry functionRegistry;
		
		public IList<ITemplateError> Errors { get; }

		internal Template(string templateText, FunctionRegistry functionRegistry, bool throwIfErrorsEncountered)
		{
			if (templateText == null)
				throw new ArgumentNullException(nameof(templateText));
			if (functionRegistry == null)
				throw new ArgumentNullException(nameof(functionRegistry));

			this.functionRegistry = functionRegistry;

			var syntaxErrorListener = new SyntaxErrorListener();
			var semanticErrorListener = new SemanticErrorListener();

			var templateParseTree = ParseTemplateText(templateText, syntaxErrorListener);

			if (!syntaxErrorListener.GetErrors().Any())
			{
				compiledTemplateTree = new RootTemplateVisitor().Visit(templateParseTree) ?? TemplateBlock.Empty();

				var analysisContext = new SemanticAnalysisContext
					(new CompilationVariableScope(),
					functionRegistry,
					semanticErrorListener);
				compiledTemplateTree.CompileVariableDefinitions(analysisContext);
				externalModelDefinition = analysisContext.VariableScope.Variables.ToModelDefinition(semanticErrorListener);
			}

			Errors =
				syntaxErrorListener.GetErrors()
					.Concat(semanticErrorListener.GetErrors())
					.ToList();

			if (throwIfErrorsEncountered && Errors.Any())
				throw new InvalidOperationException(
					string.Join(
						Environment.NewLine,
						Errors.Select(error => error.Message)));
		}
		
		public Template(string templateText)
			: this(templateText, new FunctionRegistry(GetStandardFunctions()), true)
		{
		}

		public ICompositeModelDefinition GetModelDefinition()
		{
			return externalModelDefinition;
		}

		public string Render(ICompositeModelValue model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));
			if (Errors.Any())
				throw new InvalidOperationException("Can't render template because it contains errors");

			var valueStorage = VariableValueStorage.CreateStorageForValue(model);
			var builder = new StringBuilder();
			var context = new RenderContext(new RuntimeVariableScope(valueStorage), functionRegistry);
            compiledTemplateTree.Render(builder, context);
			return builder.ToString();
		}

		private QuokkaParser.TemplateContext ParseTemplateText(string templateText, SyntaxErrorListener syntaxErrorListener)
		{
			var inputStream = new AntlrInputStream(templateText);
			var commonTokenStream = new CommonTokenStream(new QuokkaLex(inputStream));

			var parser = new QuokkaParser(commonTokenStream);
			parser.RemoveErrorListeners();
			parser.AddErrorListener(syntaxErrorListener);

			return parser.template();
		}

		internal static IEnumerable<TemplateFunction> GetStandardFunctions()
		{
			yield return new ToUpperTemplateFunction();
			yield return new ToLowerTemplateFunction();
			yield return new ReplaceIfEmptyTemplateFunction();
			yield return new FormatDecimalTemplateFunction();
			yield return new FormatDateTimeTemplateFunction();
			yield return new FormatTimeTemplateFunction();
			yield return new IfTemplateFunction();
		}
	}
}

