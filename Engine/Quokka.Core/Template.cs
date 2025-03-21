﻿// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Antlr4.Runtime;

using Mindbox.Quokka.Abstractions;
using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class Template : ITemplate
	{
		private readonly TemplateBlock compiledTemplateTree;
		private readonly ICompositeModelDefinition requiredModelDefinition;
		private readonly FunctionRegistry functionRegistry;

		public IList<ITemplateError> Errors { get; }

		/// <summary>
		/// <c>True</c> if the template is a constant string that is independent from parameters
		/// and will always be rendered the same way, otherwise <c>False</c>.
		/// </summary>
		public bool IsConstant { get; }

		internal Template(
			string templateText,
			FunctionRegistry functionRegistry,
			bool throwIfErrorsEncountered = true,
			Func<VisitingContext, IQuokkaVisitor<StaticBlock>> staticBlockVisitorCreator = null,
			IEnumerable<SemanticErrorSubListenerBase> semanticErrorSubListeners = null)
		{
			ArgumentNullException.ThrowIfNull(templateText);
			ArgumentNullException.ThrowIfNull(functionRegistry);

			this.functionRegistry = functionRegistry;

			try
			{
				var syntaxErrorListener = new SyntaxErrorListener();
				var semanticErrorListener = new SemanticErrorListener();
				if (semanticErrorSubListeners != null)
				{
					foreach (var subListener in semanticErrorSubListeners)
					{
						semanticErrorListener.RegisterSubListener(subListener);
					}
				}

				var templateParseTree = ParseTemplateText(templateText, syntaxErrorListener);

				if (!syntaxErrorListener.GetErrors().Any())
				{
					VisitingContext visitingContext = new VisitingContext(
						syntaxErrorListener,
						staticBlockVisitorCreator ?? (context => new StaticBlockVisitor(context)));

					compiledTemplateTree = new RootTemplateVisitor(visitingContext).Visit(templateParseTree);

					IsConstant = compiledTemplateTree.IsConstant;

					var analysisContext = new AnalysisContext
						(new CompilationVariableScope(),
						functionRegistry,
						semanticErrorListener);

					compiledTemplateTree.PerformSemanticAnalysis(analysisContext);
					analysisContext.VariableScope.Compile(analysisContext);
					requiredModelDefinition = ValueUsageSummary.ConvertCollectionToModelDefinition(
						analysisContext.VariableScope.Variables,
						semanticErrorListener);
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

		public string Render(ICompositeModelValue model, ICallContextContainer callContextContainer = null)
		{
			return Render(model, RenderSettings.Default, callContextContainer);
		}

		public virtual string Render(ICompositeModelValue model, RenderSettings settings, ICallContextContainer callContextContainer = null)
		{
			ArgumentNullException.ThrowIfNull(model);
			
			if (Errors.Any())
				throw new TemplateContainsErrorsException(Errors);

			var effectiveCallContextContainer = callContextContainer ?? CallContextContainer.Empty;

			var stringBuilder = new StringBuilder();
			using (var stringWriter = new StringWriter(stringBuilder, settings.CultureInfo))
			{
				DoRender(
					stringWriter,
					model,
					(scope, contextFunctionRegistry) => new RenderContext(
						scope,
						contextFunctionRegistry,
						settings,
						effectiveCallContextContainer));
			}

			return stringBuilder.ToString();
		}

		public void Render(TextWriter textWriter, ICompositeModelValue model, ICallContextContainer callContextContainer = null)
		{
			Render(textWriter, model, RenderSettings.Default, callContextContainer);
		}

		public virtual void Render(
			TextWriter textWriter,
			ICompositeModelValue model,
			RenderSettings settings,
			ICallContextContainer callContextContainer = null)
		{
			ArgumentNullException.ThrowIfNull(textWriter);
			ArgumentNullException.ThrowIfNull(model);
			
			if (Errors.Any())
				throw new TemplateContainsErrorsException(Errors);

			var effectiveCallContextContainer = callContextContainer ?? CallContextContainer.Empty;

			DoRender(
				textWriter,
				model,
				(scope, contextFunctionRegistry) => new RenderContext(
					scope, contextFunctionRegistry, settings, effectiveCallContextContainer));
		}

		protected void DoRender(
			TextWriter textWriter,
			ICompositeModelValue model,
			Func<RuntimeVariableScope, FunctionRegistry, RenderContext> renderContextCreator)
		{
			try
			{
				new ModelValidator().ValidateModel(model, requiredModelDefinition);

				var valueStorage = new CompositeVariableValueStorage(model);

				var context = renderContextCreator(new RuntimeVariableScope(valueStorage), functionRegistry);
				compiledTemplateTree.Render(textWriter, context);
				context.OnRenderingEnd(textWriter);
			}
			catch (Exception ex)
			{
				if (ex is TemplateException)
					throw;

				throw new TemplateException("Unexpected errors occured during template rendering", ex);
			}
		}

		protected void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			compiledTemplateTree.CompileGrammarSpecificData(context);
		}

		private QuokkaParser.TemplateContext ParseTemplateText(string templateText, SyntaxErrorListener syntaxErrorListener)
		{
			var inputStream = new CodePointCharStream(templateText);
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
			yield return new IsEmptyTemplateFunction();
			yield return new TableRowsTemplateFunction();
			yield return new CountTemplateFunction();
			yield return new AddDaysTemplateFunction();
			yield return new AddHoursTemplateFunction();
			yield return new ChooseRandomTextFunction();
			yield return new FloorTemplateFunction();
			yield return new CeilingTemplateFunction();
			yield return new SubstringTemplateFunction();
			yield return new SubstringWithLengthTemplateFunction();
			yield return new LengthTemplateFunction();
			yield return new Md5HashFunction();
			yield return new Sha1HashFunction();
			yield return new Sha256HashFunction();
			yield return new Sha512HashFunction();
			yield return new ToBase64TemplateFunction();
			yield return new ToHexTemplateFunction();
			yield return new GetDayTemplateFunction();
			yield return new GetMonthTemplateFunction();
			yield return new GetYearTemplateFunction();
			yield return new AppendFormsTemplateFunction();
			yield return new CapitalizeAllWordsTemplateFunction();
			yield return new CapitalizeTemplateFunction();
			yield return new FormsTemplateFunction();
			yield return new TruncateTemplateFunction();
			yield return new ToUnixTimeStampTemplateFunction();
		}

		public void Accept(ITemplateVisitor treeVisitor)
		{
			compiledTemplateTree.Accept(treeVisitor);
		}
	}
}

