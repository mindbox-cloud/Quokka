using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class TwoStageParsingTests
	{
		private const int RandomTemplatesSeed = 20261009;
		private const int RandomTemplatesCount = 20000;
		private const string ProdCorpusPathVariable = "QUOKKA_TEMPLATE_CORPUS";

		[DataTestMethod]
		[DataRow("${ (IsTest) or A }")]
		[DataRow("${ (A) }")]
		[DataRow("${ (A + 1) * 2 }")]
		[DataRow("${ (A) > 2 or B }")]
		[DataRow("${ ((A) or B) and C }")]
		[DataRow("${ not (A) or (B) and not C }")]
		[DataRow("${ if(A or B and C, 'x', 'y') }")]
		[DataRow("${ if((A) or (B), A + 1, (B)) }")]
		[DataRow("${ if(cell.isLast or row.valueCount = cell.index, '', '') }")]
		[DataRow("@{ if (A) or B }x@{ else if (A) and (B) }y@{ end if }")]
		[DataRow("@{ set v = (A) or B }${ v }")]
		[DataRow("@{ set v = (A) + B }${ (v) }")]
		[DataRow("@{ for i in Items }@{ if (i.X) or i.Y and (i.Z) }${ (i.Name) }@{ end if }@{ end for }")]
		[DataRow("${ toUpper(A) = 'X' or (B) != 'Y' }")]
		[DataRow("text ${ A } text ${ (B) } @{ if A }${ A or B }@{ end if } text")]
		public void ContextSensitiveDecision_SllParseTreeIsNotDifferentFromLl(string templateText)
		{
			var comparison = QuokkaParsing.Compare(templateText);

			Assert.AreNotEqual(
				TwoStageParsingOutcome.SllParseTreeDiffersFromLl,
				comparison.Outcome,
				QuokkaParsing.DescribeMismatch(templateText, comparison.SllParseTree, comparison.LlParseTree));
		}

		[DataTestMethod]
		[DataRow("${ A or B }", "False")]
		[DataRow("@{ set v = A and B }${ v }", "False")]
		[DataRow("${ if(A or B, 'yes', 'no') }", "no")]
		public void ValidTemplateSllCannotParse_FallsBackToLlAndRenders(string templateText, string expected)
		{
			Assert.AreEqual(
				TwoStageParsingOutcome.SllBailedOnValidTemplate,
				QuokkaParsing.Compare(templateText).Outcome);

			var result = new Template(templateText).Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(false)),
					new ModelField("B", new PrimitiveModelValue(false))));

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void InvalidTemplate_FallsBackToLlAndReportsSyntaxErrorLocation()
		{
			Assert.AreEqual(
				TwoStageParsingOutcome.SllBailedOnInvalidTemplate,
				QuokkaParsing.Compare("text ${ A + }").Outcome);

			new DefaultTemplateFactory().TryCreateTemplate("text ${ A + }", out var errors);

			var error = errors.Single();
			Assert.AreEqual(1, error.Location.Line);
			Assert.AreEqual(12, error.Location.Column);
			Assert.AreEqual("Invalid symbol", error.Message);
		}

		[TestMethod]
		public void RandomTemplates_SllParseTreeIsNeverDifferentFromLl()
		{
			var random = new Random(RandomTemplatesSeed);
			var outcomes = Enumerable.Range(0, RandomTemplatesCount)
				.Select(_ => RandomTemplate.Create(random))
				.Select(templateText => (TemplateText: templateText, Comparison: QuokkaParsing.Compare(templateText)))
				.ToList();

			var mismatches = outcomes
				.Where(o => o.Comparison.Outcome == TwoStageParsingOutcome.SllParseTreeDiffersFromLl)
				.Select(o => QuokkaParsing.DescribeMismatch(o.TemplateText, o.Comparison.SllParseTree, o.Comparison.LlParseTree))
				.ToList();
			var sllParsedCount = outcomes.Count(o => o.Comparison.Outcome == TwoStageParsingOutcome.SllParseTreeEqualsLl);

			Assert.AreEqual(0, mismatches.Count, string.Join(Environment.NewLine + Environment.NewLine, mismatches.Take(5)));
			Assert.IsTrue(sllParsedCount > RandomTemplatesCount / 2, $"only {sllParsedCount} random templates were parsed by SLL");
		}

		[TestMethod]
		public void ProdTemplateCorpus_SllParseTreeIsNeverDifferentFromLl()
		{
			var corpusPath = Environment.GetEnvironmentVariable(ProdCorpusPathVariable);
			if (string.IsNullOrEmpty(corpusPath))
				Assert.Inconclusive($"{ProdCorpusPathVariable} is not set: expected a JSON lines file with a \"text\" property per line");

			var outcomes = new Dictionary<TwoStageParsingOutcome, int>();
			var mismatches = new List<string>();
			foreach (var line in File.ReadLines(corpusPath))
			{
				using var document = JsonDocument.Parse(line);
				var templateText = document.RootElement.GetProperty("text").GetString();
				var comparison = QuokkaParsing.Compare(templateText);

				outcomes[comparison.Outcome] = outcomes.GetValueOrDefault(comparison.Outcome) + 1;
				if (comparison.Outcome == TwoStageParsingOutcome.SllParseTreeDiffersFromLl)
					mismatches.Add(QuokkaParsing.DescribeMismatch(templateText, comparison.SllParseTree, comparison.LlParseTree));
			}

			Console.WriteLine(string.Join(", ", outcomes.OrderBy(p => p.Key).Select(p => $"{p.Key}={p.Value}")));
			Assert.AreEqual(0, mismatches.Count, string.Join(Environment.NewLine + Environment.NewLine, mismatches.Take(5)));
		}

		private static class RandomTemplate
		{
			private const int MaxBlockDepth = 2;
			private const int MaxExpressionDepth = 3;

			public static string Create(Random random) =>
				string.Join(
					random.Next(2) == 0 ? " text " : "",
					Enumerable.Range(0, random.Next(1, 5)).Select(_ => Block(random, 0)));

			private static string Block(Random random, int depth) =>
				(depth > MaxBlockDepth ? random.Next(3) : random.Next(6)) switch
				{
					0 => $"${{ {Expression(random)} }}",
					1 => "plain text ",
					2 => $"@{{ set v{random.Next(3)} = {Expression(random)} }}",
					3 => $"@{{ if {Boolean(random, 0)} }}{Block(random, depth + 1)}"
						+ (random.Next(2) == 0 ? $"@{{ else if {Boolean(random, 0)} }}{Block(random, depth + 1)}" : "")
						+ (random.Next(2) == 0 ? $"@{{ else }}{Block(random, depth + 1)}" : "")
						+ "@{ end if }",
					4 => $"@{{ for item in {Member(random)} }}{Block(random, depth + 1)}@{{ end for }}",
					_ => $"${{ {Expression(random)} }}{Block(random, depth + 1)}"
				};

			private static string Expression(Random random) =>
				random.Next(4) switch
				{
					0 => Member(random),
					1 => StringExpression(random),
					2 => Boolean(random, 0),
					_ => Arithmetic(random, 0)
				};

			private static string Boolean(Random random, int depth)
			{
				if (depth > MaxExpressionDepth)
					return Member(random);

				return random.Next(8) switch
				{
					0 => $"{Boolean(random, depth + 1)} or {Boolean(random, depth + 1)}",
					1 => $"{Boolean(random, depth + 1)} and {Boolean(random, depth + 1)}",
					2 => $"not {BooleanAtom(random, depth + 1)}",
					3 => $"({Boolean(random, depth + 1)})",
					4 => $"{Arithmetic(random, depth + 1)} {Pick(random, ">", "<", ">=", "<=", "=", "!=")} {Arithmetic(random, depth + 1)}",
					5 => $"{StringExpression(random)} {Pick(random, "=", "!=")} {StringExpression(random)}",
					6 => $"isEmpty({Member(random)})",
					_ => BooleanAtom(random, depth + 1)
				};
			}

			private static string BooleanAtom(Random random, int depth) =>
				random.Next(3) switch
				{
					0 => Member(random),
					1 => $"({Member(random)})",
					_ => $"({Boolean(random, depth + 1)})"
				};

			private static string Arithmetic(Random random, int depth)
			{
				if (depth > MaxExpressionDepth)
					return random.Next(2) == 0 ? Member(random) : random.Next(100).ToString();

				return random.Next(7) switch
				{
					0 => $"{Arithmetic(random, depth + 1)} {Pick(random, "+", "-", "*", "/")} {Arithmetic(random, depth + 1)}",
					1 => $"({Arithmetic(random, depth + 1)})",
					2 => $"-{Arithmetic(random, depth + 1)}",
					3 => $"if({Boolean(random, depth + 1)}, {Arithmetic(random, depth + 1)}, {Arithmetic(random, depth + 1)})",
					4 => $"count({Member(random)})",
					5 => random.Next(1000).ToString(),
					_ => Member(random)
				};
			}

			private static string StringExpression(Random random) =>
				random.Next(3) switch
				{
					0 => $"'{Pick(random, "a", "b", "xyz")}'",
					1 => $"toUpper({Member(random)})",
					_ => $"formatDecimal({Member(random)}, 'N2')"
				};

			private static string Member(Random random) =>
				random.Next(3) switch
				{
					0 => Pick(random, "A", "B", "IsTest", "item"),
					1 => $"{Pick(random, "Recipient", "item", "Order")}.{Pick(random, "Name", "Total", "IsVip")}",
					_ => $"{Pick(random, "Order", "item")}.{Pick(random, "Lines", "Cells")}.{Pick(random, "Count", "Value")}"
				};

			private static string Pick(Random random, params string[] options) => options[random.Next(options.Length)];
		}
	}
}
