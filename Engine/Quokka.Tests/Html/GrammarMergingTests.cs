// // Copyright 2022 Mindbox Ltd
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

using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests.Html
{
	[TestClass]
	public class GrammarMergingTests
	{
		[TestMethod]
		public void HtmlMerge_SingleInnerBlockSpansMultipleOuter()
		{
			var outer = new List<IStaticBlockPart>
			{
				new ConstantBlock("abcdeABCDEabcdeABCDE", 0, 20),
				new ConstantBlock("${ param }", 20, 10),
				new ConstantBlock("0123456789A", 30, 11)
			};

			var inner = new List<IStaticBlockPart>
			{
				new ConstantBlock(
					"ZzZzYyYyXxXxYyYyZzZz",
					14,
					20)
			};

			var result = GrammarMergeTools.MergeInnerAndOuterBlocks(outer, inner)
				.Cast<ConstantBlock>()
				.ToList();

			Assert.AreEqual("abcdeABCDEabcd", result[0].Text);
			Assert.AreEqual("ZzZzYyYyXxXxYyYyZzZz", result[1].Text);
			Assert.AreEqual("456789A", result[2].Text);
		}

		[TestMethod]
		public void HtmlMerge_SingleInnerBlockExactlyMatchesOuter()
		{
			var outer = new List<IStaticBlockPart>
			{
				new ConstantBlock("abcdeABCDEabcdeABCDE", 0, 20),
				new ConstantBlock("${ param }", 20, 10),
				new ConstantBlock("0123456789A", 30, 11)
			};

			var inner = new List<IStaticBlockPart>
			{
				new ConstantBlock(
					"_!_!_!_!_!",
					20,
					10)
			};

			var result = GrammarMergeTools.MergeInnerAndOuterBlocks(outer, inner)
				.Cast<ConstantBlock>()
				.ToList();

			Assert.AreEqual("abcdeABCDEabcdeABCDE", result[0].Text);
			Assert.AreEqual("_!_!_!_!_!", result[1].Text);
			Assert.AreEqual("0123456789A", result[2].Text);
		}

		[TestMethod]
		public void HtmlMerge_SingleInnerBlockInsideOuter()
		{
			var outer = new List<IStaticBlockPart>
			{
				new ConstantBlock("abcdeABCDEabcdeABCDE", 0, 20),
				new ConstantBlock("${ param }", 20, 10),
				new ConstantBlock("0123456789A", 30, 11)
			};

			var inner = new List<IStaticBlockPart>
			{
				new ConstantBlock(
					"_!_!_!",
					11,
					6)
			};

			var result = GrammarMergeTools.MergeInnerAndOuterBlocks(outer, inner)
				.Cast<ConstantBlock>()
				.ToList();

			Assert.AreEqual("abcdeABCDEa", result[0].Text);
			Assert.AreEqual("_!_!_!", result[1].Text);
			Assert.AreEqual("CDE", result[2].Text);
			Assert.AreEqual("${ param }", result[3].Text);
			Assert.AreEqual("0123456789A", result[4].Text);
		}

		[TestMethod]
		public void HtmlMerge_MultipleInnerBlocksInsideSingleOuter()
		{
			var outer = new List<IStaticBlockPart>
			{
				new ConstantBlock("abcdeABCDEabcdeABCDE", 0, 20),
				new ConstantBlock("${ param }", 20, 10),
				new ConstantBlock("0123456789A", 30, 11)
			};

			var inner = new List<IStaticBlockPart>
			{
				new ConstantBlock(
					"ZZZZZ",
					5,
					5),
				new ConstantBlock(
					"YYYYY",
					15,
					5)
			};

			var result = GrammarMergeTools.MergeInnerAndOuterBlocks(outer, inner)
				.Cast<ConstantBlock>()
				.ToList();

			Assert.AreEqual("abcde", result[0].Text);
			Assert.AreEqual("ZZZZZ", result[1].Text);
			Assert.AreEqual("abcde", result[2].Text);
			Assert.AreEqual("YYYYY", result[3].Text);
			Assert.AreEqual("${ param }", result[4].Text);
			Assert.AreEqual("0123456789A", result[5].Text);
		}
	}
}
