using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;

using Quokka.Generated;

namespace Quokka
{
	public class Template
	{
		public Template(string templateText)
		{
			if (templateText == null)
				throw new ArgumentNullException(nameof(templateText));

			var inputStream = new AntlrInputStream(templateText);
			var lexer = new QuokkaLex(inputStream);
			var commonTokenStream = new CommonTokenStream(lexer);
			var parser = new QuokkaParser(commonTokenStream);

			var compilationVisitor = new TemplateCompilationVisitor();
			var root = compilationVisitor.Visit(parser.template());
			root = null;
		}
	}
}
