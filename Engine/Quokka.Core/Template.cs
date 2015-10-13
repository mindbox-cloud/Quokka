﻿using System;
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
		private readonly ICompositeModelDefinition requiredModelDefinition;
		private readonly FunctionRegistry functionRegistry;
		
		public IList<ITemplateError> Errors { get; }

		internal Template(string templateText, FunctionRegistry functionRegistry, bool throwIfErrorsEncountered)
		{
			if (templateText == null)
				throw new ArgumentNullException(nameof(templateText));
			if (functionRegistry == null)
				throw new ArgumentNullException(nameof(functionRegistry));

			this.functionRegistry = functionRegistry;

			try
			{
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
					requiredModelDefinition = analysisContext.VariableScope.Variables.ToModelDefinition(semanticErrorListener);
				}

				Errors =
					syntaxErrorListener.GetErrors()
						.Concat(semanticErrorListener.GetErrors())
						.ToList();

				if (throwIfErrorsEncountered && Errors.Any())
					throw new TemplateContainsErrorsException(Errors);
			}
			catch (Exception ex)
			{
				if (ex is TemplateException)
					throw;

				throw new TemplateException("Unexpected errors occured during template creation", ex);
			}
		}
		
		public Template(string templateText)
			: this(templateText, new FunctionRegistry(GetStandardFunctions()), true)
		{
		}

		public ICompositeModelDefinition GetModelDefinition()
		{
			return requiredModelDefinition;
		}

		public string Render(ICompositeModelValue model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));
			if (Errors.Any())
				throw new TemplateContainsErrorsException(Errors);

			try
			{
				new ModelValidator().ValidateModel(model, requiredModelDefinition);

				var valueStorage = VariableValueStorage.CreateStorageForValue(model);
				var builder = new StringBuilder();
				var context = new RenderContext(new RuntimeVariableScope(valueStorage), functionRegistry);
				compiledTemplateTree.Render(builder, context);
				return builder.ToString();
			}
			catch (Exception ex)
			{
				if (ex is TemplateException)
					throw;

				throw new TemplateException("Unexpected errors occured during template rendering", ex);
			}
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

